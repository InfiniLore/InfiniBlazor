// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer;
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.SyntaxSerializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class MdSyntaxSerializerStack : IMarkdownStringMdSyntaxSerializerStack, IResettable {
    private readonly Stack<MdSyntaxFragment> _stack = new();
    public static ObjectPool<MdSyntaxSerializerStack> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxSerializerStack>(PoolingHelpers.ParsersRetained);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region PushToStack
    public void PushMultiLineMatchesToStack(string input, IMdSyntaxNode node, MarkdownStringMdSyntaxSerializerOrigin handlerOrigin) {
        MatchCollection matches = MdRegexLib.MultilineStructuresRegex.Matches(input);
        int count = matches.Count;
        _stack.EnsureCapacity(_stack.Count + count);
        
        for (int i = count - 1; i >= 0; i--) {
            PushMatchToStack(matches[i], node, handlerOrigin);
        }
    }

    public void PushSingleLineMatchesToStack(string input, IMdSyntaxNode node, MarkdownStringMdSyntaxSerializerOrigin handlerOrigin) {
        MatchCollection matches = MdRegexLib.SinglelineStructuresRegex.Matches(input);
        int count = matches.Count;
        _stack.EnsureCapacity(_stack.Count + count);

        int currentIndex = input.Length;

        for (int i = count - 1; i >= 0; i--) {
            Match match = matches[i];
            int matchEnd = match.Index + match.Length;

            // If there's an uncaught text between this match's end and the last position, add it as raw input
            if (matchEnd < currentIndex) {
                ContentMdSyntaxNode contentNode = ContentMdSyntaxNode.Pool.Get();
                contentNode.Content = input[matchEnd..currentIndex];
                PushProcessedNodeToStack(node, contentNode);
            }

            PushMatchToStack(match, node, handlerOrigin);
            currentIndex = match.Index;
        }

        // ReSharper disable once InvertIf
        if (currentIndex > 0) {
            // Handle any remaining text before the first match
            ContentMdSyntaxNode contentNode = ContentMdSyntaxNode.Pool.Get();
            contentNode.Content = input[..currentIndex];
            PushProcessedNodeToStack(node, contentNode);
        }
    }

    public void PushProcessedNodeToStack(IMdSyntaxNode parentNode, IMdSyntaxNode childNode) 
        => _stack.Push(MdSyntaxFragment.AsProcessedNode(parentNode, childNode));

    private void PushMatchToStack(Match match, IMdSyntaxNode currentNode, MarkdownStringMdSyntaxSerializerOrigin handlerOrigin) 
        => _stack.Push(MdSyntaxFragment.AsUnhandledMatch(match, currentNode, handlerOrigin));
    #endregion
    
    public bool TryPopDto([NotNullWhen(true)] out MdSyntaxFragment? dto) {
        return _stack.TryPop(out dto);
    }
    
    public bool TryReset() {
        while (TryPopDto(out MdSyntaxFragment? dto)) {
            MdSyntaxFragment.Pool.Return(dto);
        }
        return _stack.Count == 0;
    }
}

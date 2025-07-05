// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class MdSyntaxParserStack : IMdSyntaxParserStack, IResettable {
    private readonly Stack<MdSyntaxFragment> _stack = new();
    public IMdSyntaxTree NodeTree { get; set; } = null!;
    
    public static ObjectPool<MdSyntaxParserStack> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxParserStack>(PoolingHelpers.ParsersRetained);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region AddToStack
    public void PushMultiLineMatchesToStack(string input, IMdSyntaxNode node, MdSyntaxHandlerOrigin handlerOrigin) {
        MatchCollection matches = MdRegexLib.MultilineStructuresRegex.Matches(input);
        int count = matches.Count;

        // ArrayPooling this is not needed, ensuring capacity should do it
        _stack.EnsureCapacity(_stack.Count + count);

        // Process matches in reverse order for _stack
        for (int i = count - 1; i >= 0; i--) {
            PushMatchToStack(matches[i], node, handlerOrigin);
        }
    }

    public void PushSingleLineMatchesToStack(string input, IMdSyntaxNode node, MdSyntaxHandlerOrigin handlerOrigin) {
        MatchCollection matches = MdRegexLib.SinglelineStructuresRegex.Matches(input);
        int count = matches.Count;

        // ArrayPooling this is not needed, ensuring capacity should do it
        _stack.EnsureCapacity(_stack.Count + count);

        int currentIndex = input.Length;

        // Process matches in reverse order for _stack
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

    private void PushMatchToStack(Match match, IMdSyntaxNode currentNode, MdSyntaxHandlerOrigin handlerOrigin) 
        => _stack.Push(MdSyntaxFragment.AsUnhandledMatch(match, currentNode, handlerOrigin));
    #endregion
    
    public bool TryPopDto([NotNullWhen(true)] out MdSyntaxFragment? dto) {
        return _stack.TryPop(out dto);
    }
    
    public bool TryReset() {
        while (TryPopDto(out MdSyntaxFragment? dto)) {
            MdSyntaxFragment.Pool.Return(dto);
        }
        NodeTree = null!;
        return _stack.Count == 0;
    }
}

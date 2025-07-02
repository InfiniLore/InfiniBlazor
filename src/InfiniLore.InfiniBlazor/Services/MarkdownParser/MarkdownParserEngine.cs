// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownParserEngine : IMarkdownParserEngine, IResettable {
    private readonly Stack<SyntaxFragment> _stack = new();
    public IMarkdownSyntaxTree NodeTree { get; set; } = null!;
    
    public static ObjectPool<MarkdownParserEngine> Pool { get; } = Pooling.CreateResettablePool<MarkdownParserEngine>(Pooling.ParsersRetained);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region AddToStack
    public void PushMultiLineMatchesToStack(string input, IMdSyntaxNode node, HandlerOrigin origin) {
        MatchCollection matches = MarkdownRegexLib.MultilineStructuresRegex.Matches(input);
        int count = matches.Count;

        // ArrayPooling this is not needed, ensuring capacity should do it
        _stack.EnsureCapacity(_stack.Count + count);

        // Process matches in reverse order for _stack
        for (int i = count - 1; i >= 0; i--) {
            PushMatchToStack(matches[i], node, origin);
        }
    }

    public void PushSingleLineMatchesToStack(string input, IMdSyntaxNode node, HandlerOrigin origin) {
        MatchCollection matches = MarkdownRegexLib.SinglelineStructuresRegex.Matches(input);
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

            PushMatchToStack(match, node, origin);
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

    public void PushProcessedNodeToStack(IMdSyntaxNode parentNode, IMdSyntaxNode childNode) {
        SyntaxFragment fragment = SyntaxFragment.Pool.Get();
        fragment.AsProcessedNode(parentNode, childNode);
        _stack.Push(fragment);
    }

    private void PushMatchToStack(Match match, IMdSyntaxNode currentNode, HandlerOrigin origin) {
        SyntaxFragment fragment = SyntaxFragment.Pool.Get();
        fragment.AsUnhandledMatch(match, currentNode, origin);
        _stack.Push(fragment);
    }
    #endregion

    public bool TryPopDto([NotNullWhen(true)] out SyntaxFragment? dto)
        => _stack.TryPop(out dto);
    
    public bool TryReset() {
        while (_stack.TryPop(out SyntaxFragment? fragment)) {
            SyntaxFragment.Pool.Return(fragment);// Makes sure we clean everything
        }

        NodeTree = null!;

        return _stack.Count == 0;
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownParserEngine : IMarkdownParserEngine, IResettable {
    private readonly Stack<MarkdownFragment> _stack = new();
    public IMarkdownSyntaxTree NodeTree { get; set; } = null!;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region AddToStack
    public void AddMultiLineMatchesToStack(string input, IMarkdownSyntaxNode node, HandlerOrigin origin) {
        MatchCollection matches = MarkdownRegexLib.MultilineStructuresRegex.Matches(input);
        int count = matches.Count;

        // ArrayPooling this is not needed, ensuring capacity should do it
        _stack.EnsureCapacity(_stack.Count + count);

        // Process matches in reverse order for _stack
        for (int i = count - 1; i >= 0; i--) {
            PushMatchToStack(matches[i], node, origin);
        }
    }

    public void AddSingleLineMatchesToStack(string input, IMarkdownSyntaxNode node, HandlerOrigin origin) {
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
                PushContentToStack(input[matchEnd..currentIndex], node, origin);
            }

            PushMatchToStack(match, node, origin);
            currentIndex = match.Index;
        }

        // ReSharper disable once InvertIf
        if (currentIndex > 0) {
            // Handle any remaining text before the first match
            PushContentToStack(input[..currentIndex], node, origin);
        }
    }

    public void PushContentToStack(string content, IMarkdownSyntaxNode currentNode, HandlerOrigin origin)
        => PushElementToStack(content, currentNode, origin, MarkdownElement.Content);

    public void PushElementToStack(string? content, IMarkdownSyntaxNode currentNode, HandlerOrigin origin, MarkdownElement element) {
        MarkdownFragment fragment = PoolCache.MarkdownFragmentPool.Get();
        fragment.AsElement(content, currentNode, origin, element);
        _stack.Push(fragment);
    }

    private void PushMatchToStack(Match match, IMarkdownSyntaxNode currentNode, HandlerOrigin origin) {
        MarkdownFragment fragment = PoolCache.MarkdownFragmentPool.Get();
        fragment.AsMatch(match, currentNode, origin);
        _stack.Push(fragment);
    }
    #endregion

    public bool TryPopDto([NotNullWhen(true)] out MarkdownFragment? dto)
        => _stack.TryPop(out dto);
    
    public bool TryReset() {
        while (_stack.TryPop(out MarkdownFragment? fragment)) {
            PoolCache.MarkdownFragmentPool.Return(fragment);// Makes sure we clean everything
        }

        NodeTree = null!;

        return _stack.Count == 0;
    }
}

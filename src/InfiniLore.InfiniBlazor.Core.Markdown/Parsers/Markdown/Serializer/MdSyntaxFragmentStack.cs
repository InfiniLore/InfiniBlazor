// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Markdown.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class MdSyntaxFragmentStack : IMdSyntaxFragmentStack, IResettable {
    public IMdSyntaxTree TreeReference { get; set; } = null!;

    private readonly Stack<MdSyntaxFragment> _stack = new();
    public static ObjectPool<MdSyntaxFragmentStack> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxFragmentStack>(PoolingHelpers.ParsersRetained);


    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region PushToStack
    public void PushMultiLineMatchesToStack(string input, IMdSyntaxNode node, int startIndex = 0) {
        MatchCollection matches = MdRegexLib.MultilineStructuresRegex.Matches(input, startIndex);
        int count = matches.Count;
        _stack.EnsureCapacity(_stack.Count + count);

        for (int i = count - 1; i >= 0; i--) {
            PushMatchToStack(matches[i], node);
        }
    }

    public void PushSingleLineMatchesToStack(string input, IMdSyntaxNode node) {
        MatchCollection matches = MdRegexLib.SinglelineStructuresRegex.Matches(input);
        int count = matches.Count;
        _stack.EnsureCapacity(_stack.Count + count);

        int currentIndex = input.Length;

        for (int i = count - 1; i >= 0; i--) {
            Match match = matches[i];
            int matchEnd = match.Index + match.Length;

            // If there's an uncaught text between this match's end and the last position, add it as raw input
            if (matchEnd < currentIndex) {
                TextMdSyntaxNode contentNode = TextMdSyntaxNode.Pool.Get();
                contentNode.WithContent(input[matchEnd..currentIndex]);
                PushProcessedNodeToStack(node, contentNode);
            }

            PushMatchToStack(match, node);
            currentIndex = match.Index;
        }

        // ReSharper disable once InvertIf
        if (currentIndex > 0) {
            // Handle any remaining text before the first match
            TextMdSyntaxNode contentNode = TextMdSyntaxNode.Pool.Get();
            contentNode.WithContent(input[..currentIndex]);
            PushProcessedNodeToStack(node, contentNode);
        }
    }

    public void PushProcessedNodeToStack(IMdSyntaxNode parentNode, IMdSyntaxNode childNode)
        => _stack.Push(MdSyntaxFragment.AsProcessedNode(parentNode, childNode));

    private void PushMatchToStack(Match match, IMdSyntaxNode currentNode)
        => _stack.Push(MdSyntaxFragment.AsUnhandledMatch(match, currentNode));
    #endregion

    public bool TryPopDto(out MdSyntaxFragment dto) {
        return _stack.TryPop(out dto);
    }

    public bool TryReset() {
        _stack.Clear();
        return _stack.Count == 0;
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class MdSyntaxFragmentStack : IMdSyntaxFragmentStack, IResettable {
    public IMdSyntaxTree TreeReference { get; set; } = null!;
    public IMdStringMdSyntaxSerializer SerializerReference { get; set; } = null!;

    private readonly Stack<MdSyntaxFragment> _stack = new();
    public static ObjectPool<MdSyntaxFragmentStack> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxFragmentStack>(PoolingHelpers.ParsersRetained);


    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region PushToStack
    public void PushMultiLineMatchesToStack(string input, IMdSyntaxNode node, int startIndex = 0) {
        ImmutableArray<IMdSyntaxNodeSerializer> serializers = SerializerReference.MultiLineSerializers;
        List<(Match Match, IMdSyntaxNodeSerializer Serializer)> matches = [];
        matches.AddRange(serializers.SelectMany(
            nodeSerializer => nodeSerializer.Syntax.Matches(input, startIndex),
            (nodeSerializer, match) => (match, nodeSerializer)
        ));

        // Sort matches by index descending so we push the latest matches first (LIFO stack behavior)
        matches.Sort((a, b) => b.Match.Index.CompareTo(a.Match.Index));

        _stack.EnsureCapacity(_stack.Count + matches.Count);
        foreach ((Match match, IMdSyntaxNodeSerializer serializer) in matches) {
            PushMatchToStack(match, node, serializer);
        }
    }

    public void PushSingleLineMatchesToStack(string input, IMdSyntaxNode node) {
        ImmutableArray<IMdSyntaxNodeSerializer> serializers = SerializerReference.SingleLineSerializers;
        List<(Match Match, IMdSyntaxNodeSerializer Serializer)> matches = [];
        matches.AddRange(serializers.SelectMany(
            nodeSerializer => nodeSerializer.Syntax.Matches(input),
            (nodeSerializer, match) => (match, nodeSerializer))
        );

        // Sort matches by index ascending to handle gaps correctly
        matches.Sort((a, b) => a.Match.Index.CompareTo(b.Match.Index));

        int inputLength = input.Length;

        // Since we want to push to stack (LIFO), we actually need to process text gaps and matches 
        // in a way that the first character of the string ends up at the TOP of the stack.
        // Thus, we iterate backwards through our sorted matches.

        int lastProcessedIndex = inputLength;

        for (int i = matches.Count - 1; i >= 0; i--) {
            (Match match, IMdSyntaxNodeSerializer serializer) = matches[i];
            int matchEnd = match.Index + match.Length;

            // Handle text after this match (gap between this match and the next/end)
            if (matchEnd < lastProcessedIndex) {
                TextMdSyntaxNode contentNode = TextMdSyntaxNode.Pool.Get();
                contentNode.WithContent(input[matchEnd..lastProcessedIndex]);
                PushProcessedNodeToStack(node, contentNode);
            }

            PushMatchToStack(match, node, serializer);
            lastProcessedIndex = match.Index;
        }

        // Handle any remaining text at the very beginning
        if (lastProcessedIndex > 0) {
            TextMdSyntaxNode contentNode = TextMdSyntaxNode.Pool.Get();
            contentNode.WithContent(input[..lastProcessedIndex]);
            PushProcessedNodeToStack(node, contentNode);
        }
    }

    public void PushProcessedNodeToStack(IMdSyntaxNode parentNode, IMdSyntaxNode childNode)
        => _stack.Push(MdSyntaxFragment.AsProcessedNode(parentNode, childNode));

    private void PushMatchToStack(Match match, IMdSyntaxNode currentNode, IMdSyntaxNodeSerializer nodeSerializer)
        => _stack.Push(MdSyntaxFragment.AsUnhandledMatch(match, currentNode, nodeSerializer));
    #endregion

    public bool TryPopDto(out MdSyntaxFragment dto) {
        return _stack.TryPop(out dto);
    }

    public bool TryReset() {
        _stack.Clear();
        return _stack.Count == 0;
    }
}

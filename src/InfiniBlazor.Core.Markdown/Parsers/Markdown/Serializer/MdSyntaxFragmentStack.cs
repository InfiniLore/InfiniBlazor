// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
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
        List<(Match Match, IMdSyntaxNodeSerializer Serializer)> topLevelWinners = [];

        int scanPos = startIndex;
        int inputLength = input.Length;

        while (scanPos < inputLength) {
            bool foundMatch = false;

            foreach (IMdSyntaxNodeSerializer serializer in serializers) {
                // Find the first match for THIS serializer starting from scanPos
                Match m = serializer.Syntax.Match(input, scanPos);
                
                // We only accept the match if it starts EXACTLY at scanPos.
                // This prevents a Paragraph further down the string from "jumping the gun".
                if (!m.Success || m.Index != scanPos) continue;

                topLevelWinners.Add((m, serializer));
                scanPos += Math.Max(1, m.Length);
                foundMatch = true;
                break; // Winner found for this position, move to next scanPos
            }

            // If absolutely nothing matched at the current scanPos, 
            // we must advance scanPos by at least 1 to avoid an infinite loop.
            // (In a perfect world, Paragraph/NewLine always catch everything).
            if (!foundMatch) scanPos++;
        }

        // Push to stack in reverse (LIFO) so the first winner is processed first
        _stack.EnsureCapacity(_stack.Count + topLevelWinners.Count);
        for (int i = topLevelWinners.Count - 1; i >= 0; i--) {
            PushMatchToStack(topLevelWinners[i].Match, node, topLevelWinners[i].Serializer);
        }
    }

    public void PushSingleLineMatchesToStack(string input, IMdSyntaxNode node) {
        ImmutableArray<IMdSyntaxNodeSerializer> serializers = SerializerReference.SingleLineSerializers;
        List<(Match Match, IMdSyntaxNodeSerializer Serializer)> matches = [];
        matches.AddRange(serializers.SelectMany(
            nodeSerializer => nodeSerializer.Syntax.Matches(input),
            (nodeSerializer, match) => (match, nodeSerializer))
        );

        // Sort by start index (ascending) then by length (descending)
        matches.Sort((a, b) => {
            int cmp = a.Match.Index.CompareTo(b.Match.Index);
            return cmp != 0 ? cmp : b.Match.Length.CompareTo(a.Match.Length);
        });

        // Filter out overlapping matches
        List<(Match Match, IMdSyntaxNodeSerializer Serializer)> filteredMatches = [];
        int lastEnd = 0;
        foreach (var m in matches) {
            if (m.Match.Index >= lastEnd) {
                filteredMatches.Add(m);
                lastEnd = m.Match.Index + m.Match.Length;
            }
        }

        int inputLength = input.Length;
        int lastProcessedIndex = inputLength;

        // Iterate backwards through filtered matches to handle gaps and LIFO stack behavior
        for (int i = filteredMatches.Count - 1; i >= 0; i--) {
            (Match match, IMdSyntaxNodeSerializer serializer) = filteredMatches[i];
            int matchEnd = match.Index + match.Length;

            // Handle text after this match
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

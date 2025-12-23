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

        // Sort by start index (ascending) then by length (descending) to prioritize longer matches at the same position
        matches.Sort((a, b) => {
            int cmp = a.Match.Index.CompareTo(b.Match.Index);
            return cmp != 0 ? cmp : b.Match.Length.CompareTo(a.Match.Length);
        });

        // Filter out overlapping matches (greedy first-win)
        List<(Match Match, IMdSyntaxNodeSerializer Serializer)> filteredMatches = [];
        int lastEnd = startIndex;
        foreach (var m in matches) {
            if (m.Match.Index >= lastEnd) {
                filteredMatches.Add(m);
                lastEnd = m.Match.Index + m.Match.Length;
            }
        }

        // Push to stack in reverse order (LIFO) so the first match in the text is processed first
        _stack.EnsureCapacity(_stack.Count + filteredMatches.Count);
        for (int i = filteredMatches.Count - 1; i >= 0; i--) {
            PushMatchToStack(filteredMatches[i].Match, node, filteredMatches[i].Serializer);
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

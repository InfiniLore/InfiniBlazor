// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using Microsoft.Extensions.ObjectPool;
using System.Buffers;
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
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region PushToStack
    public void PushMultiLineMatchesToStack(string input, IMdSyntaxNode node, int startIndex = 0) {
        ImmutableArray<IMdSyntaxNodeSerializer> serializers = SerializerReference.MultiLineSerializers;

        int scanPos = startIndex;
        int inputLength = input.Length;

        MdSyntaxFragment[] fragments = ArrayPool<MdSyntaxFragment>.Shared.Rent(8);
        int index = 0;

        try {
            while (scanPos < inputLength) {
                foreach (IMdSyntaxNodeSerializer serializer in serializers) {
                    Match m = serializer.Match(input, scanPos);
                    if (!m.Success || m.Index != scanPos) continue;

                    EnsureCapacity(ref fragments, index + 1);
                    fragments[index++] = MdSyntaxFragment.AsUnhandledMatch(m, node, serializer);

                    scanPos += Math.Max(1, m.Length);
                }
            }

            _stack.EnsureCapacity(_stack.Count + fragments.Length);
            for (int i = index - 1; i >= 0; i--) {
                _stack.Push(fragments[i]);
            }
        }
        finally {
            ArrayPool<MdSyntaxFragment>.Shared.Return(fragments);
        }
    }

    public void PushSingleLineMatchesToStack(string input, IMdSyntaxNode node) {
        ImmutableArray<IMdSyntaxNodeSerializer> serializers = SerializerReference.SingleLineSerializers;

        int scanPos = 0;
        int length = input.Length;
        MdSyntaxFragment[] fragments = ArrayPool<MdSyntaxFragment>.Shared.Rent(8);
        int index = 0;

        try {
            while (scanPos < length) {
                (Match Match, IMdSyntaxNodeSerializer Serializer)? next = null;

                foreach (IMdSyntaxNodeSerializer serializer in serializers) {
                    Match m = serializer.Match(input, scanPos);
                    if (!m.Success) continue;
                    
                    if (next is null
                        || m.Index < next.Value.Match.Index
                        || m.Index == next.Value.Match.Index && m.Length > next.Value.Match.Length) {
                        next = (m, serializer);
                    }
                }

                if (next is null) {
                    if (scanPos < length) {
                        TextMdSyntaxNode tail = MdSyntaxNodePool<TextMdSyntaxNode>.Shared.Get();
                        tail.WithContent(input[scanPos..]);
                        
                        EnsureCapacity(ref fragments, index + 1);
                        fragments[index++] = MdSyntaxFragment.AsProcessedNode(node, tail);
                    }

                    break;
                }

                Match match = next.Value.Match;
                IMdSyntaxNodeSerializer serializerWinner = next.Value.Serializer;

                if (match.Index > scanPos) {
                    TextMdSyntaxNode contentNode = MdSyntaxNodePool<TextMdSyntaxNode>.Shared.Get();
                    contentNode.WithContent(input[scanPos..match.Index]);

                    EnsureCapacity(ref fragments, index + 1);
                    fragments[index++] = MdSyntaxFragment.AsProcessedNode(node, contentNode);
                }

                EnsureCapacity(ref fragments, index + 1);
                fragments[index++] = MdSyntaxFragment.AsUnhandledMatch(match, node, serializerWinner);

                scanPos = match.Index + Math.Max(1, match.Length);
            }

            _stack.EnsureCapacity(_stack.Count + fragments.Length);
            for (int i = index - 1; i >= 0; i--) {
                _stack.Push(fragments[i]);
            }
        }
        finally {
            ArrayPool<MdSyntaxFragment>.Shared.Return(fragments);
        }
    }

    private static void EnsureCapacity(ref MdSyntaxFragment[] arr, int required) {
        if (required <= arr.Length) return;

        MdSyntaxFragment[] newArr = ArrayPool<MdSyntaxFragment>.Shared.Rent(arr.Length * 2);
        Array.Copy(arr, newArr, arr.Length);
        ArrayPool<MdSyntaxFragment>.Shared.Return(arr);
        arr = newArr;
    }

    public void PushProcessedNodeToStack(IMdSyntaxNode parentNode, IMdSyntaxNode childNode)
        => _stack.Push(MdSyntaxFragment.AsProcessedNode(parentNode, childNode));
    #endregion

    public bool TryPopDto(out MdSyntaxFragment dto) {
        return _stack.TryPop(out dto);
    }

    public bool TryReset() {
        _stack.Clear();
        return _stack.Count == 0;
    }
}

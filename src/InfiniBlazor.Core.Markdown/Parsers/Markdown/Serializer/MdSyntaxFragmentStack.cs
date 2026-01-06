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

        MdSyntaxFragment[] fragments = ArrayPool<MdSyntaxFragment>.Shared.Rent(32);
        int index = 0;

        try {
            while (scanPos < inputLength) {
                char currentChar = input[scanPos];
                bool matched = false;

                // Only try serializers if we hit a trigger character
                foreach (IMdSyntaxNodeSerializer serializer in serializers) {
                    if (!serializer.TriggerCharacters.IsEmpty() && Array.IndexOf(serializer.TriggerCharacters, currentChar) == -1) continue;

                    Match m = serializer.Match(input, scanPos);
                    if (!m.Success || m.Index != scanPos) continue;

                    EnsureCapacity(ref fragments, ref index, 1);
                    fragments[index++] = MdSyntaxFragment.AsUnhandledMatch(m, node, serializer);

                    scanPos += Math.Max(1, m.Length);
                    matched = true;
                    break;
                }

                if (matched) continue;

                // If no multiline block matched, move to the next line 
                // because most multiline structures are line-start dependent.
                int nextLine = input.IndexOf('\n', scanPos);
                scanPos = nextLine == -1 ? inputLength : nextLine + 1;
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
        int textStart = 0;
        MdSyntaxFragment[] fragments = ArrayPool<MdSyntaxFragment>.Shared.Rent(128);
        int index = 0;

        try {
            while (scanPos < length) {
                char currentChar = input[scanPos];
                IMdSyntaxNodeSerializer? winner = null;
                Match? winningMatch = null;

                // Only try serializers if we hit a trigger character
                foreach (IMdSyntaxNodeSerializer serializer in serializers) {
                    if (!serializer.TriggerCharacters.IsEmpty() && Array.IndexOf(serializer.TriggerCharacters, currentChar) == -1) continue;

                    Match m = serializer.Match(input, scanPos);
                    if (!m.Success || m.Index != scanPos) continue;

                    winner = serializer;
                    winningMatch = m;
                    break;
                }

                if (winner != null && winningMatch != null) {
                    // If we have accumulated text before this match, push it
                    if (scanPos > textStart) {
                        TextMdSyntaxNode contentNode = MdSyntaxNodePool<TextMdSyntaxNode>.Shared.Get();
                        contentNode.WithContent(input[textStart..scanPos]);
                        EnsureCapacity(ref fragments, ref index, 1);
                        fragments[index++] = MdSyntaxFragment.AsProcessedNode(node, contentNode);
                    }

                    // Push the actual match
                    EnsureCapacity(ref fragments, ref index, 1);
                    fragments[index++] = MdSyntaxFragment.AsUnhandledMatch(winningMatch, node, winner);

                    scanPos += Math.Max(1, winningMatch.Length);
                    textStart = scanPos; // Reset text pointer to after the match
                }
                else {
                    // No match at this position, just move the cursor
                    scanPos++;
                }
            }

            // Push any remaining trailing text
            if (textStart < length) {
                TextMdSyntaxNode tail = MdSyntaxNodePool<TextMdSyntaxNode>.Shared.Get();
                tail.WithContent(input[textStart..]);
                EnsureCapacity(ref fragments, ref index, 1);
                fragments[index++] = MdSyntaxFragment.AsProcessedNode(node, tail);
            }

            _stack.EnsureCapacity(_stack.Count + index);
            for (int i = index - 1; i >= 0; i--) {
                _stack.Push(fragments[i]);
            }
        }
        finally {
            ArrayPool<MdSyntaxFragment>.Shared.Return(fragments);
        }
    }

    private static void EnsureCapacity(ref MdSyntaxFragment[] arr, ref int index, int required) {
        if (index + required <= arr.Length) return;

        int newSize = arr.Length * 2;
        MdSyntaxFragment[] newArr = ArrayPool<MdSyntaxFragment>.Shared.Rent(newSize);
        Array.Copy(arr, newArr, index);
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

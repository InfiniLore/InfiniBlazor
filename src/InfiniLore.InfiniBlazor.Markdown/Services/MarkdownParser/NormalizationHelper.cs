// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Buffers;
using System.Text;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class NormalizationHelper {
    private const int StackAllocThreshold = 256;

    public static string NormalizeIndentation(ref ReadOnlySpan<char> input) {
        int matchCount = input.Count('\n');
        int splitCount = matchCount + 1;

        // Estimate initial capacity to avoid reallocations
        int estimatedCapacity = input.Length;
        StringBuilder stringBuilder = PoolCache.StringBuilderPool.Get();
        stringBuilder.EnsureCapacity(estimatedCapacity);

        Range[]? rentedArray = null;
        Span<Range> rangeSpan = splitCount <= StackAllocThreshold
            ? stackalloc Range[splitCount]
            : rentedArray = ArrayPool<Range>.Shared.Rent(splitCount);

        try {
            int minIndent = int.MaxValue;
            int index = 0;

            Regex.ValueSplitEnumerator splits = MarkdownRegexLib.NormalizeNewlinesRegex.EnumerateSplits(input);
            foreach (Range split in splits) {
                ReadOnlySpan<char> line = input[split];
                rangeSpan[index++] = split;

                if (line.IsEmpty) continue;

                int currentIndent = CountLeadingWhitespace(ref line);
                
                // Only consider non-empty lines for minIndent
                if (line.Length > currentIndent) {
                    minIndent = Math.Min(minIndent, currentIndent);
                }
            }

            if (minIndent == int.MaxValue) return input.ToString();

            // Process and append lines
            for (int i = 0; i < splitCount; i++) {
                ReadOnlySpan<char> line = input[rangeSpan[i]];
                if (!line.IsEmpty) {
                    int currentIndent = CountLeadingWhitespace(ref line);
                    stringBuilder.Append(line[Math.Min(currentIndent, minIndent)..]);
                }

                stringBuilder.Append('\n');
            }

            // Remove the last newline if it wasn't in the original input
            if (stringBuilder.Length > 0 && !input.EndsWith('\n')) {
                stringBuilder.Length--;
            }

            return stringBuilder.ToString();
        }
        finally {
            PoolCache.StringBuilderPool.Return(stringBuilder);
            if (rentedArray is not null) ArrayPool<Range>.Shared.Return(rentedArray);
        }
    }

    private static int CountLeadingWhitespace(ref ReadOnlySpan<char> line) {
        int count = 0;
        while (count < line.Length && char.IsWhiteSpace(line[count]) && line[count] != '\n')
            count++;

        return count;
    }

}

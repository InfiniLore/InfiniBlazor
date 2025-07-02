// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using System.Buffers;
using System.Text;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ILineNormalizationService>]
public class LineNormalizationService(IPoolCache poolCache) : ILineNormalizationService {
    private const int StackAllocThreshold = 256;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string NormalizeLineIndentation(ReadOnlySpan<char> input) {
        int matchCount = input.Count('\n');
        int splitCount = matchCount + 1;

        // Estimate initial capacity to avoid reallocations
        StringBuilder stringBuilder = poolCache.StringBuilderPool.Get();
        Range[]? rentedArray = null;
        Span<Range> rangeSpan = splitCount <= StackAllocThreshold
            ? stackalloc Range[splitCount]
            : rentedArray = ArrayPool<Range>.Shared.Rent(splitCount);

        try {
            int estimatedCapacity = input.Length;
            stringBuilder.EnsureCapacity(estimatedCapacity);

            int minIndent = int.MaxValue;
            int index = 0;

            Regex.ValueSplitEnumerator splits = MarkdownRegexLib.NormalizeNewlinesRegex.EnumerateSplits(input);
            foreach (Range split in splits) {
                ReadOnlySpan<char> line = input[split];
                rangeSpan[index++] = split;

                // Only consider non-empty lines for minIndent
                if (line.IsEmpty) continue;

                int currentIndent = CountLeadingWhitespace(line);
                if (line.Length > currentIndent) {
                    minIndent = Math.Min(minIndent, currentIndent);
                }
            }

            if (minIndent == int.MaxValue) return input.ToString();

            // Process and append lines
            for (int i = 0; i < splitCount; i++) {
                ReadOnlySpan<char> line = input[rangeSpan[i]];
                if (!line.IsEmpty) {
                    int currentIndent = CountLeadingWhitespace(line);
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
            poolCache.StringBuilderPool.Return(stringBuilder);
            if (rentedArray is not null) ArrayPool<Range>.Shared.Return(rentedArray);
        }
    }

    private static int CountLeadingWhitespace(ReadOnlySpan<char> line) {
        int count = 0;
        while (count < line.Length) {
            char c = line[count];
            if (!c.IsWhiteSpace() || c == '\n') return count;
            count++;
        }

        return count;
    }

}

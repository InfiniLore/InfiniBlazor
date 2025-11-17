// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions;
using InfiniBlazor.Pooling;
using System.Buffers;
using System.Text;

namespace InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class LineNormalization {
    private const int StackAllocThreshold = 256;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static string NormalizeLineIndentation(ReadOnlySpan<char> input, out int leadingSpaces) {
        int matchCount = input.Count('\n');
        int splitCount = matchCount + 1;
        leadingSpaces = -1;
        int newLineCountFromEnd = NewLineCountFromEnd(input);
        
        // Estimate initial capacity to avoid reallocations
        StringBuilder stringBuilder = GlobalPools.StringBuilder.Get();
        Range[]? rentedArray = null;
        Span<Range> rangeSpan = splitCount <= StackAllocThreshold
            ? stackalloc Range[splitCount]
            : rentedArray = ArrayPool<Range>.Shared.Rent(splitCount);

        try {
            int estimatedCapacity = input.Length;
            stringBuilder.EnsureCapacity(estimatedCapacity);

            int minIndent = int.MaxValue;
            int index = 0;
            
            foreach (Range split in input.Split('\n')) {
                ReadOnlySpan<char> line = input[split];
                rangeSpan[index++] = split;

                // Only consider non-empty lines for minIndent
                if (line.IsEmpty) continue;

                int currentIndent = CountLeadingWhitespace(line);
                if (line.Length > currentIndent) {
                    minIndent = Math.Min(minIndent, currentIndent);
                    leadingSpaces = minIndent;
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
            
            // Remove the last newlines if they weren't in the original input
            while (stringBuilder.Length > 0 && newLineCountFromEnd > 0 && stringBuilder[^1] == '\n') {
                stringBuilder.Length--;
                newLineCountFromEnd--;
            }
            
            return stringBuilder.ToString();
        }
        finally {
            GlobalPools.StringBuilder.Return(stringBuilder);
            if (rentedArray is not null) ArrayPool<Range>.Shared.Return(rentedArray);
        }
    }
    
    private  static int NewLineCountFromEnd(ReadOnlySpan<char> input) {
        int count = 0;

        for (int i = input.Length - 1; i >= 0; i--) {
            if (input[i] == '\n') {
                count++;
            } else {
                break;
            }
        }

        return count;
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
    
    public static string NormalizeBlockQuote(ReadOnlySpan<char> span, out int leadingSpaces) {
        ReadOnlySpan<char> normalized = NormalizeLinePrefixes(span, ">");
        string adjusted = NormalizeLineIndentation(normalized, out leadingSpaces);
        return adjusted;
    }

    private static ReadOnlySpan<char> NormalizeLinePrefixes(ReadOnlySpan<char> span, ReadOnlySpan<char> prefix) {
        int prefixLength = prefix.Length;
        StringBuilder builder = GlobalPools.StringBuilder.Get();
        try {
            foreach (ReadOnlySpan<char> line in span.EnumerateLines()) {
                ReadOnlySpan<char> trimmedLine = line.TrimStart();

                if (trimmedLine.StartsWith(prefix)) {
                    trimmedLine = trimmedLine[prefixLength..];// Remove prefix
                }

                // Append the normalized line back to the builder
                builder.Append(trimmedLine);
                builder.Append('\n');
            }
            if (builder.Length > 0) builder.Length--; // Remove the last newline
            return builder.ToString();
        }
        finally {
            GlobalPools.StringBuilder.Return(builder);
        }
    }
}

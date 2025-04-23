// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Text;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class NormalizationHelper {
    public static string NormalizeIndentation(ref ReadOnlySpan<char> input) {
        Regex.ValueSplitEnumerator splits = MarkdownRegexLib.NormalizeNewlinesRegex.EnumerateSplits(input);
        int minIndent = int.MaxValue;

        foreach (Range split in splits) {
            ReadOnlySpan<char> line = input[split];
            ReadOnlySpan<char> trimmed = line.TrimStart();
            if (trimmed.IsEmpty) continue;
            minIndent = Math.Min(minIndent, line.Length - trimmed.Length);
        }
        
        if (minIndent == int.MaxValue) return input.ToString();
        

        splits = MarkdownRegexLib.NormalizeNewlinesRegex.EnumerateSplits(input);
        StringBuilder stringBuilder = PoolCache.StringBuilderPool.Get();

        try {
            foreach (Range range in splits) {
                ReadOnlySpan<char> line = input[range];
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
        }
    }
    
    private static int CountLeadingWhitespace(ref ReadOnlySpan<char> line)
    {
        int count = 0;
        while (count < line.Length && line[count] <= ' ' && line[count] != '\n')
            count++;
        return count;
    }

}

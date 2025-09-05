// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Pooling;
using System.Collections.Frozen;
using System.Text;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static partial class HtmlStringUtilities {
    private static readonly FrozenSet<string> BlockElements = [
        "<div", "<p", "<h1", "<h2", "<h3", "<h4", "<h5", "<h6",
        "<ul", "<ol", "<li", "<table", "<thead", "<tbody", "<tr",
        "<blockquote", "<hr"
    ];

    [GeneratedRegex(@"\s+")]
    private static partial Regex EmptySpacesRegex { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static string PrettifyHtml(string? html) {
        if (string.IsNullOrWhiteSpace(html)) return string.Empty;

        StringBuilder sb = GlobalPools.StringBuilder.Get();

        try {
            int indentLevel = 0;
            string[] lines = html.Split('<', StringSplitOptions.RemoveEmptyEntries);
            var previousLine = ReadOnlySpan<char>.Empty;

            foreach (string line in lines) {
                if (line.IsNullOrWhiteSpace()) continue;

                string fullLine = "<" + line;
                if (fullLine.StartsWith("</")) {
                    ReadOnlySpan<char> tagName = fullLine.Substring(2, fullLine.IndexOf('>') - 2);

                    int length = previousLine.IndexOf(' ') > 0
                        ? previousLine.IndexOf(' ') - 1
                        : previousLine.IndexOf('>') - 1;
                    
                    ReadOnlySpan<char> lastOpenTag = previousLine.Slice(1, length);

                    if (lastOpenTag.SequenceEqual(tagName)) {
                        sb.Length--;// Remove the last newline
                        sb.AppendLine(fullLine);
                        indentLevel--;
                        continue;
                    }

                    indentLevel = Math.Max(0, indentLevel - 1);
                }
                
                sb.Append(new string(' ', indentLevel * 2));

                string cleanedLine = EmptySpacesRegex.Replace(fullLine, " ").Trim();
                sb.AppendLine(cleanedLine);

                if (!fullLine.StartsWith("</")
                    && !fullLine.EndsWith("/>")
                    && !fullLine.Contains("</")
                    && BlockElements.Any(e => fullLine.StartsWith(e, StringComparison.OrdinalIgnoreCase))) {
                    indentLevel++;
                }

                previousLine = fullLine;
            }

            return sb.ToString();
        }
        catch {
            return html;
        }
        finally {
            GlobalPools.StringBuilder.Return(sb);
        }

    }
}

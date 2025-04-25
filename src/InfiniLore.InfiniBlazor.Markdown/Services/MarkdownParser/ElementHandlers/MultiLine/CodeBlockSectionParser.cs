// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("codeBlock")]
public class CodeBlockHandler : IMarkdownElementHandler {

    private static readonly int CBodyId = MarkdownRegexLib.GetMultiLineGroupId("cBody");
    private static readonly int CLangId = MarkdownRegexLib.GetMultiLineGroupId("cLang");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMarkdownSyntaxNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[CBodyId].TryGetValueSpan(out ReadOnlySpan<char> codeBlockBody)) return;

        IMarkdownSyntaxNode codeNode = currentNode.AddChildNode(MarkdownElement.CodeBlock);

        string langNameValue = entireMatch.Groups[CLangId].Value;
        if (!langNameValue.IsEmpty()) codeNode.WithAttribute(MarkdownAttribute.CodeLanguage, langNameValue);

        string content = ProcessCodeBlockContent(ref codeBlockBody);
        codeNode.WithContent(content);
    }

    private static string ProcessCodeBlockContent(ref ReadOnlySpan<char> content) {
        if (!content.Contains('\r')) return content.ToString();

        const int stackAllocThreshold = 1024;

        if (content.Length <= stackAllocThreshold) {
            // Use stack allocation for small strings
            Span<char> result = stackalloc char[content.Length];
            int length = ProcessContent(content, result);
            return new string(result[..length]);
        }

        // Use array pool for larger strings
        char[] rentedArray = ArrayPool<char>.Shared.Rent(content.Length);
        try {
            Span<char> asSpan = rentedArray.AsSpan();
            int length = ProcessContent(content, asSpan);
            return new string(rentedArray.AsSpan(0, length));
        }
        finally {
            ArrayPool<char>.Shared.Return(rentedArray);
        }
    }

    private static int ProcessContent(ReadOnlySpan<char> content, Span<char> result) {
        int destinationIndex = 0;

        for (int i = 0; i < content.Length; i++) {
            switch (content[i]) {
                case '\r' when i + 1 < content.Length && content[i + 1] == '\n': {
                    // Don't add a newline if it's the last character sequence
                    if (i + 2 < content.Length) {
                        result[destinationIndex++] = '\n';
                    }
                    i++; // Skip the \n
                    break;
                }
                default: result[destinationIndex++] = content[i];
                    break;

                // Skip the last single \n if present
                case '\n' when i == content.Length - 1:
                    break;
            }
        }

        return destinationIndex;
    }

}

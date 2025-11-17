// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CodeBlockSyntaxNodeSerializer {
    private static readonly int CBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeBlockContent);
    private static readonly int CLangId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeBlockLang);
    private static readonly int CTrailingId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeBlockTrailing);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[CBodyId].TryGetValueSpan(out ReadOnlySpan<char> codeBlockBody)) return;

        CodeBlockMdSyntaxNode codeNode = CodeBlockMdSyntaxNode.Pool.Get();

        string langNameValue = match.Groups[CLangId].Value;
        if (!langNameValue.IsEmpty()) codeNode.WithLanguage(langNameValue);

        string content = ProcessCodeBlockContent(ref codeBlockBody);
        codeNode.WithContent(content);
        parentNode.AddChildNode(codeNode);
        
        // Add trailing text as a paragraph node
        if (!match.Groups[CTrailingId].TryGetValue(out string? trailing)) return;
        ParagraphMdSyntaxNode paragraphNode = ParagraphMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(paragraphNode);
        stack.PushSingleLineMatchesToStack(trailing, paragraphNode);
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

    private static int ProcessContent(
        ReadOnlySpan<char> content,
        Span<char> result
    ) {
        int destinationIndex = 0;

        for (int i = 0; i < content.Length; i++) {
            switch (content[i]) {
                case '\r' when i + 1 < content.Length && content[i + 1] == '\n': {
                    // Don't add a newline if it's the last character sequence
                    if (i + 2 < content.Length) {
                        result[destinationIndex++] = '\n';
                    }
                    i++;// Skip the \n
                    break;
                }

                default:
                    result[destinationIndex++] = content[i];
                    break;

                // Skip the last single \n if present
                case '\n' when i == content.Length - 1:
                    break;
            }
        }

        return destinationIndex;
    }

}

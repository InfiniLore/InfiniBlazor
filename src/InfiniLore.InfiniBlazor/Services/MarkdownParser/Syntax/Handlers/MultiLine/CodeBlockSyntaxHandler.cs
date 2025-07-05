// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Handlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MdRegexGroupNames.CodeBlock)]
public sealed class CodeBlockSyntaxHandler : IMdSyntaxHandler {
    private static readonly int CBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.CBody);
    private static readonly int CLangId = MdRegexLib.GetGroupId(MdRegexGroupNames.CLang);
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxHandlerOrigin parentOrigin
    ) {
        if (!entireMatch.Groups[CBodyId].TryGetValueSpan(out ReadOnlySpan<char> codeBlockBody)) return;

        CodeBlockMdSyntaxNode codeNode = CodeBlockMdSyntaxNode.Pool.Get();

        string langNameValue = entireMatch.Groups[CLangId].Value;
        if (!langNameValue.IsEmpty()) codeNode.Language = langNameValue;

        string content = ProcessCodeBlockContent(ref codeBlockBody);
        codeNode.ContentCode = content;
        parentNode.AddChildNode(codeNode);
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

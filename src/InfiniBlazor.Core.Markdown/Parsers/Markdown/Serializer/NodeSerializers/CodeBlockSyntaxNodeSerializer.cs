// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class CodeBlockSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    [GeneratedRegex(@"^(?<open>`{3,})[\ ]*(?<lang>.*?)?\n(?<body>(?>[\s\S]|(?!\k<open>))*?)\k<open>(?<tail>[^\n]+)?$", RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }
    
    private static readonly int CBodyId = Syntax.GroupNumberFromName("body");
    private static readonly int CLangId = Syntax.GroupNumberFromName("lang");
    private static readonly int CTrailId = Syntax.GroupNumberFromName("tail");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Match Match(string input, int startPosition = 0) 
        => Syntax.Match(input, startPosition);
    
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[CBodyId].TryGetValueSpan(out ReadOnlySpan<char> codeBlockBody)) return;

        CodeBlockMdSyntaxNode codeNode = MdSyntaxNodePool<CodeBlockMdSyntaxNode>.Shared.Get();

        string langNameValue = match.Groups[CLangId].Value;
        if (!langNameValue.IsEmpty()) codeNode.WithLanguage(langNameValue);

        string content = ProcessCodeBlockContent(ref codeBlockBody);
        codeNode.WithContent(content);
        parentNode.AddChildNode(codeNode);
        
        // Add trailing text as a paragraph node
        if (!match.Groups[CTrailId].TryGetValue(out string? trailing)) return;
        ParagraphMdSyntaxNode paragraphNode = MdSyntaxNodePool<ParagraphMdSyntaxNode>.Shared.Get();
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
                    result[destinationIndex++] = '\n';
                    i++;// Skip the \n
                    break;
                }

                default:
                    result[destinationIndex++] = content[i];
                    break;
            }
        }

        return destinationIndex;
    }

}

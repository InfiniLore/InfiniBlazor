// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CodeInlineSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    private static readonly int CodeContentId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeInlineContent);
    private static readonly int CodeInlineId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeInline);

    public Regex Syntax { get; } = MdRegexLib.CodeInlineRegex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        if (!match.Groups[CodeContentId].TryGetValue(out string? codeValue)) return;
        if (!match.Groups[CodeInlineId].TryGetValueSpan(out ReadOnlySpan<char> fullOriginalString)) return;

        CodeInlineMdSyntaxNode node = CodeInlineMdSyntaxNode.Pool.Get();
        node.WithContent(codeValue);

        // Calculate backtick count by comparing full string length to content length
        int totalLength = fullOriginalString.Length;
        int contentLength = codeValue.Length;
        int totalBackticks = totalLength - contentLength;
        int backtickCount = totalBackticks / 2;// Backticks on one side

        node.WithBackTickCount(backtickCount);
        parentNode.AddChildNode(node);
    }
}

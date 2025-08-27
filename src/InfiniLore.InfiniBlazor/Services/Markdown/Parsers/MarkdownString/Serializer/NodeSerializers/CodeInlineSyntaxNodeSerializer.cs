// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CodeInlineSyntaxNodeSerializer  {
    private static readonly int CodeContentId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeInlineContent);
    private static readonly int CodeInlineId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeInline);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match match) {
        if (!match.Groups[CodeContentId].TryGetValue(out string? codeValue)) return ;
        if (!match.Groups[CodeInlineId].TryGetValueSpan(out ReadOnlySpan<char> fullOriginalString)) return ;

        CodeInlineMdSyntaxNode node = CodeInlineMdSyntaxNode.Pool.Get();
        node.OriginalContentCode = codeValue;
        
        // Calculate backtick count by comparing full string length to content length
        int totalLength = fullOriginalString.Length;
        int contentLength = codeValue.Length;
        int totalBackticks = totalLength - contentLength;
        int backtickCount = totalBackticks / 2; // Backticks on one side
        
        node.BackTickCount = backtickCount;
        parentNode.AddChildNode(node);
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.CodeInline)]
public sealed class CodeInlineSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int CodeContentId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeInlineContent);
    private static readonly int CodeInlineId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeInline);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match entireMatch) {
        if (!entireMatch.Groups[CodeContentId].TryGetValue(out string? codeValue)) return ;
        if (!entireMatch.Groups[CodeInlineId].TryGetValueSpan(out ReadOnlySpan<char> fullOriginalString)) return ;

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

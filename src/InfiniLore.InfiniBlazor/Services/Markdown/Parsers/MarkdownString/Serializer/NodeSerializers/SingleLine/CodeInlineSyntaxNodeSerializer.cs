// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.CodeInline)]
public sealed class CodeInlineSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int CodeContentId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeInlineContent);
    private static readonly int CodeInlineId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeInline);
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.Code;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMdSyntaxFragmentStack stack, IMdSyntaxNode parentNode, Match entireMatch, MdSyntaxSerializerOrigin parentOrigin) {
        if (!entireMatch.Groups[CodeContentId].TryGetValue(out string? codeValue)) return ;
        if (!entireMatch.Groups[CodeInlineId].TryGetValueSpan(out ReadOnlySpan<char> fullOriginalString)) return ;

        CodeInlineMdSyntaxNode node = CodeInlineMdSyntaxNode.Pool.Get();
        node.OriginalContentCode = codeValue;
        
        int backtickCount = 0;
        foreach (char currentChar in fullOriginalString) {
            if (currentChar != '`') break;
            backtickCount++;
        }

        node.BackTickCount = backtickCount;
        parentNode.AddChildNode(node);
    }
}

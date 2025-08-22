// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.CodeInline)]
public sealed class CodeInlineSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int CId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeInlineContent);
    public MarkdownStringMdSyntaxSerializerOrigin SkipOnOrigin => MarkdownStringMdSyntaxSerializerOrigin.Code;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownStringMdSyntaxSerializerStack stack, IMdSyntaxNode parentNode, Match entireMatch, MarkdownStringMdSyntaxSerializerOrigin parentOrigin) {
        if (!entireMatch.Groups[CId].TryGetValue(out string? codeValue)) return ;

        string normalizedBackticks = codeValue.Replace("\\`", "`");
        CodeInlineMdSyntaxNode node = CodeInlineMdSyntaxNode.Pool.Get();
        node.ContentCode = normalizedBackticks;
        parentNode.AddChildNode(node);
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer.NodeSerializers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxNodeSerializer>(MdRegexGroupNames.CodeInline)]
public sealed class CodeInlineSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int CId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeInlineContent);
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.Code;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMdSyntaxSerializerStack stack, IMdSyntaxNode parentNode, Match entireMatch, MdSyntaxSerializerOrigin parentOrigin) {
        if (!entireMatch.Groups[CId].TryGetValue(out string? codeValue)) return ;

        string normalizedBackticks = codeValue.Replace("\\`", "`");
        CodeInlineMdSyntaxNode node = CodeInlineMdSyntaxNode.Pool.Get();
        node.ContentCode = normalizedBackticks;
        parentNode.AddChildNode(node);
    }
}

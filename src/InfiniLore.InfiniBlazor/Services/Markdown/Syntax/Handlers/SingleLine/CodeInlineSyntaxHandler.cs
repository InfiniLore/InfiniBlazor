// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Handlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MdRegexGroupNames.CodeInline)]
public sealed class CodeInlineSyntaxHandler : IMdSyntaxHandler {
    private static readonly int CId = MdRegexLib.GetGroupId(MdRegexGroupNames.CodeInlineContent);
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.Code;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMdSyntaxParserStack stack, IMdSyntaxNode parentNode, Match entireMatch, MdSyntaxHandlerOrigin parentOrigin) {
        if (!entireMatch.Groups[CId].TryGetValue(out string? codeValue)) return ;

        string normalizedBackticks = codeValue.Replace("\\`", "`");
        CodeInlineMdSyntaxNode node = CodeInlineMdSyntaxNode.Pool.Get();
        node.ContentCode = normalizedBackticks;
        parentNode.AddChildNode(node);
    }
}

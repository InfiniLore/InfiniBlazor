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
[InjectableSingleton<IMdSyntaxHandler>(MdRegexGroupNames.Underline)]
public sealed class UnderlineSyntaxHandler : IMdSyntaxHandler {
    private static readonly int UId = MdRegexLib.GetGroupId(MdRegexGroupNames.UnderlineContent);
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.Underline;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxHandlerOrigin parentOrigin
    ) {
        if (!entireMatch.Groups[UId].TryGetValue(out string? underlineValue)) return ;
        
        UnderlineMdSyntaxNode node = UnderlineMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(underlineValue, node, parentOrigin | SkipOnOrigin);
    }
}

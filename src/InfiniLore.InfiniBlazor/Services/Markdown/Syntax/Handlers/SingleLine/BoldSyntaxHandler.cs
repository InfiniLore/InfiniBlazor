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
[InjectableSingleton<IMdSyntaxHandler>(MdRegexGroupNames.BoldContent)]
public sealed class BoldSyntaxHandler : IMdSyntaxHandler {
    private static readonly int BId = MdRegexLib.GetGroupId(MdRegexGroupNames.Bold);
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.Bold;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMdSyntaxParserStack stack, IMdSyntaxNode parentNode, Match entireMatch, MdSyntaxHandlerOrigin parentOrigin) {
        if (!entireMatch.Groups[BId].TryGetValue(out string? boldValue)) return;

        BoldMdSyntaxNode node = BoldMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(boldValue, node, parentOrigin | SkipOnOrigin);
    }
}
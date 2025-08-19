// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Handlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MdRegexGroupNames.HeadingSimple)]
public sealed class HeadingSimpleSyntaxHandler : IMdSyntaxHandler {
    private static readonly int HsTextId = MdRegexLib.GetGroupId(MdRegexGroupNames.HeadingSimpleText);
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxHandlerOrigin parentOrigin
    ) {
        if (!entireMatch.Groups[HsTextId].TryGetValue(out string? headerSimpleText)) return;

        HeadingMdSyntaxNode headingNode = HeadingMdSyntaxNode.Pool.Get();
        headingNode.Level = 1;
        parentNode.AddChildNode(headingNode);
        
        stack.PushSingleLineMatchesToStack(headerSimpleText, headingNode, parentOrigin);
    }
}

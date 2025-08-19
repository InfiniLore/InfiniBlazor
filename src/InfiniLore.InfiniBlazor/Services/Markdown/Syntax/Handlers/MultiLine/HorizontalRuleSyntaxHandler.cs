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
[InjectableSingleton<IMdSyntaxHandler>(MdRegexGroupNames.HorizontalRule)]
public sealed class HorizontalRuleSyntaxHandler : IMdSyntaxHandler {
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
        parentNode.AddChildNode(HorizontalRuleMdSyntaxNode.Pool.Get());
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("subScript")]
public class SubScriptSectionParser : ISectionHandler {

    private static readonly int SbId = MarkdownRegexLib.GetSingleLineGroupId("sb");
    public ParserOrigin SkipOnOrigin => ParserOrigin.SubScript;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[SbId].TryGetValue(out string? subValue)) return;

        IMdNode node = currentNode.AddChildNode(MdElement.Subscript);
        engine.AddSingleLineMatchesToStack(subValue, node, origin | SkipOnOrigin);
    }
}

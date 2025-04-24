// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("heading")]
public class HeadingSectionParser : ISectionHandler {

    private static readonly int HLevelId = MarkdownRegexLib.GetMultiLineGroupId("hLevel");
    private static readonly int HTextId = MarkdownRegexLib.GetMultiLineGroupId("hText");
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[HLevelId].TryGetLength(out int headingLevel)) return;
        if (!entireMatch.Groups[HTextId].TryGetValue(out string? headerText)) return;

        MdElement mdElement = headingLevel switch {
            1 => MdElement.H1,
            2 => MdElement.H2,
            3 => MdElement.H3,
            4 => MdElement.H4,
            5 => MdElement.H5,
            6 => MdElement.H6,
            _ => MdElement.H1
        };

        IMdNode headingElement = currentNode.AddChildNode(mdElement);
        engine.AddSingleLineMatchesToStack(headerText, headingElement, origin);
    }
}

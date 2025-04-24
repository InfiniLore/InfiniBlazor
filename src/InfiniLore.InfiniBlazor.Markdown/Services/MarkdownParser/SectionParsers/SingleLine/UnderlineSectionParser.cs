// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("underline")]
public class UnderlineSectionParser : ISectionHandler {

    private static readonly int UId = MarkdownRegexLib.GetSingleLineGroupId("u");
    public ParserOrigin SkipOnOrigin => ParserOrigin.Underline;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[UId].TryGetValue(out string? underlineValue)) return;

        IMdNode underlineNode = currentNode.AddChildNode(MdElement.Underline);
        engine.AddSingleLineMatchesToStack(underlineValue, underlineNode, origin | SkipOnOrigin);
    }
}

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

    private static readonly int UId = CachedRegexGroupNames.GetSingleLineGroupId("u");
    public ParserOrigin SkipOnOrigin => ParserOrigin.Underline;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[UId].TryGetValue(out string? underlineValue)) return;

        IMdNode underlineNode = currentNode.AddChildNode(MdElement.Underline);
        parser.AddSingleLineMatchesToStack(underlineValue, underlineNode, origin | SkipOnOrigin);
    }
}

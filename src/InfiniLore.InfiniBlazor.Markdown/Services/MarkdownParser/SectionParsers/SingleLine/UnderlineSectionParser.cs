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
    public ParserOrigin SkipOnOrigin => ParserOrigin.Underline;

    private static readonly int UId = CachedRegexGroupNames.GetSingleLineGroupId("u");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[UId].TryGetValue(out string? underlineValue)) return;
        
        var underlineNode = currentNode.AddChildNode(MdElement.Underline);
        parser.AddSingleLineMatchesToStack(underlineValue, underlineNode, origin | SkipOnOrigin);
    }
}

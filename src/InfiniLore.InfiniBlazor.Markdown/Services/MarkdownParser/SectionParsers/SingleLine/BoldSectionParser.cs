// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("bold")]
public class BoldSectionParser : ISectionHandler {

    private static readonly int BId = MarkdownRegexLib.GetSingleLineGroupId("b");
    public ParserOrigin SkipOnOrigin => ParserOrigin.Bold;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[BId].TryGetValue(out string? boldValue)) return;

        IMdNode boldNode = currentNode.AddChildNode(MdElement.Bold);
        parser.AddSingleLineMatchesToStack(boldValue, boldNode, origin | SkipOnOrigin);
    }
}

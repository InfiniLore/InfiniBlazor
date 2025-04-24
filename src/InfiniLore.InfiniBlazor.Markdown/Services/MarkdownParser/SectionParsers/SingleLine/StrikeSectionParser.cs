// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("strike")]
public class StrikeSectionParser : ISectionHandler {

    private static readonly int SId = MarkdownRegexLib.GetSingleLineGroupId("s");
    public ParserOrigin SkipOnOrigin => ParserOrigin.Strike;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[SId].TryGetValue(out string? strikeValue)) return;

        IMdNode node = currentNode.AddChildNode(MdElement.Strikethrough);
        parser.AddSingleLineMatchesToStack(strikeValue, node, origin | SkipOnOrigin);
    }
}

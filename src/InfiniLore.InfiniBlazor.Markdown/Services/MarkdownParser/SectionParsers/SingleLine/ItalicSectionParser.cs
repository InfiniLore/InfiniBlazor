// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("italic")]
public class ItalicSectionParser : ISectionHandler {

    private static readonly int IId = MarkdownRegexLib.GetSingleLineGroupId("i");
    public ParserOrigin SkipOnOrigin => ParserOrigin.Italic;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[IId].TryGetValue(out string? italicValue)) return;

        IMdNode node = currentNode.AddChildNode(MdElement.Italic);
        parser.AddSingleLineMatchesToStack(italicValue, node, origin | SkipOnOrigin);
    }
}

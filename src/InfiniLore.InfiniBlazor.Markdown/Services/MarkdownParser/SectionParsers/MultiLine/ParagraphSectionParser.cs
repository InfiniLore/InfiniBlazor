// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("paragraph")]
public class ParagraphSectionParser : ISectionHandler {

    private static readonly int PId = CachedRegexGroupNames.GetSingleLineGroupId("p");
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[PId].TryGetValue(out string? paragraph)) return;
        if (paragraph.IsNullOrWhiteSpace()) return;

        bool writeParagraph = !origin.HasFlag(ParserOrigin.Html);

        if (writeParagraph) currentNode = currentNode.AddChildNode(MdElement.Paragraph);
        parser.AddSingleLineMatchesToStack(paragraph.TrimStart(), currentNode, origin);
    }
}

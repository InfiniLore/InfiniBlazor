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
public class ParagraphSectionParser(ICachedRegexGroupNames groupNames) : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    
    private readonly int PId = groupNames.GetSingleLineGroupId("p");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group group, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        if (!entireMatch.Groups[PId].TryGetValue(out string? paragraph)) return;
        if (paragraph.IsNullOrWhiteSpace()) return;
        bool writeParagraph = !origin.HasFlag(ParserOrigin.Html);

        if (writeParagraph) currentNode = currentNode.AddChildNode(MdElement.Paragraph);
        parser.AddSingleLineMatchesToStack(paragraph.TrimStart(), currentNode, origin);
    }
}

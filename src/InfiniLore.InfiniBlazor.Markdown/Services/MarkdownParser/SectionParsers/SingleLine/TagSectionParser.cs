// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("tag")]
public class TagSectionParser : ISectionHandler {

    private static readonly int TextId = CachedRegexGroupNames.GetSingleLineGroupId("tText");
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[TextId].TryGetValue(out string? tagValue)) return;

        IMdNode spanNode = currentNode.AddChildNode(MdElement.Tag);
        spanNode.WithContent(tagValue);
    }
}

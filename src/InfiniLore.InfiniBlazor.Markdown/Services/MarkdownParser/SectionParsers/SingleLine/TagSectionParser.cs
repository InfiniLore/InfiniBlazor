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
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;

    private static readonly int TextId = CachedRegexGroupNames.GetSingleLineGroupId("tText");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[TextId].TryGetValue(out string? tagValue)) return;
        
        IMdNode spanNode = currentNode.AddChildNode(MdElement.Tag);
        spanNode.WithContent(tagValue);
    }
}

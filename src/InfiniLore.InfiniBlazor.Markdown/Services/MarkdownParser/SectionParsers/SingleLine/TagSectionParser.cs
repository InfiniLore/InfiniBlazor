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
public class TagSectionParser(ICachedRegexGroupNames groupNames) : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;

    private readonly int TTextId = groupNames.GetSingleLineGroupId("tText");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group _, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        if (!entireMatch.Groups[TTextId].TryGetValueSpan(out ReadOnlySpan<char> tagValue)) return;
        
        IMdNode spanNode = currentNode.AddChild(MdElement.Span);
        spanNode.WithContent($"#{tagValue}");
    }
}

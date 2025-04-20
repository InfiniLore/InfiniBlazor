// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("escaped")]
public class EscapedSectionParser: ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group group, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser)  {
        char value = group.ValueSpan[1];
        ReadOnlySpan<char> span = [value];
        if (HtmlSymbolLookup.AlternateLookup.TryGetValue(span, out string? alternate)) {
            currentNode.WithContent(alternate);
            return;
        }
        
        currentNode.WithContent(value.ToString());
    }
}

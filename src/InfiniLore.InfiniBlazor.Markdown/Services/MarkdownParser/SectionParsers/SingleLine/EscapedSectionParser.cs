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
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin)  {
        char value = group.ValueSpan[1];
        currentNode.WithContent(value.ToString());
    }
}

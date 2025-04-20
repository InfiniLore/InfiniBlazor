// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("horizontalRule")]
public class HorizontalRuleSectionParser : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    
    public void HandleMatch(Match entireMatch, Group _, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        currentNode.AddChild(MdElement.HorizontalRule);
    }
}

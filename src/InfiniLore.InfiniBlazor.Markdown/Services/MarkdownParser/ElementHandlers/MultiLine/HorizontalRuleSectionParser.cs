// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("horizontalRule")]
public class HorizontalRuleHandler : IMarkdownElementHandler {
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;

    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        currentNode.AddChildNode(MdElement.HorizontalRule);
    }
}

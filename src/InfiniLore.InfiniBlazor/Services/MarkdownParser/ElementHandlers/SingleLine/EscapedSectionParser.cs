// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("escaped")]
public class EscapedHandler : IMarkdownElementHandler {
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMarkdownParserEngine engine,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin
    ) {
        char value = group.ValueSpan[1];
        EscapedCharacterMdSyntaxNode node = EscapedCharacterMdSyntaxNode.Shared.Get();
        node.ContentChar = value;
        parentNode.AddChildNode(node);
    }
}

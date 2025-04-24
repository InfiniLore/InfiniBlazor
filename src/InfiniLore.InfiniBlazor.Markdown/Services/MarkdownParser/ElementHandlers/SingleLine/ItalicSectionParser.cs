// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("italic")]
public class ItalicHandler : IMarkdownElementHandler {

    private static readonly int IId = MarkdownRegexLib.GetSingleLineGroupId("i");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.Italic;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMarkdownSyntaxNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[IId].TryGetValue(out string? italicValue)) return;

        IMarkdownSyntaxNode node = currentNode.AddChildNode(MarkdownElement.Italic);
        engine.AddSingleLineMatchesToStack(italicValue, node, origin | SkipOnOrigin);
    }
}

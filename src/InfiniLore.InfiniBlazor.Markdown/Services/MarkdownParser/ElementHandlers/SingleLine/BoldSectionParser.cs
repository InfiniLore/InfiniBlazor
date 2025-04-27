// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("bold")]
public class BoldHandler : IMarkdownElementHandler {

    private static readonly int BId = MarkdownRegexLib.GetSingleLineGroupId("b");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.Bold;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ValueTask HandleMatchAsync(IMarkdownParserEngine engine, IMarkdownSyntaxNode currentNode, Match entireMatch, Group group, HandlerOrigin origin, CancellationToken ct = default) {
        if (!entireMatch.Groups[BId].TryGetValue(out string? boldValue)) return ValueTask.CompletedTask;

        IMarkdownSyntaxNode boldNode = currentNode.AddChildNode(MarkdownElement.Bold);
        engine.AddSingleLineMatchesToStack(boldValue, boldNode, origin | SkipOnOrigin);
        return ValueTask.CompletedTask;
    }
}

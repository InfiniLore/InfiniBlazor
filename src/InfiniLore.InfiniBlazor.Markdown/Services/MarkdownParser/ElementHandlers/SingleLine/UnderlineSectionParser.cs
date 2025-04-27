// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("underline")]
public class UnderlineHandler : IMarkdownElementHandler {

    private static readonly int UId = MarkdownRegexLib.GetSingleLineGroupId("u");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.Underline;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ValueTask HandleMatchAsync(IMarkdownParserEngine engine, IMarkdownSyntaxNode currentNode, Match entireMatch, Group group, HandlerOrigin origin, CancellationToken ct = default) {
        if (!entireMatch.Groups[UId].TryGetValue(out string? underlineValue)) return ValueTask.CompletedTask;

        IMarkdownSyntaxNode underlineNode = currentNode.AddChildNode(MarkdownElement.Underline);
        engine.AddSingleLineMatchesToStack(underlineValue, underlineNode, origin | SkipOnOrigin);
        return ValueTask.CompletedTask;
    }
}

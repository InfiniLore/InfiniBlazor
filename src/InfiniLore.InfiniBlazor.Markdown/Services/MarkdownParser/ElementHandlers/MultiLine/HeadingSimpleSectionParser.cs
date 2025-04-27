// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("headingSimple")]
public class HeadingSimpleHandler : IMarkdownElementHandler {

    private static readonly int HsTextId = MarkdownRegexLib.GetMultiLineGroupId("hsText");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ValueTask HandleMatchAsync(
        IMarkdownParserEngine engine,
        IMarkdownSyntaxNode currentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin,
        CancellationToken ct = default
    ) {
        if (!entireMatch.Groups[HsTextId].TryGetValue(out string? headerSimpleText)) return ValueTask.CompletedTask;


        IMarkdownSyntaxNode headingElement = currentNode.AddChildNode(MarkdownElement.H1);
        engine.AddSingleLineMatchesToStack(headerSimpleText, headingElement, origin);
        return ValueTask.CompletedTask;
    }
}

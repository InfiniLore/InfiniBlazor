// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("linkRegular")]
public class LinkRegularHandler : IMarkdownElementHandler {

    private static readonly int LrTextId = MarkdownRegexLib.GetSingleLineGroupId("lrText");
    private static readonly int LrHrefId = MarkdownRegexLib.GetSingleLineGroupId("lrHref");
    private static readonly int LrTitleId = MarkdownRegexLib.GetSingleLineGroupId("lrTitle");
    private static readonly int LrBangId = MarkdownRegexLib.GetSingleLineGroupId("lrBang");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.Link;

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
        if (!entireMatch.Groups[LrTextId].TryGetValue(out string? linkText)) return ValueTask.CompletedTask;
        if (!entireMatch.Groups[LrHrefId].TryGetValue(out string? linkHref)) return ValueTask.CompletedTask;

        if (entireMatch.Groups[LrBangId].Success) {
            IMarkdownSyntaxNode imgNode = currentNode.AddChildNode(MarkdownElement.Image);
            imgNode.WithAttribute(MarkdownAttribute.ImageSource, linkHref);
            imgNode.WithAttribute(MarkdownAttribute.ImageAlt, linkText);

            if (entireMatch.Groups[LrTitleId].TryGetValue(out string? altTextValue)) {
                imgNode.WithAttribute(MarkdownAttribute.ImageTitle, altTextValue);
            }

            return ValueTask.CompletedTask;
        }

        IMarkdownSyntaxNode linkNode = currentNode.AddChildNode(MarkdownElement.Link);
        linkNode.WithAttribute(MarkdownAttribute.LinkHref, linkHref);

        engine.AddSingleLineMatchesToStack(linkText, linkNode, origin | SkipOnOrigin);
        return ValueTask.CompletedTask;
    }
}

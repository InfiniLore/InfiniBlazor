// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("linkNested")]
public class LinkNestedHandler : IMarkdownElementHandler {

    private static readonly int LnTextId = MarkdownRegexLib.GetSingleLineGroupId("lnText");
    private static readonly int LnHrefId = MarkdownRegexLib.GetSingleLineGroupId("lnHref");
    private static readonly int LnTitleId = MarkdownRegexLib.GetSingleLineGroupId("lnTitle");
    private static readonly int LnBangId = MarkdownRegexLib.GetSingleLineGroupId("lnBang");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ValueTask HandleMatchAsync(IMarkdownParserEngine engine, IMarkdownSyntaxNode currentNode, Match entireMatch, Group group, HandlerOrigin origin, CancellationToken ct = default) {
        if (!entireMatch.Groups[LnTextId].TryGetValue(out string? linkText)) return ValueTask.CompletedTask;
        if (!entireMatch.Groups[LnHrefId].TryGetValue(out string? linkHref)) return ValueTask.CompletedTask;

        if (entireMatch.Groups[LnBangId].Success) {
            IMarkdownSyntaxNode imgNode = currentNode.AddChildNode(MarkdownElement.Image);
            imgNode.WithAttribute(MarkdownAttribute.ImageSource, linkHref);
            imgNode.WithAttribute(MarkdownAttribute.ImageAlt, linkText);

            if (entireMatch.Groups[LnTitleId].TryGetValue(out string? altTextValue)) {
                imgNode.WithAttribute(MarkdownAttribute.ImageTitle, altTextValue);
            }

            return ValueTask.CompletedTask;
        }

        IMarkdownSyntaxNode linkNode = currentNode.AddChildNode(MarkdownElement.Link);
        linkNode.WithAttribute(MarkdownAttribute.LinkHref, linkHref);

        engine.AddSingleLineMatchesToStack(linkText, linkNode, origin);
        return ValueTask.CompletedTask;
    }
}

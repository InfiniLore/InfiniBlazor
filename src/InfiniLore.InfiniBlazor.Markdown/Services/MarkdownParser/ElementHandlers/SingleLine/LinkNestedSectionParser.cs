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
    public void HandleMatch(IMarkdownParserEngine engine, IMarkdownSyntaxNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[LnTextId].TryGetValue(out string? linkText)) return;
        if (!entireMatch.Groups[LnHrefId].TryGetValue(out string? linkHref)) return;

        if (entireMatch.Groups[LnBangId].Success) {
            IMarkdownSyntaxNode imgNode = currentNode.AddChildNode(MarkdownElement.Image);
            imgNode.WithAttribute("src",linkHref);
            imgNode.WithAttribute("alt", linkText);

            if (entireMatch.Groups[LnTitleId].TryGetValue(out string? altTextValue)) {
                imgNode.WithAttribute("title", altTextValue);
            }

            return;
        }

        IMarkdownSyntaxNode linkNode = currentNode.AddChildNode(MarkdownElement.Link);
        linkNode.WithAttribute("href", linkHref);

        engine.AddSingleLineMatchesToStack(linkText, linkNode, origin);
    }
}

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
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[LrTextId].TryGetValue(out string? linkText)) return;
        if (!entireMatch.Groups[LrHrefId].TryGetValue(out string? linkHref)) return;

        if (entireMatch.Groups[LrBangId].Success) {
            IMdNode imgNode = currentNode.AddChildNode(MdElement.Image);
            imgNode.WithAttribute("src", linkHref);
            imgNode.WithAttribute("alt", linkText);

            if (entireMatch.Groups[LrTitleId].TryGetValue(out string? altTextValue)) {
                imgNode.WithAttribute("title", altTextValue);
            }

            return;
        }

        IMdNode linkNode = currentNode.AddChildNode(MdElement.Link);
        linkNode.WithAttribute("href", linkHref);

        engine.AddSingleLineMatchesToStack(linkText, linkNode, origin | SkipOnOrigin);
    }
}

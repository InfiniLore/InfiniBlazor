// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.ElementHandlers.SingleLine;
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
    public void HandleMatch(
        IMarkdownParserEngine engine,
        IMarkdownSyntaxNode currentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin
    ) {
        // ReSharper disable once DuplicatedSequentialIfBodies
        if (!entireMatch.Groups[LrTextId].TryGetValue(out string? linkText)) return ;
        if (!entireMatch.Groups[LrHrefId].TryGetValue(out string? linkHref)) return ;

        if (entireMatch.Groups[LrBangId].Success) {
            IMarkdownSyntaxNode imgNode = currentNode.AddChildNode(MarkdownElement.Image);
            imgNode.WithAttribute(MarkdownAttribute.ImageSource, linkHref);
            imgNode.WithAttribute(MarkdownAttribute.ImageAlt, linkText);

            if (entireMatch.Groups[LrTitleId].TryGetValue(out string? altTextValue)) {
                imgNode.WithAttribute(MarkdownAttribute.ImageTitle, altTextValue);
            }

            return ;
        }

        IMarkdownSyntaxNode linkNode = currentNode.AddChildNode(MarkdownElement.Link);
        linkNode.WithAttribute(MarkdownAttribute.LinkHref, linkHref);

        engine.AddSingleLineMatchesToStack(linkText, linkNode, origin | SkipOnOrigin);
    }
}

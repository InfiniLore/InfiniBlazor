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
        IMdSyntaxNode parentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin
    ) {
        // ReSharper disable once DuplicatedSequentialIfBodies
        if (!entireMatch.Groups[LrTextId].TryGetValue(out string? linkText)) return ;
        if (!entireMatch.Groups[LrHrefId].TryGetValue(out string? linkHref)) return ;
        
        if (entireMatch.Groups[LrBangId].Success) {
            ImageMdSyntaxNode imgNode = ImageMdSyntaxNode.Shared.Get();
            imgNode.Href = linkHref;
            imgNode.AltText = linkText;
            
            if (entireMatch.Groups[LrTitleId].TryGetValue(out string? altTextValue)) {
                imgNode.Title = altTextValue;
            }
            
            parentNode.AddChildNode(imgNode);

            return ;
        }
        
        LinkMdSyntaxNode linkNode = LinkMdSyntaxNode.Shared.Get();
        linkNode.Href = linkHref;
        parentNode.AddChildNode(linkNode);
        
        engine.PushSingleLineMatchesToStack(linkText, linkNode, origin | SkipOnOrigin);
    }
}

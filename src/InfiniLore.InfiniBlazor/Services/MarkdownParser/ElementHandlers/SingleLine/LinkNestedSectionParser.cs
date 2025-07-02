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
    public void HandleMatch(
        IMarkdownParserEngine engine,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin
    ) {
        // ReSharper disable once DuplicatedSequentialIfBodies
        if (!entireMatch.Groups[LnTextId].TryGetValue(out string? linkText)) return ;
        if (!entireMatch.Groups[LnHrefId].TryGetValue(out string? linkHref)) return ;

        if (entireMatch.Groups[LnBangId].Success) {
            ImageMdSyntaxNode imgNode = ImageMdSyntaxNode.Shared.Get();
            imgNode.Href = linkHref;
            imgNode.AltText = linkText;
            
            if (entireMatch.Groups[LnTitleId].TryGetValue(out string? altTextValue)) {
                imgNode.Title = altTextValue;
            }
            
            parentNode.AddChildNode(imgNode);

            return ;
        }
        
        LinkMdSyntaxNode linkNode = LinkMdSyntaxNode.Shared.Get();
        linkNode.Href = linkHref;
        parentNode.AddChildNode(linkNode);

        engine.PushSingleLineMatchesToStack(linkText, linkNode, origin);
    }
}

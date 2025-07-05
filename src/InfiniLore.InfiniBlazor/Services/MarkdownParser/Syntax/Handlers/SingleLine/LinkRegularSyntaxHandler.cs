// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Handlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MarkdownRegexGroupNames.LinkRegular)]
public sealed class LinkRegularSyntaxHandler : IMdSyntaxHandler {
    private static readonly int LrTextId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.LrText);
    private static readonly int LrHrefId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.LrHref);
    private static readonly int LrTitleId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.LrTitle);
    private static readonly int LrBangId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.LrBang);
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.Link;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        Group group,
        MdSyntaxHandlerOrigin origin
    ) {
        // ReSharper disable once DuplicatedSequentialIfBodies
        if (!entireMatch.Groups[LrTextId].TryGetValue(out string? linkText)) return ;
        if (!entireMatch.Groups[LrHrefId].TryGetValue(out string? linkHref)) return ;
        
        if (entireMatch.Groups[LrBangId].Success) {
            ImageMdSyntaxNode imgNode = ImageMdSyntaxNode.Pool.Get();
            imgNode.Href = linkHref;
            imgNode.AltText = linkText;
            
            if (entireMatch.Groups[LrTitleId].TryGetValue(out string? altTextValue)) {
                imgNode.Title = altTextValue;
            }
            
            parentNode.AddChildNode(imgNode);

            return ;
        }
        
        LinkMdSyntaxNode linkNode = LinkMdSyntaxNode.Pool.Get();
        linkNode.Href = linkHref;
        parentNode.AddChildNode(linkNode);
        
        stack.PushSingleLineMatchesToStack(linkText, linkNode, origin | SkipOnOrigin);
    }
}

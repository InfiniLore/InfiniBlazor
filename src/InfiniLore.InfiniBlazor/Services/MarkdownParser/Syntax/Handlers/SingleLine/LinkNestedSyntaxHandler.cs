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
[InjectableSingleton<IMdSyntaxHandler>(MarkdownRegexGroupNames.LinkNested)]
public sealed class LinkNestedSyntaxHandler : IMdSyntaxHandler {
    private static readonly int LnTextId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.LnText);
    private static readonly int LnHrefId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.LnHref);
    private static readonly int LnTitleId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.LnTitle);
    private static readonly int LnBangId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.LnBang);
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.NotSkipped;
    
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
        if (!entireMatch.Groups[LnTextId].TryGetValue(out string? linkText)) return ;
        if (!entireMatch.Groups[LnHrefId].TryGetValue(out string? linkHref)) return ;

        if (entireMatch.Groups[LnBangId].Success) {
            ImageMdSyntaxNode imgNode = ImageMdSyntaxNode.Pool.Get();
            imgNode.Href = linkHref;
            imgNode.AltText = linkText;
            
            if (entireMatch.Groups[LnTitleId].TryGetValue(out string? altTextValue)) {
                imgNode.Title = altTextValue;
            }
            
            parentNode.AddChildNode(imgNode);

            return ;
        }
        
        LinkMdSyntaxNode linkNode = LinkMdSyntaxNode.Pool.Get();
        linkNode.Href = linkHref;
        parentNode.AddChildNode(linkNode);

        stack.PushSingleLineMatchesToStack(linkText, linkNode, origin);
    }
}

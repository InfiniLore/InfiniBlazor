// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("linkNested")]
public class LinkNestedSectionParser : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    
    private static readonly int LnTextId = CachedRegexGroupNames.GetSingleLineGroupId("lnText");
    private static readonly int LnHrefId = CachedRegexGroupNames.GetSingleLineGroupId("lnHref");
    private static readonly int LnTitleId = CachedRegexGroupNames.GetSingleLineGroupId("lnTitle");
    private static readonly int LnBangId = CachedRegexGroupNames.GetSingleLineGroupId("lnBang");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[LnTextId].TryGetValue(out string? linkText)) return;
        if (!entireMatch.Groups[LnHrefId].TryGetValue(out string? linkHref)) return;

        if (entireMatch.Groups[LnBangId].Success) {
            IMdNode imgNode = currentNode.AddChildNode(MdElement.Image);
            imgNode.WithAttribute("src", $"{linkHref}");
            imgNode.WithAttribute("alt", $"{linkText}");

            if (entireMatch.Groups[LnTitleId].TryGetValue(out string? altTextValue)) {
                imgNode.WithAttribute("title", $"{altTextValue}");
            }
            return;
        }
    
        IMdNode linkNode = currentNode.AddChildNode(MdElement.Link);
        linkNode.WithAttribute("href", $"{linkHref}");
        
        parser.AddSingleLineMatchesToStack(linkText, linkNode, origin);
    }
}

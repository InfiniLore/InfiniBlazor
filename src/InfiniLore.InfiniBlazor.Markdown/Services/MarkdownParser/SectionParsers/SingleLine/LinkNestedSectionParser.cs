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
public class LinkNestedSectionParser(ICachedRegexGroupNames groupNames) : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    
    private readonly int LnTextId = groupNames.GetSingleLineGroupId("lnText");
    private readonly int LnHrefId = groupNames.GetSingleLineGroupId("lnHref");
    private readonly int LnTitleId = groupNames.GetSingleLineGroupId("lnTitle");
    private readonly int LnBangId = groupNames.GetSingleLineGroupId("lnBang");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group _, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        if (!entireMatch.Groups[LnTextId].TryGetValue(out string? linkText)) return;
        if (!entireMatch.Groups[LnHrefId].TryGetValue(out string? linkHref)) return;

        if (entireMatch.Groups[LnBangId].Success) {
            IMdNode imgNode = currentNode.AddChildNode(MdElement.Image);
            imgNode.WithAttribute("src", $"\"{linkHref}\"");
            imgNode.WithAttribute("alt", $"\"{linkText}\"");

            if (entireMatch.Groups[LnTitleId].TryGetValue(out string? altTextValue)) {
                imgNode.WithAttribute("title", $"\"{altTextValue}\"");
            }
            return;
        }
    
        IMdNode linkNode = currentNode.AddChildNode(MdElement.Link);
        linkNode.WithAttribute("href", $"\"{linkHref}\"");
        
        parser.AddSingleLineMatchesToStack(linkText, linkNode, origin);
    }
}

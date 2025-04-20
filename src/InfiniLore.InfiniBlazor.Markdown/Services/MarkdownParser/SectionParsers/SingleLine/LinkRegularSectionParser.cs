// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("linkRegular")]
public class LinkRegularSectionParser(ICachedRegexGroupNames groupNames) : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.Link;
    
    private readonly int LrTextId = groupNames.GetSingleLineGroupId("lrText");
    private readonly int LrHrefId = groupNames.GetSingleLineGroupId("lrHref");
    private readonly int LrTitleId = groupNames.GetSingleLineGroupId("lrTitle");
    private readonly int LrBangId = groupNames.GetSingleLineGroupId("lrBang");
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group _, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        if (!entireMatch.Groups[LrTextId].TryGetValue(out string? linkText)) return;
        if (!entireMatch.Groups[LrHrefId].TryGetValue(out string? linkHref)) return;
        
        if (entireMatch.Groups[LrBangId].Success) {
            IMdNode imgNode = currentNode.AddChildNode(MdElement.Image);
            imgNode.WithAttribute("src", $"\"{linkHref}\"");
            imgNode.WithAttribute("alt", $"\"{linkText}\"");

            if (entireMatch.Groups[LrTitleId].TryGetValue(out string? altTextValue)) {
                imgNode.WithAttribute("title", $"\"{altTextValue}\"");
            }
            return;
        }
    
        IMdNode linkNode = currentNode.AddChildNode(MdElement.Link);
        linkNode.WithAttribute("href", $"\"{linkHref}\"");
        
        parser.AddSingleLineMatchesToStack(linkText, linkNode, origin | SkipOnOrigin);
    }
}

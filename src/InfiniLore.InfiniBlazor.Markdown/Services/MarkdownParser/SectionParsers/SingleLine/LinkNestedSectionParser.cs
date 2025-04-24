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

    private static readonly int LnTextId = MarkdownRegexLib.GetSingleLineGroupId("lnText");
    private static readonly int LnHrefId = MarkdownRegexLib.GetSingleLineGroupId("lnHref");
    private static readonly int LnTitleId = MarkdownRegexLib.GetSingleLineGroupId("lnTitle");
    private static readonly int LnBangId = MarkdownRegexLib.GetSingleLineGroupId("lnBang");
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[LnTextId].TryGetValue(out string? linkText)) return;
        if (!entireMatch.Groups[LnHrefId].TryGetValue(out string? linkHref)) return;

        if (entireMatch.Groups[LnBangId].Success) {
            IMdNode imgNode = currentNode.AddChildNode(MdElement.Image);
            imgNode.WithAttribute("src",linkHref);
            imgNode.WithAttribute("alt", linkText);

            if (entireMatch.Groups[LnTitleId].TryGetValue(out string? altTextValue)) {
                imgNode.WithAttribute("title", altTextValue);
            }

            return;
        }

        IMdNode linkNode = currentNode.AddChildNode(MdElement.Link);
        linkNode.WithAttribute("href", linkHref);

        parser.AddSingleLineMatchesToStack(linkText, linkNode, origin);
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("headingSimple")]
public class HeadingSimpleSectionParser : ISectionHandler {

    private static readonly int HSTextId = CachedRegexGroupNames.GetMultiLineGroupId("hsText");
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[HSTextId].TryGetValue(out string? headerSimpleText)) return;


        IMdNode headingElement = currentNode.AddChildNode(MdElement.H1);
        parser.AddSingleLineMatchesToStack(headerSimpleText, headingElement, origin);
    }
}

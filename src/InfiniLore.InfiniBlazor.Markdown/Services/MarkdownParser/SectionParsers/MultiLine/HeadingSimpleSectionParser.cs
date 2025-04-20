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
public class HeadingSimpleSectionParser(ICachedRegexGroupNames groupName) : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    
    private readonly int HSTextId = groupName.GetMultiLineGroupId("hsText");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group group, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        if (!entireMatch.Groups[HSTextId].TryGetValue(out string? headerSimpleText)) return;

        
        IMdNode headingElement = currentNode.AddChildNode(MdElement.H1);
        parser.AddSingleLineMatchesToStack(headerSimpleText, headingElement, origin);
    }
}

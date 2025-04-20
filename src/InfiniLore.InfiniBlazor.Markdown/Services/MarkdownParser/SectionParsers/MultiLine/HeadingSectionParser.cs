// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("heading")]
public class HeadingSectionParser(ICachedRegexGroupNames groupName) : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    
    private readonly int HLevelId = groupName.GetMultiLineGroupId("hLevel");
    private readonly int HTextId = groupName.GetMultiLineGroupId("hText");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group _, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        if (!entireMatch.Groups[HLevelId].TryGetLength(out int headingLevel)) return;
        if (!entireMatch.Groups[HTextId].TryGetValue(out string? headerText)) return;

        MdElement mdElement = headingLevel switch {
            1 => MdElement.H1,
            2 => MdElement.H2,
            3 => MdElement.H3,
            4 => MdElement.H4,
            5 => MdElement.H5,
            6 => MdElement.H6,
            _ => MdElement.H1
        };
        
        IMdNode headingElement = currentNode.AddChildNode(mdElement);
        parser.AddSingleLineMatchesToStack(headerText, headingElement, origin);
    }
}

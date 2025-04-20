// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("subScript")]
public class SubScriptSectionParser(ICachedRegexGroupNames groupNames) : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.SubScript;

    private readonly int SbId = groupNames.GetSingleLineGroupId("sb");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group _, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        if (!entireMatch.Groups[SbId].TryGetValue(out string? subValue)) return;
        
        IMdNode node = currentNode.AddChild(MdElement.Subscript);
        parser.AddSingleLineMatchesToStack(subValue, node, origin  | SkipOnOrigin);
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("supScript")]
public class SuperScriptSectionParser : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.SuperScript;

    private static readonly int SpId = CachedRegexGroupNames.GetSingleLineGroupId("sp");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[SpId].TryGetValue(out string? superValue)) return;
        
        IMdNode node = currentNode.AddChildNode(MdElement.Superscript);
        parser.AddSingleLineMatchesToStack(superValue, node, origin  | SkipOnOrigin);
    }
}

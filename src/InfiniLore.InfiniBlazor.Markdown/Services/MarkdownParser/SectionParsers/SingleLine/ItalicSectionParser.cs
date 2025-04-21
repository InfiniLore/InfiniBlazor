// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("italic")]
public class ItalicSectionParser : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.Italic;
    
    private static readonly int IId = CachedRegexGroupNames.GetSingleLineGroupId("i");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[IId].TryGetValue(out string? italicValue)) return;
        
        IMdNode node = currentNode.AddChildNode(MdElement.Italic);
        parser.AddSingleLineMatchesToStack(italicValue, node, origin  | SkipOnOrigin);
    }
}

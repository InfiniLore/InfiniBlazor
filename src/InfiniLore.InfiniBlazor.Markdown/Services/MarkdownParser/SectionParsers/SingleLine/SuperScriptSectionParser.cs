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

    private static readonly int SpId = MarkdownRegexLib.GetSingleLineGroupId("sp");
    public ParserOrigin SkipOnOrigin => ParserOrigin.SuperScript;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[SpId].TryGetValue(out string? superValue)) return;

        IMdNode node = currentNode.AddChildNode(MdElement.Superscript);
        engine.AddSingleLineMatchesToStack(superValue, node, origin | SkipOnOrigin);
    }
}

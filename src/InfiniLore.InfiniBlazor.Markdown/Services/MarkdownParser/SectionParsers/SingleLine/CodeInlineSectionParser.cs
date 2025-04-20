// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.Encodings.Web;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("code")]
public class CodeInlineSectionParser(ICachedRegexGroupNames groupNames) : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.Code;

    private readonly int CId = groupNames.GetSingleLineGroupId("c");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group _, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        if (!entireMatch.Groups[CId].TryGetValue(out string? codeValue)) return;

        string normalizedBackticks = codeValue.Replace("\\`", "`");
        string output = HtmlEncoder.Default.Encode(normalizedBackticks);

        IMdNode codeNode = currentNode.AddChild(MdElement.Code);
    }
}

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
public class CodeInlineSectionParser : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.Code;

    private static readonly int CId = CachedRegexGroupNames.GetSingleLineGroupId("c");
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
        if (!entireMatch.Groups[CId].TryGetValue(out string? codeValue)) return;

        string normalizedBackticks = codeValue.Replace("\\`", "`");
        string output = HtmlEncoder.Default.Encode(normalizedBackticks);

        IMdNode codeNode = currentNode.AddChildNode(MdElement.Code);
        codeNode.WithContent(output);
    }
}

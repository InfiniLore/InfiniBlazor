// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("code")]
public class CodeInlineHandler : IMarkdownElementHandler {

    private static readonly int CId = MarkdownRegexLib.GetSingleLineGroupId("c");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.Code;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownParserEngine engine, IMdNode currentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[CId].TryGetValue(out string? codeValue)) return;

        string normalizedBackticks = codeValue.Replace("\\`", "`");

        IMdNode codeNode = currentNode.AddChildNode(MdElement.CodeInline);
        codeNode.WithContent(normalizedBackticks);
    }
}

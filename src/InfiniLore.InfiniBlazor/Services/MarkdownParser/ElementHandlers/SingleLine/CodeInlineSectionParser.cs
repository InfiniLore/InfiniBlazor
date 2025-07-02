// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.ElementHandlers.SingleLine;
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
    public void HandleMatch(IMarkdownParserEngine engine, IMdSyntaxNode parentNode, Match entireMatch, Group group, HandlerOrigin origin) {
        if (!entireMatch.Groups[CId].TryGetValue(out string? codeValue)) return ;

        string normalizedBackticks = codeValue.Replace("\\`", "`");
        CodeInlineMdSyntaxNode node = CodeInlineMdSyntaxNode.Shared.Get();
        node.ContentCode = normalizedBackticks;
        parentNode.AddChildNode(node);
    }
}

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
    public ValueTask HandleMatchAsync(IMarkdownParserEngine engine, IMarkdownSyntaxNode currentNode, Match entireMatch, Group group, HandlerOrigin origin, CancellationToken ct = default) {
        if (!entireMatch.Groups[CId].TryGetValue(out string? codeValue)) return ValueTask.CompletedTask;

        string normalizedBackticks = codeValue.Replace("\\`", "`");

        IMarkdownSyntaxNode codeNode = currentNode.AddChildNode(MarkdownElement.CodeInline);
        codeNode.WithContent(normalizedBackticks);
        return ValueTask.CompletedTask;
    }
}

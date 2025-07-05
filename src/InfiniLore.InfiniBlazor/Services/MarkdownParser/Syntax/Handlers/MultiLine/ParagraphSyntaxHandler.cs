// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Handlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MarkdownRegexGroupNames.Paragraph)]
public sealed class ParagraphSyntaxHandler : IMdSyntaxHandler {

    private static readonly int PId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.P);
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        Group group,
        MdSyntaxHandlerOrigin origin
    ) {
        if (!entireMatch.Groups[PId].TryGetValue(out string? paragraph)) return;
        if (paragraph.IsNullOrWhiteSpace()) return;

        bool writeParagraph = !origin.HasFlag(MdSyntaxHandlerOrigin.Html);
        
        if (writeParagraph) {
            ParagraphMdSyntaxNode node = ParagraphMdSyntaxNode.Pool.Get();
            parentNode = parentNode.AddChildNode(node);
        }
        stack.PushSingleLineMatchesToStack(paragraph.TrimStart(), parentNode, origin);
    }
}

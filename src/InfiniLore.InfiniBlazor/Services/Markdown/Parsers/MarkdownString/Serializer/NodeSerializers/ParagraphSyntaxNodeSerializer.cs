// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.Paragraph)]
public sealed class ParagraphSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int PId = MdRegexLib.GetGroupId(MdRegexGroupNames.ParagraphContent);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch
    ) {
        if (!entireMatch.Groups[PId].TryGetValue(out string? paragraph)) return;

        if (parentNode is HtmlSpanMdSyntaxNode) {
            stack.PushSingleLineMatchesToStack(paragraph, parentNode);
            return;
        }

        ParagraphMdSyntaxNode node = ParagraphMdSyntaxNode.Pool.Get();
        parentNode = parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(paragraph, parentNode);
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer.NodeSerializers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.Paragraph)]
public sealed class ParagraphSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {

    private static readonly int PId = MdRegexLib.GetGroupId(MdRegexGroupNames.ParagraphContent);
    public MarkdownStringMdSyntaxSerializerOrigin SkipOnOrigin => MarkdownStringMdSyntaxSerializerOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMarkdownStringMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MarkdownStringMdSyntaxSerializerOrigin parentOrigin
    ) {
        if (!entireMatch.Groups[PId].TryGetValue(out string? paragraph)) return;
        if (paragraph.IsNullOrWhiteSpace()) return;

        bool writeParagraph = !parentOrigin.HasFlag(MarkdownStringMdSyntaxSerializerOrigin.Html);
        
        if (writeParagraph) {
            ParagraphMdSyntaxNode node = ParagraphMdSyntaxNode.Pool.Get();
            parentNode = parentNode.AddChildNode(node);
        }
        stack.PushSingleLineMatchesToStack(paragraph.TrimStart(), parentNode, parentOrigin);
    }
}

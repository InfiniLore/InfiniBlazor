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
[InjectableSingleton<IMdSyntaxNodeSerializer>(MdRegexGroupNames.Paragraph)]
public sealed class ParagraphSyntaxNodeSerializer : IMdSyntaxNodeSerializer {

    private static readonly int PId = MdRegexLib.GetGroupId(MdRegexGroupNames.ParagraphContent);
    public MdSyntaxSerializerOrigin SkipOnOrigin => MdSyntaxSerializerOrigin.NotSkipped;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxSerializerStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        MdSyntaxSerializerOrigin parentOrigin
    ) {
        if (!entireMatch.Groups[PId].TryGetValue(out string? paragraph)) return;
        if (paragraph.IsNullOrWhiteSpace()) return;

        bool writeParagraph = !parentOrigin.HasFlag(MdSyntaxSerializerOrigin.Html);
        
        if (writeParagraph) {
            ParagraphMdSyntaxNode node = ParagraphMdSyntaxNode.Pool.Get();
            parentNode = parentNode.AddChildNode(node);
        }
        stack.PushSingleLineMatchesToStack(paragraph.TrimStart(), parentNode, parentOrigin);
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.SyntaxSerializer.NodeSerializers.SingleLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxNodeSerializer>(MdRegexGroupNames.BoldContent)]
public sealed class BoldSyntaxNodeSerializer : IMarkdownStringMdSyntaxNodeSerializer {
    private static readonly int BId = MdRegexLib.GetGroupId(MdRegexGroupNames.Bold);
    public MarkdownStringMdSyntaxSerializerOrigin SkipOnOrigin => MarkdownStringMdSyntaxSerializerOrigin.Bold;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IMarkdownStringMdSyntaxSerializerStack stack, IMdSyntaxNode parentNode, Match entireMatch, MarkdownStringMdSyntaxSerializerOrigin parentOrigin) {
        if (!entireMatch.Groups[BId].TryGetValue(out string? boldValue)) return;

        BoldMdSyntaxNode node = BoldMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(node);
        stack.PushSingleLineMatchesToStack(boldValue, node, parentOrigin | SkipOnOrigin);
    }
}
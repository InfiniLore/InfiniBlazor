// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class EscapedSyntaxNodeSerializer  {
    private static readonly int EscapedId = MdRegexLib.GetGroupId(MdRegexGroupNames.Escaped);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        char value = match.Groups[EscapedId].ValueSpan[1];
        EscapedCharacterMdSyntaxNode node = EscapedCharacterMdSyntaxNode.Pool.Get();
        node.WithContent(value);
        parentNode.AddChildNode(node);
    }
}

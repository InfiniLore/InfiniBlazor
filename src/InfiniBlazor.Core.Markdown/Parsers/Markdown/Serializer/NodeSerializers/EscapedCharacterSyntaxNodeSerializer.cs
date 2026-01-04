// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Parsers.Markdown.Serializer.RegexLib;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class EscapedCharacterSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    public Regex Syntax { get; } = MdRegexLib.EscapedCharacterRegex;
    private static readonly int EscapedId = MdRegexLib.GetGroupId(MdRegexGroupNames.Escaped);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        char value = match.Groups[EscapedId].ValueSpan[1];
        EscapedCharacterMdSyntaxNode node = MdSyntaxNodePool<EscapedCharacterMdSyntaxNode>.Shared.Get();
        node.WithContent(value);
        parentNode.AddChildNode(node);
    }
}

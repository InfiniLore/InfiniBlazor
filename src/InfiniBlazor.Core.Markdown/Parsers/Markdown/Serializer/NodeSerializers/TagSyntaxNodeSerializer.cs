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
public class TagSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    private static readonly int TextId = MdRegexLib.GetGroupId(MdRegexGroupNames.TagText);

    public Regex Syntax { get; } = MdRegexLib.TagRegex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (!match.Groups[TextId].TryGetValue(out string? tagValue)) return;

        TagMdSyntaxNode node = MdSyntaxNodePool<TagMdSyntaxNode>.Shared.Get();
        node.WithContent(tagValue);
        parentNode.AddChildNode(node);
    }
}

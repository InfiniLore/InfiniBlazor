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
public static class FootnoteReferenceSyntaxNodeSerializer {
    private static readonly int FootnoteIdentifierId = MdRegexLib.GetGroupId(MdRegexGroupNames.FootnoteReferenceIdentifier);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (match.Groups[FootnoteIdentifierId] is not { Success: true, Value: var footnoteId }) return;

        FootnoteReferenceMdSyntaxNode node = FootnoteReferenceMdSyntaxNode.Pool.Get();
        node.WithIdentifier(footnoteId);
        parentNode.AddChildNode(node);
    }
}

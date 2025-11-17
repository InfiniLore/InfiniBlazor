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
public static class FootnoteDescriptionSyntaxNodeSerializer {
    private static readonly int FootnoteIdentifierId = MdRegexLib.GetGroupId(MdRegexGroupNames.FootnoteDescriptionIdentifier);
    private static readonly int FootnoteBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.FootnoteDescriptionBody);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (match.Groups[FootnoteIdentifierId] is not { Success: true, Value: var footnoteId }) return;
        if (match.Groups[FootnoteBodyId] is not { Success: true, Value: var body }) return;

        FootnoteDescriptionMdSyntaxNode node = FootnoteDescriptionMdSyntaxNode.Pool.Get();
        node.WithIdentifier(footnoteId);
        parentNode.AddChildNode(node);

        stack.PushMultiLineMatchesToStack(body, node);
        stack.TreeReference.StoreChildAtCache(node);
    }
}

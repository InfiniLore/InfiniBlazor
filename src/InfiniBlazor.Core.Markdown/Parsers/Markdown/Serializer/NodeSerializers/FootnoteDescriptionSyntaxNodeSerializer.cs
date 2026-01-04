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
public class FootnoteDescriptionSyntaxNodeSerializer : IMdSyntaxNodeSerializer {
    private static readonly int FootnoteIdentifierId = MdRegexLib.GetGroupId(MdRegexGroupNames.FootnoteDescriptionIdentifier);
    private static readonly int FootnoteBodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.FootnoteDescriptionBody);

    public Regex Syntax { get; } = MdRegexLib.FootnoteDescriptionRegex;
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        if (match.Groups[FootnoteIdentifierId] is not { Success: true, Value: var footnoteId }) return;
        if (match.Groups[FootnoteBodyId] is not { Success: true, Value: var body }) return;

        FootnoteDescriptionMdSyntaxNode node = MdSyntaxNodePool<FootnoteDescriptionMdSyntaxNode>.Shared.Get();
        node.WithIdentifier(footnoteId);
        parentNode.AddChildNode(node);

        stack.PushMultiLineMatchesToStack(body, node);
        stack.TreeReference.StoreChildAtCache(node);
    }
}

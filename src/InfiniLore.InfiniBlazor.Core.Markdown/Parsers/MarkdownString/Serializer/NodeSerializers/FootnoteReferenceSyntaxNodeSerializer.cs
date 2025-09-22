// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text.RegularExpressions;

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
        if (match.Groups[FootnoteIdentifierId] is not {Success: true, Value: var footnoteId}) return ;
        
        FootnoteReferenceMdSyntaxNode node = FootnoteReferenceMdSyntaxNode.Pool.Get();
        node.WithIdentifier(footnoteId);
        parentNode.AddChildNode(node);
    }
}
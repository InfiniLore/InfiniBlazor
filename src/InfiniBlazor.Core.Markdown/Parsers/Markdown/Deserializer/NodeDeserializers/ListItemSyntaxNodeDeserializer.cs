// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ListItemSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<ListItemMdSyntaxNode> {
    protected override void Deserialize(ListItemMdSyntaxNode node, StringBuilder builder) {
        switch (node) {
            case { Parent: ListUnOrderedMdSyntaxNode, IsCheckable: false }:
                builder.Append('-');
                builder.Append(' ', node.LeadingSpaces);
                break;

            case { Parent: ListOrderedMdSyntaxNode, IsCheckable: false }:
                builder.Append(node.Index);
                builder.Append('.');
                builder.Append(' ', node.LeadingSpaces);
                break;

            case { Parent: ListUnOrderedMdSyntaxNode, IsCheckable: true }:
                builder.Append('-');
                builder.Append(' ', node.CheckLeadingSpaces);
                builder.Append('[');
                builder.Append(node.OriginalCheckMarker);
                builder.Append(']');
                builder.Append(' ', node.LeadingSpaces);
                break;

            case { Parent: ListOrderedMdSyntaxNode, IsCheckable: true }:
                builder.Append(node.Index);
                builder.Append('.');
                builder.Append(' ', node.CheckLeadingSpaces);
                builder.Append('[');
                builder.Append(node.OriginalCheckMarker);
                builder.Append(']');
                builder.Append(' ', node.LeadingSpaces);
                break;
        }

        DeserializeChildren(node, builder);
    }
}

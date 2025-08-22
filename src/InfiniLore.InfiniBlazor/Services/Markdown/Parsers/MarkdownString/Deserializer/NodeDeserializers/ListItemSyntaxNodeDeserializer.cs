// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class ListItemSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<ListItemMdSyntaxNode> {
    protected override void Deserialize(ListItemMdSyntaxNode node, StringBuilder builder) {
        switch (node.Parent) {
            case ListUnOrderedMdSyntaxNode:
                builder.Append('-');
                builder.Append(' ');
                break;
            case ListOrderedMdSyntaxNode:
                builder.Append(node.Index);
                builder.Append('.');
                builder.Append(' ');
                break;
        }
        
        if (node.IsCheckable) {
            builder.Append('[');
            builder.Append(node.OriginalCheckMarker);
            builder.Append(']');
            builder.Append(' ');
        }
        DeserializeChildren(node, builder);
    }
}

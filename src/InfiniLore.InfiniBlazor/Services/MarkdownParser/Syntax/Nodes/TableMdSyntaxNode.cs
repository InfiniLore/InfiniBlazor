// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TableMdSyntaxNode : MdSyntaxNode<TableMdSyntaxNode> {
    // Yes we are using magic numbers here, dont worry about it for now, this should change unless we add more table types
    public TableHeadMdSyntaxNode Header => GetChildAt<TableHeadMdSyntaxNode>(0);
    public TableBodyMdSyntaxNode Body => GetChildAt<TableBodyMdSyntaxNode>(1);
}

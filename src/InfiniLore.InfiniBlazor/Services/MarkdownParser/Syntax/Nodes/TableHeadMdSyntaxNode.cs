// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Runtime.CompilerServices;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TableHeadMdSyntaxNode : MdSyntaxNode<TableHeadMdSyntaxNode> {
    public IEnumerable<TableHeadCellMdSyntaxNode> HeadCells {
        get {
            if (ChildCount == 0) yield break;

            var headerRow = Unsafe.As<TableRowMdSyntaxNode>(ChildNodes[0]);
            for (int i = 0; i < headerRow.ChildCount; i++) {
                yield return Unsafe.As<TableHeadCellMdSyntaxNode>(headerRow.ChildNodes[i]);
            }
        }
    }
}

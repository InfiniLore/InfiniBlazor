// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TableMdSyntaxNode : MdSyntaxNode<TableMdSyntaxNode> {
    private int HeaderIndex { get; set; } = -1;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TrySetHeader(TableRowMdSyntaxNode headerRow) {
        if (HeaderIndex is not -1) return false; // Can only add header once

        // Nothing has been set yet and it makes it a lot easier
        if (ChildCount is 0) {
            AddChildNode(headerRow);
            HeaderIndex = ChildCount - 1;
            return true;
        }
        
        // There are other children, so we need to insert it at the first location
        EnsureChildNodeCapacity();
        SetParent(headerRow);
        ChildCount++;
        for (int i = ChildCount - 1; i > 0; i--) ChildNodes[i] = ChildNodes[i - 1];
        
        // Set the new header
        ChildNodes[0] = headerRow;
        HeaderIndex = 0;
        return true;
        
    }
    
    public void AddRow(TableRowMdSyntaxNode row) => AddChildNode(row);

    public ReadOnlySpan<TableCellMdSyntaxNode> GetHeaderCells() {
        if (HeaderIndex is -1) return ReadOnlySpan<TableCellMdSyntaxNode>.Empty;
        
        var headerNode = Unsafe.As<TableRowMdSyntaxNode>(ChildNodes[HeaderIndex]);
        ReadOnlySpan<IMdSyntaxNode> childSpan = headerNode.GetChildrenSpan();
        return MemoryMarshal.CreateReadOnlySpan(
            ref Unsafe.As<IMdSyntaxNode, TableCellMdSyntaxNode>(ref MemoryMarshal.GetReference(childSpan)),
            childSpan.Length
        );

    }
    
    public ReadOnlySpan<TableRowMdSyntaxNode> GetRows() {
        if (HeaderIndex is -1) return ReadOnlySpan<TableRowMdSyntaxNode>.Empty;
        
        ReadOnlySpan<IMdSyntaxNode> rowSpan = GetChildrenSpan()[(HeaderIndex + 1)..];
        return MemoryMarshal.CreateReadOnlySpan(
            ref Unsafe.As<IMdSyntaxNode, TableRowMdSyntaxNode>(ref MemoryMarshal.GetReference(rowSpan)),
            rowSpan.Length);
    }

    public override bool TryReset() {
        if (!base.TryReset()) return false;
        HeaderIndex = -1;
        return true;
    }
}

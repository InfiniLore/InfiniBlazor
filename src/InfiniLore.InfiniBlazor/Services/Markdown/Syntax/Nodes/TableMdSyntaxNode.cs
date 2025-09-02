// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Buffers;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TableMdSyntaxNode : MdSyntaxNode<TableMdSyntaxNode> {
    public int HeaderIndex { get; internal set; } = -1;
    public Alignment[] Alignments { get; private set; } = ArrayPool<Alignment>.Shared.Rent(0);
    public bool HasAlignments { get; private set; }
    
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
        headerRow.WithParent(this);
        
        ChildCount++;
        for (int i = ChildCount - 1; i > 0; i--) ChildNodes[i] = ChildNodes[i - 1];
        
        // Set the new header
        ChildNodes[0] = headerRow;
        HeaderIndex = 0;
        return true;
        
    }
    
    public TableRowMdSyntaxNode AddRow(TableRowMdSyntaxNode row) => AddChildNode(row);

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

    public TableMdSyntaxNode WithAlignments(scoped ReadOnlySpan<Alignment> alignments) {
        int arrayLength = alignments.Length;
        
        ArrayPool<Alignment>.Shared.Return(Alignments, true);
        Alignments  = ArrayPool<Alignment>.Shared.Rent(arrayLength);
        alignments.CopyTo(Alignments);
        
        HasAlignments = true;
        return this;
    }
    
    public override bool TryReset() {
        if (!base.TryReset()) return false;
        HeaderIndex = -1;
        return true;
    }
    
    public override bool Equals(TableMdSyntaxNode? other) => base.Equals(other)
        && HeaderIndex == other.HeaderIndex;
    
    public enum Alignment {
        Left = -1,
        Center = 0,
        Right = 1,
        Unknown
    }
}

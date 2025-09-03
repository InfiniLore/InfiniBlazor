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
    private const int HeaderIndex = 0;
    public Alignment[] Alignments { get; private set; } = ArrayPool<Alignment>.Shared.Rent(0);
    public bool HasAlignments { get; private set; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TrySetHeader(TableRowMdSyntaxNode headerRow) 
        => TryAddChildNodeAtIndex(HeaderIndex, headerRow);

    public bool TryAddRow(TableRowMdSyntaxNode row)
        => TryAddChildNodeAtIndex(Math.Max(1, ChildCount), row); // Index 0 is reserved for header

    public ReadOnlySpan<TableCellMdSyntaxNode> GetHeaderCells() {
        var headerNode = Unsafe.As<TableRowMdSyntaxNode>(ChildNodes[HeaderIndex]);
        ReadOnlySpan<IMdSyntaxNode> childSpan = headerNode.GetChildrenSpan();
        return MemoryMarshal.CreateReadOnlySpan(
            ref Unsafe.As<IMdSyntaxNode, TableCellMdSyntaxNode>(ref MemoryMarshal.GetReference(childSpan)),
            childSpan.Length
        );
    }
    
    public ReadOnlySpan<TableRowMdSyntaxNode> GetRows() {
        ReadOnlySpan<IMdSyntaxNode> rowSpan = GetChildrenSpan()[(HeaderIndex + 1)..];
        return MemoryMarshal.CreateReadOnlySpan(
            ref Unsafe.As<IMdSyntaxNode, TableRowMdSyntaxNode>(ref MemoryMarshal.GetReference(rowSpan)),
            rowSpan.Length);
    }

    public TableMdSyntaxNode WithAlignments(scoped ReadOnlySpan<Alignment> alignments) {
        int arrayLength = alignments.Length;
        
        ArrayPool<Alignment>.Shared.Return(Alignments, true);
        Alignments = ArrayPool<Alignment>.Shared.Rent(arrayLength);
        alignments.CopyTo(Alignments);
        
        HasAlignments = true;
        return this;
    }

    public enum Alignment {
        Left = -1,
        Center = 0,
        Right = 1,
        Unknown
    }
}

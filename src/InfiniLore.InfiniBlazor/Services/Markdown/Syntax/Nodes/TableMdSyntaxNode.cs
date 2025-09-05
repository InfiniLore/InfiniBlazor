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
    public Alignment[] Alignments { get; private set; } = Array.Empty<Alignment>();
    public bool HasAlignments { get; private set; }

    public enum Alignment {
        Left = -1,
        Center = 0,
        Right = 1,
        Unknown
    }
    
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
        
        if (HasAlignments) ArrayPool<Alignment>.Shared.Return(Alignments, true);
        Alignments = ArrayPool<Alignment>.Shared.Rent(arrayLength);
        alignments.CopyTo(Alignments);
        
        HasAlignments = true;
        return this;
    }

    public override bool TryReset() {
        if (HasAlignments) ArrayPool<Alignment>.Shared.Return(Alignments, true);
        Alignments = Array.Empty<Alignment>();
        HasAlignments = false;
        return base.TryReset();
    }

    protected override bool Equals(TableMdSyntaxNode? other) {
        for (int i = 0; i < Alignments.Length; i++) {
            Alignment? alignmentOther = other?.Alignments.ElementAtOrDefault(i);
            if (alignmentOther is null || alignmentOther != Alignments[i]) return false;
        }
        
        return base.Equals(other)
            && HasAlignments == other.HasAlignments;
    }

}

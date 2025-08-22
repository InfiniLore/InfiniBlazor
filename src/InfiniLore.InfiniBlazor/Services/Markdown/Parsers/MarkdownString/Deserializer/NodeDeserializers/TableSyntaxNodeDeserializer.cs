// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using InfiniLore.InfiniBlazor.Pooling;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Deserializer.NodeDeserializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class TableSyntaxNodeDeserializer : BaseMarkdownStringMdSyntaxNodeDeserializer<TableMdSyntaxNode> {
    protected override void Deserialize(TableMdSyntaxNode node, StringBuilder builder) {
        
        ReadOnlySpan<TableCellMdSyntaxNode> headerCells = node.GetHeaderCells();
        ReadOnlySpan<TableRowMdSyntaxNode> rows = node.GetRows();
        int totalHeaderCells = headerCells.Length;
        int totalRows = rows.Length;
        
        string[,] tableGrid = new string[totalHeaderCells, totalRows];
        
        for (int i = 0; i < totalHeaderCells; i++) {
            StringBuilder headCellBuilder = GlobalPools.StringBuilder.Get();
            TableCellMdSyntaxNode cell = headerCells[i];
            try {
                foreach (IMdSyntaxNode child in cell.GetChildrenSpan()) {
                    if (!Deserializer.TryGetNodeDeserializer(child, out IMarkdownStringMdSyntaxNodeDeserializer? deserializer)) continue;
                    deserializer.Deserialize(child, headCellBuilder);
                }
                tableGrid[i, 0] = headCellBuilder.ToString();
            }
            finally {
                GlobalPools.StringBuilder.Return(headCellBuilder);
            }
        }

        for (int i = 0; i < totalRows; i++) {
            ReadOnlySpan<IMdSyntaxNode> cells = rows[i].GetChildrenSpan();
            for (int j = 0; j < cells.Length; j++) {
                StringBuilder cellBuilder = GlobalPools.StringBuilder.Get();
                IMdSyntaxNode cell = cells[j];
                try {
                    foreach (IMdSyntaxNode childNode in cell.GetChildrenSpan()) {
                        if (!Deserializer.TryGetNodeDeserializer(childNode, out IMarkdownStringMdSyntaxNodeDeserializer? deserializer)) continue;
                        deserializer.Deserialize(childNode, cellBuilder);
                    }
                    tableGrid[i+1, j] = cellBuilder.ToString();
                }
                finally {
                    GlobalPools.StringBuilder.Return(cellBuilder);
                }
            }
        }
        
        // int maxLineLength = 0;
        // for (int i = 0; i < totalHeaderCells; i++) {
        //     maxLineLength += tableGrid[i, 0].Length;
        // }
        // for (int i = 0; i < totalRows; i++) {
        //     int rowLength = 0;
        //     for (int j = 0; j < totalHeaderCells; j++) {
        //         rowLength += tableGrid[j, i].Length;
        //     }
        //     maxLineLength = Math.Max(maxLineLength, rowLength);
        // }
        
        for (int i = 0; i < totalHeaderCells; i++) {
            int maxCellWidth = tableGrid[i, 0].Length;
            for (int j = 1; j < totalRows; j++) {
                maxCellWidth = Math.Max(maxCellWidth, tableGrid[i, j].Length);
            }
            for (int j = 0; j < totalRows; j++) {
                tableGrid[i, j] = tableGrid[i, j].PadRight(maxCellWidth);
            }
        }
        
        // Write headerCells
        for (int i = 0; i < totalHeaderCells; i++) {
            builder.Append('|');
            builder.Append(' ');
            builder.Append(tableGrid[i, 0]);
            builder.Append(' ');
        }
        builder.Append('|');
        builder.Append('\n');
        
        // write header separator
        for (int i = 0; i < totalHeaderCells; i++) {
            builder.Append('|');
            builder.Append('-');
            builder.Append('-', tableGrid[i, 0].Length);
            builder.Append('-');
        }
        builder.Append('|');
        builder.Append('\n');
        
        // write rows
        for (int i = 1; i < totalHeaderCells; i++) {
            for (int j = 0; j < totalRows; j++) {
                builder.Append('|');
                builder.Append(' ');
                builder.Append(tableGrid[i, j]);
                builder.Append(' ');
            }
            builder.Append('|');
            builder.Append('\n');
        }

        if (!node.TryGetNextSibling(out IMdSyntaxNode? syntaxNode) || syntaxNode.Type == typeof(EmptyLineMdSyntaxNode)) builder.Remove(builder.Length - 1, 1); // Remove last \n (if exists)
    }
}

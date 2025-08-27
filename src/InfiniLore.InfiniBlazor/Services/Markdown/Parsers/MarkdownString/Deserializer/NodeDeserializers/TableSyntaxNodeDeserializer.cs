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
public sealed class TableSyntaxNodeDeserializer : MdStringMdSyntaxNodeDeserializerBase<TableMdSyntaxNode> {
    protected override void Deserialize(TableMdSyntaxNode node, StringBuilder builder) {
        ReadOnlySpan<TableCellMdSyntaxNode> headerCells = node.GetHeaderCells();
        ReadOnlySpan<TableRowMdSyntaxNode> rows = node.GetRows();
        int totalColumns = headerCells.Length;
        int totalRows = rows.Length;

        // Array is [rows, columns] - header row + data rows × columns
        string[,] tableGrid = new string[totalRows + 1, totalColumns];

        // Process header cells (row 0)
        for (int col = 0; col < totalColumns; col++) {
            StringBuilder headCellBuilder = GlobalPools.StringBuilder.Get();
            TableCellMdSyntaxNode cell = headerCells[col];
            try {
                foreach (IMdSyntaxNode child in cell.GetChildrenSpan()) {
                    if (!Deserializer.TryGetNodeDeserializer(child, out IMdStringMdSyntaxNodeDeserializer? deserializer)) continue;

                    deserializer.Deserialize(child, headCellBuilder);// Use headCellBuilder, not builder
                }

                tableGrid[0, col] = headCellBuilder.ToString();
            }
            finally {
                GlobalPools.StringBuilder.Return(headCellBuilder);
            }
        }

        // Process data rows (rows 1 and up)
        for (int row = 0; row < totalRows; row++) {
            ReadOnlySpan<IMdSyntaxNode> cells = rows[row].GetChildrenSpan();
            for (int col = 0; col < Math.Min(cells.Length, totalColumns); col++) {
                StringBuilder cellBuilder = GlobalPools.StringBuilder.Get();
                IMdSyntaxNode cell = cells[col];
                try {
                    foreach (IMdSyntaxNode childNode in cell.GetChildrenSpan()) {
                        if (!Deserializer.TryGetNodeDeserializer(childNode, out IMdStringMdSyntaxNodeDeserializer? deserializer)) continue;

                        deserializer.Deserialize(childNode, cellBuilder);// Use cellBuilder, not builder
                    }

                    tableGrid[row + 1, col] = cellBuilder.ToString();
                }
                finally {
                    GlobalPools.StringBuilder.Return(cellBuilder);
                }
            }
        }

        // Calculate the max width for each column
        for (int col = 0; col < totalColumns; col++) {
            int maxCellWidth = 0;
            for (int row = 0; row <= totalRows; row++) {
                maxCellWidth = Math.Max(maxCellWidth, tableGrid[row, col].Trim().Length);
            }

            // Pad all cells in this column
            for (int row = 0; row <= totalRows; row++) {
                tableGrid[row, col] = tableGrid[row, col].Trim().PadRight(maxCellWidth );
            }
        }

        // Write header row
        for (int col = 0; col < totalColumns; col++) {
            builder.Append('|');
            builder.Append(' ');
            builder.Append(tableGrid[0, col]);
            builder.Append(' ');
        }

        builder.Append('|');
        builder.Append('\n');

        // Write header separator
        for (int col = 0; col < totalColumns; col++) {
            builder.Append('|');
            builder.Append(' ');
            builder.Append('-', tableGrid[0, col].Length);
            builder.Append(' ');
        }

        builder.Append('|');

        // Write data rows
        for (int row = 1; row <= totalRows; row++) {
            builder.Append('\n');
            for (int col = 0; col < totalColumns; col++) {
                builder.Append('|');
                builder.Append(' ');
                builder.Append(tableGrid[row, col]);
                builder.Append(' ');
            }

            builder.Append('|');
        }
        
        if (node.TryGetNextSibling(out IMdSyntaxNode? syntaxNode) && syntaxNode.Type != typeof(NewLineMdSyntaxNode)){ builder.Append('\n');}
    }
}

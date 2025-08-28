// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class TableSyntaxNodeSerializer  {
    private const int StackAllocThreshold = 16;

    private static readonly int BodyId = MdRegexLib.GetGroupId(MdRegexGroupNames.TableBody);
    private static readonly int HeadId = MdRegexLib.GetGroupId(MdRegexGroupNames.TableHead);
    private static readonly int SepId = MdRegexLib.GetGroupId(MdRegexGroupNames.TableSeparator);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        // Extract header, separator, and rows
        ReadOnlySpan<char> header = match.Groups[HeadId].ValueSpan;
        Span<Range> headerColumns = stackalloc Range[header.Length];
        int headerColumnCount = header.Split(headerColumns, '|', StringSplitOptions.TrimEntries);

        ReadOnlySpan<char> separator = match.Groups[SepId].ValueSpan;
        Span<Range> separatorColumns = stackalloc Range[separator.Length];
        int _ = separator.Split(separatorColumns, '|', StringSplitOptions.TrimEntries);

        ReadOnlySpan<char> rows = match.Groups[BodyId].ValueSpan;
        Span<Range> rowRanges = stackalloc Range[rows.Length];
        int rowCount = rows.Split(rowRanges, '\n', StringSplitOptions.TrimEntries);

        // Construct table HTML
        TableMdSyntaxNode tableNode = TableMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(tableNode);

        // Add headers
        TableRowMdSyntaxNode tableHeadRow = TableRowMdSyntaxNode.Pool.Get();
        tableNode.TrySetHeader(tableHeadRow);
        
        for (int index = 0; index < headerColumnCount; index++) {
            TableCellMdSyntaxNode tableHeadCellNode = TableCellMdSyntaxNode.Pool.Get();
            tableHeadRow.AddChildNode(tableHeadCellNode);
            
            ReadOnlySpan<char> column = header[headerColumns[index]];
            stack.PushSingleLineMatchesToStack(column.ToString(), tableHeadCellNode);
        }

        // Add rows
        Range[]? rowColumnRanges = null;
        Span<Range> columnBuffer = headerColumnCount <= StackAllocThreshold 
            ? stackalloc Range[StackAllocThreshold]
            : rowColumnRanges = ArrayPool<Range>.Shared.Rent(headerColumnCount);
        
        try {
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++) {
                ReadOnlySpan<char> row = rows[rowRanges[rowIndex]].Trim();
                if (row.IsEmpty) continue;

                int rowColumnCount = row.Split(columnBuffer, '|', StringSplitOptions.RemoveEmptyEntries);

                TableRowMdSyntaxNode tableRow = TableRowMdSyntaxNode.Pool.Get();
                tableNode.AddRow(tableRow);
                
                for (int columnIndex = 0; columnIndex < rowColumnCount; columnIndex++) {
                    TableCellMdSyntaxNode tableCell = TableCellMdSyntaxNode.Pool.Get();
                    tableRow.AddChildNode(tableCell);
                    
                    ReadOnlySpan<char> column = row[columnBuffer[columnIndex]];
                    if (column.IsEmpty || column.IsWhiteSpace()) continue;
                    
                    stack.PushSingleLineMatchesToStack(column.ToString(), tableCell);
                }
            }
        }
        finally {
            if (rowColumnRanges != null) ArrayPool<Range>.Shared.Return(rowColumnRanges);
        }
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Handlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxHandler>(MarkdownRegexGroupNames.Table)]
public sealed class TableSyntaxHandler : IMdSyntaxHandler {
    private const int StackAllocThreshold = 16;

    private static readonly int BodyId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.TBody);
    private static readonly int HeadId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.THead);
    private static readonly int SepId = MarkdownRegexLib.GetGroupId(MarkdownRegexGroupNames.TSep);
    public MdSyntaxHandlerOrigin SkipOnOrigin => MdSyntaxHandlerOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(
        IMdSyntaxParserStack stack,
        IMdSyntaxNode parentNode,
        Match entireMatch,
        Group group,
        MdSyntaxHandlerOrigin origin
    ) {
        // Extract header, separator, and rows
        ReadOnlySpan<char> header = entireMatch.Groups[HeadId].ValueSpan;
        Span<Range> headerColumns = stackalloc Range[header.Length];
        int headerColumnCount = header.Split(headerColumns, '|', StringSplitOptions.TrimEntries);

        ReadOnlySpan<char> separator = entireMatch.Groups[SepId].ValueSpan;
        Span<Range> separatorColumns = stackalloc Range[separator.Length];
        int _ = separator.Split(separatorColumns, '|', StringSplitOptions.TrimEntries);

        ReadOnlySpan<char> rows = entireMatch.Groups[BodyId].ValueSpan;
        Span<Range> rowRanges = stackalloc Range[rows.Length];
        int rowCount = rows.Split(rowRanges, '\n', StringSplitOptions.TrimEntries);

        // Construct table HTML
        TableMdSyntaxNode tableNode = TableMdSyntaxNode.Pool.Get();
        parentNode.AddChildNode(tableNode);

        // Add headers
        TableHeadMdSyntaxNode tableHead = TableHeadMdSyntaxNode.Pool.Get();
        tableNode.AddChildNode(tableHead);
        TableRowMdSyntaxNode tableHeadRow = TableRowMdSyntaxNode.Pool.Get();
        tableHead.AddChildNode(tableHeadRow);
        
        for (int index = 0; index < headerColumnCount; index++) {
            TableHeadCellMdSyntaxNode tableHeadCellNode = TableHeadCellMdSyntaxNode.Pool.Get();
            tableHeadRow.AddChildNode(tableHeadCellNode);
            
            ReadOnlySpan<char> column = header[headerColumns[index]];
            stack.PushSingleLineMatchesToStack(column.ToString(), tableHeadCellNode, origin);
        }

        // Add rows
        TableBodyMdSyntaxNode tableBody = TableBodyMdSyntaxNode.Pool.Get();
        tableNode.AddChildNode(tableBody);

        Range[]? rowColumnRanges = null;
        Span<Range> columnBuffer = headerColumnCount <= StackAllocThreshold 
            ? stackalloc Range[StackAllocThreshold]
            : rowColumnRanges = ArrayPool<Range>.Shared.Rent(headerColumnCount);
        
        try {
            for (int rowIndex = 0; rowIndex < rowCount; rowIndex++) {
                ReadOnlySpan<char> row = rows[rowRanges[rowIndex]].Trim();
                if (row.IsEmpty) continue;

                int rowColumnCount = row.Split(columnBuffer, '|', 
                    StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

                if (rowColumnCount != headerColumnCount) continue; // Skip malformed rows

                TableRowMdSyntaxNode tableRow = TableRowMdSyntaxNode.Pool.Get();
                tableBody.AddChildNode(tableRow);
                for (int columnIndex = 0; columnIndex < rowColumnCount; columnIndex++) {
                    TableCellMdSyntaxNode tableCell = TableCellMdSyntaxNode.Pool.Get();
                    tableRow.AddChildNode(tableCell);
                    stack.PushSingleLineMatchesToStack(
                        row[columnBuffer[columnIndex]].ToString(), 
                        tableCell, 
                        origin);
                }
            }
        }
        finally {
            if (rowColumnRanges != null) ArrayPool<Range>.Shared.Return(rowColumnRanges);
        }
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.ElementHandlers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownElementHandler>("table")]
public class TableHandler : IMarkdownElementHandler {
    private const int StackAllocThreshold = 16;

    private static readonly int BodyId = MarkdownRegexLib.GetMultiLineGroupId("tBody");
    private static readonly int HeadId = MarkdownRegexLib.GetMultiLineGroupId("tHead");
    private static readonly int SepId = MarkdownRegexLib.GetMultiLineGroupId("tSep");
    public HandlerOrigin SkipOnOrigin => HandlerOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public ValueTask HandleMatchAsync(
        IMarkdownParserEngine engine,
        IMarkdownSyntaxNode currentNode,
        Match entireMatch,
        Group group,
        HandlerOrigin origin,
        CancellationToken ct = default
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
        IMarkdownSyntaxNode tableNode = currentNode.AddChildNode(MarkdownElement.Table);

        // Add headers
        IMarkdownSyntaxNode tableHeaderNode = tableNode
            .AddChildNode(MarkdownElement.TableHead)
            .AddChildNode(MarkdownElement.TableRow);

        for (int index = 0; index < headerColumnCount; index++) {
            IMarkdownSyntaxNode tableHeadCellNode = tableHeaderNode.AddChildNode(MarkdownElement.TableHeadCell);
            ReadOnlySpan<char> column = header[headerColumns[index]];
            engine.AddSingleLineMatchesToStack(column.ToString(), tableHeadCellNode, origin);
        }

        // Add rows
        IMarkdownSyntaxNode tableBodyNode = tableNode.AddChildNode(MarkdownElement.TableBody);

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

                IMarkdownSyntaxNode rowNode = tableBodyNode.AddChildNode(MarkdownElement.TableRow);
                for (int columnIndex = 0; columnIndex < rowColumnCount; columnIndex++) {
                    IMarkdownSyntaxNode cellNode = rowNode.AddChildNode(MarkdownElement.TableCell);
                    engine.AddSingleLineMatchesToStack(
                        row[columnBuffer[columnIndex]].ToString(), 
                        cellNode, 
                        origin);
                }
            }
        }
        finally {
            if (rowColumnRanges != null) ArrayPool<Range>.Shared.Return(rowColumnRanges);
        }
        return ValueTask.CompletedTask;
    }
}

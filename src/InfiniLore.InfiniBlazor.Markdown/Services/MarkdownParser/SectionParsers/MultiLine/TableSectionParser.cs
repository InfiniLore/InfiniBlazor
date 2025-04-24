// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SectionParsers.MultiLine;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ISectionHandler>("table")]
public class TableSectionParser : ISectionHandler {
    private const int StackAllocThreshold = 16;

    private static readonly int BodyId = MarkdownRegexLib.GetMultiLineGroupId("tBody");
    private static readonly int HeadId = MarkdownRegexLib.GetMultiLineGroupId("tHead");
    private static readonly int SepId = MarkdownRegexLib.GetMultiLineGroupId("tSep");
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(IRunningMarkdownParser parser, IMdNode currentNode, Match entireMatch, Group group, ParserOrigin origin) {
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
        IMdNode tableNode = currentNode.AddChildNode(MdElement.Table);

        // Add headers
        IMdNode tableHeaderNode = tableNode
            .AddChildNode(MdElement.TableHead)
            .AddChildNode(MdElement.TableRow);

        for (int index = 0; index < headerColumnCount; index++) {
            IMdNode tableHeadCellNode = tableHeaderNode.AddChildNode(MdElement.TableHeadCell);
            ReadOnlySpan<char> column = header[headerColumns[index]];
            parser.AddSingleLineMatchesToStack(column.ToString(), tableHeadCellNode, origin);
        }

        // Add rows
        IMdNode tableBodyNode = tableNode.AddChildNode(MdElement.TableBody);

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

                IMdNode rowNode = tableBodyNode.AddChildNode(MdElement.TableRow);
                for (int columnIndex = 0; columnIndex < rowColumnCount; columnIndex++) {
                    IMdNode cellNode = rowNode.AddChildNode(MdElement.TableCell);
                    parser.AddSingleLineMatchesToStack(
                        row[columnBuffer[columnIndex]].ToString(), 
                        cellNode, 
                        origin);
                }
            }
        }
        finally {
            if (rowColumnRanges != null) ArrayPool<Range>.Shared.Return(rowColumnRanges);
        }
    }
}

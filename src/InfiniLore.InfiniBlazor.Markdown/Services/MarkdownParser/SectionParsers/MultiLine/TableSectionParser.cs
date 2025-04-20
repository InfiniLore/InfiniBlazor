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
public class TableSectionParser(ICachedRegexGroupNames groupName) : ISectionHandler {
    public ParserOrigin SkipOnOrigin => ParserOrigin.NotSkipped;
    
    private readonly int TBodyId = groupName.GetMultiLineGroupId("tBody");
    private readonly int THeadId = groupName.GetMultiLineGroupId("tHead");
    private readonly int TSepId = groupName.GetMultiLineGroupId("tSep");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void HandleMatch(Match entireMatch, Group group, ParserOrigin origin, IMdNode currentNode, IRunningMarkdownParser parser) {
        // Extract header, separator, and rows
        ReadOnlySpan<char> header = entireMatch.Groups[THeadId].ValueSpan;
        Span<Range> headerColumns = stackalloc Range[header.Length];
        int headerColumnCount = header.Split(headerColumns, '|', StringSplitOptions.TrimEntries);

        ReadOnlySpan<char> separator = entireMatch.Groups[TSepId].ValueSpan;
        Span<Range> separatorColumns = stackalloc Range[separator.Length];
        int _ = separator.Split(separatorColumns, '|', StringSplitOptions.TrimEntries);

        ReadOnlySpan<char> rows = entireMatch.Groups[TBodyId].ValueSpan;
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
        ArrayPool<Range> bufferPool = ArrayPool<Range>.Shared;
        const int maxExpectedRowLength = 512;// Based on expected data characteristics
        Range[] rowColumnRanges = bufferPool.Rent(maxExpectedRowLength);

        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++) {
            Range rowRange = rowRanges[rowIndex];
            ReadOnlySpan<char> row = rows[rowRange].Trim();
            if (row.IsEmpty) continue;

            // Split the row
            int rowColumnCount = row.Split(rowColumnRanges.AsSpan(0, row.Length), '|', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries);

            IMdNode rowNode = tableBodyNode.AddChildNode(MdElement.TableRow);
            for (int columnIndex = 0; columnIndex < rowColumnCount; columnIndex++) {
                IMdNode cellNode = rowNode.AddChildNode(MdElement.TableCell);
                Range columnRange = rowColumnRanges[columnIndex];
                ReadOnlySpan<char> column = row[columnRange];
                parser.AddSingleLineMatchesToStack(column.ToString(), cellNode, origin);
            }
        }
    }
}

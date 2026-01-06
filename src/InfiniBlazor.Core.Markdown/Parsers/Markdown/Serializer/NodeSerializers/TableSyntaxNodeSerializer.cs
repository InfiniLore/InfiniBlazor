// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazor.Markdown.Syntax.Nodes;
using System.Buffers;
using System.Text.RegularExpressions;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer.NodeSerializers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class TableSyntaxNodeSerializer : IMdSyntaxNodeSerializer{
    [GeneratedRegex("""
        \G
        ^\|(?<head>.+)\|[\ ]*\n
        ^\|(?<sep>[:\-|\ ]+?)\|[\ ]*
        (?<body>(?:\n(?:^\|.*\|$))+)
        """, RegexOptions.IgnorePatternWhitespace | RegexOptions.Multiline | RegexOptions.ExplicitCapture | RegexOptions.Compiled)]
    private static partial Regex Syntax { get; }

    private static readonly int HeadId = Syntax.GroupNumberFromName("head");
    private static readonly int SepId = Syntax.GroupNumberFromName("sep");
    private static readonly int BodyId = Syntax.GroupNumberFromName("body");
    
    private const int StackAllocThreshold = 16;

    public char[] TriggerCharacters { get; } = ['|'];
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public Match Match(string input, int startPosition = 0) 
        => Syntax.Match(input, startPosition);
    
    public void Serialize(
        IMdSyntaxFragmentStack stack,
        IMdSyntaxNode parentNode,
        Match match
    ) {
        // Extract header, separator, and rows
        ReadOnlySpan<char> header = match.Groups[HeadId].ValueSpan;
        Span<Range> headerColumns = stackalloc Range[header.Length];
        int headerColumnCount = header.Split(headerColumns, '|', StringSplitOptions.TrimEntries);

        ReadOnlySpan<char> separator = match.Groups[SepId].ValueSpan;
        Span<Range> separatorColumns = stackalloc Range[separator.Length - 1];
        int separatorColumnCount = separator.Split(separatorColumns, '|', StringSplitOptions.TrimEntries);
        Span<TableMdSyntaxNode.Alignment> separatorColumData = stackalloc TableMdSyntaxNode.Alignment[separatorColumnCount];
        bool hasSeparatorData = ParseSeparatorData(separator, separatorColumns[..separatorColumnCount], separatorColumData);

        ReadOnlySpan<char> rows = match.Groups[BodyId].ValueSpan;
        Span<Range> rowRanges = stackalloc Range[rows.Length];
        int rowCount = rows.Split(rowRanges, '\n', StringSplitOptions.TrimEntries);

        // Construct table HTML
        TableMdSyntaxNode tableNode = MdSyntaxNodePool<TableMdSyntaxNode>.Shared.Get();
        parentNode.AddChildNode(tableNode);
        if (hasSeparatorData) tableNode.WithAlignments(separatorColumData);

        // Add headers
        TableRowMdSyntaxNode tableHeadRow = MdSyntaxNodePool<TableRowMdSyntaxNode>.Shared.Get();
        tableNode.TrySetHeader(tableHeadRow);

        for (int index = 0; index < headerColumnCount; index++) {
            TableCellMdSyntaxNode tableHeadCellNode = MdSyntaxNodePool<TableCellMdSyntaxNode>.Shared.Get();
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

                TableRowMdSyntaxNode tableRow = MdSyntaxNodePool<TableRowMdSyntaxNode>.Shared.Get();
                tableNode.TryAddRow(tableRow);

                for (int columnIndex = 0; columnIndex < rowColumnCount; columnIndex++) {
                    TableCellMdSyntaxNode tableCell = MdSyntaxNodePool<TableCellMdSyntaxNode>.Shared.Get();
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

    /// <summary>
    /// Parses separator data from a line of input and determines column alignments.
    /// </summary>
    /// <param name="lineInput">The input line containing separator information.</param>
    /// <param name="columnRanges">The ranges of each column in the input line.</param>
    /// <param name="target">
    /// A span to store the alignment for each column.
    /// Alignments are represented as integers: -1 for left-aligned,
    /// 0 for center-aligned, and 1 for right-aligned.
    /// </param>
    /// <returns>
    /// A boolean indicating whether separator data was successfully parsed.
    /// Returns true if valid separator data is found; otherwise, false.
    /// </returns>
    private static bool ParseSeparatorData(ReadOnlySpan<char> lineInput, Span<Range> columnRanges, Span<TableMdSyntaxNode.Alignment> target) {
        if (lineInput.IsEmpty) return false;
        if (columnRanges.IsEmpty) return false;

        bool hasSeparatorData = false;

        for (int index = 0; index < columnRanges.Length; index++) {
            Range range = columnRanges[index];
            var alignment = TableMdSyntaxNode.Alignment.Unknown;
            ReadOnlySpan<char> columnSlice = lineInput[range].Trim();
            alignment = (columnSlice[0], columnSlice[^1]) switch {
                (':', ':') => TableMdSyntaxNode.Alignment.Center,
                ('-', ':') => TableMdSyntaxNode.Alignment.Right,
                (':', '-') => TableMdSyntaxNode.Alignment.Left,

                _ => alignment
            };

            if (alignment is not TableMdSyntaxNode.Alignment.Unknown) hasSeparatorData = true;
            target[index] = alignment;
        }


        return hasSeparatorData;
    }
}

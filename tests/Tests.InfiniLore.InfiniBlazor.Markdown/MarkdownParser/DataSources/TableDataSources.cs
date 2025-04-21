// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class TableDataSources {
    private static readonly string SectionName = nameof(TableDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            """
            | Column 1      | Column 2     | Column 3      |
            | --------------| ------------ |-------------- |
            | Row 2 col 1   | Row 2 col 2  | Row 2 col 3   |
            """,
            """
            <table>
                <thead>
                    <tr>
                        <th>Column 1</th>
                        <th>Column 2</th>
                        <th>Column 3</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Row 2 col 1</td>
                        <td>Row 2 col 2</td>
                        <td>Row 2 col 3</td>
                    </tr>
                </tbody>
            </table>
            """,
            static rootNode => {
                IMdNode table = rootNode.AddTable();
                IMdNode tableHeader = table.AddTableHead();
                IMdNode tableHeaderRow = tableHeader.AddTableRow();
                tableHeaderRow.AddTableHeadCell("Column 1");
                tableHeaderRow.AddTableHeadCell("Column 2");
                tableHeaderRow.AddTableHeadCell("Column 3");
                IMdNode tableBody = table.AddTableBody();
                IMdNode tableBodyRow = tableBody.AddTableRow();
                tableBodyRow.AddTableCell("Row 2 col 1");
                tableBodyRow.AddTableCell("Row 2 col 2");
                tableBodyRow.AddTableCell("Row 2 col 3");
            }
            
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            | Tables        | Are           | Cool  |
            | ------------- | ------------- | ----- |
            | col 3 is      | right-aligned | $1600 |
            | col 2 is      | centered      |   $12 |
            | zebra stripes | are neat      |    $1 |
            
            | Tables        | Are           | Cool  |
            | ------------- | ------------- | ----- |
            | col 3 is      | right-aligned | $1600 |
            | col 2 is      | centered      |   $12 |
            | zebra stripes | are neat      |    $1 |
            
            """,
            """
            <table>
                <thead>
                    <tr>
                        <th>Tables</th>
                        <th>Are</th>
                        <th>Cool</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>col 3 is</td>
                        <td>right-aligned</td>
                        <td>$1600</td>
                    </tr>
                    <tr>
                        <td>col 2 is</td>
                        <td>centered</td>
                        <td>$12</td>
                    </tr>
                    <tr>
                        <td>zebra stripes</td>
                        <td>are neat</td>
                        <td>$1</td>
                    </tr>
                </tbody>
            </table>
            <table>
                <thead>
                    <tr>
                        <th>Tables</th>
                        <th>Are</th>
                        <th>Cool</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>col 3 is</td>
                        <td>right-aligned</td>
                        <td>$1600</td>
                    </tr>
                    <tr>
                        <td>col 2 is</td>
                        <td>centered</td>
                        <td>$12</td>
                    </tr>
                    <tr>
                        <td>zebra stripes</td>
                        <td>are neat</td>
                        <td>$1</td>
                    </tr>
                </tbody>
            </table>
            """,
            static rootNode => {
                IMdNode table = rootNode.AddTable();
                IMdNode tableHeader = table.AddTableHead();
                IMdNode tableHeaderRow = tableHeader.AddTableRow();
                tableHeaderRow.AddTableHeadCell("Tables");
                tableHeaderRow.AddTableHeadCell("Are");
                tableHeaderRow.AddTableHeadCell("Cool");
                IMdNode tableBody = table.AddTableBody();
                IMdNode tableRow1 = tableBody.AddTableRow();
                tableRow1.AddTableCell("col 3 is");
                tableRow1.AddTableCell("right-aligned");
                tableRow1.AddTableCell("$1600");
                IMdNode tableRow2 = tableBody.AddTableRow();
                tableRow2.AddTableCell("col 2 is");
                tableRow2.AddTableCell("centered");
                tableRow2.AddTableCell("$12");
                IMdNode tableBodyRow3 = tableBody.AddTableRow();
                tableBodyRow3.AddTableCell("zebra stripes");
                tableBodyRow3.AddTableCell("are neat");
                tableBodyRow3.AddTableCell("$1");
                
                IMdNode table2 = rootNode.AddTable();
                IMdNode tableHeader2 = table2.AddTableHead();
                IMdNode tableHeaderRow2 = tableHeader2.AddTableRow();
                tableHeaderRow2.AddTableHeadCell("Tables");
                tableHeaderRow2.AddTableHeadCell("Are");
                tableHeaderRow2.AddTableHeadCell("Cool");
                IMdNode tableBody2 = table2.AddTableBody();
                IMdNode tableRow12 = tableBody2.AddTableRow();
                tableRow12.AddTableCell("col 3 is");
                tableRow12.AddTableCell("right-aligned");
                tableRow12.AddTableCell("$1600");
                IMdNode tableRow22 = tableBody2.AddTableRow();
                tableRow22.AddTableCell("col 2 is");
                tableRow22.AddTableCell("centered");
                tableRow22.AddTableCell("$12");
                IMdNode tableBodyRow32 = tableBody2.AddTableRow();
                tableBodyRow32.AddTableCell("zebra stripes");
                tableBodyRow32.AddTableCell("are neat");
                tableBodyRow32.AddTableCell("$1");
            }
        );
    }
}

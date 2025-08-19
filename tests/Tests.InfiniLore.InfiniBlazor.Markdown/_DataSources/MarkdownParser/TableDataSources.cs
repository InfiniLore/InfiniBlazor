// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown._DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class TableDataSources {
    private static readonly string SectionName = nameof(TableDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        yield return static () => new MdTestData(SectionName,
            """
            | Test  |
            | ----- |
            | Value |
            """,
            """
            <table>
                <thead>
                    <tr>
                        <th>Test</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td>Value</td>
                    </tr>
                </tbody>
            </table>
            """
        );
        
        yield return static () => new MdTestData(SectionName,
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
            """
        );
        
        yield return static () => new MdTestData(SectionName,
            """
            | Column 1      | Column 2     | Column 3      |
            | --------------| ------------ |-------------- |
            | Row 2 col 1   |              | Row 2 col 3   |
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
                        <td></td>
                        <td>Row 2 col 3</td>
                    </tr>
                </tbody>
            </table>
            """
        );
        
        yield return static () => new MdTestData(SectionName,
            """
            | Column 1      | Column 2     | Column 3      |
            | --------------| ------------ |-------------- |
            | Row 2 col 1   |              |               |
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
                        <td></td>
                        <td></td>
                    </tr>
                </tbody>
            </table>
            """
        );

        yield return static () => new MdTestData(SectionName,
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
            """
        );
    }
}

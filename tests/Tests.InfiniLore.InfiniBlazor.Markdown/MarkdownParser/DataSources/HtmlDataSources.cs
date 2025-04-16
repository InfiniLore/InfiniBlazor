// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class HtmlDataSources {
    private static readonly string SectionName = nameof(HtmlDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            """
            Unrelated previous paragraph followed by a blank line
            <table>
                <tr>
                    <td>Table cell</td>
                    <td>
                        <table>
                            <tr>
                                <td>*Tables in tables*</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
            """,
            """
            <p>Unrelated previous paragraph followed by a blank line</p>
            <p>
                <table>
                    <tbody>
                    <tr>
                        <td>Table cell</td>
                        <td>
                            <table>
                                <tbody>
                                <tr>
                                    <td>*Tables in tables*</td>
                                </tr>
                                </tbody>
                            </table>
                        </td>
                    </tr>
                    </tbody>
                </table>
            </p>
            """
        );

        yield return () => new MarkdownTestDto(SectionName,
            """
            <pre>
            Buffalo Bill ’s
            defunct
                   who used to
                   ride a watersmooth-silver
                                            stallion
            and break onetwothreefourfive pigeonsjustlikethat
                                                             Jesus
            he was a handsome man
                                 and what i want to know is
            how do you like your blueeyed boy
            Mister Death
            </pre>
            """,
            """
            <p><pre>
            Buffalo Bill ’s
            defunct
                   who used to
                   ride a watersmooth-silver
                                            stallion
            and break onetwothreefourfive pigeonsjustlikethat
                                                             Jesus
            he was a handsome man
                                 and what i want to know is
            how do you like your blueeyed boy
            Mister Death
            </pre></p>
            """
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            test <div>
            <strong>something</strong>
            </div>
            """,
            """
            <p> test <div>
            <strong>something</strong>
            </div> </p>
            """
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            """
            test <div>
            <script>something</script>
            </div>
            """,
            """
            <p> test <div></div></p> 
            """
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "test <div> <script>something</script> </div>",
            "<p> test <div></div> </p> "
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "test <script>something</script>",
            "<p> test </p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            """
            *test* <div>
            <strong>something</strong>
            </div> **bold this**
            """,
            """
            <p> <em>test</em> <div>
            <strong>something</strong>
            </div> <strong>bold this</strong> </p>
            """
        );
    }
}

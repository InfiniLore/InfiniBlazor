// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
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
            </p>
            """,
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph("Unrelated previous paragraph followed by a blank line");
                IMarkdownSyntaxNode paragraph2 = rootNode.AddParagraph();
                paragraph2.WithHtmlContent("""
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
                    """);
            }
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
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithHtmlContent("""
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
                    """);
            }
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
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph("test ");
                paragraph.WithHtmlContent("""
                    <div>
                    <strong>something</strong>
                    </div>
                    """
                );
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            test <div>
            <script>something</script>
            </div>
            """,
            """
            <p> test <div><script>something</script></div></p> 
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph("test ");

                // HTML Sanitization is handled as a post-processor, so we can't test it here
                paragraph.WithHtmlContent("""
                    <div>
                    <script>something</script>
                    </div>
                    """
                );
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "test <div> <script>something</script> </div>",
            "<p> test <div> <script>something</script> </div> </p> ",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph("test ");
                // HTML Sanitization is handled as a post-processor, so we can't test it here
                paragraph.WithHtmlContent("<div> <script>something</script> </div>"
                );
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "test <script>something</script>",
            "<p> test <script>something</script></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph("test ");
                // HTML Sanitization is handled as a post-processor, so we can't test it here
                paragraph.WithHtmlContent("<script>something</script>"
                );
            }
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
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.AddItalic("test");
                paragraph.WithContent(" ");
                paragraph.WithHtmlContent("""
                    <div>
                    <strong>something</strong>
                    </div>
                    """
                );

                paragraph.WithContent(" ");
                paragraph.AddBold("bold this");

            }
        );
    }
}

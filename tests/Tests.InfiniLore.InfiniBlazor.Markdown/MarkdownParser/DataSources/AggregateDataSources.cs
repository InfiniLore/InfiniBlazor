// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class AggregateDataSources {
    private static readonly string SectionName = nameof(AggregateDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {

        yield return static () => new MarkdownTestDto(SectionName,
            """
            ## Longer Example with Multiple Sections

            ### Introduction
            Welcome to this test. This section introduces the topic, along with **bold**, *italic*, and `code` formatting.

            ### Code Snippet
            Below is an example of a C# code snippet:

            ```csharp
            public class Program {
                public static void Main() {
                    Console.WriteLine("Hello, World!");
                }
            }
            ```

            ### Bullet Points
            Here are some bullet points:
            - Point one
            - Point two
              - Subpoint A
              - Subpoint B

            ### Blockquote
            > This is a blockquote. It can contain multiple lines of text
            > and demonstrates how Markdown handles quoted content.


            ### Table Example
            | Column 1       | Column 2       | Column 3       |
            |----------------|----------------|----------------|
            | Data 1         | Data 2         | Data 3         |
            | Data 4         | Data 5         | Data 6         |

            ### Links and Images
            You can visit [Google](https://www.google.com) or check out the following image:

            ![Placeholder Image](https://via.placeholder.com/150)
            """,
            """
            <h2>Longer Example with Multiple Sections</h2>
            <h3>Introduction</h3>
            <p>Welcome to this test. This section introduces the topic, along with <strong>bold</strong>, <em>italic</em>, and <code>code</code> formatting.</p>
            <h3>Code Snippet</h3>
            <p>Below is an example of a C# code snippet:</p>
            <pre><code class="language-csharp">public class Program {
                public static void Main() {
                    Console.WriteLine("Hello, World!");
                }
            }</code></pre>
            <h3>Bullet Points</h3>
            <p>Here are some bullet points:</p>
            <ul>
                <li>Point one</li>
                <li>Point two
                <ul>
                    <li>Subpoint A</li>
                    <li>Subpoint B</li>
                </ul>
                </li>
            </ul>
            <h3>Blockquote</h3>
            <blockquote>
                <p>This is a blockquote. It can contain multiple lines of text</p>
                <p>and demonstrates how Markdown handles quoted content.</p>
            </blockquote>
            <h3>Table Example</h3>
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
                        <td>Data 1</td>
                        <td>Data 2</td>
                        <td>Data 3</td>
                    </tr>
                    <tr>
                        <td>Data 4</td>
                        <td>Data 5</td>
                        <td>Data 6</td>
                    </tr>
                </tbody>
            </table>
            <h3>Links and Images</h3>
            <p>You can visit <a href="https://www.google.com">Google</a> or check out the following image:</p>
            <p><img src="https://via.placeholder.com/150" alt="Placeholder Image"></p>
            """
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            ## Nested Lists and Complex Formatting

            - **Main Topic 1**
              - Subtopic 1.1
                - Detail 1.1.1
                - Detail 1.1.2
                  - Extra Detail 1.1.2.1
              - Subtopic 1.2
            - **Main Topic 2**
              1. Item 2.1
              2. Item 2.2
                 - Sub-Item 2.2.1
                 - Sub-Item 2.2.2
            """,
            """
            <h2>Nested Lists and Complex Formatting</h2>
            <ul>
                <li><strong>Main Topic 1</strong>
                    <ul>
                        <li>Subtopic 1.1
                            <ul>
                                <li>Detail 1.1.1</li>
                                <li>Detail 1.1.2
                                    <ul>
                                        <li>Extra Detail 1.1.2.1</li>
                                    </ul>
                                </li>
                            </ul>
                        </li>
                        <li>Subtopic 1.2</li>
                    </ul>
                </li>
                <li><strong>Main Topic 2</strong>
                    <ol>
                        <li>Item 2.1</li>
                        <li>Item 2.2
                            <ul>
                                <li>Sub-Item 2.2.1</li>
                                <li>Sub-Item 2.2.2</li>
                            </ul>
                        </li>
                    </ol>
                </li>
            </ul>
            """,
            static rootNode => {
                IMdNode heading = rootNode.AddH2();
                heading.WithContent("Nested Lists and Complex Formatting");

                IMdNode unOrderedList = rootNode.AddListUnordered();
                IMdNode topic1 = unOrderedList.AddListItem();
                topic1.AddBold("Main Topic 1");
                IMdNode topic1List = topic1.AddListUnordered();
                IMdNode subtopic1 = topic1List.AddListItem();
                subtopic1.WithContent("Subtopic 1.1");
                IMdNode subtopic1List = subtopic1.AddListUnordered();
                IMdNode detail1 = subtopic1List.AddListItem();
                detail1.WithContent("Detail 1.1.1");
                IMdNode detail2 = subtopic1List.AddListItem();
                detail2.WithContent("Detail 1.1.2");
                IMdNode detail2List = detail2.AddListUnordered();
                IMdNode extraDetail = detail2List.AddListItem();
                extraDetail.WithContent("Extra Detail 1.1.2.1");
                IMdNode subtopic2 = topic1List.AddListItem();
                subtopic2.WithContent("Subtopic 1.2");
                
                IMdNode topic2 = unOrderedList.AddListItem();
                topic2.AddBold("Main Topic 2");
                IMdNode topic2List = topic2.AddListOrdered();
                IMdNode item21 = topic2List.AddListItem();
                item21.WithContent("Item 2.1");
                IMdNode item22 = topic2List.AddListItem();
                item22.WithContent("Item 2.2");
                IMdNode item22List = item22.AddListUnordered();
                IMdNode subItem221 = item22List.AddListItem();
                subItem221.WithContent("Sub-Item 2.2.1");
                IMdNode subItem222 = item22List.AddListItem();
                subItem222.WithContent("Sub-Item 2.2.2");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            URLs and URLs in angle brackets will automatically get turned into links.
            https://www.example.com or <https://www.example.com> and sometimes
            example.com (but not on Github, for example).
            """,
            """
            <p>URLs and URLs in angle brackets will automatically get turned into links.
            </p><p>https://www.example.com or &lt;https://www.example.com&gt; and sometimes
            </p><p>example.com (but not on Github, for example).</p>
            """
        );
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
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
            """,
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddH2("Longer Example with Multiple Sections");

                rootNode.AddH3("Introduction");
                IMarkdownSyntaxNode paragraph0 = rootNode.AddParagraph();
                paragraph0.WithContent("Welcome to this test. This section introduces the topic, along with ");
                paragraph0.AddBold("bold");
                paragraph0.WithContent(", ");
                paragraph0.AddItalic("italic");
                paragraph0.WithContent(", and ");
                paragraph0.AddCodeInline("code");
                paragraph0.WithContent(" formatting.");

                rootNode.AddH3("Code Snippet");
                rootNode.AddParagraph("Below is an example of a C# code snippet:");
                IMarkdownSyntaxNode csharpCode = rootNode.AddCodeBlock("public class Program {\n    public static void Main() {\n        Console.WriteLine(\"Hello, World!\");\n    }\n}\n");
                csharpCode.WithClass("language-csharp");

                rootNode.AddH3("Bullet Points");
                rootNode.AddParagraph("Here are some bullet points:");
                IMarkdownSyntaxNode bulletList = rootNode.AddListUnordered();
                bulletList.AddListItem("Point one");
                IMarkdownSyntaxNode nestedItem = bulletList.AddListItem("Point two");
                IMarkdownSyntaxNode bulletList2 = nestedItem.AddListUnordered();
                bulletList2.AddListItem("Subpoint A");
                bulletList2.AddListItem("Subpoint B");

                rootNode.AddH3("Blockquote");
                IMarkdownSyntaxNode blockquote = rootNode.AddBlockquote();
                blockquote.AddParagraph("This is a blockquote. It can contain multiple lines of text");
                blockquote.AddParagraph("and demonstrates how Markdown handles quoted content.");

                rootNode.AddH3("Table Example");
                IMarkdownSyntaxNode table = rootNode.AddTable();
                IMarkdownSyntaxNode tableHeader = table.AddTableHead();
                IMarkdownSyntaxNode tableHeaderRow = tableHeader.AddTableRow();
                tableHeaderRow.AddTableHeadCell("Column 1");
                tableHeaderRow.AddTableHeadCell("Column 2");
                tableHeaderRow.AddTableHeadCell("Column 3");
                IMarkdownSyntaxNode tableBody = table.AddTableBody();
                IMarkdownSyntaxNode tableBodyRow0 = tableBody.AddTableRow();
                tableBodyRow0.AddTableCell("Data 1");
                tableBodyRow0.AddTableCell("Data 2");
                tableBodyRow0.AddTableCell("Data 3");
                IMarkdownSyntaxNode tableBodyRow1 = tableBody.AddTableRow();
                tableBodyRow1.AddTableCell("Data 4");
                tableBodyRow1.AddTableCell("Data 5");
                tableBodyRow1.AddTableCell("Data 6");

                rootNode.AddH3("Links and Images");
                IMarkdownSyntaxNode paragraph1 = rootNode.AddParagraph();
                paragraph1.WithContent("You can visit ");
                paragraph1.AddLink("Google").WithAttribute("href", "https://www.google.com");
                paragraph1.WithContent(" or check out the following image:");
                IMarkdownSyntaxNode image = rootNode.AddParagraph().AddImage();
                image.WithAttribute("src", "https://via.placeholder.com/150");
                image.WithAttribute("alt", "Placeholder Image");
            }
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
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode heading = rootNode.AddH2();
                heading.WithContent("Nested Lists and Complex Formatting");

                IMarkdownSyntaxNode unOrderedList = rootNode.AddListUnordered();
                IMarkdownSyntaxNode topic1 = unOrderedList.AddListItem();
                topic1.AddBold("Main Topic 1");
                IMarkdownSyntaxNode topic1List = topic1.AddListUnordered();
                IMarkdownSyntaxNode subtopic1 = topic1List.AddListItem();
                subtopic1.WithContent("Subtopic 1.1");
                IMarkdownSyntaxNode subtopic1List = subtopic1.AddListUnordered();
                IMarkdownSyntaxNode detail1 = subtopic1List.AddListItem();
                detail1.WithContent("Detail 1.1.1");
                IMarkdownSyntaxNode detail2 = subtopic1List.AddListItem();
                detail2.WithContent("Detail 1.1.2");
                IMarkdownSyntaxNode detail2List = detail2.AddListUnordered();
                IMarkdownSyntaxNode extraDetail = detail2List.AddListItem();
                extraDetail.WithContent("Extra Detail 1.1.2.1");
                IMarkdownSyntaxNode subtopic2 = topic1List.AddListItem();
                subtopic2.WithContent("Subtopic 1.2");

                IMarkdownSyntaxNode topic2 = unOrderedList.AddListItem();
                topic2.AddBold("Main Topic 2");
                IMarkdownSyntaxNode topic2List = topic2.AddListOrdered();
                IMarkdownSyntaxNode item21 = topic2List.AddListItem();
                item21.WithContent("Item 2.1");
                IMarkdownSyntaxNode item22 = topic2List.AddListItem();
                item22.WithContent("Item 2.2");
                IMarkdownSyntaxNode item22List = item22.AddListUnordered();
                IMarkdownSyntaxNode subItem221 = item22List.AddListItem();
                subItem221.WithContent("Sub-Item 2.2.1");
                IMarkdownSyntaxNode subItem222 = item22List.AddListItem();
                subItem222.WithContent("Sub-Item 2.2.2");
            }
        );
    }
}

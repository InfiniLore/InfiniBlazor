// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class EdgeCaseDataSources {
    private static readonly string SectionName = nameof(EdgeCaseDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {

        // Caused everything to be a list
        yield return static () => new MarkdownTestDto(SectionName,
            """
            1234
            1234
            1234
            """,
            """
            <p>1234</p>
            <p>1234</p>
            <p>1234</p>
            """,
            static rootNode => {
                rootNode.AddParagraph("1234");
                rootNode.AddParagraph("1234");
                rootNode.AddParagraph("1234");
            }
        );
        
        // Caused a weird issue that created a horizontal line
        yield return static () => new MarkdownTestDto(SectionName,
            """
            1234
            1234
            123
            """,
            """
            <p>1234</p>
            <p>1234</p>
            <p>123</p>
            """,
            static rootNode => {
                rootNode.AddParagraph("1234");
                rootNode.AddParagraph("1234");
                rootNode.AddParagraph("123");
            }
        );

        // Unclosed items
        yield return static () => new MarkdownTestDto(SectionName,
            "**bold",
            "<p>**bold</p>",
            static rootNode => {
                rootNode.AddParagraph("**bold");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            "*italics",
            "<p>*italics</p>",
            static rootNode => {
                rootNode.AddParagraph("*italics");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "[link](https://example.com",
            "<p>[link](https://example.com</p>",
            static rootNode => {
                rootNode.AddParagraph("[link](https://example.com");
            }
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "# Heading 1 # Not a heading",
            "<h1>Heading 1 # Not a heading</h1>",
            static rootNode => {
                rootNode.AddH1("Heading 1 # Not a heading");
            }
        );

        // Span should be allowed
        yield return static () => new MarkdownTestDto(SectionName,
            "<span style=\"color: red\">**red bold?**</span>",
            "<p><span style=\"color: rgba(255,0,0,1)\"><strong>red bold?</strong></span></p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithHtmlContent("<span style=\"color: red\">");
                paragraph.AddBold("red bold?");
                paragraph.WithHtmlContent("</span>");
            }
        );
        
    }
}

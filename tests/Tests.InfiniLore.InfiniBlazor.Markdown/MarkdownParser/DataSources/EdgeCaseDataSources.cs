// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
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
            """
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
            """
        );

        // Unclosed items
        yield return static () => new MarkdownTestDto(SectionName,
            "**bold",
            "<p>**bold</p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "*italics",
            "<p>*italics</p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "[link](https://example.com",
            "<p>[link](https://example.com</p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "# Heading 1 # Not a heading",
            "<h1>Heading 1 # Not a heading</h1>"
        );

        // Span should be allowed
        yield return static () => new MarkdownTestDto(SectionName,
            "<span style=\"color: red\">**red bold?**</span>",
            "<p><span style=\"color: rgba(255,0,0,1)\"><strong>red bold?</strong></span></p>"
        );
        
    }
}

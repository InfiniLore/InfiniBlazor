// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.Blazor.Markdown.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class HeadingDataSources {
    private static readonly string SectionName = nameof(HeadingDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        for (int i = 1; i < 7; i++) {
            string heading = new('#', i);
            int depth = i;
            yield return () => new MarkdownTestDto(SectionName,
                $"{heading} Heading",
                $"<h{depth}>Heading</h{depth}>"
            );
        }

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
            --
            """,
            "<p>Heading</p><p>--</p>"
        );
        
        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
            ==
            """,
            "<p>Heading</p><p>==</p>"
        );

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
            ---
            """,
            "<h1>Heading</h1>"
        );

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
            ===
            """,
            "<h1>Heading</h1>"
        );

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
                ========
            """,
            "<h1>Heading</h1>"
        );
        
        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
                --------
            """,
            "<h1>Heading</h1>"
        );
    }
}

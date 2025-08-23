// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class HeadingDataSources {
    private static readonly string SectionName = nameof(HeadingDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<OldMdTestData>> DataSources() {
        for (int i = 1; i < 7; i++) {
            string heading = new('#', i);
            int depth = i;
            yield return () => new OldMdTestData(SectionName,
                $"{heading} Heading",
                $"<h{depth}>Heading</h{depth}>"
            );
        }

        yield return () => new OldMdTestData(SectionName,
            """
            Heading
            --
            """,
            "<p>Heading</p><p>--</p>"
        );

        yield return () => new OldMdTestData(SectionName,
            """
            Heading
            ==
            """,
            "<p>Heading</p><p>==</p>"
        );

        yield return () => new OldMdTestData(SectionName,
            """
            Heading
            ---
            """,
            "<h1>Heading</h1>"
        );

        yield return () => new OldMdTestData(SectionName,
            """
            Heading
            ===
            """,
            "<h1>Heading</h1>"
        );

        yield return () => new OldMdTestData(SectionName,
            """
            Heading
                ========
            """,
            "<h1>Heading</h1>"
        );

        yield return () => new OldMdTestData(SectionName,
            """
            Heading
                --------
            """,
            "<h1>Heading</h1>"
        );

        yield return () => new OldMdTestData(SectionName,
            """
            #
            Something
            """,
            """
            <p>#</p>
            <p>Something</p>
            """
        );
    }
}

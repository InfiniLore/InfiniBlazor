// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class HeadingDataSources {
    private static readonly string SectionName = nameof(HeadingDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        for (int i = 1; i < 7; i++) {
            string heading = new('#', i);
            int depth = i;
            yield return () => new MdTestData(SectionName,
                $"{heading} Heading",
                $"<h{depth}>Heading</h{depth}>"
            );
        }

        yield return () => new MdTestData(SectionName,
            """
            Heading
            --
            """,
            "<p>Heading</p><p>--</p>"
        );

        yield return () => new MdTestData(SectionName,
            """
            Heading
            ==
            """,
            "<p>Heading</p><p>==</p>"
        );

        yield return () => new MdTestData(SectionName,
            """
            Heading
            ---
            """,
            "<h1>Heading</h1>"
        );

        yield return () => new MdTestData(SectionName,
            """
            Heading
            ===
            """,
            "<h1>Heading</h1>"
        );

        yield return () => new MdTestData(SectionName,
            """
            Heading
                ========
            """,
            "<h1>Heading</h1>"
        );

        yield return () => new MdTestData(SectionName,
            """
            Heading
                --------
            """,
            "<h1>Heading</h1>"
        );

        yield return () => new MdTestData(SectionName,
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

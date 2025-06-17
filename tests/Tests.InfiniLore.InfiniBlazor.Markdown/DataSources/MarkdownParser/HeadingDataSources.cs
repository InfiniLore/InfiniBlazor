// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;
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
                $"<h{depth}>Heading</h{depth}>",
                ConfigureExpectedNode: rootNode => {
                    _ = depth switch {
                        1 => rootNode.AddH1("Heading"),
                        2 => rootNode.AddH2("Heading"),
                        3 => rootNode.AddH3("Heading"),
                        4 => rootNode.AddH4("Heading"),
                        5 => rootNode.AddH5("Heading"),
                        6 => rootNode.AddH6("Heading"),
                        _ => throw new ArgumentOutOfRangeException(nameof(depth), depth, null)
                    };

                }
            );
        }

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
            --
            """,
            "<p>Heading</p><p>--</p>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph("Heading");
                rootNode.AddParagraph("--");
            }
        );

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
            ==
            """,
            "<p>Heading</p><p>==</p>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph("Heading");
                rootNode.AddParagraph("==");
            }
        );

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
            ---
            """,
            "<h1>Heading</h1>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddH1("Heading");
            }
        );

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
            ===
            """,
            "<h1>Heading</h1>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddH1("Heading");
            }
        );

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
                ========
            """,
            "<h1>Heading</h1>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddH1("Heading");
            }
        );

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
                --------
            """,
            "<h1>Heading</h1>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddH1("Heading");
            }
        );

        yield return () => new MarkdownTestDto(SectionName,
            """
            #
            Something
            """,
            """
            <p>#</p>
            <p>Something</p>
            """,
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph("#");
                rootNode.AddParagraph("Something");
            }
        );
    }
}

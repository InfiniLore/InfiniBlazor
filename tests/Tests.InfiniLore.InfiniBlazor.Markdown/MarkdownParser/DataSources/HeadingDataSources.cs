// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
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
                rootNode => {
                    IMdNode headingNode = depth switch {
                        1 => rootNode.AddH1(),
                        2 => rootNode.AddH2(),
                        3 => rootNode.AddH3(),
                        4 => rootNode.AddH4(),
                        5 => rootNode.AddH5(),
                        6 => rootNode.AddH6(),
                        _ => throw new ArgumentOutOfRangeException(nameof(depth), depth, null)
                    };
                    headingNode.WithContent("Heading");
                    
                }
            );
        }

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
            --
            """,
            "<p>Heading</p><p>--</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Heading");
                IMdNode paragraph2 = rootNode.AddParagraph();
                paragraph2.WithContent("--");
            }
        );
        
        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
            ==
            """,
            "<p>Heading</p><p>==</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Heading");
                IMdNode paragraph2 = rootNode.AddParagraph();
                paragraph2.WithContent("==");
            }
        );

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
            ---
            """,
            "<h1>Heading</h1>",
            static rootNode => {
                IMdNode heading = rootNode.AddH1();
                heading.WithContent("Heading");
            }
        );

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
            ===
            """,
            "<h1>Heading</h1>",
            static rootNode => {
                IMdNode heading = rootNode.AddH1();
                heading.WithContent("Heading");
            }
        );

        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
                ========
            """,
            "<h1>Heading</h1>",
            static rootNode => {
                IMdNode heading = rootNode.AddH1();
                heading.WithContent("Heading");
            }
        );
        
        yield return () => new MarkdownTestDto(SectionName,
            """
            Heading
                --------
            """,
            "<h1>Heading</h1>",
            static rootNode => {
                IMdNode heading = rootNode.AddH1();
                heading.WithContent("Heading");
            }
        );
    }
}

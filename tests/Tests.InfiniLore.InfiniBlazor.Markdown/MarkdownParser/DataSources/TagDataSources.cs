// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class TagDataSources {
    private static readonly string SectionName = nameof(TagDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "#tag",
            "<p><span>#tag</span></p>",
            static rootNode => rootNode.AddParagraph()
                .AddTag("tag")
        );
        yield return static () => new MarkdownTestDto(SectionName,
            "#不",
            "<p><span>#不</span></p>",
            static rootNode => rootNode.AddParagraph()
                .AddTag("不")
            
        );
        yield return static () => new MarkdownTestDto(SectionName,
            "#öäüÖÄÜß",
            "<p><span>#öäüÖÄÜß</span></p>",
            static rootNode => rootNode.AddParagraph()
                .AddTag("öäüÖÄÜß")
            
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "**#tag**",
            "<p><strong><span>#tag</span></strong></p>",
            static rootNode => rootNode.AddParagraph()
                .AddBold().AddTag("tag")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "*#tag*",
            "<p><em><span>#tag</span></em></p>",
            static rootNode => rootNode.AddParagraph()
            .AddItalic().AddTag("tag")
        );
    }
}

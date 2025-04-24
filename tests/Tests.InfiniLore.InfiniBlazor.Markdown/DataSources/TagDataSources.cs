// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
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
            "<p><span class=\"tag\">tag</span></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .AddTag("tag")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "#不",
            "<p><span class=\"tag\">不</span></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .AddTag("不")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "#öäüÖÄÜß",
            "<p><span class=\"tag\">öäüÖÄÜß</span></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .AddTag("öäüÖÄÜß")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "**#tag**",
            "<p><strong><span class=\"tag\">tag</span></strong></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .AddBold().AddTag("tag")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "*#tag*",
            "<p><em><span class=\"tag\">tag</span></em></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .AddItalic().AddTag("tag")
        );
    }
}

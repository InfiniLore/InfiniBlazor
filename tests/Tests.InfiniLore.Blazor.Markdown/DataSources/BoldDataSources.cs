// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.Blazor.Markdown.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class BoldDataSources {
    private static readonly string SectionName = nameof(BoldDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "**bold**",
            "<p><strong>bold</strong></p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            @"**\*bold**",
            "<p><strong>*bold</strong></p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            @"**bold\***",
            "<p><strong>bold*</strong></p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "**bold *nested italic***",
            "<p><strong>bold <em>nested italic</em></strong></p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "***nested italic* bold**",
            "<p><strong><em>nested italic</em> bold</strong></p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "** \\* **",
            "<p><strong> * </strong></p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "**bold *nested \\* italic***",
            "<p><strong>bold <em>nested * italic</em></strong></p>"
        );
    }
}

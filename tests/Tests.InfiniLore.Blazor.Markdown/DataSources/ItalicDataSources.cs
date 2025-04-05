// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.Blazor.Markdown.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ItalicDataSources {
    private static readonly string SectionName = nameof(ItalicDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "*italic*",
            "<p><em>italic</em></p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            @"*\*italic*",
            "<p><em>*italic</em></p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            @"*italic\**",
            "<p><em>italic*</em></p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"* \* *",
            "<p><em> * </em></p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "* something **bold** in italic *",
            "<p><em>something <strong>bold</strong> in italic</em></p>"
        );
    }
}

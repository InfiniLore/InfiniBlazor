// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.Blazor.Markdown.MarkdownParser.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class BoldAndItalicDataSources {
    private static readonly string SectionName = nameof(BoldAndItalicDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "***bold and italic***",
            "<p><strong><em>bold and italic</em></strong></p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            @"***\*bold and italic***",
            "<p><strong><em>*bold and italic</em></strong></p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            @"***bold and italic\****",
            "<p><strong><em>bold and italic*</em></strong></p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            @"*** \* \* \* ***",
            "<p><strong><em> * * * </em></strong></p>"
        );
    }
}

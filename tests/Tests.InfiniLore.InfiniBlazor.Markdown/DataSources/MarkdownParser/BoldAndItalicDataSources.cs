// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class BoldAndItalicDataSources {
    private static readonly string SectionName = nameof(BoldAndItalicDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<OldMdTestData>> DataSources() {
        yield return static () => new OldMdTestData(SectionName,
            "***bold and italic***",
            "<p><strong><em>bold and italic</em></strong></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            @"***\*bold and italic***",
            "<p><strong><em>*bold and italic</em></strong></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            @"***bold and italic\****",
            "<p><strong><em>bold and italic*</em></strong></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            @"*** \* \* \* ***",
            "<p><strong><em> * * * </em></strong></p>"
        );
    }
}

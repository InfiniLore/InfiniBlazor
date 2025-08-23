// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ItalicDataSources {
    private static readonly string SectionName = nameof(ItalicDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<OldMdTestData>> DataSources() {
        yield return static () => new OldMdTestData(SectionName,
            "*italic*",
            "<p><em>italic</em></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            @"*\*italic*",
            "<p><em>*italic</em></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            @"*italic\**",
            "<p><em>italic*</em></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            @"* \* *",
            "<p><em> * </em></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "* something **bold** in italic *",
            "<p><em>something <strong>bold</strong> in italic</em></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "**",
            "<p>**</p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "some text ****",// to exclude the <hr> tag being triggered
            "<p>some text <em>**</em></p>"
        );
    }
}

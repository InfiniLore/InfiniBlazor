// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ItalicDataSources {
    private static readonly string SectionName = nameof(ItalicDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        yield return static () => new MdTestData(SectionName,
            "*italic*",
            "<p><em>italic</em></p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"*\*italic*",
            "<p><em>*italic</em></p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"*italic\**",
            "<p><em>italic*</em></p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"* \* *",
            "<p><em> * </em></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "* something **bold** in italic *",
            "<p><em>something <strong>bold</strong> in italic</em></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "**",
            "<p>**</p>"
        );

        yield return static () => new MdTestData(SectionName,
            "some text ****",// to exclude the <hr> tag being triggered
            "<p>some text <em>**</em></p>"
        );
    }
}

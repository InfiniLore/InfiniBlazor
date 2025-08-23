// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class TagDataSources {
    private static readonly string SectionName = nameof(TagDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<OldMdTestData>> DataSources() {
        yield return static () => new OldMdTestData(SectionName,
            "#tag",
            "<p><span class=\"md-tag\">#tag</span></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "#不",
            "<p><span class=\"md-tag\">#不</span></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "#öäüÖÄÜß",
            "<p><span class=\"md-tag\">#öäüÖÄÜß</span></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "**#tag**",
            "<p><strong><span class=\"md-tag\">#tag</span></strong></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "*#tag*",
            "<p><em><span class=\"md-tag\">#tag</span></em></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "#this is not a valid tag",
            "<p><span class=\"md-tag\">#this</span> is not a valid tag</p>");
        
        yield return static () => new OldMdTestData(SectionName,
            "#[link](https://www.transgenderinfo.be)",
            "<p>#<a href=\"https://www.transgenderinfo.be\">link</a></p>");
    }
}

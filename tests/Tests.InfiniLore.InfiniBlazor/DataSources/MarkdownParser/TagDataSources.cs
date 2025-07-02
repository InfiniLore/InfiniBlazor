// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class TagDataSources {
    private static readonly string SectionName = nameof(TagDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        yield return static () => new MdTestData(SectionName,
            "#tag",
            "<p><span class=\"tag\">#tag</span></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "#不",
            "<p><span class=\"tag\">#不</span></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "#öäüÖÄÜß",
            "<p><span class=\"tag\">#öäüÖÄÜß</span></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "**#tag**",
            "<p><strong><span class=\"tag\">#tag</span></strong></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "*#tag*",
            "<p><em><span class=\"tag\">#tag</span></em></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "#this is not a valid tag",
            "<p><span class=\"tag\">#this</span> is not a valid tag</p>");
        
        yield return static () => new MdTestData(SectionName,
            "#[link](https://www.transgenderinfo.be)",
            "<p>#<a href=\"https://www.transgenderinfo.be\">link</a></p>");
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class SpecialCharacterDataSources {
    private static readonly string SectionName = nameof(SpecialCharacterDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<OldMdTestData>> DataSources() {
        yield return static () => new OldMdTestData(SectionName,
            "&",
            "<p>&</p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "<",
            "<p><</p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            ">",
            "<p>></p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "&copy;",
            "<p>&copy;</p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "This contains an emoji: 😀",
            "<p>This contains an emoji: 😀</p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "@username mentions",
            "<p>@username mentions</p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "test <br/> test",
            "<p>test <br/> test</p>"
        );
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class SpecialCharacterDataSources {
    private static readonly string SectionName = nameof(SpecialCharacterDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        yield return static () => new MdTestData(SectionName,
            "&",
            "<p>&</p>"
        );

        yield return static () => new MdTestData(SectionName,
            "<",
            "<p><</p>"
        );

        yield return static () => new MdTestData(SectionName,
            ">",
            "<p>></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "&copy;",
            "<p>&copy;</p>"
        );

        yield return static () => new MdTestData(SectionName,
            "This contains an emoji: 😀",
            "<p>This contains an emoji: 😀</p>"
        );

        yield return static () => new MdTestData(SectionName,
            "@username mentions",
            "<p>@username mentions</p>"
        );

        yield return static () => new MdTestData(SectionName,
            "test <br/> test",
            "<p>test <br/> test</p>"
        );
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown._DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class StrikethroughDataSources {
    private static readonly string SectionName = nameof(StrikethroughDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        yield return static () => new MdTestData(
            SectionName,
            "~something~",
            "<p><s>something</s></p>"
        );

        yield return static () => new MdTestData(
            SectionName,
            @"\~escaped~",
            "<p>~escaped~</p>"
        );

        yield return static () => new MdTestData(
            SectionName,
            @"~escaped\~",
            "<p>~escaped~</p>"
        );

        yield return static () => new MdTestData(
            SectionName,
            "~nested ~strikethrough~ does not work~",
            "<p><s>nested</s>strikethrough<s> does not work</s></p>"
        );

        yield return static () => new MdTestData(
            SectionName,
            "~**bold and strike-through**~",
            "<p><s><strong>bold and strike-through</strong></s></p>"
        );

        yield return static () => new MdTestData(
            SectionName,
            "~*italic and strike-through*~",
            "<p><s><em>italic and strike-through</em></s></p>"
        );

        yield return static () => new MdTestData(
            SectionName,
            "**~bold strike~** normal ~strikethrough~",
            "<p><strong><s>bold strike</s></strong> normal <s>strikethrough</s></p>"
        );

        yield return static () => new MdTestData(
            SectionName,
            "~~",
            "<p>~~</p>"
        );

        yield return static () => new MdTestData(
            SectionName,
            "~one~ and ~two~!",
            "<p><s>one</s> and <s>two</s>!</p>"
        );

    }
}

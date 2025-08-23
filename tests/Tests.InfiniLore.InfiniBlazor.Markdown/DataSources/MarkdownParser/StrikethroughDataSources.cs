// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class StrikethroughDataSources {
    private static readonly string SectionName = nameof(StrikethroughDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<OldMdTestData>> DataSources() {
        yield return static () => new OldMdTestData(
            SectionName,
            "~something~",
            "<p><s>something</s></p>"
        );

        yield return static () => new OldMdTestData(
            SectionName,
            @"\~escaped~",
            "<p>~escaped~</p>"
        );

        yield return static () => new OldMdTestData(
            SectionName,
            @"~escaped\~",
            "<p>~escaped~</p>"
        );

        yield return static () => new OldMdTestData(
            SectionName,
            "~nested ~strikethrough~ does not work~",
            "<p><s>nested</s>strikethrough<s> does not work</s></p>"
        );

        yield return static () => new OldMdTestData(
            SectionName,
            "~**bold and strike-through**~",
            "<p><s><strong>bold and strike-through</strong></s></p>"
        );

        yield return static () => new OldMdTestData(
            SectionName,
            "~*italic and strike-through*~",
            "<p><s><em>italic and strike-through</em></s></p>"
        );

        yield return static () => new OldMdTestData(
            SectionName,
            "**~bold strike~** normal ~strikethrough~",
            "<p><strong><s>bold strike</s></strong> normal <s>strikethrough</s></p>"
        );

        yield return static () => new OldMdTestData(
            SectionName,
            "~~",
            "<p>~~</p>"
        );

        yield return static () => new OldMdTestData(
            SectionName,
            "~one~ and ~two~!",
            "<p><s>one</s> and <s>two</s>!</p>"
        );

    }
}

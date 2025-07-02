// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class EmoteDataSources {
    private static readonly string SectionName = nameof(EmoteDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        yield return static () => new MdTestData(
            SectionName,
            ":flag-transgender:",
            "<p>\ud83c\udff3\ufe0f\u200d\u26a7\ufe0f</p>"
        );

        yield return static () => new MdTestData(
            SectionName,
            "[:flag-transgender:](https://www.twitch.tv/annasasdev)",
            "<p><a href=\"https://www.twitch.tv/annasasdev\">\ud83c\udff3\ufe0f\u200d\u26a7\ufe0f</a></p>"
        );

        yield return static () => new MdTestData(
            SectionName,
            ":not-an-emote:",
            "<p>:not-an-emote:</p>"
        );

    }
}

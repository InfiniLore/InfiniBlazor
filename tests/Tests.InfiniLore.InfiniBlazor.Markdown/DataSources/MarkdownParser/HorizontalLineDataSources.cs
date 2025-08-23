// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class HorizontalLineDataSources {
    private static readonly string SectionName = nameof(HorizontalLineDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<OldMdTestData>> DataSources() {
        char[] chars = new[] { '-', '=' };
        foreach (char c in chars) {
            for (int i = 1; i < 10; i++) {
                string text = new(c, i);
                string content = i < 3
                    ? $"<p>{text}</p>"
                    : "<hr>";

                // int index = i;
                yield return () => new OldMdTestData(
                    SectionName,
                    text,
                    content
                );
            }
        }
    }
}

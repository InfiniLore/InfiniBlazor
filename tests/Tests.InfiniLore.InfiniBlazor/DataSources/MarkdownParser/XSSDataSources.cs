// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class XSSDataSources {
    private static readonly string SectionName = nameof(XSSDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        yield return static () => new MdTestData(SectionName,
            "<a href=\"javascript:alert('XSS')\">Click Me</a>",
            "<p><a href=\"javascript:alert('XSS')\">Click Me</a></p>"
        );

        // Some tests based on https://github.com/cujanovic/Markdown-XSS-Payloads/blob/master/Markdown-XSS-Payloads.txt
        yield return static () => new MdTestData(SectionName,
            "![Uh oh...](\"onerror=\"alert('XSS'))",
            "<p>![Uhoh...](\"onerror=\"alert('XSS'))</p>"
        );

        yield return static () => new MdTestData(SectionName,
            "[a](javascript:prompt(document.cookie))",
            "<p>[a](javascript:prompt(document.cookie))</p>"
        );

        yield return static () => new MdTestData(SectionName,
            "![a](javascript:prompt(document.cookie))\\",
            "<p>![a](javascript:prompt(document.cookie))\\</p>"
        );

        yield return static () => new MdTestData(SectionName,
            "<javascript:prompt(document.cookie)>",
            "<p><javascript:prompt(document.cookie)></p>"
        );
    }
}

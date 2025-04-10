// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.Blazor.Markdown.MarkdownParser.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable once InconsistentNaming
public static class XSSDataSources {
    private static readonly string SectionName = nameof(XSSDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "<a href=\"javascript:alert('XSS')\">Click Me</a>",
            "<p><a>Click Me</a></p>"
        );
        
        // Some tests based on https://github.com/cujanovic/Markdown-XSS-Payloads/blob/master/Markdown-XSS-Payloads.txt
        yield return static () => new MarkdownTestDto(SectionName,
            "![Uh oh...](\"onerror=\"alert('XSS'))",
            "<p>![Uhoh...](&quot;onerror=&quot;alert('XSS'))</p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "[a](javascript:prompt(document.cookie))",
            "<p>[a](javascript:prompt(document.cookie))</p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "![a](javascript:prompt(document.cookie))\\",
            "<p>![a](javascript:prompt(document.cookie))\\</p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "<javascript:prompt(document.cookie)>",
            "<p>&lt;javascript:prompt(document.cookie)&gt;</p>"
        );
    }
}

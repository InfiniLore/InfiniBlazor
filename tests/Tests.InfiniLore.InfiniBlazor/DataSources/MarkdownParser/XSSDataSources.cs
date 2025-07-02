// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Syntax;

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
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "<a href=\"javascript:alert('XSS')\">Click Me</a>",
            "<p><a href=\"javascript:alert('XSS')\">Click Me</a></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithHtmlContent("<a href=\"javascript:alert('XSS')\">Click Me</a>");
            }
        );

        // Some tests based on https://github.com/cujanovic/Markdown-XSS-Payloads/blob/master/Markdown-XSS-Payloads.txt
        yield return static () => new MarkdownTestDto(SectionName,
            "![Uh oh...](\"onerror=\"alert('XSS'))",
            "<p>![Uhoh...](\"onerror=\"alert('XSS'))</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("![Uh oh...](\"onerror=\"alert('XSS'))");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "[a](javascript:prompt(document.cookie))",
            "<p>[a](javascript:prompt(document.cookie))</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("[a](javascript:prompt(document.cookie))");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "![a](javascript:prompt(document.cookie))\\",
            "<p>![a](javascript:prompt(document.cookie))\\</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("![a](javascript:prompt(document.cookie))\\");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "<javascript:prompt(document.cookie)>",
            "<p><javascript:prompt(document.cookie)></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("<javascript:prompt(document.cookie)>");
            }
        );
    }
}

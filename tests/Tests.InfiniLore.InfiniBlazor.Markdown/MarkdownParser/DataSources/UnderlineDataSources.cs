// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class UnderlineDataSources {
    private static readonly string SectionName = nameof(UnderlineDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "something _underlined_",
            """
            <p>something <span style="text-decoration: underline;">underlined</span></p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("something ");
                paragraph.AddUnderline("underlined");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "something _underlined with an \\_ escaped_",
            """
            <p>something <span style="text-decoration: underline;">underlined with an _ escaped</span></p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("something ");
                paragraph.AddUnderline()
                    .WithContent("underlined with an _ escaped");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "something _**bold and underlined**_",
            """
            <p>something <span style="text-decoration: underline;"><strong>bold and underlined</strong></span></p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("something ");
                paragraph.AddUnderline()
                    .AddBold()
                    .WithContent("bold and underlined");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "something _*italic and underlined*_",
            """
            <p>something <span style="text-decoration: underline;"><em>italic and underlined</em></span></p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("something ");
                paragraph.AddUnderline()
                    .AddItalic()
                    .WithContent("italic and underlined");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "something _***bold, italic, and underlined***_",
            """
            <p>something <span style="text-decoration: underline;"><strong><em>bold, italic, and underlined</em></strong></span></p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("something ");
                paragraph.AddUnderline()
                    .AddBold()
                    .AddItalic()
                    .WithContent("bold, italic, and underlined");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "click _[here](https://example.com)_",
            """
            <p>click <span style="text-decoration: underline;"><a href="https://example.com">here</a></span></p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("click ");
                paragraph.AddUnderline()
                    .AddLink()
                    .WithAttribute("href", "https://example.com")
                    .WithContent("here");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "something _**[bold link](https://example.com)**_",
            """
            <p>something <span style="text-decoration: underline;"><strong><a href="https://example.com">bold link</a></strong></span></p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("something ");
                paragraph.AddUnderline()
                    .AddBold()
                    .AddLink()
                    .WithAttribute("href", "https://example.com")
                    .WithContent("bold link");
            }
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            "this has _**multiple** elements to *underline*_",
            """
            <p>this has <span style="text-decoration: underline;"><strong>multiple</strong> elements to<em> underline</em></span></p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("this has ");
                IMdNode underline = paragraph.AddUnderline();
                underline.AddBold("multiple");
                underline.WithContent(" elements to ");
                underline.AddItalic("underline");
            }
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            "__",
            "<p>__</p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .WithContent("__")
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            @"\_escaped_",
            "<p>_escaped_</p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .WithContent("_")// Escaped Char
                .WithContent("escaped_")
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            @"_escaped\_",
            "<p>_escaped_</p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .WithContent("_escaped")
                .WithContent("_")// Escaped Char
        );
    }
}

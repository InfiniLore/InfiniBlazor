// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class BoldDataSources {
    private static readonly string SectionName = nameof(BoldDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "**bold**",
            "<p><strong>bold</strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.AddBold("bold");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            @"**\*bold**",
            "<p><strong>*bold</strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode bold = paragraph.AddBold("*");// Escaped char
                bold.WithContent("bold");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            @"**bold\***",
            "<p><strong>bold*</strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode bold = paragraph.AddBold("bold");
                bold.WithContent("*");// Escaped char
            }// Escaped char
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "**bold *nested italic***",
            "<p><strong>bold <em>nested italic</em></strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode bold = paragraph.AddBold("bold ");
                bold.AddItalic("nested italic");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            "***nested italic* bold**",
            "<p><strong><em>nested italic</em> bold</strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode bold = paragraph.AddBold();
                bold.AddItalic("nested italic");
                bold.WithContent(" bold");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "** \\* **",
            "<p><strong> * </strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode bold = paragraph.AddBold(" ");
                bold.WithContent("*");// Escaped char
                bold.WithContent(" ");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            "**bold *nested \\* italic***",
            "<p><strong>bold <em>nested * italic</em></strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode bold = paragraph.AddBold("bold ");
                IMarkdownSyntaxNode italic = bold.AddItalic();
                italic.WithContent("nested ");
                italic.WithContent("*");// Escaped char
                italic.WithContent(" italic");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            "some text *****",// to exclude the <hr> tag being triggered
            "<p>some text <strong>*</strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("some text ");
                paragraph.AddBold("*");
            }
        );
    }
}

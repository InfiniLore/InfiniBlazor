// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
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
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode bold = paragraph.AddBold();
                bold.WithContent("bold");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            @"**\*bold**",
            "<p><strong>*bold</strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode bold = paragraph.AddBold();
                bold.WithContent("*"); // Escaped char
                bold.WithContent("bold");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            @"**bold\***",
            "<p><strong>bold*</strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode bold = paragraph.AddBold();
                bold.WithContent("bold");
                bold.WithContent("*"); // Escaped char
            }// Escaped char
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "**bold *nested italic***",
            "<p><strong>bold <em>nested italic</em></strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode bold = paragraph.AddBold();
                bold.WithContent("bold ");
                bold.AddItalic().WithContent("nested italic");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            "***nested italic* bold**",
            "<p><strong><em>nested italic</em> bold</strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode bold = paragraph.AddBold();
                bold.AddItalic().WithContent("nested italic");
                bold.WithContent(" bold");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "** \\* **",
            "<p><strong> * </strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode bold = paragraph.AddBold();
                bold.WithContent(" ");
                bold.WithContent("*"); // Escaped char
                bold.WithContent(" ");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            "**bold *nested \\* italic***",
            "<p><strong>bold <em>nested * italic</em></strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode bold = paragraph.AddBold();
                bold.WithContent("bold ");
                IMdNode italic =  bold.AddItalic();
                italic.WithContent("nested ");
                italic.WithContent("*"); // Escaped char
                italic.WithContent(" italic");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            "some text *****",// to exclude the <hr> tag being triggered
            "<p>some text <strong>*</strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("some text ");
                paragraph.AddBold().WithContent("*");
            }
        );
    }
}

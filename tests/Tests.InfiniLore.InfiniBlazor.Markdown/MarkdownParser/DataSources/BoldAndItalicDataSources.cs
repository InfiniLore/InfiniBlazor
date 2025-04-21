// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class BoldAndItalicDataSources {
    private static readonly string SectionName = nameof(BoldAndItalicDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "***bold and italic***",
            "<p><strong><em>bold and italic</em></strong></p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode bold = paragraph.AddBold();
                bold.AddItalic("bold and italic");
            }
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            @"***\*bold and italic***",
            "<p><strong><em>*bold and italic</em></strong></p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode bold = paragraph.AddBold();
                IMdNode italic = bold.AddItalic();
                italic.WithContent("*bold and italic");
            }
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            @"***bold and italic\****",
            "<p><strong><em>bold and italic*</em></strong></p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode bold = paragraph.AddBold();
                IMdNode italic = bold.AddItalic();
                italic.WithContent("bold and italic*");
            }
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            @"*** \* \* \* ***",
            "<p><strong><em> * * * </em></strong></p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode bold = paragraph.AddBold();
                bold.AddItalic(" * * * ");
            }
        );
    }
}

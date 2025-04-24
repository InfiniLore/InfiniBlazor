// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;
using Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ItalicDataSources {
    private static readonly string SectionName = nameof(ItalicDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "*italic*",
            "<p><em>italic</em></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.AddItalic("italic");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"*\*italic*",
            "<p><em>*italic</em></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.AddItalic("*italic");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"*italic\**",
            "<p><em>italic*</em></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.AddItalic("italic*");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"* \* *",
            "<p><em> * </em></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.AddItalic(" * ");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "* something **bold** in italic *",
            "<p><em>something <strong>bold</strong> in italic</em></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode italic = paragraph.AddItalic(" something ");
                italic.AddBold("bold");
                italic.WithContent(" in italic ");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "**",
            "<p>**</p>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph("**");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "some text ****",// to exclude the <hr> tag being triggered
            "<p>some text <em>**</em></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph("some text ");
                paragraph.AddItalic("**");
            }
        );
    }
}

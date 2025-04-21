// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class SpecialCharacterDataSources {
    private static readonly string SectionName = nameof(SpecialCharacterDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {

        yield return static () => new MarkdownTestDto(SectionName,
            "",
            "",
            ConfigureExpectedNode: static _ => {
                // Nothing to add
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "&",
            "<p>&amp;</p>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph("&");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "<",
            "<p>&lt;</p>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph("<");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            ">",
            "<p>&gt;</p>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph(">");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "&copy;",
            "<p>\u00a9</p>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph("&copy;");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "This contains an emoji: 😀",
            "<p>This contains an emoji: 😀</p>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph("This contains an emoji: 😀");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "@username mentions",
            "<p>@username mentions</p>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph("@username mentions");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "test <br/> test",
            "<p>test <br/> test</p>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph("test <br/> test");
            }
        );
    }
}

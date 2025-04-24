// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class EmoteDataSources {
    private static readonly string SectionName = nameof(EmoteDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(
            SectionName,
            ":flag-transgender:",
            "<p>\ud83c\udff3\ufe0f\u200d\u26a7\ufe0f</p>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph("\ud83c\udff3\ufe0f\u200d\u26a7\ufe0f");
            }
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            "[:flag-transgender:](https://www.twitch.tv/annasasdev)",
            "<p><a href=\"https://www.twitch.tv/annasasdev\">\ud83c\udff3\ufe0f\u200d\u26a7\ufe0f</a></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode link = paragraph.AddLink("\ud83c\udff3\ufe0f\u200d\u26a7\ufe0f");
                link.WithAttribute("href", "https://www.twitch.tv/annasasdev");
            }
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            ":not-an-emote:",
            "<p>:not-an-emote:</p>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph(":not-an-emote:");
            }
        );

    }
}

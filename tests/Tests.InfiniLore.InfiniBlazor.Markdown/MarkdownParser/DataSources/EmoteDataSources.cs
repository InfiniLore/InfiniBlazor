// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;

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
            static rootNode => {
                rootNode.AddParagraph("\ud83c\udff3\ufe0f\u200d\u26a7\ufe0f");
            }
        );
        
        yield return static () => new MarkdownTestDto(
            SectionName,
            "[:flag-transgender:](https://www.twitch.tv/annasasdev)",
            "<p><a href=\"https://www.twitch.tv/annasasdev\">\ud83c\udff3\ufe0f\u200d\u26a7\ufe0f</a></p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode link = paragraph.AddLink("\ud83c\udff3\ufe0f\u200d\u26a7\ufe0f");
                link.WithAttribute("href", "https://www.twitch.tv/annasasdev");
            }
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            ":not-an-emote:",
            "<p>:not-an-emote:</p>",
            static rootNode => {
                rootNode.AddParagraph(":not-an-emote:");
            }
        );

    }
}

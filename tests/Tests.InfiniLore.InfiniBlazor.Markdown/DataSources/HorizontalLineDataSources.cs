// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class HorizontalLineDataSources {
    private static readonly string SectionName = nameof(HorizontalLineDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        char[] chars = new[] { '-', '=' };
        foreach (char c in chars) {
            for (int i = 1; i < 10; i++) {
                string text = new(c, i);
                string content = i < 3
                    ? $"<p>{text}</p>"
                    : "<hr>";

                int index = i;
                yield return () => new MarkdownTestDto(
                    SectionName,
                    text,
                    content,
                    ConfigureExpectedNode: rootNode => {
                        if (index < 3) rootNode.AddParagraph(text);
                        else rootNode.AddHorizontalRule();
                    }
                );
            }
        }
    }
}

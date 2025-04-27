// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class StrikethroughDataSources {
    private static readonly string SectionName = nameof(StrikethroughDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(
            SectionName,
            "~something~",
            "<p><s>something</s></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.AddStrikethrough("something");
            }
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            @"\~escaped~",
            "<p>~escaped~</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("~escaped~");
            }
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            @"~escaped\~",
            "<p>~escaped~</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("~escaped~");
            }
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            "~nested ~strikethrough~ does not work~",
            "<p><s>nested</s>strikethrough<s> does not work</s></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.AddStrikethrough("nested ");
                paragraph.WithContent("strikethrough");
                paragraph.AddStrikethrough(" does not work");
            }
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            "~**bold and strike-through**~",
            "<p><s><strong>bold and strike-through</strong></s></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode strike = paragraph.AddStrikethrough();
                strike.AddBold("bold and strike-through");
            }
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            "~*italic and strike-through*~",
            "<p><s><em>italic and strike-through</em></s></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode strike = paragraph.AddStrikethrough();
                strike.AddItalic("italic and strike-through");
            }
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            "**~bold strike~** normal ~strikethrough~",
            "<p><strong><s>bold strike</s></strong> normal <s>strikethrough</s></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.AddBold().AddStrikethrough("bold strike");
                paragraph.WithContent(" normal ");
                paragraph.AddStrikethrough("strikethrough");
            }
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            "~~",
            "<p>~~</p>",
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddParagraph("~~");
            }
        );

        yield return static () => new MarkdownTestDto(
            SectionName,
            "~one~ and ~two~!",
            "<p><s>one</s> and <s>two</s>!</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.AddStrikethrough("one");
                paragraph.WithContent(" and ");
                paragraph.AddStrikethrough("two");
                paragraph.WithContent("!");
            }
        );

    }
}

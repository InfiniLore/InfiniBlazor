// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class TagDataSources {
    private static readonly string SectionName = nameof(TagDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "#tag",
            "<p><span class=\"tag\">tag</span></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .AddTag("tag")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "#不",
            "<p><span class=\"tag\">不</span></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .AddTag("不")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "#öäüÖÄÜß",
            "<p><span class=\"tag\">öäüÖÄÜß</span></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .AddTag("öäüÖÄÜß")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "**#tag**",
            "<p><strong><span class=\"tag\">tag</span></strong></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .AddBold().AddTag("tag")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "*#tag*",
            "<p><em><span class=\"tag\">tag</span></em></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .AddItalic().AddTag("tag")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "#this is not a valid tag",
            "<p><span class=\"tag\">this</span> is not a valid tag</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.AddTag("this");
                paragraph.WithContent(" is not a valid tag");
            });
        
        yield return static () => new MarkdownTestDto(SectionName,
            "#[link](https://www.transgenderinfo.be)",
            "<p>#<a href=\"https://www.transgenderinfo.be\">link</a></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("#"); 
                
                IMarkdownSyntaxNode link = paragraph.AddLink("link");
                link.WithAttribute(MarkdownAttribute.LinkHref, "https://www.transgenderinfo.be");
                
            });
    }
}

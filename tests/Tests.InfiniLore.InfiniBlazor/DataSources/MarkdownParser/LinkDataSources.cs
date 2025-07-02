// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class LinkDataSources {
    private static readonly string SectionName = nameof(LinkDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "This is an [-->*example*<--](https://www.transgenderinfo.be) of a link.",
            """<p>This is an <a href="https://www.transgenderinfo.be">--><em>example</em><--</a> of a link.</p>""",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph("This is an ");
                IMarkdownSyntaxNode link = paragraph.AddLink();
                link.WithAttribute(MarkdownAttribute.LinkHref, "https://www.transgenderinfo.be");

                link.WithContent("-->");
                link.AddItalic("example");
                link.WithContent("<--");
                paragraph.WithContent(" of a link.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "This is an **[-->*example*<--](https://www.transgenderinfo.be)** of a link.",
            """<p>This is an <strong><a href="https://www.transgenderinfo.be">--><em>example</em><--</a></strong> of a link.</p>""",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph("This is an ");
                IMarkdownSyntaxNode bold = paragraph.AddBold();
                IMarkdownSyntaxNode link = bold.AddLink();
                link.WithAttribute(MarkdownAttribute.LinkHref, "https://www.transgenderinfo.be");

                link.WithContent("-->");
                link.AddItalic("example");
                link.WithContent("<--");
                paragraph.WithContent(" of a link.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "This is an ^[-->*example*<--](https://www.transgenderinfo.be)^ of a link.",
            """<p>This is an <sub><a href="https://www.transgenderinfo.be">--><em>example</em><--</a></sub> of a link.</p>""",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph("This is an ");
                IMarkdownSyntaxNode sub = paragraph.AddSubscript();
                IMarkdownSyntaxNode link = sub.AddLink();
                link.WithAttribute(MarkdownAttribute.LinkHref, "https://www.transgenderinfo.be");

                link.WithContent("-->");
                link.AddItalic("example");
                link.WithContent("<--");
                paragraph.WithContent(" of a link.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "This is an ^^[-->*example*<--](https://www.transgenderinfo.be)^^ of a link.",
            """<p>This is an <sup><a href="https://www.transgenderinfo.be">--><em>example</em><--</a></sup> of a link.</p>""",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph("This is an ");
                IMarkdownSyntaxNode super = paragraph.AddSuperscript();
                IMarkdownSyntaxNode link = super.AddLink();
                link.WithAttribute(MarkdownAttribute.LinkHref, "https://www.transgenderinfo.be");

                link.WithContent("-->");
                link.AddItalic("example");
                link.WithContent("<--");
                paragraph.WithContent(" of a link.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "![Specs](https://i.imgur.com/aV8o3rE.png)",
            "<p><img src=\"https://i.imgur.com/aV8o3rE.png\" alt=\"Specs\"></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.AddImage()
                    .WithAttribute(MarkdownAttribute.ImageSource, "https://i.imgur.com/aV8o3rE.png")
                    .WithAttribute(MarkdownAttribute.ImageAlt, "Specs");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "[![Specs](https://i.imgur.com/aV8o3rE.png)](https://imgur.com/)",
            """
            <p>
                <a href="https://imgur.com/">
                    <img src="https://i.imgur.com/aV8o3rE.png" alt="Specs">
                </a>
            </p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode link = paragraph.AddLink();
                link.WithAttribute(MarkdownAttribute.LinkHref, "https://imgur.com/");
                IMarkdownSyntaxNode image = link.AddImage();
                image.WithAttribute(MarkdownAttribute.ImageSource, "https://i.imgur.com/aV8o3rE.png");
                image.WithAttribute(MarkdownAttribute.ImageAlt, "Specs");
            }
        );
    }
}

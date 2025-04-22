// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
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
                IMdNode paragraph = rootNode.AddParagraph("This is an ");
                IMdNode link = paragraph.AddLink();
                link.WithAttribute("href", "https://www.transgenderinfo.be");

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
                IMdNode paragraph = rootNode.AddParagraph("This is an ");
                IMdNode bold = paragraph.AddBold();
                IMdNode link = bold.AddLink();
                link.WithAttribute("href", "https://www.transgenderinfo.be");

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
                IMdNode paragraph = rootNode.AddParagraph("This is an ");
                IMdNode sub = paragraph.AddSubscript();
                IMdNode link = sub.AddLink();
                link.WithAttribute("href", "https://www.transgenderinfo.be");

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
                IMdNode paragraph = rootNode.AddParagraph("This is an ");
                IMdNode super = paragraph.AddSuperscript();
                IMdNode link = super.AddLink();
                link.WithAttribute("href", "https://www.transgenderinfo.be");

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
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.AddImage()
                    .WithAttribute("src", "https://i.imgur.com/aV8o3rE.png")
                    .WithAttribute("alt", "Specs");
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
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode link = paragraph.AddLink();
                link.WithAttribute("href", "https://imgur.com/");
                IMdNode image = link.AddImage();
                image.WithAttribute("src", "https://i.imgur.com/aV8o3rE.png");
                image.WithAttribute("alt", "Specs");
            }
        );
    }
}

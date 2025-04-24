// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class SuperScriptDataSources {
    private static readonly string SectionName = nameof(SuperScriptDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "^^superscript^^",
            "<p><sup>superscript</sup></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph().AddSuperscript("superscript")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "^^\\^superscript^^",
            "<p><sup>^superscript</sup></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph().AddSuperscript()
                .WithContent("^")// Escaped Char
                .WithContent("superscript")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "^^superscript\\^^^",
            "<p><sup>superscript^</sup></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph().AddSuperscript()
                .WithContent("superscript")
                .WithContent("^")// Escaped Char
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "This is a **bold^^superscript^^ text**.",
            "<p>This is a <strong>bold<sup>superscript</sup> text</strong>.</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("This is a ");
                IMarkdownSyntaxNode bold = paragraph.AddBold();
                bold.WithContent("bold");
                bold.AddSuperscript("superscript");
                bold.WithContent(" text");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Text with *italic^^superscript^^*.",
            "<p>Text with <em>italic<sup>superscript</sup></em>.</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Text with ");
                IMarkdownSyntaxNode italic = paragraph.AddItalic();
                italic.WithContent("italic");
                italic.AddSuperscript("superscript");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "A [link with ^^superscript^^](https://example.com).",
            """
            <p>A <a href="https://example.com">link with <sup>superscript</sup></a>.</p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("A ");
                IMarkdownSyntaxNode linkNode = paragraph.AddLink();
                linkNode.WithAttribute("href", "https://example.com");
                linkNode.WithContent("link with ");
                linkNode.AddSuperscript("superscript");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - **Bold^^sup^^**
            - *Italic^^sup^^*
            """,
            """
            <ul>
                <li><strong>Bold<sup>sup</sup></strong></li>
                <li><em>Italic<sup>sup</sup></em></li>
            </ul>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();
                IMarkdownSyntaxNode item1 = list.AddListItem();
                item1.AddBold()
                    .WithContent("Bold")
                    .AddSuperscript()
                    .WithContent("sup");

                IMarkdownSyntaxNode item2 = list.AddListItem();
                item2.AddItalic()
                    .WithContent("Italic")
                    .AddSuperscript()
                    .WithContent("sup");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Nested formatting: **Bold ^^superscript^^ in a [link](https://example.com)**.",
            """
            <p>Nested formatting: <strong>Bold <sup>superscript</sup> in a <a href="https://example.com">link</a></strong>.</p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Nested formatting: ");
                IMarkdownSyntaxNode bold = paragraph.AddBold();
                bold.WithContent("Bold ");
                bold.AddSuperscript("superscript");
                bold.WithContent(" in a ");
                IMarkdownSyntaxNode linkNode = bold.AddLink();
                linkNode.WithAttribute("href", "https://example.com");
                linkNode.WithContent("link");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Inline code with superscript: `x = y^^2^^`",
            "<p>Inline code with superscript: <code>x = y^^2^^</code></p>",
            ConfigureExpectedNode: static rootNode => rootNode.AddParagraph()
                .WithContent("Inline code with superscript: ")
                .AddCodeInline("x = y^^2^^")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Complex: ***Bold and italic^^super^^***.",
            "<p>Complex: <strong><em>Bold and italic<sup>super</sup></em></strong>.</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Complex: ");
                IMarkdownSyntaxNode boldItalic = paragraph.AddBold().AddItalic();
                boldItalic.WithContent("Bold and italic");
                boldItalic.AddSuperscript("super");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - **Bold link [with ^^superscript^^ text](https://example.com)**.
            - *Italic link [and ^^superscript^^ text](https://example.org)*.
            """,
            """
            <ul>
                <li><strong>Bold link <a href="https://example.com">with <sup>superscript</sup> text</a></strong>.</li>
                <li><em>Italic link <a href="https://example.org">and <sup>superscript</sup> text</a></em>.</li>
            </ul>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();

                IMarkdownSyntaxNode item1 = list.AddListItem();
                IMarkdownSyntaxNode bold = item1.AddBold();
                bold.WithContent("Bold link ");
                IMarkdownSyntaxNode linkNode1 = bold.AddLink();
                linkNode1.WithAttribute("href", "https://example.com");
                linkNode1.WithContent("with ");
                linkNode1.AddSuperscript("superscript");
                linkNode1.WithContent(" text");
                item1.WithContent(".");

                IMarkdownSyntaxNode item2 = list.AddListItem();
                IMarkdownSyntaxNode italic = item2.AddItalic();
                italic.WithContent("Italic link ");
                IMarkdownSyntaxNode linkNode2 = italic.AddLink();
                linkNode2.WithAttribute("href", "https://example.org");
                linkNode2.WithContent("and ");
                linkNode2.AddSuperscript("superscript");
                linkNode2.WithContent(" text");
                item2.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "^^superscript with ^subscript^ inside^^",
            "<p><sup>superscript with <sub>subscript</sub> inside</sup></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode super = paragraph.AddSuperscript();
                super.WithContent("superscript with ");
                super.AddSubscript("subscript");
                super.WithContent(" inside");
            }
        );
    }
}

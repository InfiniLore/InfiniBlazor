// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
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
            static rootNode => rootNode.AddParagraph().AddSuperscript().WithContent("superscript")
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "^^\\^superscript^^",
            "<p><sup>^superscript</sup></p>",
            static rootNode => rootNode.AddParagraph().AddSuperscript()
                .WithContent("^") // Escaped Char
                .WithContent("superscript")
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "^^superscript\\^^^",
            "<p><sup>superscript^</sup></p>",
            static rootNode => rootNode.AddParagraph().AddSuperscript()
                .WithContent("superscript")
                .WithContent("^") // Escaped Char
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "This is a **bold^^superscript^^ text**.",
            "<p>This is a <strong>bold<sup>superscript</sup> text</strong>.</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("This is a ");
                IMdNode bold = paragraph.AddBold();
                bold.WithContent("bold");
                bold.AddSuperscript().WithContent("superscript");
                bold.WithContent(" text");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Text with *italic^^superscript^^*.",
            "<p>Text with <em>italic<sup>superscript</sup></em>.</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Text with ");
                IMdNode italic = paragraph.AddItalic();
                italic.WithContent("italic");
                italic.AddSuperscript().WithContent("superscript");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "A [link with ^^superscript^^](https://example.com).",
            """
            <p>A <a href="https://example.com">link with <sup>superscript</sup></a>.</p>
            """,
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("A ");
                IMdNode linkNode = paragraph.AddLink();
                linkNode.WithAttribute("href", "https://example.com");
                linkNode.WithContent("link with ");
                linkNode.AddSuperscript().WithContent("superscript");
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
            static rootNode => {
                IMdNode list = rootNode.AddListUnordered();
                IMdNode item1 = list.AddListItem();
                item1.AddBold()
                    .WithContent("Bold")
                    .AddSuperscript()
                        .WithContent("sup");
                
                IMdNode item2 = list.AddListItem();
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
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Nested formatting: ");
                IMdNode bold = paragraph.AddBold();
                bold.WithContent("Bold ");
                bold.AddSuperscript().WithContent("superscript");
                bold.WithContent(" in a ");
                IMdNode linkNode = bold.AddLink();
                linkNode.WithAttribute("href", "https://example.com");
                linkNode.WithContent("link");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Inline code with superscript: `x = y^^2^^`",
            "<p>Inline code with superscript: <code>x = y^^2^^</code></p>",
            static rootNode => rootNode.AddParagraph()
                .WithContent("Inline code with superscript: ")
                .AddCode().WithContent("x = y^^2^^")
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Complex: ***Bold and italic^^super^^***.",
            "<p>Complex: <strong><em>Bold and italic<sup>super</sup></em></strong>.</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Complex: ");
                IMdNode boldItalic = paragraph.AddBold().AddItalic();
                boldItalic.WithContent("Bold and italic");
                boldItalic.AddSuperscript().WithContent("super");
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
            static rootNode => {
                IMdNode list = rootNode.AddListUnordered();
                
                IMdNode item1 = list.AddListItem();
                IMdNode bold = item1.AddBold();
                bold.WithContent("Bold link ");
                IMdNode linkNode1 = bold.AddLink();
                linkNode1.WithAttribute("href", "https://example.com");
                linkNode1.WithContent("with ");
                linkNode1.AddSuperscript().WithContent("superscript");
                linkNode1.WithContent(" text");
                item1.WithContent(".");
                
                IMdNode item2 = list.AddListItem();
                IMdNode italic = item2.AddItalic();
                italic.WithContent("Italic link ");
                IMdNode linkNode2 = italic.AddLink();
                linkNode2.WithAttribute("href", "https://example.org");
                linkNode2.WithContent("and ");
                linkNode2.AddSuperscript().WithContent("superscript");
                linkNode2.WithContent(" text");
                item2.WithContent(".");
            }
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "^^superscript with ^subscript^ inside^^",
            "<p><sup>superscript with <sub>subscript</sub> inside</sup></p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                IMdNode super = paragraph.AddSuperscript();
                super.WithContent("superscript with ");
                super.AddSubscript().WithContent("subscript");
                super.WithContent(" inside");
            }
        );
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class SubScriptDataSources {
    private static readonly string SectionName = nameof(SubScriptDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "^subscript^",
            "<p><sub>subscript</sub></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.AddSubscript("subscript");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "^subscript\\^^",
            "<p><sub>subscript^</sub></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.AddSubscript("subscript^");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "^\\^subscript^",
            "<p><sub>^subscript</sub></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.AddSubscript("^subscript");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "This is a **bold^subscript^ text**.",
            "<p>This is a <strong>bold<sub>subscript</sub> text</strong>.</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("This is a ");
                IMarkdownSyntaxNode bold = paragraph.AddBold("bold");
                bold.AddSubscript("subscript");
                bold.WithContent(" text");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Text with *italic^subscript^ and ^^superscript^^*.",
            "<p>Text with <em>italic<sub>subscript</sub> and <sup>superscript</sup></em>.</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Text with ");
                IMarkdownSyntaxNode italic = paragraph.AddItalic("italic");
                italic.AddSubscript("subscript");
                italic.WithContent(" and ");
                IMarkdownSyntaxNode sup = italic.AddSuperscript();
                sup.WithContent("superscript");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "A [link with ^subscript^](https://example.com).",
            """
            <p>A <a href="https://example.com">link with <sub>subscript</sub></a>.</p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("A ");
                IMarkdownSyntaxNode link = paragraph.AddLink();
                link.WithAttribute("href", "https://example.com");
                link.WithContent("link with ");
                link.AddSubscript("subscript");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - **Bold^sub^**
            - *Italic^sub^*
            """,
            """
            <ul>
                <li><strong>Bold<sub>sub</sub></strong></li>
                <li><em>Italic<sub>sub</sub></em></li>
            </ul>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();

                IMarkdownSyntaxNode item0 = list.AddListItem();
                item0.AddBold("Bold").AddSubscript("sub");

                IMarkdownSyntaxNode item1 = list.AddListItem();
                item1.AddItalic("Italic").AddSubscript("sub");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "**Bold ^subscript^ in a [link](https://example.com)**.",
            """
            <p><strong>Bold <sub>subscript</sub> in a <a href="https://example.com">link</a></strong>.</p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode bold = paragraph.AddBold("Bold ");
                bold.AddSubscript("subscript");
                bold.WithContent(" in a ");
                IMarkdownSyntaxNode link = bold.AddLink();
                link.WithAttribute("href", "https://example.com");
                link.WithContent("link");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Inline code with subscript: `x = z^2^`",
            "<p>Inline code with subscript: <code>x = z^2^</code></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Inline code with subscript: ");
                paragraph.AddCodeInline("x = z^2^");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "***Bold and italic^sub^ and italic^sub^***.",
            "<p><strong><em>Bold and italic<sub>sub</sub> and italic<sub>sub</sub></em></strong>.</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode bold = paragraph.AddBold().AddItalic();
                bold.WithContent("Bold and italic");
                bold.AddSubscript("sub");
                bold.WithContent(" and italic");
                bold.AddSubscript("sub");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - **Bold link [with ^sub^ text](https://example.com)**.
            - *Italic link [and ^sub^ text](https://example.org)*.
            """,
            """
            <ul>
                <li><strong>Bold link <a href="https://example.com">with <sub>sub</sub> text</a></strong>.</li>
                <li><em>Italic link <a href="https://example.org">and <sub>sub</sub> text</a></em>.</li>
            </ul>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();
                IMarkdownSyntaxNode item1 = list.AddListItem();
                IMarkdownSyntaxNode bold = item1.AddBold();
                bold.WithContent("Bold link ");
                IMarkdownSyntaxNode link = bold.AddLink();
                link.WithAttribute("href", "https://example.com");
                link.WithContent("with ");
                link.AddSubscript("sub");
                link.WithContent(" text");
                item1.WithContent(".");

                IMarkdownSyntaxNode item2 = list.AddListItem();
                IMarkdownSyntaxNode italic = item2.AddItalic();
                italic.WithContent("Italic link ");
                IMarkdownSyntaxNode link2 = italic.AddLink();
                link2.WithAttribute("href", "https://example.org");
                link2.WithContent("and ");
                link2.AddSubscript("sub");
                link2.WithContent(" text");
                item2.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "^subscript with ^^superscript^^ inside^",
            "<p><sub>subscript with <sup>superscript</sup> inside</sub></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                IMarkdownSyntaxNode sub = paragraph.AddSubscript("subscript with ");
                sub.AddSuperscript("superscript");
                sub.WithContent(" inside");
            }
        );
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class SubAndSuperScriptDataSources {
    private static readonly string SectionName = nameof(SubAndSuperScriptDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            "Example of ^^super^^ and ^sub^ and ^^^super sub^^^.",
            "<p>Example of <sup>super</sup> and <sub>sub</sub> and <sup><sub>super sub</sub></sup>.</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Example of ");
                paragraph.AddSuperscript("super");
                paragraph.WithContent(" and ");
                paragraph.AddSubscript("sub");
                paragraph.WithContent(" and ");
                paragraph.AddSuperscript().AddSubscript("super sub");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "A [link with ^^superscript^^ and ^subscript^](https://example.com).",
            """
            <p>A <a href="https://example.com">link with <sup>superscript</sup> and <sub>subscript</sub></a>.</p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("A ");

                IMarkdownSyntaxNode link = paragraph.AddLink();
                link.WithAttribute(MarkdownAttribute.LinkHref, "https://example.com");
                link.WithContent("link with ");
                link.AddSuperscript("superscript");
                link.WithContent(" and ");
                link.AddSubscript("subscript");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - **Bold^^sup^^**
            - *Italic^sub^*
            """,
            """
            <ul>
                <li><strong>Bold<sup>sup</sup></strong></li>
                <li><em>Italic<sub>sub</sub></em></li>
            </ul>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();
                IMarkdownSyntaxNode item0 = list.AddListItem();
                item0.AddBold("Bold").AddSuperscript("sup");

                IMarkdownSyntaxNode item1 = list.AddListItem();
                item1.AddItalic("Italic").AddSubscript("sub");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Nested formatting: **Bold ^subscript^ and ^^superscript^^ in a [link](https://example.com)**.",
            """
            <p>Nested formatting: <strong>Bold <sub>subscript</sub> and <sup>superscript</sup> in a <a href="https://example.com">link</a></strong>.</p>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Nested formatting: ");
                IMarkdownSyntaxNode bold = paragraph.AddBold();
                bold.WithContent("Bold ");
                bold.AddSubscript("subscript");
                bold.WithContent(" and ");
                bold.AddSuperscript("superscript");
                bold.WithContent(" in a ");
                IMarkdownSyntaxNode link = bold.AddLink();
                link.WithAttribute(MarkdownAttribute.LinkHref, "https://example.com");
                link.WithContent("link");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Inline code with superscript and subscript: `x = y^^2^^ - z^2^`",
            "<p>Inline code with superscript and subscript: <code>x = y^^2^^ - z^2^</code></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Inline code with superscript and subscript: ");
                paragraph.AddCodeInline("x = y^^2^^ - z^2^");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Complex: ***Bold and italic^^super^^ and italic^sub^***.",
            "<p>Complex: <strong><em>Bold and italic<sup>super</sup> and italic<sub>sub</sub></em></strong>.</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Complex: ");
                IMarkdownSyntaxNode bold = paragraph.AddBold().AddItalic();
                bold.WithContent("Bold and italic");
                bold.AddSuperscript("super");
                bold.WithContent(" and italic");
                bold.AddSubscript("sub");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            - **Bold link [with ^^superscript^^ text](https://example.com)**.
            - *Italic link [and subscript ^sub^ text](https://example.org)*.
            """,
            """
            <ul>
                <li><strong>Bold link <a href="https://example.com">with <sup>superscript</sup> text</a></strong>.</li>
                <li><em>Italic link <a href="https://example.org">and subscript <sub>sub</sub> text</a></em>.</li>
            </ul>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode list = rootNode.AddListUnordered();
                IMarkdownSyntaxNode item1 = list.AddListItem();
                IMarkdownSyntaxNode bold = item1.AddBold();
                bold.WithContent("Bold link ");
                IMarkdownSyntaxNode link = bold.AddLink();
                link.WithAttribute(MarkdownAttribute.LinkHref, "https://example.com");
                link.WithContent("with ");
                link.AddSuperscript("superscript");
                link.WithContent(" text");
                item1.WithContent(".");

                IMarkdownSyntaxNode item2 = list.AddListItem();
                IMarkdownSyntaxNode italic = item2.AddItalic();
                italic.WithContent("Italic link ");
                IMarkdownSyntaxNode link2 = italic.AddLink();
                link2.WithAttribute(MarkdownAttribute.LinkHref, "https://example.org");
                link2.WithContent("and subscript ");
                link2.AddSubscript("sub");
                link2.WithContent(" text");
                item2.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Complex nesting: **Bold ^subscript ^^with superscript^^ inside^ and ^^superscript ^with subscript^ inside^^**",
            "<p>Complex nesting: <strong>Bold <sub>subscript <sup>with superscript</sup> inside</sub> and <sup>superscript <sub>with subscript</sub> inside</sup></strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Complex nesting: ");
                IMarkdownSyntaxNode bold = paragraph.AddBold();
                bold.WithContent("Bold ");
                IMarkdownSyntaxNode sub = bold.AddSubscript("subscript ");
                sub.AddSuperscript("with superscript");
                sub.WithContent(" inside");
                bold.WithContent(" and ");
                IMarkdownSyntaxNode super = bold.AddSuperscript("superscript ");
                super.AddSubscript("with subscript");
                super.WithContent(" inside");
            }
        );
    }
}

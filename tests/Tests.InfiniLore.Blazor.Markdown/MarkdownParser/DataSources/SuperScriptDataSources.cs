// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.Blazor.Markdown.MarkdownParser.DataSources;
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
            "<p><sup>superscript</sup></p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "^^\\^superscript^^",
            "<p><sup>^superscript</sup></p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "^^superscript\\^^^",
            "<p><sup>superscript^</sup></p>"
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "This is a **bold^^superscript^^ text**.",
            "<p>This is a <strong>bold<sup>superscript</sup> text</strong>.</p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Text with *italic^^superscript^^*.",
            "<p>Text with <em>italic<sup>superscript</sup></em>.</p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "A [link with ^^superscript^^](https://example.com).",
            """
            <p>A <a href="https://example.com">link with <sup>superscript</sup></a>.</p>
            """
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
            """
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Nested formatting: **Bold ^^superscript^^ in a [link](https://example.com)**.",
            """
            <p>Nested formatting: <strong>Bold <sup>superscript</sup> in a <a href="https://example.com">link</a></strong>.</p>
            """
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Inline code with superscript: `x = y^^2^^`",
            "<p>Inline code with superscript: <code>x = y^^2^^</code></p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Complex: ***Bold and italic^^super^^***.",
            "<p>Complex: <strong><em>Bold and italic<sup>super</sup></em></strong>.</p>"
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
            """
        );
        
        yield return static () => new MarkdownTestDto(SectionName,
            "^^superscript with ^subscript^ inside^^",
            "<p><sup>superscript with <sub>subscript</sub> inside</sup></p>"
        );
    }
}

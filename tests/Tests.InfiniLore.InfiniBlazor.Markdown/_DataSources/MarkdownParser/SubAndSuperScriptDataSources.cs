// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown._DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class SubAndSuperScriptDataSources {
    private static readonly string SectionName = nameof(SubAndSuperScriptDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        yield return static () => new MdTestData(SectionName,
            "Example of ^^super^^ and ^sub^ and ^^^super sub^^^.",
            "<p>Example of <sup>super</sup> and <sub>sub</sub> and <sup><sub>super sub</sub></sup>.</p>"
        );

        yield return static () => new MdTestData(SectionName,
            "A [link with ^^superscript^^ and ^subscript^](https://example.com).",
            """
            <p>A <a href="https://example.com">link with <sup>superscript</sup> and <sub>subscript</sub></a>.</p>
            """
        );

        yield return static () => new MdTestData(SectionName,
            """
            - **Bold^^sup^^**
            - *Italic^sub^*
            """,
            """
            <ul>
                <li><strong>Bold<sup>sup</sup></strong></li>
                <li><em>Italic<sub>sub</sub></em></li>
            </ul>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "Nested formatting: **Bold ^subscript^ and ^^superscript^^ in a [link](https://example.com)**.",
            """
            <p>Nested formatting: <strong>Bold <sub>subscript</sub> and <sup>superscript</sup> in a <a href="https://example.com">link</a></strong>.</p>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "Inline code with superscript and subscript: `x = y^^2^^ - z^2^`",
            "<p>Inline code with superscript and subscript: <code>x = y^^2^^ - z^2^</code></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "Complex: ***Bold and italic^^super^^ and italic^sub^***.",
            "<p>Complex: <strong><em>Bold and italic<sup>super</sup> and italic<sub>sub</sub></em></strong>.</p>"
        );

        yield return static () => new MdTestData(SectionName,
            """
            - **Bold link [with ^^superscript^^ text](https://example.com)**.
            - *Italic link [and subscript ^sub^ text](https://example.org)*.
            """,
            """
            <ul>
                <li><strong>Bold link <a href="https://example.com">with <sup>superscript</sup> text</a></strong>.</li>
                <li><em>Italic link <a href="https://example.org">and subscript <sub>sub</sub> text</a></em>.</li>
            </ul>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "Complex nesting: **Bold ^subscript ^^with superscript^^ inside^ and ^^superscript ^with subscript^ inside^^**",
            "<p>Complex nesting: <strong>Bold <sub>subscript <sup>with superscript</sup> inside</sub> and <sup>superscript <sub>with subscript</sub> inside</sup></strong></p>"
        );
    }
}

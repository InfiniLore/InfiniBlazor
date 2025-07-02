// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class SubScriptDataSources {
    private static readonly string SectionName = nameof(SubScriptDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        yield return static () => new MdTestData(SectionName,
            "^subscript^",
            "<p><sub>subscript</sub></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "^subscript\\^^",
            "<p><sub>subscript^</sub></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "^\\^subscript^",
            "<p><sub>^subscript</sub></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "This is a **bold^subscript^ text**.",
            "<p>This is a <strong>bold<sub>subscript</sub> text</strong>.</p>"
        );

        yield return static () => new MdTestData(SectionName,
            "Text with *italic^subscript^ and ^^superscript^^*.",
            "<p>Text with <em>italic<sub>subscript</sub> and <sup>superscript</sup></em>.</p>"
        );

        yield return static () => new MdTestData(SectionName,
            "A [link with ^subscript^](https://example.com).",
            """
            <p>A <a href="https://example.com">link with <sub>subscript</sub></a>.</p>
            """
        );

        yield return static () => new MdTestData(SectionName,
            """
            - **Bold^sub^**
            - *Italic^sub^*
            """,
            """
            <ul>
                <li><strong>Bold<sub>sub</sub></strong></li>
                <li><em>Italic<sub>sub</sub></em></li>
            </ul>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "**Bold ^subscript^ in a [link](https://example.com)**.",
            """
            <p><strong>Bold <sub>subscript</sub> in a <a href="https://example.com">link</a></strong>.</p>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "Inline code with subscript: `x = z^2^`",
            "<p>Inline code with subscript: <code>x = z^2^</code></p>"
        );

        yield return static () => new MdTestData(SectionName,
            "***Bold and italic^sub^ and italic^sub^***.",
            "<p><strong><em>Bold and italic<sub>sub</sub> and italic<sub>sub</sub></em></strong>.</p>"
        );

        yield return static () => new MdTestData(SectionName,
            """
            - **Bold link [with ^sub^ text](https://example.com)**.
            - *Italic link [and ^sub^ text](https://example.org)*.
            """,
            """
            <ul>
                <li><strong>Bold link <a href="https://example.com">with <sub>sub</sub> text</a></strong>.</li>
                <li><em>Italic link <a href="https://example.org">and <sub>sub</sub> text</a></em>.</li>
            </ul>
            """
        );

        yield return static () => new MdTestData(SectionName,
            "^subscript with ^^superscript^^ inside^",
            "<p><sub>subscript with <sup>superscript</sup> inside</sub></p>"
        );
    }
}

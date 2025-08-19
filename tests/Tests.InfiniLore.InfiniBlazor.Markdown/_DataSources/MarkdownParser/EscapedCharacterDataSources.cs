// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown._DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class EscapedCharacterDataSources {
    private static readonly string SectionName = nameof(EscapedCharacterDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {

        yield return static () => new MdTestData(SectionName,
            @"\*literal asterisks\*",
            "<p>*literal asterisks*</p>"
        );

        yield return static () => new MdTestData(SectionName,
            """
            \"She told me that \'he isn't here right *now*\' - so I left.\"
            """,
            """<p>"She told me that 'he isn't here right <em>now</em>' - so I left."</p>"""
        );

        yield return static () => new MdTestData(SectionName,
            @"Escape characters like backticks: \`code\`",
            "<p>Escape characters like backticks: `code`</p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"A backslash at the end of a line: This is a line\\",
            "<p>A backslash at the end of a line: This is a line\\</p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"Escaping underscores:\_This is not italicized\_",
            "<p>Escaping underscores:_This is not italicized_</p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"Double escape: \\*this is not emphasized\\*",
            @"<p>Double escape: \*this is not emphasized\*</p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"Handling escaped square brackets: \[link text\]\(https://example.com\)",
            "<p>Handling escaped square brackets: [link text](https://example.com)</p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"Inline code with escaped backticks: ``code with \`escaped backticks\```",
            "<p>Inline code with escaped backticks: <code>code with `escaped backticks`</code></p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"Asterisks visibly escaped: \*\*not bold\*\*",
            "<p>Asterisks visibly escaped: **not bold**</p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"Testing emphasis escape: \*not italic\* and also \*\*not bold\*\*",
            "<p>Testing emphasis escape: *not italic* and also **not bold**</p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"Partially escaped inline formatting: *italic \*escaped\** and **bold**",
            "<p>Partially escaped inline formatting: <em>italic *escaped*</em> and <strong>bold</strong></p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"Inline code containing emphasis: `\*code with emphasis\*`",
            @"<p>Inline code containing emphasis: <code>\*code with emphasis\*</code></p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"Trying to confuse the parser: \``code\`` and backslashes \\\\\\\`",
            @"<p>Trying to confuse the parser: `<code>code`</code> and backslashes \\\`</p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"Escaping pipes in tables: \| column 1 \| column 2 \|",
            "<p>Escaping pipes in tables: | column 1 | column 2 |</p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"Double escaping \\* and then *italicize*",
            "<p>Double escaping \\<em> and then </em>italicize*</p>"
        );

        yield return static () => new MdTestData(SectionName,
            @"\!\""\#\$\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^\_\`\{\|\}\~",
            @"<p>!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~</p>"
        );
    }
}

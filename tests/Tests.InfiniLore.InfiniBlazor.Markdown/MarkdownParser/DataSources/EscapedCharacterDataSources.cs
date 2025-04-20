// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class EscapedCharacterDataSources {
    private static readonly string SectionName = nameof(EscapedCharacterDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {

        yield return static () => new MarkdownTestDto(SectionName,
            @"\*literal asterisks\*",
            "<p>*literal asterisks*</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("*"); // Escaped char
                paragraph.WithContent("literal asterisks");
                paragraph.WithContent("*"); // Escaped char
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"\!\""\#\$\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^\_\`\{\|\}\~",
            "<p>!&quot;#$%&amp;'()*+,-./:;&lt;=&gt;?@[\\]^_`{|}~</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("!"); // Escaped char
                paragraph.WithContent("&quot;"); // Escaped char
                paragraph.WithContent("#"); // Escaped char
                paragraph.WithContent("$"); // Escaped char
                paragraph.WithContent("%"); // Escaped char
                paragraph.WithContent("&amp;"); // Escaped char
                paragraph.WithContent("'"); // Escaped char
                paragraph.WithContent("("); // Escaped char
                paragraph.WithContent(")"); // Escaped char
                paragraph.WithContent("*"); // Escaped char
                paragraph.WithContent("+"); // Escaped char
                paragraph.WithContent(","); // Escaped char
                paragraph.WithContent("-"); // Escaped char
                paragraph.WithContent("."); // Escaped char
                paragraph.WithContent("/"); // Escaped char
                paragraph.WithContent(":"); // Escaped char
                paragraph.WithContent(";"); // Escaped char
                paragraph.WithContent("&lt;"); // Escaped char
                paragraph.WithContent("="); // Escaped char
                paragraph.WithContent("&gt;"); // Escaped char
                paragraph.WithContent("?"); // Escaped char
                paragraph.WithContent("@"); // Escaped char
                paragraph.WithContent("["); // Escaped char
                paragraph.WithContent("\\"); // Escaped char
                paragraph.WithContent("]"); // Escaped char
                paragraph.WithContent("^"); // Escaped char
                paragraph.WithContent("_"); // Escaped char
                paragraph.WithContent("`"); // Escaped char
                paragraph.WithContent("{"); // Escaped char
                paragraph.WithContent("|"); // Escaped char
                paragraph.WithContent("}"); // Escaped char
                paragraph.WithContent("~"); // Escaped char
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            \"She told me that \'he isn't here right *now*\' - so I left.\"
            """,
            "<p>&quot;She told me that 'he isn't here right <em>now</em>' - so I left.&quot;</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("&quot;"); // Escaped char
                paragraph.WithContent("She told me that ");
                paragraph.WithContent("'"); // Escaped char")
                paragraph.WithContent("he isn't here right ");
                paragraph.AddItalic().WithContent("now"); 
                paragraph.WithContent("'"); // Escaped char
                paragraph.WithContent(" - so I left.");
                paragraph.WithContent("&quot;"); // Escaped char
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Escape characters like backticks: \`code\`",
            "<p>Escape characters like backticks: `code`</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Escape characters like backticks: ");
                paragraph.WithContent("`"); // Escaped char
                paragraph.WithContent("code");
                paragraph.WithContent("`"); // Escaped char
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"A backslash at the end of a line: This is a line\\",
            "<p>A backslash at the end of a line: This is a line\\</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("A backslash at the end of a line: This is a line");
                paragraph.WithContent("\\"); // Escaped char
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Escaping underscores:\_This is not italicized\_",
            "<p>Escaping underscores:_This is not italicized_</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Escaping underscores:");
                paragraph.WithContent("_"); // Escaped char
                paragraph.WithContent("This is not italicized");
                paragraph.WithContent("_"); // Escaped char
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Double escape: \\*this is not emphasized\\*",
            @"<p>Double escape: \*this is not emphasized\*</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Double escape: ");
                paragraph.WithContent("\\"); // Escaped char
                paragraph.WithContent("*this is not emphasized");
                paragraph.WithContent("\\"); // Escaped char
                paragraph.WithContent("*");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Handling escaped square brackets: \[link text\]\(https://example.com\)",
            "<p>Handling escaped square brackets: [link text](https://example.com)</p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Inline code with escaped backticks: ``code with \`escaped backticks\```",
            "<p>Inline code with escaped backticks: <code>code with `escaped backticks`</code></p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Asterisks visibly escaped: \*\*not bold\*\*",
            "<p>Asterisks visibly escaped: **not bold**</p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Testing emphasis escape: \*not italic\* and also \*\*not bold\*\*",
            "<p>Testing emphasis escape: *not italic* and also **not bold**</p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Partially escaped inline formatting: *italic \*escaped\** and **bold**",
            "<p>Partially escaped inline formatting: <em>italic *escaped*</em> and <strong>bold</strong></p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Inline code containing emphasis: `\*code with emphasis\*`",
            @"<p>Inline code containing emphasis: <code>\*code with emphasis\*</code></p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Trying to confuse the parser: \``code\`` and backslashes \\\\\\\`",
            @"<p>Trying to confuse the parser: `<code>code`</code> and backslashes \\\`</p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Escaping pipes in tables: \| column 1 \| column 2 \|",
            "<p>Escaping pipes in tables: | column 1 | column 2 |</p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Double escaping \\* and then *italicize*",
            "<p>Double escaping \\<em> and then </em>italicize*</p>"
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"\!\""\#\$\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^\_\`\{\|\}\~",
            @"<p>!&quot;#$%&amp;'()*+,-./:;&lt;=&gt;?@[\]^_`{|}~</p>"
        );
    }
}

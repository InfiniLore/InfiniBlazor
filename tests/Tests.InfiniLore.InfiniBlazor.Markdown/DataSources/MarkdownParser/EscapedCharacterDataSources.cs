// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;
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
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("*literal asterisks*");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            """
            \"She told me that \'he isn't here right *now*\' - so I left.\"
            """,
            """<p>"She told me that 'he isn't here right <em>now</em>' - so I left."</p>""",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("\"She told me that 'he isn't here right ");
                paragraph.AddItalic("now");
                paragraph.WithContent("' - so I left.\"");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Escape characters like backticks: \`code\`",
            "<p>Escape characters like backticks: `code`</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Escape characters like backticks: `code`");// Escaped char
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"A backslash at the end of a line: This is a line\\",
            "<p>A backslash at the end of a line: This is a line\\</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("A backslash at the end of a line: This is a line\\");// Escaped char
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Escaping underscores:\_This is not italicized\_",
            "<p>Escaping underscores:_This is not italicized_</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Escaping underscores:_This is not italicized_");// Escaped char
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Double escape: \\*this is not emphasized\\*",
            @"<p>Double escape: \*this is not emphasized\*</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent(@"Double escape: \*this is not emphasized\*");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Handling escaped square brackets: \[link text\]\(https://example.com\)",
            "<p>Handling escaped square brackets: [link text](https://example.com)</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Handling escaped square brackets: [link text](https://example.com)");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Inline code with escaped backticks: ``code with \`escaped backticks\```",
            "<p>Inline code with escaped backticks: <code>code with `escaped backticks`</code></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Inline code with escaped backticks: ");
                IMarkdownSyntaxNode code = paragraph.AddCodeInline();
                code.WithContent("code with `escaped backticks`");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Asterisks visibly escaped: \*\*not bold\*\*",
            "<p>Asterisks visibly escaped: **not bold**</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Asterisks visibly escaped: **not bold**");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Testing emphasis escape: \*not italic\* and also \*\*not bold\*\*",
            "<p>Testing emphasis escape: *not italic* and also **not bold**</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Testing emphasis escape: *not italic* and also **not bold**");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Partially escaped inline formatting: *italic \*escaped\** and **bold**",
            "<p>Partially escaped inline formatting: <em>italic *escaped*</em> and <strong>bold</strong></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Partially escaped inline formatting: ");
                IMarkdownSyntaxNode italic = paragraph.AddItalic();
                italic.WithContent("italic *escaped*");
                paragraph.WithContent(" and ");
                IMarkdownSyntaxNode bold = paragraph.AddBold();
                bold.WithContent("bold");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Inline code containing emphasis: `\*code with emphasis\*`",
            @"<p>Inline code containing emphasis: <code>\*code with emphasis\*</code></p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Inline code containing emphasis: ");
                IMarkdownSyntaxNode code = paragraph.AddCodeInline();
                code.WithContent(@"\*code with emphasis\*");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Trying to confuse the parser: \``code\`` and backslashes \\\\\\\`",
            @"<p>Trying to confuse the parser: `<code>code`</code> and backslashes \\\`</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Trying to confuse the parser: `");
                IMarkdownSyntaxNode code = paragraph.AddCodeInline();
                code.WithContent("code`");
                paragraph.WithContent(@" and backslashes \\\`");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Escaping pipes in tables: \| column 1 \| column 2 \|",
            "<p>Escaping pipes in tables: | column 1 | column 2 |</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Escaping pipes in tables: | column 1 | column 2 |");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"Double escaping \\* and then *italicize*",
            "<p>Double escaping \\<em> and then </em>italicize*</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent(@"Double escaping \");
                paragraph.AddItalic(" and then ");
                paragraph.WithContent("italicize*");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            @"\!\""\#\$\%\&\'\(\)\*\+\,\-\.\/\:\;\<\=\>\?\@\[\\\]\^\_\`\{\|\}\~",
            @"<p>!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~</p>",
            ConfigureExpectedNode: static rootNode => {
                IMarkdownSyntaxNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent(@"!""#$%&'()*+,-./:;<=>?@[\]^_`{|}~");// Escaped char
            }
        );
    }
}

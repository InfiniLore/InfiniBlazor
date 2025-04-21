// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CodeInlineDataSources {
    private static readonly string SectionName = nameof(CodeInlineDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {

        yield return static () => new MarkdownTestDto(SectionName,
            "This is an `example` of some inline code.",
            "<p>This is an <code>example</code> of some inline code.</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("This is an ");
                paragraph.AddCode("example");
                paragraph.WithContent(" of some inline code.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "`\\``",
            "<p><code>`</code></p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.AddCode("`");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Here is some `inline code` inside a sentence.",
            "<p>Here is some <code>inline code</code> inside a sentence.</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Here is some ");
                paragraph.AddCode("inline code");
                paragraph.WithContent(" inside a sentence.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "`code` at the start of the line.",
            "<p><code>code</code> at the start of the line.</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.AddCode("code");
                paragraph.WithContent(" at the start of the line.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "This is the `last example`.",
            "<p>This is the <code>last example</code>.</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("This is the ");
                paragraph.AddCode("last example");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Multiple `inline` `code` segments.",
            "<p>Multiple <code>inline</code> <code>code</code> segments.</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Multiple ");
                paragraph.AddCode("inline");
                paragraph.WithContent(" ");
                paragraph.AddCode("code");
                paragraph.WithContent(" segments.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Backticks inside inline code: ``Code with `backticks` inside``.",
            "<p>Backticks inside inline code: <code>Code with `backticks` inside</code>.</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Backticks inside inline code: ");
                paragraph.AddCode("Code with `backticks` inside");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "`Nested ` is not valid syntax.`",
            "<p><code>Nested </code> is not valid syntax.`</p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.AddCode("Nested ");
                paragraph.WithContent(" is not valid syntax.`");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "`inline code with special characters !@#$%^&*()`",
            "<p><code>inline code with special characters !@#$%^&amp;*()</code></p>",
            static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.AddCode("inline code with special characters !@#$%^&amp;*()");
            }
        );
    }
}

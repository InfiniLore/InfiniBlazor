// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;
using Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
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
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("This is an ");
                paragraph.AddCodeInline("example");
                paragraph.WithContent(" of some inline code.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "`\\``",
            "<p><code>`</code></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.AddCodeInline("`");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Here is some `inline code` inside a sentence.",
            "<p>Here is some <code>inline code</code> inside a sentence.</p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Here is some ");
                paragraph.AddCodeInline("inline code");
                paragraph.WithContent(" inside a sentence.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "`code` at the start of the line.",
            "<p><code>code</code> at the start of the line.</p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.AddCodeInline("code");
                paragraph.WithContent(" at the start of the line.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "This is the `last example`.",
            "<p>This is the <code>last example</code>.</p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("This is the ");
                paragraph.AddCodeInline("last example");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Multiple `inline` `code` segments.",
            "<p>Multiple <code>inline</code> <code>code</code> segments.</p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Multiple ");
                paragraph.AddCodeInline("inline");
                paragraph.WithContent(" ");
                paragraph.AddCodeInline("code");
                paragraph.WithContent(" segments.");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "Backticks inside inline code: ``Code with `backticks` inside``.",
            "<p>Backticks inside inline code: <code>Code with `backticks` inside</code>.</p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.WithContent("Backticks inside inline code: ");
                paragraph.AddCodeInline("Code with `backticks` inside");
                paragraph.WithContent(".");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "`Nested ` is not valid syntax.`",
            "<p><code>Nested </code> is not valid syntax.`</p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.AddCodeInline("Nested ");
                paragraph.WithContent(" is not valid syntax.`");
            }
        );

        yield return static () => new MarkdownTestDto(SectionName,
            "`inline code with special characters !@#$%^&*()`",
            "<p><code>inline code with special characters !@#$%^&*()</code></p>",
            ConfigureExpectedNode: static rootNode => {
                IMdNode paragraph = rootNode.AddParagraph();
                paragraph.AddCodeInline("inline code with special characters !@#$%^&*()");
            }
        );
    }
}

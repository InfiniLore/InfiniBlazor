// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CodeInlineDataSources {
    private static readonly string SectionName = nameof(CodeInlineDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<OldMdTestData>> DataSources() {

        // yield return static () => new OldMdTestData(SectionName,
        //     "This is an `example` of some inline code.",
        //     "<p>This is an <code>example</code> of some inline code.</p>"
        // );
        //
        // yield return static () => new OldMdTestData(SectionName,
        //     "`\\``",
        //     "<p><code>`</code></p>"
        // );

        yield return static () => new OldMdTestData(SectionName,
            "`code` at the start of the line.",
            "<p><code>code</code> at the start of the line.</p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "This is the `last example`.",
            "<p>This is the <code>last example</code>.</p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "Multiple `inline` `code` segments.",
            "<p>Multiple <code>inline</code> <code>code</code> segments.</p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "Backticks inside inline code: ``Code with `backticks` inside``.",
            "<p>Backticks inside inline code: <code>Code with `backticks` inside</code>.</p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "`Nested ` is not valid syntax.`",
            "<p><code>Nested </code> is not valid syntax.`</p>"
        );

        yield return static () => new OldMdTestData(SectionName,
            "`inline code with special characters !@#$%^&*()`",
            "<p><code>inline code with special characters !@#$%^&*()</code></p>"
        );
    }
}

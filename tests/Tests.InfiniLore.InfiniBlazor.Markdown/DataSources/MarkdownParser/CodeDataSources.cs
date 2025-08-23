// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CodeDataSources {
    private static readonly string SectionName = nameof(CodeDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<OldMdTestData>> DataSources() {
        yield return static () => new OldMdTestData(SectionName,
            """
            ```
            const code = sample();
            ```
            """,
            """
            <pre><code>
            const code = sample();
            </code></pre>
            """);

        yield return static () => new OldMdTestData(SectionName,
            """
            ```javascript
            const code = sample();
            ```
            """,
            """
            <pre><code class="language-javascript">
            const code = sample();
            </code></pre>
            """);

        yield return static () => new OldMdTestData(SectionName,
            """
            ```
            tell application "Foo"
                beep
            end tell
            ```
            """,
            """
            <pre><code>
            tell application "Foo"
                beep
            end tell
            </code></pre>
            """);

        yield return static () => new OldMdTestData(SectionName,
            """
            ```
            **some valid markdown**
            ```
            """,
            """
            <pre><code>
            **some valid markdown**
            </code></pre>
            """);
    }
}

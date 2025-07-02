// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CodeDataSources {
    private static readonly string SectionName = nameof(CodeDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MdTestData>> DataSources() {
        yield return static () => new MdTestData(SectionName,
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

        yield return static () => new MdTestData(SectionName,
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

        yield return static () => new MdTestData(SectionName,
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

        yield return static () => new MdTestData(SectionName,
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

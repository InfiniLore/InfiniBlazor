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
public static class CodeDataSources {
    private static readonly string SectionName = nameof(CodeDataSources)[..^nameof(DataSources).Length];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<MarkdownTestDto>> DataSources() {
        yield return static () => new MarkdownTestDto(SectionName,
            """
            ```
            const code = sample();
            ```
            """,
            """
            <pre><code>
            const code = sample();
            </code></pre>
            """,
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddCodeBlock("const code = sample();");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            """
            ```javascript
            const code = sample();
            ```
            """,
            """
            <pre><code class="language-javascript">
            const code = sample();
            </code></pre>
            """,
            ConfigureExpectedNode: static rootNode => {
                IMdNode code = rootNode.AddCodeBlock("const code = sample();");
                code.WithClass("language-javascript");
            });

        yield return static () => new MarkdownTestDto(SectionName,
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
            """,
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddCodeBlock("tell application \"Foo\"\n    beep\nend tell");
            });

        yield return static () => new MarkdownTestDto(SectionName,
            """
            ```
            **some valid markdown**
            ```
            """,
            """
            <pre><code>
            **some valid markdown**
            </code></pre>
            """,
            ConfigureExpectedNode: static rootNode => {
                rootNode.AddCodeBlock("**some valid markdown**");
            });
    }
}

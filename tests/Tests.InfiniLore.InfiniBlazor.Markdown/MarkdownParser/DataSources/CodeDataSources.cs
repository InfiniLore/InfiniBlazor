// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser.DataSources;
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
            static rootNode => {
                IMdNode pre = rootNode.AddPre();
                pre.AddCode("const code = sample();\n");
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
            static rootNode => {
                IMdNode pre = rootNode.AddPre();
                IMdNode code = pre.AddCode("const code = sample();\n");
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
            static rootNode => {
                IMdNode pre = rootNode.AddPre();
                pre.AddCode("tell application \"Foo\"\n    beep\nend tell\n");
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
            static rootNode => {
                IMdNode pre = rootNode.AddPre();
                pre.AddCode("**some valid markdown**\n");
            });
    }
}

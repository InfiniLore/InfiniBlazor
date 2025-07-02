// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.DataSources.MarkdownParser;
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
                IMarkdownSyntaxNode code = rootNode.AddCodeBlock("const code = sample();");
                code.WithAttribute(MarkdownAttribute.CodeLanguage, "javascript");
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

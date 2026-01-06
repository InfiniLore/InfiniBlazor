// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using InfiniBlazor.Markdown;
using InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.DependencyInjection;

namespace Benchmarks.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.Declared)]
public class IndividualMarkdownBenchmarks {
    private IMarkdownParser Parser { get; set; } = null!;

    [GlobalSetup]
    public void Setup() {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddSingleton<Microsoft.AspNetCore.Components.NavigationManager, MockNavigationManager>();
        serviceCollection.AddSingleton<Microsoft.JSInterop.IJSRuntime, MockJsRuntime>();
        serviceCollection.AddInfiniBlazor();
        serviceCollection.AddLogging();
        ServiceProvider provider = serviceCollection.BuildServiceProvider();

        Parser = provider.GetRequiredService<IMarkdownParser>();
    }

    public sealed record BenchmarkCase(string Name, string Markdown) {
        public override string ToString() => Name;
    }

    [ParamsSource(nameof(Cases))]
    public BenchmarkCase InputCase { get; set; } = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Data
    // -----------------------------------------------------------------------------------------------------------------
    private static IEnumerable<BenchmarkCase> Cases => [
        #region BlockQuote
        new("BlockQuote_1Line", "> Blockquote text"),
        new("BlockQuote_2Lines", """
            > Blockquote text
            > Continued
            """
        ),
        new("BlockQuote_3Lines", """
            > A blockquote
            > with multiple lines.
            """
        ),
        #endregion

        #region Bold
        new("Bold", "**bold**"),
        new("Bold_2InLine", "**bold** **again**"),
        #endregion

        #region Break
        new("Break", "line <br/> break"),
        #endregion

        #region Callout
        new("Callout", """
            >[!note] title
            > body
            """),
        new("Callout_withoutBody", ">[!note] title"),
        #endregion

        #region CodeBlock
        new("CodeBlock", """
            ```csharp
            public class MyClass {
                public void MyMethod() {
                    Console.WriteLine("Hello, world!");
                }
            }
            ```
            """),
        new("CodeBlock_NoLanguage", """
            ```
            something code related
            ```
            """),
        #endregion

        #region CodeInline
        new("CodeInline", "`code`"),
        new("CodeInline_2ticks", "``code``"),
        new("CodeInline_3ticks", "```code```"),
        #endregion

        #region Emote
        new("Emote", ":flag-trans:"),
        #endregion

        #region EscapedCharacters
        new("EscapedCharacters", @"\*escaped italic\*"),
        #endregion

        #region Footnote
        new("FootnoteReference", "[^1]"),
        new("FootnoteDescription", "[^1]: footnote description"),
        #endregion

        #region FrontMatter
        new("FrontMatter", """
            ---
            title: My Document
            ---
            """),
        new("FrontMatter_2Entries", """
                ---
                title: My Document
                author: John Doe
                ---
            """),
        #endregion

        #region Heading
        new("Heading_1", "# Header 1"),
        new("Heading_2", "## Header 2"),
        new("Heading_3", "### Header 3"),
        new("Heading_4", "#### Header 4"),
        new("Heading_5", "##### Header 5"),
        new("Heading_6", "###### Header 6"),

        new("HeadingSimple", """
            heading
            ---
            """),
        #endregion

        #region Highlight
        new("Highlight", "==highlighted text=="),
        #endregion

        #region HorizontalRule
        new("HorizontalRule", "---"),
        #endregion

        #region HtmlBlock
        new("HtmlBlock", "<div><p>HTML content</p></div>"),
        #endregion

        #region Italic
        new("Italic", "*italic*"),
        new("Italic_2InLine", "*italic* *again*"),
        #endregion

        #region Link
        new("Link", "[Regular Link](https://example.com)"),
        new("Link_Nested", "[![nested link](image_url)](outer_url)"),
        #endregion

        #region List
        new("List_UnOrdered", """
            - list item 1
            - list item 2
            """),
        new("List_Ordered", """
            1. list item 1
            2. list item 2
            """),
        new("List_Task", """
            - [ ] task item 1
            """),
        new("List_Task_checked", """
            - [x] task item 1
            """),
        #endregion
        
        #region NewLine
        new("NewLine", """
            line 1
            line 2
            """),
        #endregion
        
        #region Paragraph
        new("Paragraph", "This is a paragraph."),
        #endregion
        
        #region Strikethrough
        new("Strikethrough", "~~strikethrough~~"),
        new("Strikethrough_2InLine", "~~strikethrough~~ ~~again~~"),
        #endregion
        
        #region Subscript
        new("Subscript", "~sub-script~"),
        new("Subscript_2InLine", "~sub-script~ ~again~"),
        #endregion
        
        #region Superscript
        new("Superscript", "^sup-script^"),
        new("Superscript_2InLine", "^sup-script^ ^again^"),
        #endregion
        
        #region Table
        new("Table", """
            | Header 1 | Header 2 |
            | -------- | -------- |
            | Row 1    | Data 1   |
            """),
        new("Table_2Rows", """
            | Header 1 | Header 2 |
            | -------- | -------- |
            | Row 1    | Data 1   |
            | Row 2    | Data 2   |
            """),
        new("Table_3Rows", """
            | Header 1 | Header 2 |
            | -------- | -------- |
            | Row 1    | Data 1   |
            | Row 2    | Data 2   |
            | Row 3    | Data 3   |
            """),
        #endregion
        
        #region Tag
        new("Tag", "#tag"),
        #endregion
        
        #region Template
        new("Template", "{{template}}"),
        #endregion
        
        #region Underline
        new("Underline", "_underline_"),
        #endregion
        
        #region User
        new("User", "@user"),
        #endregion
        
        #region WikiLink
        new("WikiLink", "[[WikiLink]]"),
        #endregion
        
        #region Wrapper
        new("Wrapper", "<|color=red>red text</>"),
        #endregion
        
        #region BoldAndItalic
        new("BoldAndItalic", "***bold and italic***"),
        new("BoldAndItalic_AfterEachOther", "**bold** and *italic*"),
        #endregion
    ];

    // -----------------------------------------------------------------------------------------------------------------
    // Benchmarks
    // -----------------------------------------------------------------------------------------------------------------
    [Benchmark]
    public IMdSyntaxTree SerializeToSyntaxTree() {
        IMdSyntaxTree tree = Parser.Markdown.SerializeToSyntaxTree(InputCase.Markdown);
        return tree;
    }
}

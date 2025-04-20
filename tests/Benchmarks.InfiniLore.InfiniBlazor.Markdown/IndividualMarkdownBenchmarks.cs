// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using InfiniLore.InfiniBlazor;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Benchmarks.InfiniLore.InfiniBlazor.Markdown;
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
        serviceCollection.AddInfiniBlazor(config => config.AddMarkdown());
        serviceCollection.AddLogging();
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        Parser = serviceProvider.GetRequiredService<IMarkdownParser>();
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Benchmarks for SinglelineStructuresRegex Features
    // -----------------------------------------------------------------------------------------------------------------

    [Benchmark]
    public string EscapedCharacters() {
        return Parser.ParseToString(@"\*escaped text\*");
    }

    [Benchmark]
    public string BoldAndItalic() {
        return Parser.ParseToString("***bold and italic***");
    }
    
    [Benchmark]
    public string BoldOnly() {
        return Parser.ParseToString("**bold**");
    }
    
    [Benchmark]
    public string ItalicOnly() {
        return Parser.ParseToString("*italic*");
    }
    
    [Benchmark]
    public string Superscript() {
        return Parser.ParseToString("^^sup-script^^");
    }
    
    [Benchmark]
    public string Subscript() {
        return Parser.ParseToString("^^sub-script^^");
    }
    
    [Benchmark]
    public string Strikethrough() {
        return Parser.ParseToString("~~strikethrough~~");
    }
    
    [Benchmark]
    public string Underline() {
        return Parser.ParseToString("_underline_");
    }
    
    [Benchmark]
    public string InlineCode() {
        return Parser.ParseToString("`inline code`");
    }
    
    [Benchmark]
    public string Emotes() {
        return Parser.ParseToString(":flag-trans:");
    }
    
    [Benchmark]
    public string NestedLinks() {
        return Parser.ParseToString("[![nested link](image_url)](outer_url)");
    }
    
    [Benchmark]
    public string RegularLinks() {
        return Parser.ParseToString("[Regular Link](https://example.com)");
    }
    
    [Benchmark]
    public string Tags() {
        return Parser.ParseToString("#tag");
    }
    
    [Benchmark]
    public string HtmlSpecialCharacters() {
        return Parser.ParseToString("&copy; & < >");
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Benchmarks for MultilineStructuresRegex Features
    // -----------------------------------------------------------------------------------------------------------------
    
    [Benchmark]
    public string Headings() {
        return Parser.ParseToString("""
            # Header 1
            ## Header 2
            ### Header 3
            """);
    }
    
    [Benchmark]
    public string CodeBlocks() {
        return Parser.ParseToString("""
            ```csharp
            code block
            ```
            """);
    }
    
    [Benchmark]
    public string SimpleHeadings() {
        return Parser.ParseToString("""
            Simple Heading
            ---
            """);
    }
    
    [Benchmark]
    public string UnorderedLists() {
        return Parser.ParseToString("""
            - Item 1
            - Item 2
              - Nested Item
            """);
    }
    
    [Benchmark]
    public string OrderedLists() {
        return Parser.ParseToString("""
            1. First Item
            2. Second Item
              3. Nested Item
            """);
    }
    
    [Benchmark]
    public string Tables() {
        return Parser.ParseToString(
            """
            | Column 1 | Column 2 |
            |----------|----------|
            | Value 1  | Value 2  |
            """
        );
    }
    
    [Benchmark]
    public string BlockQuotes() {
        return Parser.ParseToString("""
            > A blockquote
            > with multiple lines.
            """);
    }
    
    [Benchmark]
    public string HtmlBlocks() {
        return Parser.ParseToString("<div><p>HTML content</p></div>");
    }
    
    [Benchmark]
    public string HorizontalRules() {
        return Parser.ParseToString("""
            ---

            ***

            ___
            """);
    }
    
    [Benchmark]
    public string RemainderText() {
        return Parser.ParseToString("This is normal text left over.");
    }
}
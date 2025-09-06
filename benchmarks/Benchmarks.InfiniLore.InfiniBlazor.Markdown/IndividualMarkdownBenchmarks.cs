// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using InfiniLore.InfiniBlazor.Markdown;
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
        serviceCollection.AddInfiniBlazor();
        serviceCollection.AddLogging();
        ServiceProvider provider = serviceCollection.BuildServiceProvider();
        
        Parser = provider.GetRequiredService<IMarkdownParser>();
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Benchmarks for SinglelineStructuresRegex Features
    // -----------------------------------------------------------------------------------------------------------------
    [Benchmark]
    public string EscapedCharacters() {
        const string input = @"\*escaped text\*";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string BoldAndItalic() {
        const string input = "***bold and italic***";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string BoldOnly() {
        const string input = "**bold**";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string ItalicOnly() {
        const string input = "*italic*";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string Superscript() {
        const string input = "^^sup-script^^";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string Subscript() {
        const string input = "^^sub-script^^";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string Strikethrough() {
        const string input = "~~strikethrough~~";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string Underline() {
        const string input = "_underline_";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string InlineCode() {
        const string input = "`inline code`";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string Emotes() {
        const string input = ":flag-trans:";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string NestedLinks() {
        const string input = "[![nested link](image_url)](outer_url)";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string RegularLinks() {
        const string input = "[Regular Link](https://example.com)";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string Tags() {
        const string input = "#tag";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string HtmlSpecialCharacters() {
        const string input = "&copy; & < >";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Benchmarks for MultilineStructuresRegex Features
    // -----------------------------------------------------------------------------------------------------------------

    [Benchmark]
    public string Headings() {
        const string input = """
            # Header 1
            ## Header 2
            ### Header 3
            """;
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string CodeBlocks() {
        const string input = """
            ```csharp
            code block
            ```
            """;
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string SimpleHeadings() {
        const string input = """
            Simple Heading
            ---
            """;
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string UnorderedLists() {
        const string input = """
            - Item 1
            - Item 2
              - Nested Item
            """;
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string OrderedLists() {
        const string input = """
            1. First Item
            2. Second Item
              - Nested Item
            """;
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string Tables() {
        const string input =
            """
            | Column 1 | Column 2 |
            |----------|----------|
            | Value 1  | Value 2  |
            """;
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string BlockQuotes() {
        const string input = """
            > A blockquote
            > with multiple lines.
            """;
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string HtmlBlocks() {
        const string input = "<div><p>HTML content</p></div>";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string HorizontalRules() {
        const string input = """
            ---

            ***

            ___
            """;
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }

    [Benchmark]
    public string RemainderText() {
        const string input = "This is normal text left over.";
        
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(input);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }
}

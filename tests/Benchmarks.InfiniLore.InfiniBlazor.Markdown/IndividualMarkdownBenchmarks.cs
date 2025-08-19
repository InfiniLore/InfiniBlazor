// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.SyntaxDeserializer;
using InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer;
using Microsoft.Extensions.DependencyInjection;

namespace Benchmarks.InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.Declared)]
public class IndividualMarkdownBenchmarks {
    private IMdSyntaxSerializer Parser { get; set; } = null!;
    private IMdSyntaxDeserializer Converter { get; set; } = null!;

    [GlobalSetup]
    public void Setup() {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddInfiniBlazor(config => config.AddMarkdownLogic());
        serviceCollection.AddLogging();
        ServiceProvider provider = serviceCollection.BuildServiceProvider();
        
        Parser = provider.GetRequiredService<IMdSyntaxSerializer>();
        Converter = provider.GetRequiredService<IMdSyntaxDeserializer>();
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Benchmarks for SinglelineStructuresRegex Features
    // -----------------------------------------------------------------------------------------------------------------
    [Benchmark]
    public string EscapedCharacters() {
        const string input = @"\*escaped text\*";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string BoldAndItalic() {
        const string input = "***bold and italic***";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string BoldOnly() {
        const string input = "**bold**";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string ItalicOnly() {
        const string input = "*italic*";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string Superscript() {
        const string input = "^^sup-script^^";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string Subscript() {
        const string input = "^^sub-script^^";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string Strikethrough() {
        const string input = "~~strikethrough~~";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string Underline() {
        const string input = "_underline_";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string InlineCode() {
        const string input = "`inline code`";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string Emotes() {
        const string input = ":flag-trans:";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string NestedLinks() {
        const string input = "[![nested link](image_url)](outer_url)";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string RegularLinks() {
        const string input = "[Regular Link](https://example.com)";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string Tags() {
        const string input = "#tag";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string HtmlSpecialCharacters() {
        const string input = "&copy; & < >";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
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
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string CodeBlocks() {
        const string input = """
            ```csharp
            code block
            ```
            """;
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string SimpleHeadings() {
        const string input = """
            Simple Heading
            ---
            """;
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string UnorderedLists() {
        const string input = """
            - Item 1
            - Item 2
              - Nested Item
            """;
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string OrderedLists() {
        const string input = """
            1. First Item
            2. Second Item
              - Nested Item
            """;
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string Tables() {
        const string input =
            """
            | Column 1 | Column 2 |
            |----------|----------|
            | Value 1  | Value 2  |
            """;
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string BlockQuotes() {
        const string input = """
            > A blockquote
            > with multiple lines.
            """;
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string HtmlBlocks() {
        const string input = "<div><p>HTML content</p></div>";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string HorizontalRules() {
        const string input = """
            ---

            ***

            ___
            """;
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public string RemainderText() {
        const string input = "This is normal text left over.";
        
        IMdSyntaxTree tree = Parser.ParseToTree(input);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }
}

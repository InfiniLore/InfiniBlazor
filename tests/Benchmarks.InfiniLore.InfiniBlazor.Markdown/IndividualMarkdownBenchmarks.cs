// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
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
    private IMarkdownParser<string, string> Parser { get; set; } = null!;

    [GlobalSetup]
    public void Setup() {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddInfiniBlazor(config => config.AddMarkdownLogic(static config => config.AddMarkdownParser<string, string>()));
        serviceCollection.AddLogging();
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        Parser = serviceProvider.GetRequiredService<IMarkdownParser<string, string>>();
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Benchmarks for SinglelineStructuresRegex Features
    // -----------------------------------------------------------------------------------------------------------------

    [Benchmark]
    public async ValueTask<string> EscapedCharacters() {
        const string input = @"\*escaped text\*";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> BoldAndItalic() {
        const string input = "***bold and italic***";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> BoldOnly() {
        const string input = "**bold**";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> ItalicOnly() {
        const string input = "*italic*";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> Superscript() {
        const string input = "^^sup-script^^";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> Subscript() {
        const string input = "^^sub-script^^";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> Strikethrough() {
        const string input = "~~strikethrough~~";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> Underline() {
        const string input = "_underline_";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> InlineCode() {
        const string input = "`inline code`";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> Emotes() {
        const string input = ":flag-trans:";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> NestedLinks() {
        const string input = "[![nested link](image_url)](outer_url)";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> RegularLinks() {
        const string input = "[Regular Link](https://example.com)";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> Tags() {
        const string input = "#tag";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> HtmlSpecialCharacters() {
        const string input = "&copy; & < >";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Benchmarks for MultilineStructuresRegex Features
    // -----------------------------------------------------------------------------------------------------------------

    [Benchmark]
    public async ValueTask<string> Headings() {
        const string input = """
            # Header 1
            ## Header 2
            ### Header 3
            """;
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> CodeBlocks() {
        const string input = """
            ```csharp
            code block
            ```
            """;
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> SimpleHeadings() {
        const string input = """
            Simple Heading
            ---
            """;
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> UnorderedLists() {
        const string input = """
            - Item 1
            - Item 2
              - Nested Item
            """;
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> OrderedLists() {
        const string input = """
            1. First Item
            2. Second Item
              3. Nested Item
            """;
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> Tables() {
        const string input =
            """
            | Column 1 | Column 2 |
            |----------|----------|
            | Value 1  | Value 2  |
            """;
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> BlockQuotes() {
        const string input = """
            > A blockquote
            > with multiple lines.
            """;
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> HtmlBlocks() {
        const string input = "<div><p>HTML content</p></div>";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> HorizontalRules() {
        const string input = """
            ---

            ***

            ___
            """;
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    [Benchmark]
    public async ValueTask<string> RemainderText() {
        const string input = "This is normal text left over.";
        
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }
}

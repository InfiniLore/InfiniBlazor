// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.Extensions.DependencyInjection;
using System.Text;
using MarkdownConfig = InfiniLore.InfiniBlazor.Config.MarkdownConfig;

namespace Benchmarks.InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.Declared)]
public class MarkdownBenchmarks {
    private string Markdown { get; set; } = string.Empty;
    
    private IMarkdownParser Parser { get; set; } = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [GlobalSetup]
    public async Task Setup() {
        // Read the file content
        const string filePath = "markdownBenchmark.md";
        Markdown = await File.ReadAllTextAsync(filePath, new UTF8Encoding(encoderShouldEmitUTF8Identifier:false));
        Markdown = Markdown.ReplaceLineEndings("\n");
        
        ServiceProvider provider = CreateProvider();
        Parser = provider.GetRequiredService<IMarkdownParser>();
    }

    private static ServiceProvider CreateProvider(Action<MarkdownConfig>? configure = null) {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddInfiniBlazor(config => config.AddMarkdownLogic(configure));
        serviceCollection.AddLogging();
        return serviceCollection.BuildServiceProvider();
    }

    [Benchmark(Baseline = true)]
    public string RenderMarkdown() {
        IMdSyntaxTree tree = Parser.MarkdownString.SerializeToSyntaxTree(Markdown);
        string? output = Parser.HtmlString.DeserializeToString(tree);
        return output ?? throw new InvalidOperationException("The Markdown input should not be empty.");
    }
    
    // [Benchmark()]
    // public async ValueTask<string> RenderMarkdownSanitized() {
    //     string input = Markdown;
    //     string? output = await SanitizedParser.TryParseAsync(input);
    //     if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
    //     return output;
    // }
}
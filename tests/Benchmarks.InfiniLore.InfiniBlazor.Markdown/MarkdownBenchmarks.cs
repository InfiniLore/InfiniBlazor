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
public class MarkdownBenchmarks {
    private string Markdown { get; set; } = string.Empty;
    private IMarkdownParser<string, string> Parser { get; set; } = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [GlobalSetup]
    public async Task Setup() {
        const string filePath = "markdownBenchmark.md";
        if (!File.Exists(filePath)) throw new FileNotFoundException($"The file {filePath} does not exist.");

        // Read the file content
        Markdown = await File.ReadAllTextAsync(filePath);
        if (Markdown.IsNullOrWhiteSpace()) throw new InvalidOperationException("The Markdown input should not be empty.");
        
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddInfiniBlazor(config => config.AddMarkdown());
        serviceCollection.AddLogging();
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        Parser = serviceProvider.GetRequiredService<IMarkdownParser<string, string>>();
    }


    // [Benchmark(OperationsPerInvoke = 1000)]
    [Benchmark(Baseline = true)]
    public string RenderMarkdown() {
        string input = Markdown;
        if(!Parser.TryParse(input, out string? output)) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }

    // [Benchmark]
    // public StringWriter RenderMarkdownToStream() {
    //     var streamWriter = new StringWriter();
    //
    //     Parser.ParseToWriter(Markdown, streamWriter);
    //
    //     streamWriter.Flush();
    //     return streamWriter;
    //
    // }
}
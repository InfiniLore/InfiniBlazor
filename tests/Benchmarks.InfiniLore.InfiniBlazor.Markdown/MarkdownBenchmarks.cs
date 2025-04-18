// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using InfiniLore.InfiniBlazor;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Config;
using InfiniLore.InfiniBlazor.MarkdownParser;
using InfiniLore.InfiniBlazor.Services;
using Microsoft.Extensions.DependencyInjection;

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
        const string url = "https://gist.githubusercontent.com/allysonsilva/85fff14a22bbdf55485be947566cc09e/raw/fa8048a906ebed3c445d08b20c9173afd1b4a1e5/Full-Markdown.md";
        var client = new HttpClient{Timeout = TimeSpan.FromSeconds(10)};
        HttpResponseMessage response = await client.GetAsync(url);
        
        // Check that the first line has "# Headers"
        Markdown = await response.Content.ReadAsStringAsync();
        if (Markdown.IsNullOrWhiteSpace()) throw new InvalidOperationException("The Markdown input should not be empty.");
        
        string firstLine = Markdown.Split('\n')[0];
        if (!firstLine.StartsWith("# Headers")) throw new InvalidOperationException("The first line should start with '# Headers'.");

        var serviceCollection = new ServiceCollection();
        serviceCollection.AddInfiniBlazor(config => config.AddMarkdown());
        serviceCollection.AddLogging();
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        Parser = serviceProvider.GetRequiredService<IMarkdownParser>();
    }


    // [Benchmark(OperationsPerInvoke = 1000)]
    [Benchmark(Baseline = true)]
    public string RenderMarkdown() {
        string input = Markdown;

        string output = Parser.Parse(input);
        return output;
    }

    [Benchmark]
    public StringWriter RenderMarkdownToStream() {
        var streamWriter = new StringWriter();

        Parser.Parse(Markdown, streamWriter);

        streamWriter.Flush();
        return streamWriter;

    }
}
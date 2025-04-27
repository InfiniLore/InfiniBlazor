// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Config;
using InfiniLore.InfiniBlazor.Markdown.Processors.OutputProcessors;
using Microsoft.Extensions.DependencyInjection;
using System.Text;

namespace Benchmarks.InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[MemoryDiagnoser]
[Orderer(SummaryOrderPolicy.Declared)]
public class MarkdownBenchmarks {
    private string Markdown { get; set; } = string.Empty;
    private IMarkdownParser<string, string> Parser { get; set; } = null!;
    private IMarkdownParser<string, string> SanitizedParser { get; set; } = null!;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [GlobalSetup]
    public async Task Setup() {
        // Read the file content
        const string filePath = "markdownBenchmark.md";
        Markdown = await File.ReadAllTextAsync(filePath, new UTF8Encoding(encoderShouldEmitUTF8Identifier:false));
        Markdown = Markdown.Replace("\r\n", "\n");
        
        // const string url = "https://gist.githubusercontent.com/allysonsilva/85fff14a22bbdf55485be947566cc09e/raw/fa8048a906ebed3c445d08b20c9173afd1b4a1e5/Full-Markdown.md";
        // var client = new HttpClient{Timeout = TimeSpan.FromSeconds(10)};
        // HttpResponseMessage response = await client.GetAsync(url);
        // Markdown = await response.Content.ReadAsStringAsync();
        
        Parser = CreateParser();
        SanitizedParser = CreateParser(static config => {
            config.AddMarkdownParser<string, string>()
                .AddOutputProcessor<StringOutputSanitizerProcessor>();
        });
    }
    private static IMarkdownParser<string, string> CreateParser(Action<MarkdownConfig>? configure = null) {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddInfiniBlazor(config => config.AddMarkdown(configure));
        serviceCollection.AddLogging();
        ServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();
        return serviceProvider.GetRequiredService<IMarkdownParser<string, string>>();
    }

    [Benchmark(Baseline = true)]
    public async ValueTask<string> RenderMarkdown() {
        string input = Markdown;
        string? output = await Parser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }
    
    [Benchmark()]
    public async ValueTask<string> RenderMarkdownSanitized() {
        string input = Markdown;
        string? output = await SanitizedParser.TryParseAsync(input);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
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
// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Order;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown;
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
    
    private IMdSyntaxParser Parser { get; set; } = null!;
    private IMdSyntaxTreeConverter Converter { get; set; } = null!;

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
        
        var provider = CreateProvider();
        Parser = provider.GetRequiredService<IMdSyntaxParser>();
        Converter = provider.GetRequiredService<IMdSyntaxTreeConverter>();
    }

    private static IServiceProvider CreateProvider(Action<MarkdownConfig>? configure = null) {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddInfiniBlazor(config => config.AddMarkdownLogic(configure));
        serviceCollection.AddLogging();
        return serviceCollection.BuildServiceProvider();
    }

    [Benchmark(Baseline = true)]
    public string RenderMarkdown() {
        IMdSyntaxTree tree = Parser.ParseToTree(Markdown);
        string? output = Converter.ConvertToString(tree);
        if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
        return output;
    }
    
    // [Benchmark()]
    // public async ValueTask<string> RenderMarkdownSanitized() {
    //     string input = Markdown;
    //     string? output = await SanitizedParser.TryParseAsync(input);
    //     if(output is null) throw new InvalidOperationException("The Markdown input should not be empty.");
    //     return output;
    // }
}
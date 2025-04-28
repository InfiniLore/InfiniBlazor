// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownSyntaxTreeParser<ITextSource>>]
public sealed class TextSourceMarkdownSyntaxTreeParser(IServiceProvider serviceProvider, ILogger<TextSourceMarkdownSyntaxTreeParser> logger) : StringMarkdownSyntaxTreeParser(serviceProvider, logger),  IMarkdownSyntaxTreeParser<ITextSource> {
    public ValueTask ParseToNodeTreeAsync(ITextSource input, IMarkdownSyntaxTree nodeTree, CancellationToken ct = default) => ParseToNodeTreeAsync(input.Text, nodeTree, ct);
}

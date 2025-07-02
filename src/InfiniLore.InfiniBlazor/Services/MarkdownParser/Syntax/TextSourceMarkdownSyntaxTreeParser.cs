// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.Extensions.Logging;

namespace InfiniLore.InfiniBlazor.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownSyntaxTreeParser<ITextSource>>]
public sealed class TextSourceMarkdownSyntaxTreeParser(IServiceProvider serviceProvider, ILogger<TextSourceMarkdownSyntaxTreeParser> logger) : StringMarkdownSyntaxTreeParser(serviceProvider, logger),  IMarkdownSyntaxTreeParser<ITextSource> {
    public void ParseToNodeTreeAsync(ITextSource input, IMarkdownSyntaxTree nodeTree) => ParseToNodeTreeAsync(input.Text, nodeTree);
}

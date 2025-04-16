// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownConfig(IInfiniBlazorConfig infiniLoreBlazorConfig) {
    public MarkdownTextEditorConfig TextEditor { get; } = new(infiniLoreBlazorConfig);
    public MarkdownParserConfig Parser { get; } = new(infiniLoreBlazorConfig);
}

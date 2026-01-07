// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownSerializerOptions {
    public List<IMdSyntaxNodeSerializer> SingleLine { get; init; } = [];
    public List<IMdSyntaxNodeSerializer> MultiLine { get; init; } = [];
    public IMdSyntaxNodeSerializer? FrontMatter { get; init; }
}

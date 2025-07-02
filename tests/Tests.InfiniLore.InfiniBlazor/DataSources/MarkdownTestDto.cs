// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax;

namespace Tests.InfiniLore.InfiniBlazor.DataSources;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record MarkdownTestDto(
    string Section,
    string Markdown,
    string ExpectedStringOutput,

    // ReSharper disable once NotAccessedPositionalProperty.Global
    Action<IMarkdownSyntaxNode>? ConfigureExpectedNode = null
) {
    public IMarkdownSyntaxNode? ExpectedNode { get; } = CreateNode(ConfigureExpectedNode);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static MarkdownSyntaxNode? CreateNode(Action<IMarkdownSyntaxNode>? configureNode) {
        if (configureNode == null) return null;

        var node = new MarkdownSyntaxNode();
        configureNode.Invoke(node);
        return node;
    }
}

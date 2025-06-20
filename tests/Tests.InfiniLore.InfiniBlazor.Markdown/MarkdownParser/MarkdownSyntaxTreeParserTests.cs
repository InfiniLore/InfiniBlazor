// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class MarkdownSyntaxTreeParserTests(IMarkdownSyntaxTreeParser<string> nodeTreeParser)  {

    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task ParseToNodeTree_ValidInputs(MarkdownTestDto dto) {
        // Arrange
        var nodeTree = new MarkdownSyntaxTree();

        // Act
        nodeTreeParser.ParseToNodeTreeAsync(dto.Markdown, nodeTree);
        Skip.When(dto.ExpectedNode == null, "The node tree is null and thus cannot be compared.");

        // Assert
        await Assert.That(nodeTree.RootNode).IsEquivalentTo(dto.ExpectedNode);
    }
    
}

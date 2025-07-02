// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Syntax;
using Tests.InfiniLore.InfiniBlazor.DataSources;

namespace Tests.InfiniLore.InfiniBlazor.MarkdownParser;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class MarkdownSyntaxTreeConverterTests(IMarkdownSyntaxTreeConverter<string> toStringConverter) {
    
    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task Convert_ToString_ValidInputs(MarkdownTestDto dto) {
        // Arrange
        Skip.When(dto.ExpectedNode == null, "The node tree is null and thus cannot be compared.");
        MarkdownSyntaxTree nodeTree = MarkdownSyntaxTree.WithRootNode(dto.ExpectedNode);
        
        // Act
        string output = toStringConverter.Convert(nodeTree);

        // Assert
        await Assert.That(output).IsEqualTo(dto.ExpectedStringOutput).IgnoringWhitespace();
    }
}

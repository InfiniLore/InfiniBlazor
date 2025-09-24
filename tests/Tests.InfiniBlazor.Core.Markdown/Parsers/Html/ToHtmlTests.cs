// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Parsers.Html;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using Tests.InfiniBlazor.Shared;

namespace Tests.InfiniBlazor.Core.Markdown.Parsers.Html;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InfiniBlazorDiDataSource]
public class ToHtmlTests(IHtmlMdSyntaxTreeParser htmlMdSyntaxTreeParser) {

    [Test]
    public async Task ToHtml_ShouldReturnExpected() {
        // Arrange
        var tree = new MdSyntaxTree {
            RootNode = new RootMdSyntaxNode().WithChild(
            new BoldMdSyntaxNode()
                .WithText("Example Content")
            )
        };

        // Act
        string output = await htmlMdSyntaxTreeParser.DeserializeToStringAsync(tree);

        // Assert
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo("<strong>Example Content</strong>");
    }
}

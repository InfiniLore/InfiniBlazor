// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.Syntax;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeEqualityTests {
    
    [Test]
    public async Task Empty_ShouldBeEqual() {
        // Arrange
        var paragraph1 = new ParagraphMdSyntaxNode();
        var paragraph2 = new ParagraphMdSyntaxNode();
        
        // Act
        bool result = paragraph1.Equals(paragraph2);
        
        // Assert
        await Assert.That(result).IsTrue();
    }
}

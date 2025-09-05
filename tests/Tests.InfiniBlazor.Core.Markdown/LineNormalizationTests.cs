// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor;

namespace Tests.InfiniBlazor.Core.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class LineNormalizationTests {
    [Test]
    [Arguments("> test\n>", "test\n")]
    [Arguments("> test\n> ", "test\n")]
    [Arguments("> text\n> more text\n>", "text\nmore text\n")]
    [Arguments(">\n>\n\n", "\n\n\n")] // should never be caught by the regex anyway
    [Arguments("", "")]
    [Arguments(">", "")]
    [Arguments("> ", " ")]
    public async Task NormalizeBlockQuote_ShouldWork(string input, string expected) {
        // Arrange
        
        // Act
        string result = LineNormalization.NormalizeBlockQuote(input, out _);
        
        // Assert
        await Assert.That(result).IsEqualTo(expected);
    }
}

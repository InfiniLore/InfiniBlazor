// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown.Editors;

namespace InfiniBlazorTests.Services.TextEditor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class TextSourceTests {
    [Test]
    public async Task Constructor_InitializesWithEmptyText() {
        // Arrange & Act
        var textSource = new TextSource();

        // Assert
        await Assert.That(textSource.Text).IsEmpty();
        await Assert.That(textSource.Length).IsEqualTo(0);
        await Assert.That(textSource.LineCount).IsEqualTo(1);
    }

    [Test]
    [Arguments("Hello\r\nWorld", 2)]// Windows line endings
    [Arguments("Hello\nWorld", 2)]// Unix line endings
    [Arguments("Single line", 1)]// Single line
    [Arguments("", 1)]// Empty text
    [Arguments("Line1\n\nLine3", 3)]// Empty line in between
    public async Task Text_SetProperty_NormalizesLineEndingsAndUpdatesLines(string input, int expectedLineCount) {
        // Arrange
        var textSource = new TextSource(input);

        // Act & Assert
        await Assert.That(textSource.LineCount).IsEqualTo(expectedLineCount);
        await Assert.That(textSource.Text).DoesNotContain("\r\n");
        await Assert.That(textSource.Text).DoesNotContain("\r");
    }

    [Test]
    public async Task Length_ReflectsActualTextLength() {
        // Arrange
        var textSource = new TextSource();
        const string text = "Hello\nWorld";

        // Act
        textSource.UpdateSource(text);

        // Assert
        await Assert.That(textSource.Length).IsEqualTo(text.Length);
    }

    [Test]
    public async Task TextSpan_ReturnsCorrectSpan() {
        // Arrange
        var textSource = new TextSource();
        const string text = "Hello\nWorld";

        // Act
        textSource.UpdateSource(text);
        ReadOnlySpan<char> span = textSource.TextSpan;

        // Assert
        string spanStr = span.ToString();
        await Assert.That(span.Length).IsEqualTo(text.Length);
        await Assert.That(spanStr).IsEqualTo(text);
    }

    [Test]
    public async Task Lines_ContainsCorrectRanges() {
        // Arrange
        var textSource = new TextSource();
        const string text = "Line1\nLine2\nLine3";

        // Act
        textSource.UpdateSource(text);

        // Assert
        await Assert.That(textSource.LineCount).IsEqualTo(3);

        await Assert.That(text[textSource.LineRanges[0]]).IsEquatableOrEqualTo("Line1");
        await Assert.That(text[textSource.LineRanges[1]]).IsEquatableOrEqualTo("Line2");
        await Assert.That(text[textSource.LineRanges[2]]).IsEquatableOrEqualTo("Line3");
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.TextEditor;
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
        await Assert.That(textSource.Lines).IsEmpty();
    }

    [Test]
    [Arguments("Hello\r\nWorld", 2)]// Windows line endings
    [Arguments("Hello\nWorld", 2)]// Unix line endings
    [Arguments("Single line", 1)]// Single line
    [Arguments("", 1)]// Empty text
    [Arguments("Line1\n\nLine3", 3)]// Empty line in between
    public async Task Text_SetProperty_NormalizesLineEndingsAndUpdatesLines(string input, int expectedLineCount) {
        // Arrange
        var textSource = new TextSource {
            Text = input
        };

        // Act & Assert
        await Assert.That(textSource.Lines).HasCount(expectedLineCount);
        await Assert.That(textSource.Text).DoesNotContain("\r\n");
        await Assert.That(textSource.Text).DoesNotContain("\r");
    }

    [Test]
    public async Task Length_ReflectsActualTextLength() {
        // Arrange
        var textSource = new TextSource();
        const string text = "Hello\nWorld";

        // Act
        textSource.Text = text;

        // Assert
        await Assert.That(textSource.Length).IsEqualTo(text.Length);
    }

    [Test]
    public async Task TextSpan_ReturnsCorrectSpan() {
        // Arrange
        var textSource = new TextSource();
        const string text = "Hello\nWorld";

        // Act
        textSource.Text = text;
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
        textSource.Text = text;

        // Assert
        await Assert.That(textSource.Lines).HasCount(3);

        await Assert.That(text[textSource.Lines[0]]).IsEquatableOrEqualTo("Line1");
        await Assert.That(text[textSource.Lines[1]]).IsEquatableOrEqualTo("Line2");
        await Assert.That(text[textSource.Lines[2]]).IsEquatableOrEqualTo("Line3");
    }
}

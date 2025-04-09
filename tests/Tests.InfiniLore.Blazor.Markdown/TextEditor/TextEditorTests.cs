// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Blazor.Markdown;

namespace Tests.InfiniLore.Blazor.Markdown.TextEditor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DIDataSource]
public class TextEditorTests(ITextEditor textEditor) {
    [Test]
    public async Task Test1() {
        // Arrange
        textEditor.Text = "Hello World!";
        Range range = ..;

        // Act
        textEditor.Modify("bold", range);

        // Assert
        await Assert.That(textEditor.Text).IsEqualTo("**Hello World!**");
    }
}

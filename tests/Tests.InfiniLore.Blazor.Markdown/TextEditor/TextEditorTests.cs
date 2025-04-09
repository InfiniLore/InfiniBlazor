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
    public static IEnumerable<Func<(int, int, string)>> HelloWoldDataSource() {
        const string input = "Hello World!";
        const string boldFormat = "**";

        for (int start = 0; start <= input.Length; start++) {
            for (int end = input.Length; end >= start; end--) {
                string modified = $"{input[..start]}{boldFormat}{input[start..end]}{boldFormat}{input[end..]}";
                int i = start;
                int j = end;
                yield return () => (i, j, modified);
            }
        }

    }
    
    [Test]
    [MethodDataSource(nameof(HelloWoldDataSource))]
    public async Task HelloWorld_RangeTest(int start, int end, string expected) {
        // Arrange
        textEditor.Text = "Hello World!";
        Range range = start..end;

        // Act
        textEditor.Modify("bold", range);

        // Assert
        await Assert.That(textEditor.Text).IsEqualTo(expected);
    }

    [Test]
    [Arguments("- Hello World!", "- **Hello World!**")]
    [Arguments("1. Hello World!", "1. **Hello World!**")]
    [Arguments("- Hello World!\n- Hello World!", "- **Hello World!**\n- **Hello World!**")]
    [Arguments("1. Hello World!\n2. Hello World!", "1. **Hello World!**\n2. **Hello World!**")]
    public async Task ListItem_RangeTest(string input, string expected) {
        // Arrange
        textEditor.Text = input;
        Range range = ..input.Length;
        
        // Act
        textEditor.Modify("bold", range);
        
        // Assert
        await Assert.That(textEditor.Text).IsEqualTo(expected);
    }
}

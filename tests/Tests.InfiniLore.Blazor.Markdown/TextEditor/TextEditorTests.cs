// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Blazor.Markdown;
using InfiniLore.Blazor.Markdown.Services;

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

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    [MethodDataSource(nameof(HelloWoldDataSource))]
    public async Task Modify_Bold_SingleLine(int start, int end, string expected) {
        // Arrange
        var source = new TextSource { Text = "Hello World!" };
        Range range = start..end;

        // Act
        textEditor.Modify(source,"bold", range);

        // Assert
        await Assert.That(source.Text).IsEqualTo(expected);
    }

    [Test]
    [Arguments("- Hello World!", "- **Hello World!**")]
    [Arguments("1. Hello World!", "1. **Hello World!**")]
    [Arguments("- Hello World!\n- Hello World!", "- **Hello World!**\n- **Hello World!**")]
    [Arguments("1. Hello World!\n2. Hello World!", "1. **Hello World!**\n2. **Hello World!**")]
    public async Task Modify_Bold_MultiLine(string input, string expected) {
        // Arrange
        var source = new TextSource { Text =input };
        Range range = ..input.Length;
        
        // Act
        textEditor.Modify(source, "bold", range);
        
        // Assert
        await Assert.That(source.Text).IsEqualTo(expected);
    }

    [Test]
    [Arguments("Hello World", "!", 11, 11, "Hello World!")]
    [Arguments("Hello World", "INSERTED ", 6, 6, "Hello INSERTED World")] 
    [Arguments("Hello World", "INSERTED", 0, 0, "INSERTEDHello World")]  
    [Arguments("Hello World", "Example", 0, 5, "Example World")]        
    [Arguments("Hello World", "Everyone", 6, 11, "Hello Everyone")]      
    [Arguments("Hello World", "", 6, 11, "Hello ")]                      
    public async Task Insert(string original, string input, int start, int end, string expected) {
        // Arrange
        var source = new TextSource { Text = original};
        Range range = start..end;
        
        // Act
        textEditor.Insert(source, input, range);
        
        // Assert
        await Assert.That(source.Text).IsEqualTo(expected);
    }
}

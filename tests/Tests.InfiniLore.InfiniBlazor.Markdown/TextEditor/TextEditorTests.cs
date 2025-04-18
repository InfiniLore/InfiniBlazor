// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.TextEditor;

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

    public static IEnumerable<Func<(string, string)>> ModifyBoldMultiLineDataSource() {
        yield return () => ("- ", "- ****");
        yield return () => ("- Hello World!", "- **Hello World!**");
        yield return () => ("1. Hello World!", "1. **Hello World!**");
        yield return () => ("- Hello World!\n- Hello World!", "- **Hello World!**\n- **Hello World!**");
        yield return () => ("1. Hello World!\n2. Hello World!", "1. **Hello World!**\n2. **Hello World!**");
        yield return () => (
            """
            | test | something |
            | ---- | --------- |
            | alpha | beta |
            """,
            """
            | **test** | **something** |
            | ---- | --------- |
            | **alpha** | **beta** |
            """
        );
        yield return () => (
            "alpha | beta |",
            "**alpha** | **beta** |"
        );
        yield return () => (
            "alpha | beta",
            "**alpha** | **beta**"
        );
        yield return () => (
            "| alpha | beta",
            "| **alpha** | **beta**"
        );
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    [MethodDataSource(nameof(HelloWoldDataSource))]
    public async Task Modify_Bold_SingleLine(int start, int end, string expected) {
        // Arrange
        var source = new TextSource { Text = "Hello World!" };
        var expectedSource = new TextSource { Text = expected };
        Range range = start..end;

        // Act
        textEditor.Modify(source,"bold", range);

        // Assert
        await Assert.That(source.Text).IsEqualTo(expectedSource.Text);
    }

    [Test]
    [MethodDataSource(nameof(ModifyBoldMultiLineDataSource))]
    public async Task Modify_Bold_MultiLine(string input, string expected) {
        // Arrange
        var source = new TextSource { Text =input };
        var expectedSource = new TextSource { Text = expected };
        Range range = ..input.Length;
        
        // Act
        textEditor.Modify(source, "bold", range);
        
        // Assert
        await Assert.That(source.Text).IsEqualTo(expectedSource.Text);
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

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor;
using InfiniLore.InfiniBlazor.TextEditor;
using Microsoft.Extensions.DependencyInjection;
using Tests.InfiniBlazor.DataSources.TextEditor;
using Tests.Shared.Infinilore.InfiniBlazor;

namespace Tests.InfiniBlazor.Services.TextEditor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class BoldOnlyTextEditorTests(IServiceProvider provider) {
    private readonly ITextEditor textEditor = provider.GetRequiredKeyedService<ITextEditor>("boldOnly");
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    [MethodDataSource(typeof(ModifyBoldMultiLineDataSources), nameof(ModifyBoldMultiLineDataSources.HelloWoldDataSource))]
    public async Task Modify_SpecificRange(string sectionName, int start, int end, string expected) {
        // Arrange
        var source = new TextSource { Text = "Hello World!" };
        var expectedSource = new TextSource { Text = expected };
        Range range = start..end;

        // Act
        textEditor.Modify(source, sectionName, range);

        // Assert
        await Assert.That(source.Text).IsEqualTo(expectedSource.Text);
    }

    [Test]
    [MethodDataSource(typeof(ModifyBoldMultiLineDataSources), nameof(ModifyBoldMultiLineDataSources.DataSources))]
    public async Task Modify_WholeRange(string sectionName, string input, string expected) {
        // Arrange
        var source = new TextSource { Text = input };
        var expectedSource = new TextSource { Text = expected };
        Range range = ..input.Length;

        // Act
        textEditor.Modify(source, sectionName, range);

        // Assert
        await Assert.That(source.Text).IsEqualTo(expectedSource.Text);
    }

    [Test]
    [MethodDataSource(typeof(ModifyItalicMultiLineDataSources), nameof(ModifyItalicMultiLineDataSources.DataSources))]
    public async Task Italic_DoesNotWork(string sectionName, string input, string _) {
        // Arrange
        var source = new TextSource { Text = input };
        var expectedSource = new TextSource { Text = input };
        Range range = ..input.Length;

        // Act
        textEditor.Modify(source, sectionName, range);

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
        var source = new TextSource { Text = original };
        Range range = start..end;

        // Act
        textEditor.Insert(source, input, range);

        // Assert
        await Assert.That(source.Text).IsEqualTo(expected);
    }
}

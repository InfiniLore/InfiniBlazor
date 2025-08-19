// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.TextEditor;
using Tests.InfiniLore.InfiniBlazor.DataSources;
using Tests.InfiniLore.InfiniBlazor.Markdown._DataSources;
using Tests.Shared.Infinilore.InfiniBlazor;

namespace Tests.InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class MarkdownParserTests(IMarkdownParser simpleParser) {
    
    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task TryParse_String_ValidInputs(MdTestData data) {
        // Arrange
        
        // Act
        var output = simpleParser.ParseToString(data.Markdown);

        // Assert;
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo(data.ExpectedStringOutput).IgnoringWhitespace();
    }
    
    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task TryParse_TextSource_ValidInputs(MdTestData data) {
        // Arrange
        var textSource = new TextSource {
            Text = data.Markdown
        };

        // Act
        var output = simpleParser.ParseToString(textSource.Text);

        // Assert
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo(data.ExpectedStringOutput).IgnoringWhitespace();
    }
}

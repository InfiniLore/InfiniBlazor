// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.TextEditor;
using Tests.InfiniLore.InfiniBlazor.DataSources;

namespace Tests.InfiniLore.InfiniBlazor.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class MarkdownParserTests(IMarkdownParser<string, string> parser, IMarkdownParser<ITextSource, string> textSourceParser) {
    
    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task TryParse_String_ValidInputs(MdTestData data) {
        // Arrange

        // Act
        string? output = parser.TryParse(data.Markdown);

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
        string? output = textSourceParser.TryParse(textSource);

        // Assert
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo(data.ExpectedStringOutput).IgnoringWhitespace();
    }
}

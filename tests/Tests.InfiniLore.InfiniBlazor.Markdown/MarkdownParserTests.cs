// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.TextEditor;
using Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
using Tests.Shared.Infinilore.InfiniBlazor;

namespace Tests.InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class MarkdownParserTests(IMarkdownParser parser) {
    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task FromMarkdownString_ToMarkdownString_ShouldBeSame(OldMdTestData data) {
        // Arrange
        string input = data.Markdown.ReplaceLineEndings("\n");
        string expectedOutput = data.Markdown.ReplaceLineEndings("\n");
        
        // Act
        using IMdSyntaxTree tree = parser.MarkdownString.SerializeToSyntaxTree(input);
        string output = parser.MarkdownString.DeserializeToString(tree);
        
        // Assert
        await Assert.That(output).IsNotNullOrWhitespace().IsEqualTo(expectedOutput);
    }
    
    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task TryParse_String_ValidInputs(OldMdTestData data) {
        // Arrange
        
        // Act
        var output = parser.FromMarkdownStringToHtmlString(data.Markdown);

        // Assert;
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo(data.ExpectedStringOutput).IgnoringWhitespace();
    }
    
    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task TryParse_TextSource_ValidInputs(OldMdTestData data) {
        // Arrange
        var textSource = new TextSource {
            Text = data.Markdown
        };

        // Act
        var output = parser.FromMarkdownStringToHtmlString(textSource.Text);

        // Assert
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo(data.ExpectedStringOutput).IgnoringWhitespace();
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Ganss.Xss;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.TextEditor;
using Microsoft.Extensions.DependencyInjection;
using Tests.InfiniLore.InfiniBlazor.DataSources;

namespace Tests.InfiniLore.InfiniBlazor.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class SanitizedMarkdownParserTests(IHtmlSanitizer sanitizer, IServiceProvider provider) {
    private readonly IMarkdownParser<string, string> parser = provider.GetRequiredKeyedService<IMarkdownParser<string, string>>("sanitized");
    private readonly IMarkdownParser<ITextSource, string> textSourceParser = provider.GetRequiredKeyedService<IMarkdownParser<ITextSource, string>>("sanitized");

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task TryParse_String_ValidInputs(MdTestData data) {
        // Arrange
        string sanitizedOutput = sanitizer.Sanitize(data.ExpectedStringOutput);

        // Act
        string? output = parser.TryParse(data.Markdown);

        // Assert
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo(sanitizedOutput).IgnoringWhitespace();
    }
    
    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task TryParse_TextSource_ValidInputs(MdTestData data) {
        // Arrange
        string sanitizedOutput = sanitizer.Sanitize(data.ExpectedStringOutput);
        var textSource = new TextSource {
            Text = data.Markdown
        };

        // Act
        string? output = textSourceParser.TryParse(textSource);

        // Assert
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo(sanitizedOutput).IgnoringWhitespace();
    }
}

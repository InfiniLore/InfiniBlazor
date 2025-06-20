// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Ganss.Xss;
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.Extensions.DependencyInjection;
using Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser;
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
    public async Task TryParse_String_ValidInputs(MarkdownTestDto dto) {
        // Arrange
        string sanitizedOutput = sanitizer.Sanitize(dto.ExpectedStringOutput);

        // Act
        string? output = parser.TryParse(dto.Markdown);

        // Assert
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo(sanitizedOutput).IgnoringWhitespace();
    }
    
    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task TryParse_TextSource_ValidInputs(MarkdownTestDto dto) {
        // Arrange
        string sanitizedOutput = sanitizer.Sanitize(dto.ExpectedStringOutput);
        var textSource = new TextSource {
            Text = dto.Markdown
        };

        // Act
        string? output = textSourceParser.TryParse(textSource);

        // Assert
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo(sanitizedOutput).IgnoringWhitespace();
    }
}

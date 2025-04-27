// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Ganss.Xss;
using InfiniLore.InfiniBlazor.Markdown;
using Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.MarkdownParser;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[SanitizedDiDataSource]
public class SanitizedMarkdownParserTests(IHtmlSanitizer sanitizer, IMarkdownParser<string, string> parser, IMarkdownParser<ITextSource, string> textSourceParser) {

    [Test]
    [MethodDataSource(typeof(MarkdownDataSources), nameof(MarkdownDataSources.DataSources))]
    public async Task TryParse_String_ValidInputs(MarkdownTestDto dto) {
        // Arrange
        string sanitizedOutput = sanitizer.Sanitize(dto.ExpectedStringOutput);

        // Act
        string? output = await parser.TryParseAsync(dto.Markdown);

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
        string? output = await textSourceParser.TryParseAsync(textSource);

        // Assert
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo(sanitizedOutput).IgnoringWhitespace();
    }
}

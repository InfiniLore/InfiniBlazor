// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Html;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using InfiniLore.InfiniBlazor.Markdown.Syntax.Nodes;
using Tests.InfiniBlazor.Core.Markdown.DataSources;
using Tests.InfiniBlazor.Shared;
using Tests.InfiniBlazor.Shared.Markdown;

namespace Tests.InfiniBlazor.Core.Markdown.Parsers.Html;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InfiniBlazorDiDataSource]
public class ToHtmlTests(IHtmlMdSyntaxTreeParser htmlMdSyntaxTreeParser) {

    [Test]
    [MethodDataSource<MdTestDataSources>(nameof(MdTestDataSources.GetBlankTest))]
    [MethodDataSource(typeof(MdTestDataSources), nameof(MdTestDataSources.GetTestDataAsync))]
    public async Task ToHtml_ShouldReturnExpected(MdTestData data) {
        // Arrange
        IMdSyntaxTree tree = data.MdSyntaxTree;
        string? expectedOutput = data.ExpectedHtmlString;

        // Act
        string output = await htmlMdSyntaxTreeParser.DeserializeToStringAsync(tree);

        // Assert
        await Assert.That(output)
            .IsNotNullOrWhitespace()
            .IsEqualTo(expectedOutput);
    }
}

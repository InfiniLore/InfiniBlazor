// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazorTests.Core.Markdown.DataSources;
using InfiniBlazorTests.Shared;
using InfiniBlazorTests.Shared.Markdown;

namespace InfiniBlazorTests.Core.Markdown.Parsers.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InfiniBlazorDiDataSource]
public class FromMarkdownTests(IMarkdownParser parser) {

    [Test]
    [MethodDataSource<MdTestDataSources>(nameof(MdTestDataSources.GetBlankTest))]
    [MethodDataSource(typeof(MdTestDataSources), nameof(MdTestDataSources.GetTestDataAsync))]
    public async Task FromMarkdown_ToSyntaxTree_ShouldBeSame(MdTestData data) {
        // Arrange
        string input = data.MdString;
        IMdSyntaxTree expectedOutput = data.MdSyntaxTree;

        // Act
        IMdSyntaxTree foundTree = parser.Markdown.SerializeToSyntaxTree(input);

        // Assert
        await Assert.That(foundTree).IsNotNull();
        await Assert.That(foundTree).IsEquatableTo(expectedOutput);
    }
}

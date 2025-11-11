// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Tests.InfiniBlazor.Core.Markdown.DataSources;
using Tests.InfiniBlazor.Shared;
using Tests.InfiniBlazor.Shared.Markdown;

namespace Tests.InfiniBlazor.Core.Markdown.Parsers.Markdown;
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
        await Assert.That(foundTree)
            .IsNotNull()
            .And.IsEqualTo(expectedOutput);
    }
}

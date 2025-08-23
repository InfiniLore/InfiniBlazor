// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
using Tests.Shared.Infinilore.InfiniBlazor;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class MdTestDataTests(IMarkdownParser parser) {
    
    [Test]
    [MethodDataSource(typeof(MdTestDataSources), nameof(MdTestDataSources.GetAsyncTestData))]
    public async Task FromMarkdownString_ToSyntaxTree_ShouldBeSame(MdTestData data) {
        // Arrange
        string input = data.MdString;
        IMdSyntaxTree expectedOutput = data.MdSyntaxTree;

        // Act
        IMdSyntaxTree foundTree = parser.MarkdownString.SerializeToSyntaxTree(input);

        // Assert
        await Assert.That(foundTree).IsEquivalentTo(expectedOutput);

    }
}

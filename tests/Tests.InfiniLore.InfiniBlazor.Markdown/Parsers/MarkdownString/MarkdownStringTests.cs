// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
using Tests.Shared.Infinilore.InfiniBlazor;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class MarkdownStringTests(IMarkdownParser parser) {

    [Test]
    [MethodDataSource<MdTestDataSources>(nameof(MdTestDataSources.GetBlankTest))]
    [MethodDataSource<MdTestDataSources>(nameof(MdTestDataSources.GetTestData))]
    // [MethodDataSource(typeof(MdTestDataSources), nameof(MdTestDataSources.GetTestDataAsync))] // TODO: Use this when TUnit fixes https://github.com/thomhurst/TUnit/issues/2990
    public async Task FromMarkdownString_ToSyntaxTree_ShouldBeSame(MdTestData data) {
        // Arrange
        string input = data.MdString;
        IMdSyntaxTree expectedOutput = data.MdSyntaxTree;

        // Act
        IMdSyntaxTree foundTree = parser.MarkdownString.SerializeToSyntaxTree(input);

        // Assert
        await Assert.That(foundTree)
            .IsNotNull()
            .IsEquivalentTo(expectedOutput);
    }
    
    [Test]
    [MethodDataSource<MdTestDataSources>(nameof(MdTestDataSources.GetBlankTest))]
    [MethodDataSource<MdTestDataSources>(nameof(MdTestDataSources.GetTestData))]
    // [MethodDataSource(typeof(MdTestDataSources), nameof(MdTestDataSources.GetTestDataAsync))] // TODO: Use this when TUnit fixes https://github.com/thomhurst/TUnit/issues/2990
    public async Task FromSyntaxTree_ToMarkdownString_ShouldBeExpected(MdTestData data) {
        // Arrange
        IMdSyntaxTree input = data.MdSyntaxTree;
        string? expectedOutput = data.ExpectedMarkdownString;
        Skip.When(expectedOutput is null, "The expected output is null.");

        // Act
        string foundOutput = parser.MarkdownString.DeserializeToString(input);

        // Assert
        await Assert.That(foundOutput)
            .IsNotNull()
            .IsEquivalentTo(expectedOutput);
    }
}

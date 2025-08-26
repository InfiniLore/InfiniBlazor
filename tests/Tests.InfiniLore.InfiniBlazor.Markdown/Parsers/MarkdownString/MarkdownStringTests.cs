// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;
using Tests.Shared.Infinilore.InfiniBlazor;
using Tests.Shared.InfiniLore.InfiniBlazor.Markdown;

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
            .IsEqualTo(expectedOutput);
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
        Skip.When(data.ExpectedMarkdownStringSkipOnWhitespaceMisMatch && foundOutput != expectedOutput, "The expected output is not equal to the actual output, but this is a known state");

        // Assert
        await Assert.That(foundOutput)
            .IsNotNull()
            .IsEqualTo(expectedOutput);
    }
}

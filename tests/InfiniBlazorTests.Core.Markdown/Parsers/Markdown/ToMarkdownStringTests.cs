// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazorTests.Core.Markdown.DataSources;
using InfiniBlazorTests.Shared.Markdown;

namespace InfiniBlazorTests.Core.Markdown.Parsers.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InfiniBlazorMarkdownDIDataSource]
public class ToMarkdownTests(IMarkdownParser parser) {
    [Test]
    [MethodDataSource<MdTestDataSources>(nameof(MdTestDataSources.GetBlankTest))]
    [MethodDataSource(typeof(MdTestDataSources), nameof(MdTestDataSources.GetTestDataAsync))]
    public async Task FromSyntaxTree_ToMarkdown_ShouldBeExpected(MdTestData data) {
        // Arrange
        IMdSyntaxTree input = data.MdSyntaxTree;
        string? expectedOutput = data.ExpectedMarkdown;
        Skip.When(expectedOutput is null, "The expected output is null.");

        // Act
        string foundOutput = parser.Markdown.DeserializeToString(input);
        Skip.When(data.ExpectedMarkdownSkipOnWhitespaceMisMatch && foundOutput != expectedOutput, "The expected output is not equal to the actual output, but this is a known state");

        // Assert
        await Assert.That(foundOutput)
            .IsNotNull()
            .And.IsEqualTo(expectedOutput);
    }
}

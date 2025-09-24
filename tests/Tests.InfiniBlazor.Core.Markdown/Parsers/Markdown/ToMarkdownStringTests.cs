// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using Tests.InfiniBlazor.Core.Markdown.DataSources;
using Tests.InfiniBlazor.Shared;
using Tests.InfiniBlazor.Shared.Markdown;

namespace Tests.InfiniBlazor.Core.Markdown.Parsers.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InfiniBlazorDiDataSource]
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
            .IsEqualTo(expectedOutput);
    }
}

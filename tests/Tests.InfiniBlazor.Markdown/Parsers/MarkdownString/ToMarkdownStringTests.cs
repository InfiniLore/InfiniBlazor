// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using Tests.InfiniBlazor.Markdown.DataSources;
using Tests.InfiniBlazor.Shared.Markdown;
using Tests.Shared.Infinilore.InfiniBlazor;

namespace Tests.InfiniBlazor.Markdown.Parsers.MarkdownString;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class ToMarkdownStringTests(IMarkdownParser parser) {
    [Test]
    [MethodDataSource<MdTestDataSources>(nameof(MdTestDataSources.GetBlankTest))]
    [MethodDataSource(typeof(MdTestDataSources), nameof(MdTestDataSources.GetTestDataAsync))]
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

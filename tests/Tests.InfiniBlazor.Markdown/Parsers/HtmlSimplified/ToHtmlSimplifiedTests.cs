// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using Tests.InfiniBlazor.Markdown.DataSources;
using Tests.InfiniBlazor.Shared.Markdown;
using Tests.Shared.Infinilore.InfiniBlazor;

namespace Tests.InfiniBlazor.Markdown.Parsers.HtmlSimplified;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class ToHtmlSimplifiedTests(IMarkdownParser parser) {
    [Test]
    [MethodDataSource<MdTestDataSources>(nameof(MdTestDataSources.GetBlankTest))]
    [MethodDataSource(typeof(MdTestDataSources), nameof(MdTestDataSources.GetTestDataAsync))]
    public async Task FromSyntaxTree_ToHtmlSimplified_ShouldBeExpected(MdTestData data) {
        // Arrange
        IMdSyntaxTree input = data.MdSyntaxTree;
        string? expectedOutput = data.ExpectedHtmlStringSimplified;
        Skip.When(expectedOutput is null, "The expected output is null.");

        // Act
        string foundOutput = parser.HtmlString.DeserializeToString(input);

        // Assert
        await Assert.That(foundOutput)
            .IsNotNull()
            .IsEqualTo(expectedOutput);
    }
}

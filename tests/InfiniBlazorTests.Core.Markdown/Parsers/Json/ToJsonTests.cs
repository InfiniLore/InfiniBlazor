// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Markdown;
using InfiniBlazor.Markdown.Syntax;
using InfiniBlazorTests.Core.Markdown.DataSources;
using InfiniBlazorTests.Shared;
using InfiniBlazorTests.Shared.Markdown;

namespace InfiniBlazorTests.Core.Markdown.Parsers.Json;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InfiniBlazorDiDataSource]
public class ToJsonTests(IMarkdownParser parser) {
    [Test]
    [MethodDataSource<MdTestDataSources>(nameof(MdTestDataSources.GetBlankTest))]
    [MethodDataSource(typeof(MdTestDataSources), nameof(MdTestDataSources.GetTestDataAsync))]
    public async Task FromSyntaxTree_ToJson_ShouldBeExpected(MdTestData data) {
        // Arrange
        IMdSyntaxTree input = data.MdSyntaxTree;
        string? expectedOutput = data.ExpectedJsonString?.ReplaceLineEndings("\n");
        Skip.When(expectedOutput is null, "The expected output is null.");

        // Act
        string foundOutput = parser.Json.DeserializeToString(input);
        string foundOutputNormalized = foundOutput.ReplaceLineEndings("\n");
        
        // Assert
        await Assert.That(foundOutputNormalized)
            .IsNotNull()
            .And.IsEqualTo(expectedOutput);
    }
}

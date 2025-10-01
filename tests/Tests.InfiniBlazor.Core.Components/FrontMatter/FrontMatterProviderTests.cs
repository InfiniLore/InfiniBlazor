// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components;
using InfiniLore.InfiniBlazor.Components.FrontMatter;
using Tests.InfiniBlazor.Shared;

namespace Tests.InfiniBlazor.Core.Components.FrontMatter;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InfiniBlazorDiDataSource]
public class FrontMatterProviderTests(IFrontMatterProvider provider) {
    public record TestData(bool ExpectedResult, string Input, string? Lang, IEnumerable<IFrontMatterEntry>? ExpectedEntries);

    public static IEnumerable<Func<TestData>> GetTestData() {
        yield return () => new TestData(
            false,
            "",
            null,
            null
        );
        
        yield return () => new TestData(
            true,
            "name: Anna",
            null,
            [
                new FrontMatterEntryData("name", "Anna")
            ]
        );
        
        yield return () => new TestData(
            true,
            "name: Anna",
            "yaml",
            [
                new FrontMatterEntryData("name", "Anna")
            ]
        );
        
        yield return () => new TestData(
            true,
            """
            name: Anna
            gender: female
            """,
            "yml",
            [
                new FrontMatterEntryData("name", "Anna"),
                new FrontMatterEntryData("gender", "female"),
            ]
        );
        
        yield return () => new TestData(
            true,
            """
            "name": "Anna"
            """,
            "json",
            [
                new FrontMatterEntryData("name", "Anna")
            ]
        );
        
        yield return () => new TestData(
            true,
            """
            {
                "name": "Anna"
            }
            """,
            "json",
            [
                new FrontMatterEntryData("name", "Anna")
            ]
        );
        
        yield return () => new TestData(
            true,
            """
            "name": "Anna",
            "gender": "female"
            """,
            "json",
            [
                new FrontMatterEntryData("name", "Anna"),
                new FrontMatterEntryData("gender", "female")
            ]
        );
        
        yield return () => new TestData(
            true,
            """
            {
                "name": "Anna",
                "gender": "female"
            }
            """,
            "json",
            [
                new FrontMatterEntryData("name", "Anna"),
                new FrontMatterEntryData("gender", "female")
            ]
        );
    }
    
    [Test]
    [MethodDataSource(nameof(GetTestData))]
    public async Task TryParse_ShouldReturnExpected(TestData testData) {
        // Arrange
        bool expectedResult = testData.ExpectedResult;
        string input = testData.Input;
        string? lang = testData.Lang;
        IEnumerable<IFrontMatterEntry>? expectedEntries = testData.ExpectedEntries;
        
        // Act
        bool result = provider.TryParse(input, lang, out IEnumerable<IFrontMatterEntry>? entries);

        // Assert
        await Assert.That(result).IsEqualTo(expectedResult);
        await Assert.That(entries).IsEquivalentTo(expectedEntries);
    }
}

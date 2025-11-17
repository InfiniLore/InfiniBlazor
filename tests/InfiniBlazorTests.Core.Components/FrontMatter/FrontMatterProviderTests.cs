// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor;
using InfiniBlazorTests.Shared;

namespace InfiniBlazorTests.Core.Components.FrontMatter;

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
        IFrontMatterEntry[]? expectedEntries = testData.ExpectedEntries as IFrontMatterEntry[] ?? testData.ExpectedEntries?.ToArray();
        
        // Act
        bool result = provider.TryParse(input, lang, out IEnumerable<IFrontMatterEntry>? entries);
        IFrontMatterEntry[]? entriesArray = entries as IFrontMatterEntry[] ?? entries?.ToArray();

        // Assert
        await Assert.That(result).IsEqualTo(expectedResult);
        if (result) await Assert.That(entriesArray).IsNotNull();

        for (int i = 0; i < entriesArray?.Length; i++) {
            IFrontMatterEntry? expected = expectedEntries?.ElementAt(i);
            IFrontMatterEntry found = entriesArray.ElementAt(i);
            await Assert.That(found.Key).IsEqualTo(expected?.Key);
            await Assert.That(found.Value?.ToString()).IsEqualTo(expected?.Value?.ToString());
        }
    }
}

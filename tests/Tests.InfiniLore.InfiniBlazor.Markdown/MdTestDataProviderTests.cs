// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Tests.Shared.Infinilore.InfiniBlazor;

namespace Tests.InfiniLore.InfiniBlazor.Markdown.DataSources;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdTestDataProviderTests {
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    public async Task TryGetFileNames_ReturnsExpected() {
        // Arrange
        
        // Act
        string[]? fileNames = MdTestDataProvider.TestInstance.TryGetFileNames();
        
        // Assert
        await Assert.That(fileNames)
            .IsNotNull()
            .HasCount().GreaterThanOrEqualTo(2)
            .Contains(filePath => Path.GetFileName(filePath) == "bold.xml");
    }

    [Test]
    [Arguments("bold.xml", 1)]
    [Arguments("italic.xml", 1)]
    public async Task TryGetXmlMdTestDataAsync_ReturnsDataSet(string fileName, int minimumExpectedCount) {
        // Arrange
        
        // Act
        List<MdTestData>? data = await MdTestDataProvider.TestInstance.TryGetXmlMdTestDataAsync(fileName);

        // Assert
        await Assert.That(data)
            .IsNotNull()
            .HasCount().GreaterThanOrEqualTo(minimumExpectedCount);
    }
    
}

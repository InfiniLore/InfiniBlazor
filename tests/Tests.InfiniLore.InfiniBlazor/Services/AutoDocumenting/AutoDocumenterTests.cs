// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.AutoDocumenting;
using Microsoft.AspNetCore.Components;
using Tests.Shared.InfiniLore.InfiniBlazor.AutoDocumenting;
using Tests.Shared.InfiniLore.InfiniBlazor.AutoDocumenting.DataSources;

namespace Tests.InfiniLore.InfiniBlazor.Services.AutoDocumenting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class AutoDocumenterTests {
    private AutoDocumenter AutoDocumenter { get; } = new();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Test Data
    // -----------------------------------------------------------------------------------------------------------------
    public static IEnumerable<Func<AutoDocumentTestData>> GetRenderFragments() {
        yield return ParagraphTest.GetDefault;
        yield return ParagraphTest.GetEmpty;
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Test Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    [MethodDataSource(nameof(GetRenderFragments))]
    public async Task Render_ReturnsExpected(AutoDocumentTestData testData) {
        // Arrange
        RenderFragment renderFragment = testData.RenderFragment;
        string expectedOutput = testData.ExpectedOutput;
        
        // Act
        string result = AutoDocumenter.ConvertToString(renderFragment);

        // Assert
        await Assert.That(result).IsEqualTo(expectedOutput);
    } 
}

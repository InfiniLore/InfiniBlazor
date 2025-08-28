// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.AutoDocumenting;
using Microsoft.AspNetCore.Components;
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
    public static IEnumerable<Func<(RenderFragment, string)>> GetRenderFragments() {
        yield return () => (
            builder => {
                builder.OpenComponent(0, typeof(ParagraphTest));
                builder.CloseComponent();
            },
            """
            <ParagraphTest>
                <p>Something</p>
                <p>Else</p>
            </ParagraphTest>
            """
        );
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Test Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    [MethodDataSource(nameof(GetRenderFragments))]
    public async Task Render_ReturnsExpected(RenderFragment renderFragment, string expected) {
        // Arrange
        
        // Act
        string result = AutoDocumenter.ConvertToString(renderFragment);

        // Assert
        await Assert.That(result).IsEqualTo(expected);
    } 
}

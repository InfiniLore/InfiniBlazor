// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.AutoDocumentation;
using Tests.InfiniBlazor.Extensions.AutoDocumentation.DataSources;
using Tests.InfiniBlazor.Extensions.AutoDocumentation.DataSources.CodeFiles;

namespace Tests.InfiniBlazor.Extensions.AutoDocumentation;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class AutoDocumentationProviderTests {
    public AutoDocumentationProvider Provider { get; } = new([new AutoDocumenterData_TestsInfiniBlazorExtensionsAutoDocumentation()]);


    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    [MethodDataSource<ClassTestData>(nameof(ClassTestData.GetTestData))]
    [MethodDataSource<PropertyTestData>(nameof(PropertyTestData.GetTestData))]
    public async Task ShouldGetValidFragment(AutoDocumentationTestData testData) {
        // Arrange
        (string id, bool expectedResult, IAutoDocumentationFragment? expectedFragment) = testData;
        
        // Act
        bool result = Provider.TryGetDocumentationFragment(id, out IAutoDocumentationFragment? fragment);

        // Assert
        await Assert.That(result).IsEqualTo(expectedResult);
        await Assert.That(fragment).IsEquivalentTo(expectedFragment);
    }
}

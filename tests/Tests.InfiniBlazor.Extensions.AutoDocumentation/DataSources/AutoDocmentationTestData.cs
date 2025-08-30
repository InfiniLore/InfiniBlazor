// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.AutoDocumentation;

namespace Tests.InfiniBlazor.Extensions.AutoDocumentation.DataSources;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record AutoDocmentationTestData(
    string Id, 
    bool ExpectedResult, 
    IAutoDocumentationFragment? ExpectedFragment
);

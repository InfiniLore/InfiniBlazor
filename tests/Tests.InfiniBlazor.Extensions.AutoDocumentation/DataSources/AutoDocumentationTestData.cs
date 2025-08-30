// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.AutoDocumentation;

namespace Tests.InfiniBlazor.Extensions.AutoDocumentation.DataSources;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record AutoDocumentationTestData(
    string Id, 
    bool ExpectedResult, 
    IAutoDocumentationFragment? ExpectedFragment
);

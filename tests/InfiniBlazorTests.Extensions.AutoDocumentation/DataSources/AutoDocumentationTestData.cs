// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.AutoDocumentation;

namespace InfiniBlazorTests.Extensions.AutoDocumentation.DataSources;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record AutoDocumentationTestData(
    string Id, 
    bool ExpectedResult, 
    IAutoDocumentationFragment? ExpectedFragment
);

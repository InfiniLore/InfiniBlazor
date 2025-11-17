// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniBlazor.AutoDocumentation;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IAutoDocumentationProvider {
    bool TryGetDocumentationFragment(string id,[NotNullWhen(true)] out IAutoDocumentationFragment? fragment);
}

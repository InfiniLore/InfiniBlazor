// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;
using System.Collections.Immutable;

namespace InfiniBlazor.AutoDocumentation;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IAutoDocumenterData {
     Lazy<FrozenDictionary<string, ImmutableArray<string>>> CsharpData { get; } 
     Lazy<FrozenDictionary<string, ImmutableArray<string>>> RazorData { get; } 
}

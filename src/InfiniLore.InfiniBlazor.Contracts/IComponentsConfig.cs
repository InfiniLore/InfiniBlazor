// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Immutable;
using System.Reflection;

namespace InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IComponentsConfig {
    ImmutableArray<string> GetEmoteJsonLibFilePaths();
    bool TryGetEmbeddedResourceAssemblies(out ImmutableArray<Assembly> assemblies);
}

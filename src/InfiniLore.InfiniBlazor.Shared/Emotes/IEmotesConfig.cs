// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Immutable;
using System.Reflection;

namespace InfiniLore.InfiniBlazor.Emotes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IEmotesConfig {
    ImmutableArray<string> GetEmoteJsonLibFilePaths();
    bool TryGetEmbeddedResourceAssemblies(out ImmutableArray<Assembly> assemblies);
}

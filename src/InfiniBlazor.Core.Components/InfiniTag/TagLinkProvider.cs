// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Diagnostics.CodeAnalysis;

namespace InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITagLinkProvider>]
public class TagLinkProvider : ITagLinkProvider {
    
    public bool TryGetLink(string tag, [NotNullWhen(true)] out string? link) {
        link = null;
        return false;
    }
}

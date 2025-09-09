// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Components.Tags;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Components;

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

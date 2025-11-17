// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniBlazor.Config;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ICodeBlockLanguageHandlerProvider>]
public class CodeBlockLanguageHandlerProvider(IComponentsConfig componentsConfig) : ICodeBlockLanguageHandlerProvider {
    private FrozenDictionary<string, Type> Handlers { get; } = componentsConfig.GetCodeBlockLanguageHandlers();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetComponentType(string? language, [NotNullWhen(true)] out Type? type) {
        type = null;
        return language.IsNotNullOrWhiteSpace() 
            && Handlers.TryGetValue(language, out type);
    }
}

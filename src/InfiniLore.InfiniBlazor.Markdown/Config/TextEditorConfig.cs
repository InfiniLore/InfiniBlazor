// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using Microsoft.Extensions.DependencyInjection;

namespace InfiniLore.InfiniBlazor.Markdown.Config;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class TextEditorConfig(IInfiniBlazorConfig config, object? key) {
    private static readonly HashSet<(Type Type, object? Key)> AllRegisteredTypes = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public TextEditorConfig AddModifier<TModifier>() where TModifier : class, ITextModifier {
        (Type, object? key) registrationKey = (typeof(TModifier), key);
        if (AllRegisteredTypes.Add(registrationKey)) {
            config.Services.AddKeyedSingleton<ITextModifier, TModifier>(key);
        }
        return this;
    }

    public TextEditorConfig AddDefaultModifiers() {
        if (key is null) return this;
        List<Type> servicesToAdd = config.Services
            .Where(d => d is { IsKeyedService: false, ImplementationType: not null })
            .Where(d => typeof(ITextModifier).IsAssignableFrom(d.ServiceType))
            .Select(d => d.ImplementationType!)
            .Where(type => AllRegisteredTypes.Add((type, key)))
            .ToList();

        foreach (Type implType in servicesToAdd) {
            config.Services.AddKeyedSingleton(typeof(ITextModifier), key, implType);
        }

        return this;
    }
}

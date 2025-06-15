// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Toasting;

namespace InfiniLore.InfiniBlazor.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class FrozenToastingConfig : IToastingConfig {
    public required int AutoRemoveDuration { get; init; }
    public required IReadOnlyDictionary<string, Type> AppearanceComponentMapping { get; init; }
}

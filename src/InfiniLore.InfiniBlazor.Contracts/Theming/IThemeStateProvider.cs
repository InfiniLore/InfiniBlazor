// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IThemeStateProvider {
    event Action? OnChanged;
    event Func<Task>? OnChangedAsync;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    IThemeState GetState();
    
    ValueTask<bool> TrySelectCollectionAsync(string themeName, CancellationToken ct = default);
    ValueTask<bool> TrySelectModeAsync(string modeName, CancellationToken ct = default);
    ValueTask<bool> TrySelectNextModeAsync(CancellationToken ct = default);

    ValueTask<IThemeCollection?> TryGetCollectionAsync(string themeName, CancellationToken ct = default);
}

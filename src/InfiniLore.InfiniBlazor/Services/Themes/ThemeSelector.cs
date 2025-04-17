// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Themes;

namespace InfiniLore.InfiniBlazor.Services.Themes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IThemeSelector>]
public class ThemeSelector : IThemeSelector {
    public IInfiniLoreTheme CurrentTheme { get; set; } = new DefaultTheme();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void SelectTheme(string themeName) {
        throw new NotImplementedException();
    }
}

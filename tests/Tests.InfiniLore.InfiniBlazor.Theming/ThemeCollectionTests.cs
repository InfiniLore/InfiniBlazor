// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;
using InfiniLore.InfiniBlazor.Theming.Library;
using Microsoft.Extensions.DependencyInjection;
using Tests.InfiniLore.InfiniBlazor.Theming.DataSources;

namespace Tests.InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[DiDataSource]
public class ThemeCollectionTests(IServiceProvider provider) {
    
    [Test]
    [Arguments(typeof(InfiniBlazorThemeCollection), null)] // default
    [Arguments(typeof(AnnaSasDevThemeCollection), "anna")]
    public async Task RegisteredThemes_ShouldExist(Type themeType, object? themeName){
        // Arrange
        
        // Act
        var theme = provider.GetKeyedService<IThemeCollection>(themeName);
        
        // Assert
        await Assert.That(theme).IsNotNull().And.IsTypeOf(themeType);
    }
    
    [Test]
    [Arguments(typeof(InfiniBlazorThemeCollection), null)] // default
    [Arguments(typeof(AnnaSasDevThemeCollection), "anna")]
    public async Task Theme_ShouldHaveDefaultModes_ByThemeMode(Type themeType, object? themeName){
        // Arrange
        var theme = provider.GetKeyedService<IThemeCollection>(themeName);
        ITheme? lightModeTheme = null;
        ITheme? darkModeTheme = null;
        
        // Act
        bool? lightModeResult = theme?.TryGetMode(ThemeMode.LightMode, out lightModeTheme);
        bool? darkModeResult = theme?.TryGetMode(ThemeMode.DarkMode, out darkModeTheme);
        
        // Assert
        await Assert.That(theme).IsNotNull().And.IsTypeOf(themeType);
        await Assert.That(lightModeResult).IsNotNull().And.IsTrue();
        await Assert.That(lightModeTheme).IsNotNull();
        await Assert.That(darkModeResult).IsNotNull().And.IsTrue();
        await Assert.That(darkModeTheme).IsNotNull();
    }
    
    [Test]
    [Arguments(typeof(InfiniBlazorThemeCollection), null)] // default
    [Arguments(typeof(AnnaSasDevThemeCollection), "anna")]
    public async Task Theme_ShouldHaveDefaultModes_ByString(Type themeType, object? themeName){
        // Arrange
        var theme = provider.GetKeyedService<IThemeCollection>(themeName);
        ITheme? lightModeTheme = null;
        ITheme? darkModeTheme = null;
        
        // Act
        bool? lightModeResult = theme?.TryGetModeByName(ThemeMode.LightMode.Name, out lightModeTheme);
        bool? darkModeResult = theme?.TryGetModeByName(ThemeMode.DarkMode.Name, out darkModeTheme);
        
        // Assert
        await Assert.That(theme).IsNotNull().And.IsTypeOf(themeType);
        await Assert.That(lightModeResult).IsNotNull().And.IsTrue();
        await Assert.That(lightModeTheme).IsNotNull();
        await Assert.That(darkModeResult).IsNotNull().And.IsTrue();
        await Assert.That(darkModeTheme).IsNotNull();
    }
    
    [Test]
    [Arguments(typeof(InfiniBlazorThemeCollection), null)] // default
    [Arguments(typeof(AnnaSasDevThemeCollection), "anna")]
    public async Task Theme_ShouldHaveDefaultModes_ByReadonlySpan(Type themeType, object? themeName){
        // Arrange
        var theme = provider.GetKeyedService<IThemeCollection>(themeName);
        ITheme? lightModeTheme = null;
        ITheme? darkModeTheme = null;
        ReadOnlySpan<char> lightModeName = ThemeMode.LightMode.Name;
        ReadOnlySpan<char> darkModeName = ThemeMode.DarkMode.Name;
        
        // Act
        bool? lightModeResult = theme?.TryGetModeByName(lightModeName, out lightModeTheme);
        bool? darkModeResult = theme?.TryGetModeByName(darkModeName, out darkModeTheme);
        
        // Assert
        await Assert.That(theme).IsNotNull().And.IsTypeOf(themeType);
        await Assert.That(lightModeResult).IsNotNull().And.IsTrue();
        await Assert.That(lightModeTheme).IsNotNull();
        await Assert.That(darkModeResult).IsNotNull().And.IsTrue();
        await Assert.That(darkModeTheme).IsNotNull();
    }
}

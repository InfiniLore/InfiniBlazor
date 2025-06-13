// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;
using InfiniLore.InfiniBlazor.Theming.Collections;

namespace Tests.InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ThemeCollectionTests {
    private static DefaultThemeCollection GetDefaultThemeCollection() => new();
    
    [Test]
    [Arguments(ThemeMode.LigtModeName, true)]
    [Arguments(ThemeMode.DarkModeName, true)]
    [Arguments("random-name", false)]
    public async Task TryGetCssData_ShouldReturnExpected(string modeName, bool expectedResult) {
        // Arrange
        DefaultThemeCollection collection = GetDefaultThemeCollection();
        
        // Act
        bool result = collection.TryGetCssData(modeName, out ICssData? cssData);
        
        // Assert
        await Assert.That(result).IsEqualTo(expectedResult);
        if (expectedResult) await Assert.That(cssData).IsNotNull();
        else await Assert.That(cssData).IsNull();
    }

    public static IEnumerable<Func<(string?, bool, ThemeMode)>> TryGetNextThemeMode_ShouldReturnExpected_DataSources() {
        yield return () => (null, true, ThemeMode.DarkMode);
        yield return () => ("", true, ThemeMode.DarkMode);
        yield return () => ("random-name", false, ThemeMode.Empty);
        yield return () => (ThemeMode.LigtModeName, true, ThemeMode.DarkMode);
        yield return () => (ThemeMode.DarkModeName, true, ThemeMode.LightMode);
    }
    
    [Test]
    [MethodDataSource(nameof(TryGetNextThemeMode_ShouldReturnExpected_DataSources))]
    public async Task TryGetNextThemeMode_ShouldReturnExpected(string? modeName, bool expectedResult, ThemeMode expectedMode) {
        // Arrange
        DefaultThemeCollection collection = GetDefaultThemeCollection();
        
        // Act
        bool result = collection.TryGetNextThemeMode(modeName, out ThemeMode mode);

        // Assert
        await Assert.That(result).IsEqualTo(expectedResult);
        await Assert.That(mode).IsEqualTo(expectedMode);
    }
}

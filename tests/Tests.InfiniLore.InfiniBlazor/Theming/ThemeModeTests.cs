// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;

namespace Tests.InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ThemeModeTests {
     [Test]
    public async Task LightMode_ShouldHaveCorrectProperties() {
        // Arrange & Act
        ThemeMode mode = ThemeMode.LightMode;

        // Assert
        await Assert.That(mode.Name).IsEqualTo("light-mode");
        await Assert.That(mode.Variant).IsEqualTo(ThemeModeVariants.Light);
    }

    [Test]
    public async Task DarkMode_ShouldHaveCorrectProperties() {
        // Arrange & Act
        ThemeMode mode = ThemeMode.DarkMode;

        // Assert
        await Assert.That(mode.Name).IsEqualTo("dark-mode");
        await Assert.That(mode.Variant).IsEqualTo(ThemeModeVariants.Dark);
    }

    [Test]
    public async Task Equals_WhenSameName_ShouldReturnTrue() {
        // Arrange
        var mode1 = new ThemeMode("test-mode", ThemeModeVariants.Dark);
        var mode2 = new ThemeMode("test-mode", ThemeModeVariants.Custom);

        // Act & Assert
        await Assert.That(mode1.Equals(mode2)).IsEqualTo(true);
    }

    [Test]
    public async Task Equals_WhenDifferentName_ShouldReturnFalse() {
        // Arrange
        var mode1 = new ThemeMode("mode1", ThemeModeVariants.Dark);
        var mode2 = new ThemeMode("mode2", ThemeModeVariants.Dark);

        // Act & Assert
        await Assert.That(mode1.Equals(mode2)).IsEqualTo(false);
    }

    [Test]
    public async Task GetHashCode_ShouldBeBasedOnName() {
        // Arrange
        var mode = new ThemeMode("test-mode", ThemeModeVariants.Dark);
        int expectedHash = "test-mode".GetHashCode();

        // Act & Assert
        await Assert.That(mode.GetHashCode()).IsEqualTo(expectedHash);
    }

    [Test]
    public async Task GetHashCode_SameNames_ShouldHaveSameHashCode() {
        // Arrange
        var mode1 = new ThemeMode("test-mode", ThemeModeVariants.Dark);
        var mode2 = new ThemeMode("test-mode", ThemeModeVariants.Light);

        // Act & Assert
        await Assert.That(mode1.GetHashCode()).IsEqualTo(mode2.GetHashCode());
    }

}

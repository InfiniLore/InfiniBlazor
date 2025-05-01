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
        IThemeMode lightMode = ThemeMode.LightMode;

        // Assert
        await Assert.That(lightMode.Name).IsEqualTo("light-mode");
        await Assert.That(lightMode.IsDark).IsEqualTo(false);
        await Assert.That(lightMode.IsLight).IsEqualTo(true);
    }

    [Test]
    public async Task DarkMode_ShouldHaveCorrectProperties() {
        // Arrange & Act
        IThemeMode darkMode = ThemeMode.DarkMode;

        // Assert
        await Assert.That(darkMode.Name).IsEqualTo("dark-mode");
        await Assert.That(darkMode.IsDark).IsEqualTo(true);
        await Assert.That(darkMode.IsLight).IsEqualTo(false);
    }

    [Test]
    public async Task Equals_WhenSameName_ShouldReturnTrue() {
        // Arrange
        var mode1 = new ThemeMode("test-mode", true, false);
        var mode2 = new ThemeMode("test-mode", false, true);

        // Act & Assert
        await Assert.That(mode1.Equals(mode2)).IsEqualTo(true);
    }

    [Test]
    public async Task Equals_WhenDifferentName_ShouldReturnFalse() {
        // Arrange
        var mode1 = new ThemeMode("mode1", true, false);
        var mode2 = new ThemeMode("mode2", true, false);

        // Act & Assert
        await Assert.That(mode1.Equals(mode2)).IsEqualTo(false);
    }

    [Test]
    public async Task Equals_WhenNull_ShouldReturnFalse() {
        // Arrange
        var mode = new ThemeMode("test-mode", true, false);

        // Act & Assert
        await Assert.That(mode.Equals(null)).IsEqualTo(false);
    }

    [Test]
    public async Task Equals_WhenSameReference_ShouldReturnTrue() {
        // Arrange
        var mode = new ThemeMode("test-mode", true, false);

        // Act & Assert
        await Assert.That(mode.Equals(mode)).IsEqualTo(true);
    }

    [Test]
    public async Task GetHashCode_ShouldBeBasedOnName() {
        // Arrange
        var mode = new ThemeMode("test-mode", true, false);
        int expectedHash = "test-mode".GetHashCode();

        // Act & Assert
        await Assert.That(mode.GetHashCode()).IsEqualTo(expectedHash);
    }

    [Test]
    public async Task GetHashCode_SameNames_ShouldHaveSameHashCode() {
        // Arrange
        var mode1 = new ThemeMode("test-mode", true, false);
        var mode2 = new ThemeMode("test-mode", false, true);

        // Act & Assert
        await Assert.That(mode1.GetHashCode()).IsEqualTo(mode2.GetHashCode());
    }

}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Testing.TUnit;
using InfiniLore.InfiniBlazor.Themes;
using InfiniLore.InfiniBlazor.Themes.Config;
using InfiniLore.InfiniBlazor.Themes.Library;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.InfiniLore.InfiniBlazor.Themes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ServiceCollectionTests {
    [Test]
    public async Task ShouldAddServices_AddThemes() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfiniBlazor(config => config.AddThemes());

        // Assert
        await Assert.That(services).ContainsServiceType<IThemeSelector>();
    }
    
    [Test]
    public async Task ShouldAddServices_RegisterTheme() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfiniBlazor(config => config.AddThemes(cfg => cfg.RegisterTheme<AnnaSasDevTheme>("anna")));

        // Assert
        await Assert.That(services).ContainsKeyedServiceImplementation<IInfiniLoreTheme, AnnaSasDevTheme>("anna");
    }
}

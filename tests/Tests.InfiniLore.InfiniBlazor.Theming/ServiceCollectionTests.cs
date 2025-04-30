// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Testing.TUnit;
using InfiniLore.InfiniBlazor.Theming;
using InfiniLore.InfiniBlazor.Theming.Config;
using InfiniLore.InfiniBlazor.Theming.Library;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable MemberCanBeMadeStatic.Global
public class ServiceCollectionTests {
    [Test]
    public async Task ShouldAddServices_AddThemes() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfiniBlazor(config => config.AddThemingLogic());

        // Assert
        await Assert.That(services).ContainsServiceType<IThemeSelector>();
    }

    [Test]
    public async Task ShouldAddServices_RegisterTheme() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfiniBlazor(config => {
            config.AddThemingLogic(cfg =>
                cfg.RegisterTheme<AnnaSasDevThemeCollection>("anna")
            );
        });

        // Assert
        await Assert.That(services).ContainsKeyedServiceImplementation<IThemeCollection, AnnaSasDevThemeCollection>("anna");
    }
}

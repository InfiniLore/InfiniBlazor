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
    public async Task ShouldAddServices_RegisterTheme_ByGenericType() {
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
    
    [Test]
    [Arguments(typeof(AnnaSasDevThemeCollection), "anna")]
    public async Task ShouldAddServices_RegisterTheme_ByTypeArgument(Type themeType, string themeName) {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfiniBlazor(config => {
            config.AddThemingLogic(cfg =>
                cfg.RegisterTheme(themeType, themeName)
            );
        });

        // Assert
        await Assert.That(services).ContainsKeyedServiceImplementation(typeof(IThemeCollection), themeType, themeName);
    }
}

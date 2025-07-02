// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Testing.TUnit;
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.JsRuntime;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ServiceCollectionTests {
    [Test]
    public async Task ShouldAddServices() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfiniBlazor();

        // Assert
        await Assert.That(services).ContainsServiceType<IJsRuntimeHelper>();
    }
    [Test]
    public async Task ShouldAddServices_AddMarkdownLogic() {
        // Arrange
        var services = new ServiceCollection();

        // Act
        services.AddInfiniBlazor(config => config.AddMarkdownLogic());

        // Assert
        await Assert.That(services).ContainsServiceImplementation(typeof(IMdSyntaxParser), typeof(MdSyntaxParser));
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Testing.TUnit;
using InfiniLore.InfiniBlazor.JsRuntime;
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
}

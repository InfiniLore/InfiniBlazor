// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Testing.TUnit;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Config;
using Microsoft.Extensions.DependencyInjection;

namespace Tests.InfiniLore.InfiniBlazor.Markdown;

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
        services.AddInfiniBlazor(config => config.AddMarkdownLogic());

        // Assert
        await Assert.That(services).ContainsServiceImplementation(typeof(IMarkdownParser<,>), typeof(MarkdownParser<,>));
    }
}

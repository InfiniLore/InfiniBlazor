// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Testing.TUnit;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.Markdown.Config;
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
        services.AddInfiniBlazor(config => config.AddMarkdown());

        // Assert
        await Assert.That(services).ContainsServiceImplementation(typeof(IMarkdownParser<,>), typeof(MarkdownParser<,>));
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;
using System.Text.RegularExpressions;

namespace Tests.InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class InfiniBlazorThemeTests {
    
    [GeneratedRegex("^#[0-9A-Fa-f]{3}([0-9A-Fa-f]{3})?$")]
    public static partial Regex IsHexColorRegex { get; }
    
    [Test]
    public async Task AsCssVariables_ShouldHoldData() {
        // Arrange
        ITheme theme = InfiniBlazorTheme.Instance;
        
        // Act 
        List<(string, string)> variables = theme.AsCssVariables().ToList();

        // Assert
        await Parallel.ForEachAsync(variables, async (tuple, token) => {
            (string cssVariable, string data) = tuple;
            await Assert.That(cssVariable).IsNotNullOrWhitespace()
                .And.StartsWith("--");

            if (data.StartsWith('#')) {
                await Assert.That(data).Matches(IsHexColorRegex);
                await Assert.That(cssVariable).DoesNotEndWith("-rgb");
            }
            
            if (!data.StartsWith('#')) {
                // await Assert.That(data).DoesNotMatch(IsHexColorRegex); //TODO Uncomment if https://github.com/thomhurst/TUnit/pull/2305 is implemented
                await Assert.That(cssVariable).EndsWith("-rgb");
            }
            
            token.ThrowIfCancellationRequested();
        });
        
        await Assert.That(variables).HasCount().GreaterThanOrEqualTo(40);
    }
}

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
    [GeneratedRegex("^#[0-9A-Fa-f]{3}$|^#[0-9A-Fa-f]{6}$")]
    private static partial Regex IsHexColorRegex { get; }

    [GeneratedRegex("^[0-9]{1,3}, [0-9]{1,3}, [0-9]{1,3}$")]
    private static partial Regex IsRgbColorRegex { get; }
    
    [GeneratedRegex(@"^--[a-z0-9]+(-[a-z0-9]+)*$")]
    private static partial Regex IsCssVariableNameRegex { get; }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    public async Task AsCssVariables_ShouldDeliverCorrectData() {
        // Arrange
        ITheme theme = InfiniBlazorTheme.Instance;
        int executionCount = 0;
        
        // Act 
        List<(string, string)> variables = theme.AsCssVariables().ToList();

        // Assert
        await Parallel.ForEachAsync(variables, async (tuple, token) => {
            Interlocked.Increment(ref executionCount);

            (string cssVariable, string data) = tuple;
            await Assert.That(cssVariable).IsNotNullOrWhitespace()
                .And.StartsWith("--")
                .And.Matches(IsCssVariableNameRegex);

            if (data.StartsWith('#')) {
                await Assert.That(data).Matches(IsHexColorRegex);
                // And.DoesNotMatch(IsRgbColorRegex); //TODO Uncomment if https://github.com/thomhurst/TUnit/pull/2305 is implemented
                await Assert.That(cssVariable).DoesNotEndWith("-rgb");
            }
            
            if (!data.StartsWith('#')) {
                await Assert.That(data).Matches(IsRgbColorRegex);
                // And.DoesNotMatch(IsHexColorRegex); //TODO Uncomment if https://github.com/thomhurst/TUnit/pull/2305 is implemented
                await Assert.That(cssVariable).EndsWith("-rgb");
            }
            
            token.ThrowIfCancellationRequested();
        });
        
        await Assert.That(variables).HasCount().GreaterThanOrEqualTo(40);
        await Assert.That(executionCount).IsEqualTo(variables.Count);

    }
}

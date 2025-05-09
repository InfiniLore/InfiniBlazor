// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;
using System.Text.RegularExpressions;
using InfiniBlazorTheme = InfiniLore.InfiniBlazor.Theming.InfiniBlazorTheme;

namespace Tests.InfiniLore.InfiniBlazor.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// ReSharper disable MemberCanBeMadeStatic.Global
public partial class InfiniBlazorThemeTests {
    [GeneratedRegex("^#[0-9A-Fa-f]{3}$|^#[0-9A-Fa-f]{6}$")]
    private static partial Regex IsHexColorRegex { get; }

    [GeneratedRegex("^[0-9]{1,3}, [0-9]{1,3}, [0-9]{1,3}$")]
    private static partial Regex IsRgbColorRegex { get; }
    
    [GeneratedRegex(@"^--[a-z0-9]+(-[a-z]+|-[0-9]+)*$")]
    private static partial Regex IsCssVariableNameRegex { get; }
    
    [GeneratedRegex(@"^var\(--[a-z0-9]+(-[a-z]+|-[0-9]+)*\)$")]
    private static partial Regex IsCssVariableDefinitionRegex { get; }

    private static readonly HashSet<string> AllowedKeywords = [
        "transparent"
    ];

    public static IEnumerable<Func<ITheme>> ThemeDataSources() {
        yield return () => InfiniBlazorTheme.DarkModeInstance; 
        yield return () => InfiniBlazorTheme.LightModeInstance; 
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    [MethodDataSource<InfiniBlazorThemeTests>(nameof(ThemeDataSources))]
    public async Task AsCssVariables_ShouldDeliverCorrectData(ITheme theme) {
        // Arrange
        int executionCount = 0;
        
        // Act 
        List<(string, string)> variables = theme.AsCssVariables().ToList();

        // Assert
        await Parallel.ForEachAsync(variables, async (tuple, token) => {
            Interlocked.Increment(ref executionCount);

            (string cssVariable, string data) = tuple;
            await Assert.That(cssVariable).Matches(IsCssVariableNameRegex);
            if (AllowedKeywords.Contains(data)) return;
            
            await Assert.That(data)
                .Matches(IsCssVariableDefinitionRegex)
                .Or.Matches(IsRgbColorRegex)
                .Or.Matches(IsHexColorRegex);
            
            token.ThrowIfCancellationRequested();
        });
        
        await Assert.That(variables).HasCount().GreaterThanOrEqualTo(40);
        await Assert.That(executionCount).IsEqualTo(variables.Count);

    }
}

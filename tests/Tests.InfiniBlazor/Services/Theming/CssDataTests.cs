// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;
using InfiniLore.InfiniBlazor.Theming.CssData;
using System.Text.RegularExpressions;

namespace Tests.InfiniBlazor.Services.Theming;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public partial class CssDataTests {
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

    public static IEnumerable<Func<ICssData>> AsCssVariables_ShouldDeliverCorrectData_DataSources() {
        yield return () => EmptyCssData.Instance;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    [MethodDataSource(nameof(AsCssVariables_ShouldDeliverCorrectData_DataSources))]
    public async Task AsCssVariables_ShouldDeliverCorrectData(ICssData cssData) {
        // Arrange
        int executionCount = 0;

        // Act 
        List<(string, string)> variables = cssData.AsCssVariables().ToList();

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
        await Assert.That(executionCount).IsEqualTo(variables.Count);
    }
}

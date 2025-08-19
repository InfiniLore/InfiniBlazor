// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;
using InfiniLore.InfiniBlazor.Theming.CssData;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Tests.InfiniLore.InfiniBlazor.Services.Theming;

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
    
    public static IEnumerable<Func<(ICssData, int)>> AsCssVariables_ShouldReturn_DataSources() {
        yield return () => (EmptyCssData.Instance, 0);
        
        Type type = typeof(ICssData);
        PropertyInfo[] properties = type.GetProperties();
        IEnumerable<PropertyInfo> cssDataProperties = properties.Where(p => !p.GetCustomAttributes<CssDataAttribute>().IsEmpty());
        int count = cssDataProperties.Count();
        
        yield return () => (InfiniBlazorCssData.Instance, count);
    }
    
    [Test]
    [MethodDataSource(nameof(AsCssVariables_ShouldReturn_DataSources))]
    public async Task AsCssVariables_ShouldReturn(ICssData cssData, int expectedCount) {
        // Arrange
        IEnumerable<(string, string)> enumerable = cssData.AsCssVariables();
        
        // Act
        int result = enumerable.Count();

        // Assert
        await Assert.That(result).IsEqualTo(expectedCount);
    }

    public static IEnumerable<Func<ICssData>> AsCssVariables_ShouldDeliverCorrectData_DataSources() {
        yield return () => InfiniBlazorCssData.Instance;
        yield return () => EmptyCssData.Instance;
        yield return () => EmptyCssData.Instance with {
            ColorBase00 = InfiniBlazorCssData.Instance.ColorBase100,
            ColorBase05 = InfiniBlazorCssData.Instance.ColorBase95,
            ColorBase10 = InfiniBlazorCssData.Instance.ColorBase90,
            ColorBase20 = InfiniBlazorCssData.Instance.ColorBase80,
            ColorBase30 = InfiniBlazorCssData.Instance.ColorBase70,
            ColorBase40 = InfiniBlazorCssData.Instance.ColorBase60,
            ColorBase50 = InfiniBlazorCssData.Instance.ColorBase50,
            ColorBase60 = InfiniBlazorCssData.Instance.ColorBase40,
            ColorBase70 = InfiniBlazorCssData.Instance.ColorBase30,
            ColorBase80 = InfiniBlazorCssData.Instance.ColorBase20,
            ColorBase90 = InfiniBlazorCssData.Instance.ColorBase10,
            ColorBase95 = InfiniBlazorCssData.Instance.ColorBase05,
            ColorBase100 = InfiniBlazorCssData.Instance.ColorBase00,

            ButtonDefault = InfiniBlazorCssData.Instance.ColorBase10,
            ButtonPrimary = InfiniBlazorCssData.Instance.ColorBase80,
        };
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

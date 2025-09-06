// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components;
using Microsoft.Extensions.Logging;
using NSubstitute;

namespace Tests.InfiniBlazor.Services.Emotes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class EmoteProviderTests {
    private static EmoteProvider _initializedProvider = null!;
    
    private static EmoteProvider GetEmoteProvider() {
        var logger = Substitute.For<ILogger<EmoteProvider>>();
        return new EmoteProvider(logger, Substitute.For<IHttpClientFactory>());
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Test Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Before(Class)]
    public static async Task SetupAsync() {
        EmoteProvider provider = GetEmoteProvider();
        await provider.InitializeAsync();
        _initializedProvider = provider;
    }
    
    [Test]
    public async Task Initialize_ShouldPopulateData() {
        // Arrange
        EmoteProvider provider = GetEmoteProvider();

        // Act
        await provider.InitializeAsync();

        // Assert
        await Assert.That(provider.Count).IsGreaterThanOrEqualTo(4);
    }

    public static IEnumerable<Func<(string? Key, bool ExpectedResult, EmoteEntry? ExpectedEntry)>> TryGetEntry_ShouldReturnExpected_DataSources() {
        yield return () => (null, false, null);
        yield return () => ("", false, null);
        yield return () => ("random-name", false, null);
        
        yield return () => ("flag-trans", true, new EmoteEntry(
            ["flag-transgender", "flag-trans"],
            "\ud83c\udff3\ufe0f\u200d\u26a7\ufe0f",
            EmoteContentType.Emoji
        ));
        
        yield return () => ("flag-transgender", true, new EmoteEntry(
            ["flag-transgender", "flag-trans"],
            "\ud83c\udff3\ufe0f\u200d\u26a7\ufe0f",
            EmoteContentType.Emoji
        ));
    }
    
    [Test]
    [MethodDataSource(nameof(TryGetEntry_ShouldReturnExpected_DataSources))]
    public async Task TryGetEntry_ShouldReturnExpected(string? key, bool expectedResult, EmoteEntry? expectedEntry) {
        // Arrange
        
        // Act
        bool result = _initializedProvider.TryGetEntry(key, out IEmoteEntry? entry);
        
        // Assert
        await Assert.That(result).IsEqualTo(expectedResult);
        
        var entryCast = entry as EmoteEntry;
        await Assert.That(entryCast).IsEquivalentTo(expectedEntry);   
    }
}

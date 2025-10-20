// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components;
using InfiniLore.InfiniBlazor.Emotes;
using Tests.InfiniBlazor.Shared;

namespace Tests.InfiniBlazor.Core.Emotes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InfiniBlazorDiDataSource]
public class EmoteProviderTests(IEmoteProvider provider) {

    // -----------------------------------------------------------------------------------------------------------------
    // Test Methods
    // -----------------------------------------------------------------------------------------------------------------
    [Test]
    public async Task CountShouldReturnExpected() {
        // Arrange
        const int expectedMinCount = 1600;// Lucide has around 1600 icons and those should all be loaded.

        // Act
        int result = provider.Count;

        // Assert
        await Assert.That(result).IsGreaterThanOrEqualTo(expectedMinCount);
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

        yield return () => ("ducky-trans", true, new EmoteEntry(
            ["ducky-trans"],
            "InfiniLore.InfiniBlazor.wwwroot.libs.emotes.assets.ducky-trans.png",
            EmoteContentType.ResourcePathPng
        ));
    }

    [Test]
    [MethodDataSource(nameof(TryGetEntry_ShouldReturnExpected_DataSources))]
    public async Task TryGetEntry_ShouldReturnExpected(string? key, bool expectedResult, EmoteEntry? expectedEntry) {
        // Arrange

        // Act
        #pragma warning disable CS8604// Possible null reference argument.

        bool result = provider.TryGetEntry(key, out IEmoteEntry? entry);

        #pragma warning restore CS8604// Possible null reference argument.

        // Assert
        var entryCast = entry as EmoteEntry;
        await Assert.That(result).IsEqualTo(expectedResult);
        await Assert.That(entryCast).IsEquivalentTo(expectedEntry);
    }
}

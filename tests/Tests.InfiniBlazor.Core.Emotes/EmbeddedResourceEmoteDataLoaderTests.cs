// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Emotes;
using InfiniLore.InfiniBlazor.Emotes.DataLoaders;
using Tests.InfiniBlazor.Shared;

namespace Tests.InfiniBlazor.Core.Emotes;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InfiniBlazorDiDataSource]
public class EmbeddedResourceEmoteDataLoaderTests(IEmotesConfig componentConfig) {
    
    [Test]
    public async Task CountShouldReturnExpected() {
        // Arrange
        var dataLoader = new EmbeddedResourceEmoteDataLoader(componentConfig);
        
        // Act
        IEnumerable<Stream> streams = dataLoader.LoadEmoteStreams();
        Stream[] streamArray = streams.ToArray();
        
        // Assert
        await Assert.That(streamArray).HasCount().GreaterThan(1);
    }
}

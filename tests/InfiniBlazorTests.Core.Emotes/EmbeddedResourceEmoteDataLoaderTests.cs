// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Emotes;
using InfiniBlazor.Emotes.DataLoaders;
using InfiniBlazorTests.Shared;

namespace InfiniBlazorTests.Core.Emotes;

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
        await Assert.That(streamArray).Count().IsGreaterThanOrEqualTo(4);
    }
}

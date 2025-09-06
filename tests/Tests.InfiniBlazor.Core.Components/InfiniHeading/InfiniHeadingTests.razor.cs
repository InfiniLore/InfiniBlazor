using Bunit;
using Tests.InfiniBlazor.Shared;

namespace Tests.InfiniBlazor.Core.Components.InfiniHeading;
// -----------------------------------------------------------------------------------------------------------------
// Methods
// -----------------------------------------------------------------------------------------------------------------

[DiDataSource]
public partial class InfiniHeadingTests(IServiceProvider services) {
    [Test]
    public async Task RendersCorrectly() {
        // Arrange
        Services.AddFallbackServiceProvider(services);
        
        // Act
        IRenderedFragment renderedFragment = Render(Input);
        
        // Assert
        renderedFragment.MarkupMatches(ExpectedOutput);

    }
}

using Bunit;
using Microsoft.AspNetCore.Components;
using Tests.InfiniBlazor.Shared;

namespace Tests.InfiniBlazor.Core.Components.InfiniHeading;
// -----------------------------------------------------------------------------------------------------------------
// Methods
// -----------------------------------------------------------------------------------------------------------------
[DiDataSource]
public partial class InfiniHeadingTests(IServiceProvider services) {
    [Test]
    [MethodDataSource(nameof(GetComponentDataSources))]
    public Task RendersCorrectly(RenderFragment input, RenderFragment expectedOutput) {
        // Arrange
        Services.AddFallbackServiceProvider(services);
        
        // Act
        IRenderedFragment renderedFragment = Render(input);
        
        // Assert
        renderedFragment.MarkupMatches(expectedOutput);
        return Task.CompletedTask;
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Bunit;
using Microsoft.AspNetCore.Components;
using Tests.InfiniBlazor.Shared;

namespace Tests.InfiniBlazor.Core.Components.InfiniHeading;
// -----------------------------------------------------------------------------------------------------------------
// Methods
// -----------------------------------------------------------------------------------------------------------------
[InfiniBlazorDiDataSource]
public partial class InfiniHeadingTests(IServiceProvider services) : InfiniBlazorBunitTest(services) {
    [Test]
    [MethodDataSource(nameof(GetComponentDataSources))]
    public Task RendersCorrectly(RenderFragment input, RenderFragment expectedOutput) {
        // Arrange
        
        // Act
        IRenderedFragment renderedFragment = Render(input);
        
        // Assert
        renderedFragment.MarkupMatches(expectedOutput);
        return Task.CompletedTask;
    }
}

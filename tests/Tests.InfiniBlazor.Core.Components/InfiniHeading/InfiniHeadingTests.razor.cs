// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Bunit;
using Bunit.Rendering;
using Microsoft.AspNetCore.Components;
using Tests.InfiniBlazor.Shared;

namespace Tests.InfiniBlazor.Core.Components.InfiniHeading;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InfiniBlazorDiDataSource]
public partial class InfiniHeadingTests(IServiceProvider services) : InfiniBlazorBunitTest(services) {
    [Test]
    [MethodDataSource(nameof(GetComponentDataSources))]
    public Task RendersCorrectly(BunitTestData testData) {
        // Arrange
        RenderFragment input = testData.Input;
        RenderFragment expectedMarkup = testData.ExpectedMarkup;
        
        // Act
        IRenderedComponent<ContainerFragment> renderedFragment = Render(input);
        
        // Assert
        renderedFragment.MarkupMatches(expectedMarkup);
        return Task.CompletedTask;
    }
}

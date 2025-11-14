// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Bunit;
using Bunit.Rendering;
using Microsoft.AspNetCore.Components;
using Tests.InfiniBlazor.Shared;

namespace Tests.InfiniBlazor.Core.Components.InfiniButton;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InfiniBlazorDiDataSource]
public partial class InfiniButtonTests(IServiceProvider services) : InfiniBlazorBunitTest(services) {
    [Test]
    [MethodDataSource<InfiniButtonTests>(nameof(GetComponentDataSources))]
    public async Task RendersCorrectly(BunitTestData testData) {
        // Arrange
        RenderFragment input = testData.Input;
        // RenderFragment expectedMarkup = testData.ExpectedMarkup;
        
        // Act
        IRenderedComponent<ContainerFragment> renderedFragment = Render(input);
        
        // Assert
        // renderedFragment.MarkupMatches(expectedMarkup);
        await Assert.That(renderedFragment.Find("button").TextContent).IsEqualTo("Button");
    }
}

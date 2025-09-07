// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Bunit;
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
    public Task RendersCorrectly(BunitTestData testData) {
        // Arrange
        RenderFragment input = testData.Input;
        RenderFragment expectedMarkup = testData.ExpectedMarkup;
        
        // Act
        IRenderedFragment renderedFragment = Render(input);
        
        // Assert
        renderedFragment.MarkupMatches(expectedMarkup);
        return Task.CompletedTask;
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;

namespace Benchmarks.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MockNavigationManager : NavigationManager {
    public MockNavigationManager() => Initialize("http://localhost/", "http://localhost/");
    protected override void NavigateToCore(string uri, bool forceLoad) { }
}

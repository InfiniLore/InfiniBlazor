// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components;
using Microsoft.AspNetCore.Components;

namespace Tests.InfiniBlazor.Services.QueryParameters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class QueryParameterManagerTests {
    private QueryParameterManager _manager = null!;

    private class TestNavigationManager : NavigationManager {
        public TestNavigationManager(string baseUri, string uri) {
            Initialize(baseUri, uri);
        }

        protected override void NavigateToCore(string uri, bool forceLoad) {
            // Mock implementation for testing
            Uri = ToAbsoluteUri(uri).ToString();
        }
    }


    [Before(Test)]
    public void Setup() {
        _manager = new QueryParameterManager(new TestNavigationManager("https://localhost/?alpha=1", "https://localhost/?alpha=1"));
        _manager.RegisterTrackedQueryParameter("alpha");
    }

    [Test]
    [Arguments("/page", "/page?alpha=1")]
    [Arguments("/page#header", "/page?alpha=1#header")]
    public async Task AddTrackedQueryParameters_ShouldWork(string input, string expected) {
        // Arrange

        // Act
        string output = _manager.ApplyTrackedQueryParameters(input);

        // Assert
        await Assert.That(output).IsEqualTo(expected);

    }
}

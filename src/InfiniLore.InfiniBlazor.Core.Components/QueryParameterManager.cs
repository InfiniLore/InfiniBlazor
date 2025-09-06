// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Primitives;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableScoped<IQueryParameterManager>]
public class QueryParameterManager(NavigationManager navigationManager) : IQueryParameterManager {
    private HashSet<string> TrackedQueryParameters { get; } = [
        QueryParameterNames.Debug,
        QueryParameterNames.ThemeCollection,
        QueryParameterNames.ThemeMode
    ];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public void RegisterTrackedQueryParameter(string key) 
        => TrackedQueryParameters.Add(key);

    // Set a single query parameter
    public void SetParam<T>(string key, T value)
        => SetParams((key, value));

    // Set multiple query parameters
    public void SetParams(params Span<(string key, object? value)> parameters) {
        Dictionary<string, string?> current = GetCurrentQueryDict();
        foreach ((string key, object? value) in parameters) {
            current[key] = value?.ToString();
        }

        NavigateWithQuery(current);
    }

    // Remove a single query parameter
    public void RemoveParam(string key) 
        => RemoveParams(key);

    // Remove multiple query parameters
    public void RemoveParams(params Span<string> keys) {
        Dictionary<string, string?> current = GetCurrentQueryDict();
        foreach (string key in keys) {
            current.Remove(key);
        }

        NavigateWithQuery(current);
    }

    // Get and convert a query parameter to a given type
    public T? GetParam<T>(string key) {
        Dictionary<string, StringValues> query = GetQuery();
        if (!query.TryGetValue(key, out StringValues value))
            return default;

        try {
            return (T?)Convert.ChangeType(value.ToString(), typeof(T));
        }
        catch {
            return default;
        }
    }
    
    public string ApplyTrackedQueryParameters(string uri) {
        if (uri.IsNullOrEmpty()) return uri;
        
        // Extract query string manually without creating a Uri for relative URIs
        int qIdx = uri.IndexOf('?');
        string inputQueryString = qIdx >= 0 ? uri[(qIdx + 1)..] : string.Empty;

        // Parse existing query params from the input URI string
        Dictionary<string, StringValues> inputQuery = QueryHelpers.ParseQuery(inputQueryString);

        // Use an existing method to get current query params from the NavigationManager
        Dictionary<string, StringValues> currentQuery = GetQuery();

        // Prepare a dictionary for new combined query params
        Dictionary<string, string?> combinedParams = new(StringComparer.OrdinalIgnoreCase);

        // Start with input URI query params
        foreach ((string key, StringValues value) in inputQuery) {
            combinedParams[key] = value.ToString();
        }

        // Add or overwrite tracked params from the current URI
        foreach (string key in TrackedQueryParameters) {
            if (currentQuery.TryGetValue(key, out StringValues values) && values.Count > 0)
                combinedParams[key] = values[0];
        }

        // Rebuild base URI without a query
        string baseUri = qIdx >= 0 ? uri[..qIdx] : uri;

        // Build a new URI with combined query parameters
        return QueryHelpers.AddQueryString(baseUri, combinedParams);
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Private Helpers
    // -----------------------------------------------------------------------------------------------------------------
    private Dictionary<string, string?> GetCurrentQueryDict()
        => GetQuery()
            .ToDictionary<KeyValuePair<string, StringValues>, string, string?>(
                keySelector: pair => pair.Key,
                elementSelector: pair => pair.Value.ToString(),
                StringComparer.OrdinalIgnoreCase
            );

    private Dictionary<string, StringValues> GetQuery() {
        Uri uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
        return QueryHelpers.ParseQuery(uri.Query);
    }

    private void NavigateWithQuery(Dictionary<string, string?> query) {
        Uri uri = navigationManager.ToAbsoluteUri(navigationManager.Uri);
        string baseUri = uri.GetLeftPart(UriPartial.Path);
        string newUri = QueryHelpers.AddQueryString(baseUri, query);
        navigationManager.NavigateTo(newUri, false);
    }
}

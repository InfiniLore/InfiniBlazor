// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IQueryParameterManager {
    void SetParam<T>(string key, T value);
    void SetParams(params Span<(string key, object? value)> parameters);
    void RemoveParam(string key);
    void RemoveParams(params Span<string> keys);
    T? GetParam<T>(string key);

    public string ApplyTrackedQueryParameters(string uri);
}

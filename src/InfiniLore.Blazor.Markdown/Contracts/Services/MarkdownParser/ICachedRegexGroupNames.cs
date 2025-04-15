// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.Blazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ICachedRegexGroupNames {
    int GetSingleLineGroupId(string groupName);
    int GetMultiLineGroupId(string groupName);
    int GetSpanGroupId(string groupName);
    int GetListGroupId(string groupName);
}

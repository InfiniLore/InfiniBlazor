// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface ICachedRegexGroupNames {
    int GetSingleLineGroupId(ReadOnlySpan<char> groupName);
    int GetMultiLineGroupId(ReadOnlySpan<char> groupName);
    int GetSpanGroupId(ReadOnlySpan<char> groupName);
    int GetListGroupId(ReadOnlySpan<char> groupName);
}

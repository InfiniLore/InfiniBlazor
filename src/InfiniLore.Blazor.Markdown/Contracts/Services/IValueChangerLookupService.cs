// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IValueChangerLookupService {
    public FrozenDictionary<string, string>.AlternateLookup<ReadOnlySpan<char>> AlternateLookup { get; }
    public Regex LookupDictRegex { get; }
}

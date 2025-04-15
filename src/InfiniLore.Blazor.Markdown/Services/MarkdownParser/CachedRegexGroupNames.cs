// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ICachedRegexGroupNames>]
public class CachedRegexGroupNames : ICachedRegexGroupNames{
    private ImmutableDictionary<string, int> GroupNameToGroupId { get; set; } = ImmutableDictionary<string, int>.Empty;
    private readonly Lock _lock = new();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public int GetSingleLineGroupId(string groupName) 
        => GetOrAdd(groupName, MarkdownRegexLib.SinglelineStructuresRegex);

    public int GetMultiLineGroupId(string groupName) 
        => GetOrAdd(groupName, MarkdownRegexLib.MultilineStructuresRegex);

    public int GetSpanGroupId(string groupName)
        => GetOrAdd(groupName, MarkdownRegexLib.FindSpanHtmlRegex);

    public int GetListGroupId(string groupName) 
        => GetOrAdd(groupName, MarkdownRegexLib.ListItemBodyRegex);

    private int GetOrAdd(string groupName, Regex regex) {
        if (GroupNameToGroupId.TryGetValue(groupName, out int value)) return value;
        
        lock (_lock) {
            if (GroupNameToGroupId.TryGetValue(groupName, out value)) return value;

            value = regex.GroupNumberFromName(groupName);
            GroupNameToGroupId = GroupNameToGroupId.Add(groupName, value);
        }

        return value;
    }

}

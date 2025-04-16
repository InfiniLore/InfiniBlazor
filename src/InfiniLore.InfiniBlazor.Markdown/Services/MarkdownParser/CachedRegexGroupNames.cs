// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ICachedRegexGroupNames>]
public class CachedRegexGroupNames : ICachedRegexGroupNames{
    private static FrozenDictionary<string, int> GroupNameToGroupId { get; } = GetGroupNames();
    private static FrozenDictionary<string, int>.AlternateLookup<ReadOnlySpan<char>> AlternateLookup { get; } = GroupNameToGroupId.GetAlternateLookup<ReadOnlySpan<char>>();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static FrozenDictionary<string, int> GetGroupNames() {
        Regex[] regexes = [
            MarkdownRegexLib.SinglelineStructuresRegex,
            MarkdownRegexLib.MultilineStructuresRegex,
            MarkdownRegexLib.FindSpanHtmlRegex,
            MarkdownRegexLib.ListItemBodyRegex,
        ];

        IEnumerable<(Regex regex, string[])> a = regexes.Select(regex => (regex, regex.GetGroupNames()));
        var dictionary = new Dictionary<string, int>(32);
        
        foreach ((Regex regex, string[] names) in a) {
            foreach (string name in names) {
                dictionary.AddOrUpdate(name, regex.GroupNumberFromName(name));
            }
        }
        
        return dictionary.ToFrozenDictionary();
    }
    
    public int GetSingleLineGroupId(ReadOnlySpan<char> groupName) =>AlternateLookup[groupName]; 
    public int GetMultiLineGroupId(ReadOnlySpan<char> groupName) =>AlternateLookup[groupName]; 
    public int GetSpanGroupId(ReadOnlySpan<char> groupName)=>AlternateLookup[groupName]; 
    public int GetListGroupId(ReadOnlySpan<char> groupName) =>AlternateLookup[groupName]; 
    
}

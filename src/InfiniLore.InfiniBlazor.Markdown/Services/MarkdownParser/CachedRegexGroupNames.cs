// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class CachedRegexGroupNames{
    private static FrozenDictionary<string, int> GroupNameToGroupId { get; } = GetGroupNames();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private static FrozenDictionary<string, int> GetGroupNames() {
        Regex[] regexes = [
            MarkdownRegexLib.SinglelineStructuresRegex,
            MarkdownRegexLib.MultilineStructuresRegex,
            MarkdownRegexLib.FindSpanHtmlRegex,
            MarkdownRegexLib.ListItemBodyRegex
        ];

        IEnumerable<(Regex regex, string[])> a = regexes.Select(regex => (regex, regex.GetGroupNames()));
        var dictionary = new Dictionary<string, int>(regexes.Length * 16); // early approximation
        
        foreach ((Regex regex, string[] names) in a) {
            dictionary.EnsureCapacity(dictionary.Count + names.Length);
            
            foreach (string name in names) {
                dictionary.AddOrUpdate(name, regex.GroupNumberFromName(name));
            }
        }
        
        return dictionary.ToFrozenDictionary();
    }
    
    public static int GetSingleLineGroupId(string groupName) => GroupNameToGroupId[groupName]; 
    public static int GetMultiLineGroupId(string groupName) => GroupNameToGroupId[groupName]; 
    public static int GetSpanGroupId(string groupName)=> GroupNameToGroupId[groupName]; 
    public static int GetListGroupId(string groupName) => GroupNameToGroupId[groupName]; 
    
}

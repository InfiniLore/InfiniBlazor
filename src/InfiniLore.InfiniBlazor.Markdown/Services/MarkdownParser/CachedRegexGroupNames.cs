// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CachedRegexGroupNames {
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

        int totalGroups = regexes.Sum(regex => regex.GetGroupNames().Length);
        Dictionary<string, int> dictionary = new(totalGroups);

        foreach (Regex regex in regexes) {
            string[] names = regex.GetGroupNames();
            foreach (string name in names) {
                dictionary.AddOrUpdate(name, regex.GroupNumberFromName(name));
            }
        }
        return dictionary.ToFrozenDictionary();
    }

    public static int GetSingleLineGroupId(string groupName) => GroupNameToGroupId[groupName];
    public static int GetMultiLineGroupId(string groupName) => GroupNameToGroupId[groupName];
    public static int GetSpanGroupId(string groupName) => GroupNameToGroupId[groupName];
    public static int GetListGroupId(string groupName) => GroupNameToGroupId[groupName];
}

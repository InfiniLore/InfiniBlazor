// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.Blazor.Markdown.Services;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableService<IValueChangerLookupService>(ServiceLifetime.Singleton)]
public class ValueChangerLookupService : IValueChangerLookupService {
    private static readonly FrozenDictionary<string, string> LookupDict = new Dictionary<string, string>(5) {
        { "&copy;", "\u00a9" },
        { "<br/>", "<br/>" },
        { "&", "&amp;" },
        { "<", "&lt;" },
        { ">", "&gt;" },
        { "\"", "&quot;" },
    }.ToFrozenDictionary();
    public FrozenDictionary<string, string>.AlternateLookup<ReadOnlySpan<char>> AlternateLookup { get; } = LookupDict.GetAlternateLookup<ReadOnlySpan<char>>();
    public Regex LookupDictRegex { get; } = new(string.Join('|', LookupDict.Keys), RegexOptions.Compiled | RegexOptions.Singleline);
}

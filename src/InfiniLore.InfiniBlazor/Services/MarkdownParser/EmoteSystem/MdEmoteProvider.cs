// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.MarkdownParser.EmoteSystem;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdEmoteProvider>]
public sealed class MdEmoteProvider : IMdEmoteProvider{
    private static FrozenDictionary<EmoteKey, string> EmoteDictionary { get; } = new Dictionary<EmoteKey, string> {
        {
            EmoteKey.FromPossibleKeys("flag-transgender", "flag-trans", "flag_trans", "flag_transgender"),
            "\ud83c\udff3\ufe0f\u200d\u26a7\ufe0f" // 🏳️‍⚧️,
        }, {
            EmoteKey.FromPossibleKeys("li-signature"),
            "<span style=\"display: inline-block;vertical-align: middle;\"><svg xmlns=\"http://www.w3.org/2000/svg\" width=\"24\" height=\"24\" viewBox=\"0 0 24 24\" fill=\"none\" stroke=\"currentColor\" stroke-width=\"2\" stroke-linecap=\"round\" stroke-linejoin=\"round\" class=\"lucide lucide-signature-icon lucide-signature\"><path d=\"m21 17-2.156-1.868A.5.5 0 0 0 18 15.5v.5a1 1 0 0 1-1 1h-2a1 1 0 0 1-1-1c0-2.545-3.991-3.97-8.5-4a1 1 0 0 0 0 5c4.153 0 4.745-11.295 5.708-13.5a2.5 2.5 0 1 1 3.31 3.284\"/><path d=\"M3 21h18\"/></svg></span>"// Transgender flag
        }
    }.ToFrozenDictionary(comparer: EmoteKeyComparer.Instance);
    
    private static FrozenDictionary<EmoteKey, string>.AlternateLookup<ReadOnlySpan<char>> EmoteSpanLookup 
        => EmoteDictionary.GetAlternateLookup<ReadOnlySpan<char>>();
    
    private static FrozenDictionary<EmoteKey, string>.AlternateLookup<string> EmoteStringLookup 
        => EmoteDictionary.GetAlternateLookup<string>();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetValue(string lookupValue, [NotNullWhen(true)] out string? s)
        => EmoteStringLookup.TryGetValue(lookupValue, out s);
    
    public bool TryGetValueSpan(string lookupValue, out ReadOnlySpan<char> s) {
        if (!EmoteStringLookup.TryGetValue(lookupValue, out string? value)) {
            s = ReadOnlySpan<char>.Empty;
            return false;
        }
        s = value.AsSpan();
        return true;
    }
    
    public bool TryGetValue(in ReadOnlySpan<char> lookupValue, [NotNullWhen(true)] out string? s)
        => EmoteSpanLookup.TryGetValue(lookupValue, out s);
    
    public bool TryGetValueSpan(in ReadOnlySpan<char> lookupValue, out ReadOnlySpan<char> s) {
        if (!EmoteSpanLookup.TryGetValue(lookupValue, out string? value)) {
            s = ReadOnlySpan<char>.Empty;
            return false;
        }
        s = value.AsSpan();
        return true;
    }
}

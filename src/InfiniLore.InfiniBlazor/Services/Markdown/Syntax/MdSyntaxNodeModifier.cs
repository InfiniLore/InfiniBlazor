// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Collections.Frozen;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeModifier : IMdSyntaxNodeModifier, IResettable {
    private Lazy<FrozenDictionary<string, Range>> InternalAttributesDictionary { get; set; } = new(static () => FrozenDictionary<string, Range>.Empty);
    
    public FrozenDictionary<string, Range> Attributes => InternalAttributesDictionary.Value;
    public string OriginalInput { get; private set; } = string.Empty;
    public ReadOnlySpan<char> OriginalInputSpan => OriginalInput.AsSpan();
    
    public static ObjectPool<MdSyntaxNodeModifier> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxNodeModifier>(16);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static MdSyntaxNodeModifier FromString(string input) {
        MdSyntaxNodeModifier mod = Pool.Get();
        mod.OriginalInput = input;
        
        if (mod.OriginalInputSpan.IsWhiteSpace()) return mod;

        mod.InternalAttributesDictionary = new Lazy<FrozenDictionary<string, Range>>(() => GetLazyDictionary(mod.OriginalInputSpan));
        
        return mod;
    }
    
    // ReSharper disable once InvertIf
    private static FrozenDictionary<string, Range> GetLazyDictionary(scoped ReadOnlySpan<char> span) {
        int spanLength = span.Length;
        
        Span<char> buffer = stackalloc char[spanLength-1]; // set it to the max possible, and all mods start with a | based on the regex, so we can trim one char
        
        int keyStart = 0;
        int keyEnd = -1;
        int valueStart = 0;
        
        var dictionary = new Dictionary<string, Range>(StringComparer.OrdinalIgnoreCase);
        
        for(int pointer = 0; pointer < spanLength;) {
            char currentCharacter = span[pointer];
            switch (currentCharacter) {
                case '|' when IsNonEscapedCharacter(span, pointer): {
                    if (pointer > keyStart) {
                        if (keyEnd == -1) {
                            // No '=' found, this is a flag attribute
                            int length = span[keyStart..pointer].ToLowerInvariant(buffer);
                            string value = buffer[..length].ToString();
                            dictionary.AddOrUpdate(value, new Range(pointer, pointer));
                        }
                        else {
                            // Normal key=value attribute
                            int length = span[keyStart..keyEnd].ToLowerInvariant(buffer);
                            string value = buffer[..length].ToString();
                            dictionary.AddOrUpdate(value, new Range(valueStart, pointer));
                        }
                    }
                    keyStart = ++pointer;
                    keyEnd = -1;
                    continue;
                }

                case '=': {
                    keyEnd = pointer;
                    valueStart = ++pointer;
                    continue;
                }

                default: {
                    pointer++;
                    continue;
                }
            }
        }
        
        // Handle the last attribute if there is one
        if (spanLength > keyStart) {
            if (keyEnd == -1) {
                // The last attribute is a flag
                int length = span[keyStart..spanLength].ToLowerInvariant(buffer);
                string value = buffer[..length].ToString();
                dictionary.AddOrUpdate(value, new Range(spanLength, spanLength));
            }
            else if (valueStart < spanLength) {
                // The last attribute is key=value
                int length = span[keyStart..keyEnd].ToLowerInvariant(buffer);
                string value = buffer[..length].ToString();
                dictionary.AddOrUpdate(value, new Range(valueStart, spanLength));
            }
        }
        
        return dictionary.ToFrozenDictionary();
    }
    
    private static bool IsNonEscapedCharacter(scoped ReadOnlySpan<char> source, int atIndex) {
        int backslashCount = 0;
        for (int i = atIndex - 1; i >= 0 && source[i] == '\\'; i--) {
            backslashCount++;
        }
        return backslashCount % 2 == 0;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetValue(string key, [NotNullWhen(true)] out string? value) {
        if (Attributes.TryGetValue(key, out Range range)) {
            value = OriginalInput[range];
            return true;
        }

        value = null;
        return false;
    }

    // ReSharper disable once InvertIf
    public bool TryGetFlag(string key, out bool value) {
        if (!Attributes.TryGetValue(key, out Range range)) {
            value = false;
            return false;
        }

        if (range.Start.Equals(range.End)) {
            value = true;
            return true;
        }
        
        ReadOnlySpan<char> span = OriginalInputSpan[range];
        if (bool.TryParse(span, out value)) return true;
        
        // Only accept 0 or 1 as valid int values
        if (int.TryParse(span, out int intValue) && intValue is 0 or 1) {
            value = intValue != 0;
            return true;
        }
        
        return false;
    }
    
    public void ReturnToPool() => Pool.Return(this);

    public bool TryReset() {
        InternalAttributesDictionary = new Lazy<FrozenDictionary<string, Range>>(static () => FrozenDictionary<string, Range>.Empty);
        OriginalInput = string.Empty;

        return true;
    }
    
    public bool Equals(IMdSyntaxNodeModifier? other) {
        // Don't need to check the attribute dictionary as it is fully dependent on the OriginalInput on creation.
        return StringComparer.Ordinal.Equals(OriginalInput, other?.OriginalInput);
    }
}

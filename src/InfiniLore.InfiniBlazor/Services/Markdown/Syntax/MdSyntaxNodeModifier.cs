// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeModifier : IMdSyntaxNodeModifier, IResettable {
    public Dictionary<string, Range> Attributes { get; } = new();
    public string OriginalInput { get; internal set; } = string.Empty;
    public ReadOnlySpan<char> OriginalInputSpan => OriginalInput.AsSpan();
    
    public static ObjectPool<MdSyntaxNodeModifier> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxNodeModifier>(16);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    
    // ReSharper disable once InvertIf
    public static MdSyntaxNodeModifier FromString(string input) {
        MdSyntaxNodeModifier mod = Pool.Get();
        mod.OriginalInput = input;
        
        if (mod.OriginalInputSpan.IsWhiteSpace()) return mod;
        
        ReadOnlySpan<char> span = input.AsSpan();
        Span<char> buffer = stackalloc char[span.Length-1]; // set it to the max possible, and all mods start with a | based on the regex so we can trim one char
        
        int keyStart = 0;
        int keyEnd = -1;
        int valueStart = 0;
        
        for(int pointer = 0; pointer < span.Length;) {
            switch (span[pointer]) {
                case '|': {
                    if (pointer > keyStart) {
                        if (keyEnd == -1) {
                            // No '=' found, this is a flag attribute
                            int length = span[keyStart..pointer].ToLowerInvariant(buffer);
                            string value = buffer[..length].ToString();
                            mod.Attributes.AddOrUpdate(value, new Range(pointer, pointer));
                        }
                        else {
                            // Normal key=value attribute
                            int length = span[keyStart..keyEnd].ToLowerInvariant(buffer);
                            string value = buffer[..length].ToString();
                            mod.Attributes.AddOrUpdate(value, new Range(valueStart, pointer));
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
        
        int spanLength = span.Length;
        
        // Handle the last attribute if there is one
        if (spanLength > keyStart) {
            if (keyEnd == -1) {
                // Last attribute is a flag
                int length = span[keyStart..spanLength].ToLowerInvariant(buffer);
                string value = buffer[..length].ToString();
                mod.Attributes.AddOrUpdate(value, new Range(spanLength, spanLength));
            }
            else if (valueStart < spanLength) {
                // Last attribute is key=value
                int length = span[keyStart..keyEnd].ToLowerInvariant(buffer);
                string value = buffer[..length].ToString();
                mod.Attributes.AddOrUpdate(value, new Range(valueStart, spanLength));
            }
        }
        
        return mod;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetAttributeValue(string key, [NotNullWhen(true)] out string? value) {
        if (Attributes.TryGetValue(key, out Range range)) {
            value = OriginalInput[range];
            return true;
        }

        value = null;
        return false;
    }

    // ReSharper disable once InvertIf
    public bool TryGetAttributeFlag(string key, out bool value) {
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
    
    public bool TryReset() {
        Attributes.Clear();
        OriginalInput = string.Empty;

        return true;
    }
}

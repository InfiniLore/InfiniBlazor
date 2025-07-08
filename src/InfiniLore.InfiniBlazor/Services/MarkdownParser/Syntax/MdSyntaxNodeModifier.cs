// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeModifier : IResettable {
    public Dictionary<string, Range> Attributes { get; } = new();
    public string OriginalInput { get; set; } = string.Empty;
    public ReadOnlySpan<char> OriginalInputSpan => OriginalInput.AsSpan();
    
    public static ObjectPool<MdSyntaxNodeModifier> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxNodeModifier>(16);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    
    // ReSharper disable once InvertIf
    public static MdSyntaxNodeModifier FromString(string input) {
        MdSyntaxNodeModifier mod = Pool.Get();
        mod.OriginalInput = input;
        
        ReadOnlySpan<char> span = input.AsSpan();
        
        int keyStart = 0;
        int keyEnd = -1;
        int valueStart = 0;
        
        for(int pointer = 0; pointer < span.Length;) {
            switch (span[pointer]) {
                case '|': {
                    if (pointer > keyStart) {
                        if (keyEnd == -1) {
                            // No '=' found, this is a flag attribute
                            mod.Attributes.AddOrUpdate(span[keyStart..pointer].ToString().ToLowerInvariant(), new Range(pointer, pointer));
                        }
                        else {
                            // Normal key=value attribute
                            mod.Attributes.AddOrUpdate(span[keyStart..keyEnd].ToString().ToLowerInvariant(), new Range(valueStart, pointer));
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
        if (span.Length > keyStart) {
            if (keyEnd == -1) {
                // Last attribute is a flag
                mod.Attributes.AddOrUpdate(span[keyStart..].ToString().ToLowerInvariant(), new Range(span.Length, span.Length));
            }
            else if (valueStart < span.Length) {
                // Last attribute is key=value
                mod.Attributes.AddOrUpdate(span[keyStart..keyEnd].ToString().ToLowerInvariant(), new Range(valueStart, span.Length));
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

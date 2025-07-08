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
public class MdSyntaxMod : IResettable {
    public Dictionary<string, Range> Attributes { get; } = new();
    public string OriginalInput { get; set; } = string.Empty;
    public ReadOnlySpan<char> OriginalInputSpan => OriginalInput.AsSpan();
    
    public static ObjectPool<MdSyntaxMod> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxMod>(16);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static MdSyntaxMod FromString(string input) {
        MdSyntaxMod mod = Pool.Get();
        mod.OriginalInput = input;
        
        ReadOnlySpan<char> span = input.AsSpan();
        
        // |something=else|something=else
        int keyStart = 0;
        int keyEnd = -1;
        int valueStart = 0;
        
        for(int pointer = 0; pointer < span.Length;) {
            switch (span[pointer]) {
                case '|': {
                    if (pointer > keyStart) {
                        if (keyEnd == -1) {
                            // No '=' found, this is a flag attribute
                            mod.Attributes.Add(span[keyStart..pointer].ToString().ToLowerInvariant(), new Range(pointer, pointer));
                        }
                        else {
                            // Normal key=value attribute
                            mod.Attributes.Add(span[keyStart..keyEnd].ToString().ToLowerInvariant(), new Range(valueStart, pointer));
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
                mod.Attributes.Add(span[keyStart..].ToString().ToLowerInvariant(), new Range(span.Length, span.Length));
            }
            else if (valueStart < span.Length) {
                // Last attribute is key=value
                mod.Attributes.Add(span[keyStart..keyEnd].ToString().ToLowerInvariant(), new Range(valueStart, span.Length));
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
    
    public bool TryReset() {
        Attributes.Clear();
        OriginalInput = string.Empty;

        return true;
    }
}

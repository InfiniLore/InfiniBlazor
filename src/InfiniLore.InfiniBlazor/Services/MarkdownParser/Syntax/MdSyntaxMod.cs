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
    private ReadOnlySpan<char> OriginalInputSpan => OriginalInput.AsSpan();
    
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
    
    public bool TryGetTitle([NotNullWhen(true)] out string? title) 
        => TryGetAttributeValue("title", out title);

    public bool TryGetSize(out (int Width, int Height) size) {
        size = (-1,-1);
        if (!Attributes.TryGetValue("size", out Range range)) return false;
            
        ReadOnlySpan<char> sizeValue = OriginalInputSpan[range];
        if (sizeValue.Contains('x')) {
            MemoryExtensions.SpanSplitEnumerator<char> split = sizeValue.Split('x');
            if (!split.MoveNext()) return false;

            Range firstValue = split.Current;
            if (!split.MoveNext()) return false;
            
            Range secondValue = split.Current;
            
            size = (int.Parse(sizeValue[firstValue]), int.Parse(sizeValue[secondValue]));
            return true;
        }

        // ReSharper disable once InvertIf
        if (int.TryParse(sizeValue, out int sizeInt)) {
            size = (sizeInt, sizeInt);
            return true;
        }
        
        return false;
    }
    
    public bool GetFit() => Attributes.ContainsKey("fit");
    
    public bool TryReset() {
        Attributes.Clear();
        OriginalInput = string.Empty;

        return true;
    }
}

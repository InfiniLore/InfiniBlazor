// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class MdSyntaxNodeModifierExtensions {
    public static bool TryGetIconName(this IMdSyntaxNodeModifier mod, [NotNullWhen(true)] out string? iconName) 
        => mod.TryGetValue("icon", out iconName) && iconName.IsNotNullOrWhiteSpace();
    
    public static bool TryGetTitle(this IMdSyntaxNodeModifier mod, [NotNullWhen(true)] out string? title) 
        => mod.TryGetValue("title", out title);

    public static bool TryGetSize(this IMdSyntaxNodeModifier mod, out (int Width, int Height) size) {
        size = (-1,-1);
        if (!mod.Attributes.TryGetValue("size", out Range range)) return false;
            
        ReadOnlySpan<char> sizeValue = mod.OriginalInputSpan[range];
        if (sizeValue.Contains('x')) {
            MemoryExtensions.SpanSplitEnumerator<char> split = sizeValue.Split('x');
            if (!split.MoveNext()) return false;

            Range firstValue = split.Current;
            if (!split.MoveNext()) return false;
            
            Range secondValue = split.Current;

            if (!int.TryParse(sizeValue[firstValue], out int first) || !int.TryParse(sizeValue[secondValue], out int second)) {
                return false;
            }
            
            size = (first, second);
            return true;
        }

        // ReSharper disable once InvertIf
        if (int.TryParse(sizeValue, out int sizeInt)) {
            size = (sizeInt, sizeInt);
            return true;
        }
        
        return false;
    }
    
    public static bool TryGetFit(this IMdSyntaxNodeModifier mod, out bool state) 
        => mod.TryGetFlag("fit", out state);

    public static bool TryGetAlign(this IMdSyntaxNodeModifier mod,[NotNullWhen(true)] out string? align) {
        align = null;
        if (!mod.TryGetValue("align", out  align)) return false;
        return VerticalAlignImageUtilities.TryGetFromString(align, out _) || align.IsNotNullOrWhiteSpace();
    }
    
}

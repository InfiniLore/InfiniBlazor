// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.GeneratorTools;
using Microsoft.CodeAnalysis;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace InfiniLore.InfiniBlazor.Theming.Generators;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record ThemeSymbolsDto(
    INamedTypeSymbol Symbol
) {
    public string ClassName => Symbol.Name;
    public string NameSpace => Symbol.ContainingNamespace.ToDisplayString();
    public string Accessibility => Symbol.DeclaredAccessibility switch {
        Microsoft.CodeAnalysis.Accessibility.Private => "private",
        Microsoft.CodeAnalysis.Accessibility.Protected => "protected",
        Microsoft.CodeAnalysis.Accessibility.Internal => "internal",
        Microsoft.CodeAnalysis.Accessibility.Public => "public",
        _ => "UNKNOWN"
    };
    
    public string TypeKeyword => Symbol switch {
        {IsRecord: true} => "record",
        {TypeKind : TypeKind.Class} => "class",
        {TypeKind : TypeKind.Struct} => "struct",
        {TypeKind : TypeKind.Interface} => "interface",
        _ => Symbol.TypeKind.ToString().ToLowerInvariant()
    };
    

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public static void WriteRgbValues(GeneratorStringBuilder builder, ImmutableArray<IPropertySymbol> themeProperties) {
        IEnumerable<IPropertySymbol> rgbProperties = themeProperties
            .Where(property => property.GetAttributes().Any(attr => attr.IsDisplayName(TypeNames.InterpretAsRgbAttribute)));

        builder.ForEachAppendLine(rgbProperties, symbol => {
            string propertyName = symbol.Name.Substring(0, symbol.Name.Length - 3);
            string converterCall = $"StringColorExtensions.ConvertToRgbValues({propertyName})";
            return $"public string {symbol.Name} => {converterCall};";
        });
    }

    public static void WriteAsCssVariables(GeneratorStringBuilder builder, ImmutableArray<IPropertySymbol> themeProperties) {
        builder.AppendLine("public IEnumerable<(string, string)> AsCssVariables() {");

        // ReSharper disable once HeapView.CanAvoidClosure
        builder.Indent(b => {
            b.ForEachAppendLineIndented(themeProperties, symbol => {
                string cssVariableName = ToCssVariable(symbol.Name.AsSpan());
                string property = symbol.Name;
                return $"yield return({cssVariableName.ToQuotedString()}, {property});";
            });
        });

        builder.AppendLine("}");
    }

    private static string ToCssVariable(ReadOnlySpan<char> input) {
        int[] tempArray = ArrayPool<int>.Shared.Rent(input.Length);
        char[] result = ArrayPool<char>.Shared.Rent(input.Length + input.Length + 1); 
    
        try {
            int index = 0;
            int count = 0;

            for (int i = 1; i < input.Length; i++) {
                bool currentIsDigit = char.IsDigit(input[i]);
                bool previousIsDigit = char.IsDigit(input[i - 1]);
            
                if (!(char.IsUpper(input[i]) || currentIsDigit && !previousIsDigit)) continue;

                tempArray[count * 2] = index;
                tempArray[count * 2 + 1] = i - index;
                count++;
                index = i;
            }

            if (index < input.Length) {
                tempArray[count * 2] = index;
                tempArray[count * 2 + 1] = input.Length - index;
                count++;
            }

            result[0] = '-';// First '-'
            result[1] = '-';// Second '-'
            int writePos = 2;// Start after '--'

            for (int i = 0; i < count; i++) {
                if (i > 0)
                    result[writePos++] = '-';

                int start = tempArray[i * 2];
                int length = tempArray[i * 2 + 1];

                for (int j = 0; j < length; j++) {
                    result[writePos++] = char.ToLower(input[start + j]);
                }
            }

            return new string(result, 0, writePos);
        }
        finally {
            ArrayPool<int>.Shared.Return(tempArray);
            ArrayPool<char>.Shared.Return(result);
        }
    }


}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.Theming.Generators.Helpers.Extensions;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class StringExtensions {
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? str)
        => string.IsNullOrEmpty(str);

    public static bool IsNotNullOrEmpty([NotNullWhen(true)] this string? str)
        => !string.IsNullOrEmpty(str);

    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? str)
        => string.IsNullOrWhiteSpace(str);

    public static bool IsNotNullOrWhiteSpace([NotNullWhen(true)] this string? str)
        => !string.IsNullOrWhiteSpace(str);
    
    public static string ToQuotedString(this string str)
        => $"\"{str}\"";
 
    // ReSharper disable once ReplaceSubstringWithRangeIndexer
    public static string Truncate(this string input, int maxLength)
        => input.Length <= maxLength ? input : input.Substring(0, maxLength);

    public static bool EndsWith(this string input, char expected) {
        int lastIndex = input.Length - 1;
        return lastIndex >= 0 && input[lastIndex] == expected;
    }
}

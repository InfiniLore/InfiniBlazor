// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniBlazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public enum EmoteContentType {
    Emoji,
    LucideIconName,
    SvgData,
    ResourcePathPng
}

public static class EmoteContentTypeUtility {
    public static string ToString(EmoteContentType type) => type switch {
        EmoteContentType.Emoji => nameof(EmoteContentType.Emoji),
        EmoteContentType.LucideIconName => nameof(EmoteContentType.LucideIconName),
        EmoteContentType.SvgData => nameof(EmoteContentType.SvgData),
        EmoteContentType.ResourcePathPng => nameof(EmoteContentType.ResourcePathPng),
        _ => nameof(EmoteContentType.Emoji)
    };
}
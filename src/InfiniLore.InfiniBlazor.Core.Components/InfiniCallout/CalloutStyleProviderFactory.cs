// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.Callouts;
using InfiniLore.Lucide;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Components;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CalloutStyleProviderFactory {
    public static ICalloutStyleProvider Create(IServiceProvider serviceProvider) {
        Dictionary<string, CalloutStyle> map = new Dictionary<string, CalloutStyle>()
            .AddStyle(new CalloutStyle(
                "note",
                ["quote"],
                LucideNames.Quote,
                "border-neutral-400 bg-zinc-900",
                "text-neutral-300",
                "border-neutral-600 text-neutral-400"
            ))
            .AddStyle(new CalloutStyle(
                "warning",
                ["warn"],
                LucideNames.TriangleAlert,
                "border-orange-500 bg-amber-950",
                "text-orange-300",
                "border-orange-700 text-orange-400"
            ))
            .AddStyle(new CalloutStyle(
                "tip",
                [],
                LucideNames.Lightbulb,
                "border-violet-500 bg-violet-950",
                "text-violet-300",
                "border-violet-700 text-violet-400"
            ))
            .AddStyle(new CalloutStyle(
                "danger",
                ["false"],
                LucideNames.Flame,
                "border-red-500 bg-red-950",
                "text-red-300",
                "border-red-700 text-red-400"
            ))
            .AddStyle(new CalloutStyle(
                "info",
                [],
                LucideNames.Info,
                "border-blue-500 bg-blue-950",
                "text-blue-300",
                "border-blue-700 text-blue-400"
            ))
            .AddStyle(new CalloutStyle(
                "success",
                ["true"],
                LucideNames.CircleCheck,
                "border-green-500 bg-green-950",
                "text-green-300",
                "border-green-700 text-green-400"
            ))
        ;

        var aliasMap = new Dictionary<string, string>();
        foreach (CalloutStyle style in map.Values) {
            foreach (string alternateKey in style.AlternateKeys) {
                aliasMap.Add(alternateKey, style.Key);
            }
        }

        return new CalloutStyleProvider {
            CalloutStyles = map.ToFrozenDictionary(keySelector: pair => pair.Key, elementSelector: ICalloutStyle (pair) => pair.Value),
            AliasMap = aliasMap.ToFrozenDictionary(),
            DefaultStyle = new CalloutStyle(
                string.Empty,
                [],
                LucideNames.Info,
                "border-(--border) bg-(--color-base-90)",
                "text-(--color-base-20)",
                "border-(--color-base-50) text-(--color-base-30)"
            )
        };
    }

    private static Dictionary<string, CalloutStyle> AddStyle(this Dictionary<string, CalloutStyle> map, CalloutStyle style) {
        map.Add(style.Key, style);
        return map;
    }
}

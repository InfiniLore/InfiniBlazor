// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Callouts;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class CalloutStyleProviderFactory {
    public static ICalloutStyleProvider Create(IServiceProvider serviceProvider) {
        var map = new Dictionary<string, (string, string)> {
            ["note"] = (LucideNames.Quote, "border-neutral-400 bg-zinc-900 text-neutral-300"),
            ["warning"] = (LucideNames.TriangleAlert, "border-orange-500 bg-amber-950 text-orange-300"),
            ["tip"] = (LucideNames.Lightbulb, "border-violet-500 bg-violet-950 text-violet-300"),
            ["danger"] = (LucideNames.Flame, "border-red-500 bg-red-950 text-red-300"),
            ["info"] = (LucideNames.Info, "border-blue-500 bg-blue-950 text-blue-300"),
            ["success"] = (LucideNames.CircleCheck, "border-green-500 bg-green-950 text-green-300")
        };

        return new CalloutStyleProvider {
            CalloutMakeup = map.ToFrozenDictionary(),
            DefaultCssClasses = "border-(--border) bg-(--color-base-90) text-(--color-base-20)",
            DefaultLucideIconName = LucideNames.Info
        };
    }
}

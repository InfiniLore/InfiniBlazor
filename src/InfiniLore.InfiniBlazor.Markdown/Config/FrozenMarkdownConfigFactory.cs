// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.DependencyInjection;
using System.Collections.Frozen;
using System.Collections.Immutable;

namespace InfiniLore.InfiniBlazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class FrozenMarkdownConfigFactory {
    public static FrozenMarkdownConfig FromServiceProvider(IServiceProvider serviceProvider) {
        var originalConfig = serviceProvider.GetRequiredService<MarkdownConfig>();
        
        return new FrozenMarkdownConfig {
            TextEditorModifierNames = originalConfig.TextEditor.GetModifierNames().OrderBy(name => name).ToImmutableArray(),
            ParserPreProcessors = CreateProcessorDictionary(serviceProvider, originalConfig.Parser.PreProcessors),
            ParserPostProcessors = CreateProcessorDictionary(serviceProvider, originalConfig.Parser.PostProcessors),
        };
    }

    private static FrozenDictionary<Type, ImmutableArray<object>> CreateProcessorDictionary(IServiceProvider serviceProvider, Dictionary<Type, List<Type>> originalDictionary) {
        int count = originalDictionary.Count;
        if (count == 0) return FrozenDictionary<Type, ImmutableArray<object>>.Empty;
        
        var newDictionary = new Dictionary<Type, List<object>>(originalDictionary.Count);
        foreach (KeyValuePair<Type, List<Type>> entry in originalDictionary) {
            foreach (Type type in entry.Value) {
                if (!newDictionary.TryGetValue(entry.Key, out List<object>? existing)) {
                    existing = new List<object>();
                }
                existing.Add(serviceProvider.GetRequiredService(type));
                newDictionary[entry.Key] = existing;
            }
        }
        
        return newDictionary.ToFrozenDictionary(
            pair => pair.Key,
            pair => pair.Value.ToImmutableArray()
        );
    }
}

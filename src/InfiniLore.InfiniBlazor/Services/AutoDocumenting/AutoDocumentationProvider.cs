// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.AutoDocumenting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IAutoDocumentationProvider>]
public class AutoDocumentationProvider(IEnumerable<IAutoDocumenterData> documentation) : IAutoDocumentationProvider {
    private Lazy<FrozenDictionary<string, IAutoDocumentationFragment>> Fragments { get; } = new(() => BuildFragments(documentation));

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    private static FrozenDictionary<string, IAutoDocumentationFragment> BuildFragments(IEnumerable<IAutoDocumenterData> documentation) {
        var dictionary = new Dictionary<string, AutoDocumentationFragment>();
        foreach (IAutoDocumenterData data in documentation) {
            foreach (KeyValuePair<string, ImmutableArray<string>> pair in data.CsharpData.Value) {
                foreach (string value in pair.Value) {
                    dictionary.AddOrUpdate(
                        pair.Key,
                        _ => new AutoDocumentationFragment(new List<string>(), new List<string> { value }),
                        (_, fragment) => {
                            fragment.CsharpDataList.Add(value);
                            return fragment;
                        });
                }
            }
            foreach (KeyValuePair<string, ImmutableArray<string>> pair in data.RazorData.Value) {
                foreach (string value in pair.Value) {
                    dictionary.AddOrUpdate(
                        pair.Key,
                        _ => new AutoDocumentationFragment(new List<string> { value }, new List<string>()),
                        (_, fragment) => {
                            fragment.RazorDataList.Add(value);
                            return fragment;
                        });
                }
            }
        }
        
        return dictionary
            .Select<KeyValuePair<string, AutoDocumentationFragment>, KeyValuePair<string, IAutoDocumentationFragment>>(pair => new KeyValuePair<string, IAutoDocumentationFragment>(pair.Key, pair.Value))
            .ToFrozenDictionary();
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryGetDocumentationFragment(string id, [NotNullWhen(true)] out IAutoDocumentationFragment? fragment) 
        => Fragments.Value.TryGetValue(id, out fragment);
}

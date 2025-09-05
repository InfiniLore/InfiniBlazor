// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Config;
using InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class MarkdownConfig(IInfiniBlazorConfig config) : IMarkdownConfig {
    private Dictionary<Type, MdComponentRecord> ComponentRecords { get; } = new(32);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public MarkdownConfig RegisterComponent<TNode, TComponent>() where TComponent : InfiniBlazorMdComponentBase<TNode>, IMdSyntaxNode where TNode : class, IMdSyntaxNode {
        int count = ComponentRecords.Count;
        if (ComponentRecords.Capacity < count + 1) ComponentRecords.EnsureCapacity(count * 2);
        
        ComponentRecords.Add(typeof(TNode), MdComponentRecord.FromType<TComponent, TNode>());   
        return this;
    }
    
    public FrozenDictionary<Type, IMdComponentRecord> GetComponentRecords() {
        ComponentRecords.TrimExcess();
        return ComponentRecords.ToFrozenDictionary(
            pair => pair.Key, 
            IMdComponentRecord (pair) => pair.Value);
    }
    
    
}

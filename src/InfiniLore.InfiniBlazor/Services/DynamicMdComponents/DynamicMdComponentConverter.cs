// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.DynamicMdComponents;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class DynamicMdComponentConverter : IDynamicMdComponentConverter {
    public required FrozenDictionary<Type, DynamicMdComponentRecord> NodeToComponentMap { get; init; }
    private bool RenderUnknownComponents { get; set; } = true;
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------

    // ReSharper disable once UnusedVariable
    private void RenderComponent(RenderTreeBuilder builder, IMdSyntaxNode node) {
        if (!NodeToComponentMap.TryGetValue(node.Type, out DynamicMdComponentRecord? data)){
            if (!RenderUnknownComponents) return;
            data = DynamicMdComponentRecord.Empty;
        }
            
        int sequence = 0;
        builder.OpenComponent(sequence++, data.ComponentType);
        int newSequence = data.Builder(builder, sequence, node); // newSequence is not used so far
            
        builder.CloseComponent();
    }
    
    // ReSharper disable once InconsistentNaming
    public RenderFragment RenderChildComponents(IMdSyntaxNode? node) => __builder => {
        int childCount = node?.ChildCount ?? 0;
        if (node is null || childCount == 0) return;
        
        ReadOnlySpan<IMdSyntaxNode> childSpan = node.GetChildrenSpan();
        for (int i = 0; i < childCount; i++) {
            RenderComponent(__builder, childSpan[i]);
        }
    };

    // ReSharper disable once InconsistentNaming
    public RenderFragment RenderRootComponents(IEnumerable<IMdSyntaxNode> nodes) => __builder => {
        foreach (IMdSyntaxNode child in nodes) RenderComponent(__builder, child);
    };
}



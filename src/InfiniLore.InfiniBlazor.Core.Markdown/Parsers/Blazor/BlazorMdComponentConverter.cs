// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class BlazorMdComponentConverter : IBlazorMdComponentConverter {
    public required FrozenDictionary<Type, MdComponentRecord> NodeToComponentMap { get; init; }
    public required FrozenSet<Type> SkippedComponentTypes { get; init; }
    public required bool RenderUnknownComponents { get; init; }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------

    // ReSharper disable once UnusedVariable
    private void RenderNodeAsComponent(RenderTreeBuilder builder, IMdSyntaxNode node) {
        if (SkippedComponentTypes.Contains(node.Type)) return;
        
        if (!NodeToComponentMap.TryGetValue(node.Type, out MdComponentRecord? data)){
            if (!RenderUnknownComponents) return;
            data = MdComponentRecord.Empty;
        }
            
        int sequence = 0;
        builder.OpenComponent(sequence++, data.ComponentType);
        int newSequence = data.Builder(builder, sequence, node); // newSequence is not used so far
        builder.SetKey(node.Id);
            
        builder.CloseComponent();
    }
    
    // ReSharper disable once InconsistentNaming
    public RenderFragment RenderComponent(IMdSyntaxNode node) 
        => __builder => RenderNodeAsComponent(__builder, node);
    
    // ReSharper disable once InconsistentNaming
    public RenderFragment RenderComponentDebug(IMdSyntaxNode node) => __builder => {
        var data = MdComponentRecord.Empty;
        int sequence = 0;
        
        __builder.OpenComponent(sequence++, data.ComponentType);
        data.Builder(__builder, sequence, node); // newSequence is not used so far
        __builder.CloseComponent();
    };
    
    // ReSharper disable once InconsistentNaming
    public RenderFragment RenderChildComponents(IMdSyntaxNode node) => __builder => {
        int childCount = node.ChildCount;
        if (childCount == 0) return;
        
        ReadOnlySpan<IMdSyntaxNode> childSpan = node.GetChildrenSpan();
        for (int i = 0; i < childCount; i++) {
            RenderNodeAsComponent(__builder, childSpan[i]);
        }
    };

    // ReSharper disable once InconsistentNaming
    public RenderFragment RenderRootComponents(IEnumerable<IMdSyntaxNode> nodes) => __builder => {
        foreach (IMdSyntaxNode child in nodes) RenderNodeAsComponent(__builder, child);
    };
}



// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Config;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IBlazorMdComponentConverter>]
public class BlazorMdComponentConverter(IMarkdownConfig config) : IBlazorMdComponentConverter {
    public required FrozenDictionary<Type, IMdComponentRecord> NodeToComponentMap { get; init; } = config.GetComponentRecords();
    public required FrozenSet<Type> SkippedComponentTypes { get; init; } = config.GetSkippedBlazorComponentTypes();
    public required bool RenderUnknownComponents { get; init; } = config.RenderUnknownBlazorComponents;

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    private void RenderNodeAsComponent(RenderTreeBuilder builder, IMdSyntaxNode node, bool ignoreSkipComponents = false) {
        if (!ignoreSkipComponents && SkippedComponentTypes.Contains(node.Type)) return;

        if (!NodeToComponentMap.TryGetValue(node.Type, out IMdComponentRecord? data)) {
            if (!RenderUnknownComponents) return;

            data = MdComponentRecord.Empty;
        }

        int sequence = 0;
        builder.OpenComponent(sequence++, data.ComponentType);
        int _ = data.Builder(builder, sequence, node);// newSequence is not used so far
        builder.SetKey(node.Id);

        builder.CloseComponent();
    }

    public RenderFragment RenderComponent(IMdSyntaxNode node)
        => builder => RenderNodeAsComponent(builder, node);

    public RenderFragment RenderComponentDebug(IMdSyntaxNode node) => builder => {
        var data = MdComponentRecord.Empty;
        int sequence = 0;

        builder.OpenComponent(sequence++, data.ComponentType);
        data.Builder(builder, sequence, node);// newSequence is not used so far
        builder.CloseComponent();
    };

    public RenderFragment RenderChildComponents(IMdSyntaxNode node) => builder => {
        int childCount = node.ChildCount;
        if (childCount == 0) return;

        ReadOnlySpan<IMdSyntaxNode> childSpan = node.GetChildrenSpan();
        for (int i = 0; i < childCount; i++) {
            RenderNodeAsComponent(builder, childSpan[i]);
        }
    };

    public RenderFragment RenderRootComponents(IEnumerable<IMdSyntaxNode> nodes) => builder => {
        foreach (IMdSyntaxNode child in nodes) RenderNodeAsComponent(builder, child);
    };

    public RenderFragment RenderRootComponentsWithSkipped(IEnumerable<IMdSyntaxNode> nodes) => builder => {
        foreach (IMdSyntaxNode child in nodes) RenderNodeAsComponent(builder, child, true);
    };
}

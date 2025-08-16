// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.DynamicMdComponents;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax.Nodes;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Rendering;
using System.Collections.Frozen;
using System.Runtime.CompilerServices;

namespace InfiniLore.InfiniBlazor.Components.DynamicMarkdownComponents;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IDynamicMdComponentConverter>]
public class DynamicMdComponentConverter : IDynamicMdComponentConverter {
    private FrozenDictionary<Type, ComponentRecord> NodeToComponentMap { get; } = CreateNodeToComponentMap();

    private record ComponentRecord(Type ComponentType, Func<RenderTreeBuilder, int, IMdSyntaxNode, int> Builder) {
        public static ComponentRecord FromType<TComponent, TNode>() 
            where TComponent : DynamicMdComponentBase<TNode> 
            where TNode : class, IMdSyntaxNode {
            return new ComponentRecord(
                typeof(TComponent),
                static (builder, sequence, node) => {
                    builder.AddAttribute(sequence++, "SyntaxNode", Unsafe.As<TNode>(node));
                    return sequence;
                });
        }
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    private static FrozenDictionary<Type, ComponentRecord> CreateNodeToComponentMap() {
        var mapBuilder = new Dictionary<Type, ComponentRecord> {
            [typeof(HeadingMdSyntaxNode)] = ComponentRecord.FromType<HeadingDynamicMdComponent, HeadingMdSyntaxNode>(),
            [typeof(ContentMdSyntaxNode)] = ComponentRecord.FromType<ContentDynamicMdComponent, ContentMdSyntaxNode>()
        };
        return mapBuilder.ToFrozenDictionary();
    }
    
    private static ComponentRecord EmptyComponentRecord { get; } = new(
        typeof(UnknownDynamicMdComponent),
        static (builder, sequence, node) => {
            builder.AddAttribute(sequence++, "SyntaxNode", node);
            return sequence;
        });
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------

    // ReSharper disable once UnusedVariable
    private void RenderComponent(RenderTreeBuilder builder, IMdSyntaxNode node) {
        ComponentRecord data = NodeToComponentMap.GetValueOrDefault(node.Type, EmptyComponentRecord);
            
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



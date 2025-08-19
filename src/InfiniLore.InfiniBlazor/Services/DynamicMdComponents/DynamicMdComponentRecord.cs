// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.DynamicMarkdownComponents;
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.AspNetCore.Components.Rendering;
using System.Runtime.CompilerServices;

namespace InfiniLore.InfiniBlazor.DynamicMdComponents;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record DynamicMdComponentRecord(Type ComponentType, Func<RenderTreeBuilder, int, IMdSyntaxNode, int> Builder) {
    public static DynamicMdComponentRecord Empty { get; } = new(
        typeof(UnknownDynamicMdComponent),
        static (builder, sequence, node) => {
            builder.AddAttribute(sequence++, "SyntaxNode", node);
            return sequence;
        });
    
    public static DynamicMdComponentRecord FromType<TComponent, TNode>() 
        where TComponent : DynamicMdComponentBase<TNode> 
        where TNode : class, IMdSyntaxNode {
        return new DynamicMdComponentRecord(
            typeof(TComponent),
            static (builder, sequence, node) => {
                builder.AddAttribute(sequence++, "SyntaxNode", Unsafe.As<TNode>(node));
                return sequence;
            });
    }
}
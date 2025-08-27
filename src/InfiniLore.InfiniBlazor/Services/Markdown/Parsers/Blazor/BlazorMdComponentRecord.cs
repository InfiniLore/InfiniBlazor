// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Components.DynamicMarkdownComponents;
using Microsoft.AspNetCore.Components.Rendering;
using System.Runtime.CompilerServices;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record BlazorMdComponentRecord(Type ComponentType, Func<RenderTreeBuilder, int, IMdSyntaxNode, int> Builder) {
    public static BlazorMdComponentRecord Empty { get; } = new(
        typeof(UnknownBlazorMdComponent),
        static (builder, sequence, node) => {
            builder.AddAttribute(sequence++, "SyntaxNode", node);
            return sequence;
        });
    
    public static BlazorMdComponentRecord FromType<TComponent, TNode>() 
        where TComponent : InfiniBlazorMdComponentBase<TNode> 
        where TNode : class, IMdSyntaxNode {
        return new BlazorMdComponentRecord(
            typeof(TComponent),
            static (builder, sequence, node) => {
                builder.AddAttribute(sequence++, "SyntaxNode", Unsafe.As<TNode>(node));
                return sequence;
            });
    }
}
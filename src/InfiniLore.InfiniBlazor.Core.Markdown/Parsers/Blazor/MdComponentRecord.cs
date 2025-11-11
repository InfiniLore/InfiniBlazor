// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.AspNetCore.Components.Rendering;
using System.Runtime.CompilerServices;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.Blazor;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed record MdComponentRecord(Type ComponentType, Func<RenderTreeBuilder, int, IMdSyntaxNode, int> Builder) : IMdComponentRecord {
    public static MdComponentRecord Empty { get; } = new(
        typeof(UnknownBlazorMdComponent),
        static (builder, sequence, node) => {
            builder.AddAttribute(sequence++, "SyntaxNode", node);
            return sequence;
        });

    public static MdComponentRecord FromType<TComponent, TNode>()
        where TComponent : InfiniBlazorMdComponentBase<TNode>
        where TNode : class, IMdSyntaxNode {
        return new MdComponentRecord(
            typeof(TComponent),
            static (builder, sequence, node) => {
                builder.AddAttribute(sequence++, "SyntaxNode", Unsafe.As<TNode>(node));
                return sequence;
            });
    }
}

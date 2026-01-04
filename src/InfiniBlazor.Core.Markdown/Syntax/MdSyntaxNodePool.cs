// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Runtime.CompilerServices;

namespace InfiniBlazor.Markdown.Syntax;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodePool<T> where T : class, IMdSyntaxNode, new() {
    public static MdSyntaxNodePool<T> Shared { get; } = new();
    
    private static ObjectPool<T> Pool { get; } = PoolingHelpers.CreateResettablePool<T>(PoolingHelpers.VisitorPerParserRetained);
    
    public T Get() => Pool.Get();
    public void Return(T node) => Pool.Return(node);
    public void Return(IMdSyntaxNode node) => Pool.Return(Unsafe.As<T>(node));
}
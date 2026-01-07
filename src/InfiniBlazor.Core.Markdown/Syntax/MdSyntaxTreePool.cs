// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;

namespace InfiniBlazor.Markdown.Syntax;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxTreePool {
    public static MdSyntaxTreePool Shared { get; } = new();
    
    private ObjectPool<MdSyntaxTree> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxTree>(16);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMdSyntaxTree Get() => Pool.Get();
    public void Return(MdSyntaxTree tree) => Pool.Return(tree);
    public void Return(IMdSyntaxTree tree) => Pool.Return(tree as MdSyntaxTree ?? throw new ArgumentException("Tree must be of type MdSyntaxTree", nameof(tree)));
}

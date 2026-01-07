// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxFragmentStackPool {
    public static MdSyntaxFragmentStackPool Shared { get; } = new();
    
    private ObjectPool<MdSyntaxFragmentStack> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxFragmentStack>(16);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public MdSyntaxFragmentStack Get() => Pool.Get();
    public void Return(MdSyntaxFragmentStack stack) => Pool.Return(stack);
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using System.Buffers;

namespace InfiniLore.Blazor.Markdown.Services.TextModifiers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextModifier>("bold")]
public class BoldModifier : ITextModifier {
    public string IconName { get; } = "bold";
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string Modify(string input, Range range) {
        // Calculate indices for range
        int start = range.Start.GetOffset(input.Length);
        int end = range.End.GetOffset(input.Length);

        // Precompute the final size needed for the buffer
        int finalLength = input.Length + 4;// Add 4 because you're inserting `**` twice

        // Rent a buffer using ArrayPool to avoid frequent allocations
        char[] buffer = ArrayPool<char>.Shared.Rent(finalLength);

        try {
            ReadOnlySpan<char> mod = "**".AsSpan();
            
            input.AsSpan(0, start).CopyTo(buffer.AsSpan(0, start));
            mod.CopyTo(buffer.AsSpan(start));
            input.AsSpan(start, end - start).CopyTo(buffer.AsSpan(start + 2));
            mod.CopyTo(buffer.AsSpan(start + 2 + (end - start)));
            input.AsSpan(end).CopyTo(buffer.AsSpan(start + 4 + (end - start)));

            // Create a result string from the buffer
            return new string(buffer, 0, finalLength);
        }
        finally {
            // Return the buffer to the pool to avoid memory leaks
            ArrayPool<char>.Shared.Return(buffer);
        }

    }
}

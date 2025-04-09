// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Buffers;

namespace InfiniLore.Blazor.Markdown.Services.TextModifiers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<ITextModifier>("italic")]
public class ItalicModifier(ILogger<ItalicModifier> logger) : ITextModifier {
    public string IconName => "italic";

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string Modify(string input, Range range) {
        // Calculate indices for range
        int length = input.Length;
        int start = range.Start.GetOffset(length);
        int end = range.End.GetOffset(length);
        
        // Validate range
        if (start < 0 || end > length || start > end) {
            logger.LogWarning("Invalid range: start={start}, end={end}, length={length}", start, end, length);
            return input;
        }

        
        // Precompute the final size needed for the buffer
        int finalLength = length + 2;// Add 2 because you're inserting `*` twice

        // Rent a buffer using ArrayPool to avoid frequent allocations
        char[] buffer = ArrayPool<char>.Shared.Rent(finalLength);

        try {
            ReadOnlySpan<char> mod = "*".AsSpan();

            input.AsSpan(0, start).CopyTo(buffer.AsSpan(0, start));
            mod.CopyTo(buffer.AsSpan(start));
            input.AsSpan(start, end - start).CopyTo(buffer.AsSpan(start + 1));
            mod.CopyTo(buffer.AsSpan(start + 1 + (end - start)));
            input.AsSpan(end).CopyTo(buffer.AsSpan(start + 2 + (end - start)));

            return new string(buffer, 0, finalLength);

        }
        finally {
            // Return the buffer to the pool to avoid memory leaks
            ArrayPool<char>.Shared.Return(buffer);
        }
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.Logging;
using System.Buffers;

namespace InfiniLore.Blazor.Markdown.Services.TextModifiers;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class SingleInstructionModifiers(ILogger logger) : ITextModifier {
    public abstract string IconName { get; }
    public abstract string ModifierName { get; }
    public bool IsSingleLineStructure => true;

    protected abstract string Instruction { get; }

    public string Modify(string input, Range range, ITextEditor editor) {
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
        ReadOnlySpan<char> mod = Instruction.AsSpan();
        int modLength = mod.Length;
        int finalLength = length + modLength*2;

        // Rent a buffer using ArrayPool to avoid frequent allocations
        char[] buffer = ArrayPool<char>.Shared.Rent(finalLength);

        try {
            input.AsSpan(0, start).CopyTo(buffer.AsSpan(0, start));
            mod.CopyTo(buffer.AsSpan(start));
            input.AsSpan(start, end - start).CopyTo(buffer.AsSpan(start + modLength));
            mod.CopyTo(buffer.AsSpan(start + modLength + (end - start)));
            input.AsSpan(end).CopyTo(buffer.AsSpan(start + modLength*2 + (end - start)));
            
            editor.UpdateCaret(range.Start.Value + mod.Length);
            return new string(buffer, 0, finalLength);

        }
        finally {
            // Return the buffer to the pool to avoid memory leaks
            ArrayPool<char>.Shared.Return(buffer);
        }
    }
}

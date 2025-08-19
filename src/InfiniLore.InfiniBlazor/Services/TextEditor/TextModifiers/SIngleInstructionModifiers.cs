// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.Logging;
using System.Buffers;

namespace InfiniLore.InfiniBlazor.TextEditor.TextModifiers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class SingleInstructionModifiers(ILogger logger) : ITextModifier {

    protected abstract string Instruction { get; }
    public abstract string IconName { get; }
    public abstract string ModifierName { get; }
    public bool IsSingleLineStructure { get; } = true;

    public void Modify(ITextSource source, Range range, ITextEditor editor) {
        ReadOnlySpan<char> inputSpan = source.TextSpan;

        // Calculate indices for range
        int length = source.Length;
        int start = range.Start.GetOffset(length);
        int end = range.End.GetOffset(length);

        // Validate range
        if (start < 0 || end > length || start > end) {
            logger.Warning("Invalid range: start={start}, end={end}, length={length}", start, end, length);
            return;
        }

        // Precompute the final size needed for the buffer
        ReadOnlySpan<char> instructionSpan = Instruction.AsSpan();
        int instructionLength = instructionSpan.Length;
        int finalLength = length + instructionLength * 2;

        // Rent a buffer using ArrayPool to avoid frequent allocations
        char[] buffer = ArrayPool<char>.Shared.Rent(finalLength);
        Span<char> bufferSpan = buffer.AsSpan();

        try {
            // Build the resulting string with modifications
            inputSpan[..start].CopyTo(bufferSpan[..start]);
            instructionSpan.CopyTo(bufferSpan[start..]);
            inputSpan.Slice(start, end - start).CopyTo(bufferSpan[(start + instructionLength)..]);
            instructionSpan.CopyTo(bufferSpan[(start + instructionLength + (end - start))..]);
            inputSpan[end..].CopyTo(bufferSpan[(start + instructionLength * 2 + (end - start))..]);

            // Place caret after the first instruction
            editor.UpdateCaret(range.Start.Value + instructionLength);

            // Return only the relevant portion of the buffer as a string
            source.Text = new string(bufferSpan[..finalLength]);
        }
        finally {
            // Return the buffer to the pool to avoid memory leaks
            ArrayPool<char>.Shared.Return(buffer);
        }
    }
}

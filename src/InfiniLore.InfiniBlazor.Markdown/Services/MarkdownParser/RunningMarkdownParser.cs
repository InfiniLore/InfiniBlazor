// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Microsoft.Extensions.ObjectPool;
using System.Buffers;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class RunningMarkdownParser : IRunningMarkdownParser, IResettable {
    private readonly Stack<ParserDataDto> _stack = new();
    public IMdNodeTree NodeTree { get; set; } = null!;

    public bool TryPopDto([NotNullWhen(true)] out ParserDataDto? dto)
        => _stack.TryPop(out dto);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region AddToStack
    public void AddMultiLineMatchesToStack(string input, IMdNode node, ParserOrigin origin) {
        MatchCollection matches = MarkdownRegexLib.MultilineStructuresRegex.Matches(input);
        int count = matches.Count;
        
        Match[] matchArray = ArrayPool<Match>.Shared.Rent(count);
        matches.CopyTo(matchArray, 0);
        _stack.EnsureCapacity(_stack.Count + matches.Count);

        // Process matches in reverse order for _stack
        for (int i = count - 1; i >= 0; i--) {
            PushMatchToStack(matchArray[i], node, origin);
        }
        
        ArrayPool<Match>.Shared.Return(matchArray);
    }

    public void AddSingleLineMatchesToStack(string input, IMdNode node, ParserOrigin origin) {
        MatchCollection matches = MarkdownRegexLib.SinglelineStructuresRegex.Matches(input);
        int count = matches.Count;
        
        Match[] matchArray = ArrayPool<Match>.Shared.Rent(count);
        matches.CopyTo(matchArray, 0);
        _stack.EnsureCapacity(_stack.Count + matches.Count);
        
        int currentIndex = input.Length;

        // Process matches in reverse order for _stack
        for (int i = count - 1; i >= 0; i--) {
            Match match = matchArray[i];
            int matchEnd = match.Index + match.Length;

            // If there's an uncaught text between this match's end and the last position, add it as raw input
            if (matchEnd < currentIndex) {
                PushContentToStack(input[matchEnd..currentIndex], node, origin);
            }

            PushMatchToStack(match, node, origin);
            currentIndex = match.Index;
        }

        // ReSharper disable once InvertIf
        if (currentIndex > 0) {
            // Handle any remaining text before the first match
            PushContentToStack(input[..currentIndex], node, origin);
        }
        ArrayPool<Match>.Shared.Return(matchArray);
    }

    public void PushContentToStack(string content, IMdNode currentNode, ParserOrigin origin) 
        => PushElementToStack(content, currentNode, origin, MdElement.Content);

    public void PushElementToStack(string? content, IMdNode currentNode, ParserOrigin origin, MdElement element) {
        ParserDataDto dto = PoolCache.ParserDataDtoPool.Get();
        dto.AsElement(content, currentNode, origin, element);
        _stack.Push(dto);
    }

    private void PushMatchToStack(Match match, IMdNode currentNode, ParserOrigin origin) {
        ParserDataDto dto = PoolCache.ParserDataDtoPool.Get();
        dto.AsMatch(match, currentNode, origin);
        _stack.Push(dto);
    }
    #endregion
    
    public bool TryReset() {
        while (_stack.TryPop(out ParserDataDto? dto)) PoolCache.ParserDataDtoPool.Return(dto); // Makes sure we clean everything
        NodeTree = null!;
        
        return _stack.Count == 0 ;
    }
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Markdown.Pools;
using System.Collections.Immutable;
using System.Diagnostics.CodeAnalysis;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class RunningMarkdownParser : IRunningMarkdownParser {
    public IMdNode RootNode { get; set; } = null!;
    private readonly Stack<ParserDataDto> _stack = new();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region AddToStack
    public void AddMultiLineMatchesToStack(string input, IMdNode node, ParserOrigin origin) {
        MatchCollection matches = MarkdownRegexLib.MultilineStructuresRegex.Matches(input);
        IEnumerable<Match> matchesList = matches.ToImmutableArray().Reverse();
        _stack.EnsureCapacity(_stack.Count + matches.Count);
        
        // Process matches in reverse order for _stack
        foreach (Match match in matchesList) {
            ParserDataDto dto = ParserDataDtoPool.Get();
            dto.AsMatch(node, origin, match);
            _stack.Push(dto);
        }
    }
    
    public void AddSingleLineMatchesToStack(string input, IMdNode node, ParserOrigin origin) {
        MatchCollection matches = MarkdownRegexLib.SinglelineStructuresRegex.Matches(input);
        IEnumerable<Match> matchesList = matches.ToImmutableArray().Reverse();
        _stack.EnsureCapacity(_stack.Count + matches.Count);
        int currentIndex = input.Length;

        // Process matches in reverse order for _stack
        foreach (Match match in matchesList) {
            int matchEnd = match.Index + match.Length;
        
            // If there's text between this match's end and the last position, add it as raw input
            if (matchEnd < currentIndex) {
                ParserDataDto preDto = ParserDataDtoPool.Get();
                preDto.AsElement(node, origin, input[matchEnd..currentIndex], MdElement.Content);
                _stack.Push(preDto);
            }
        
            
            ParserDataDto dto = ParserDataDtoPool.Get();
            dto.AsMatch(node, origin, match);
            _stack.Push(dto);
            currentIndex = match.Index;
        }

        // ReSharper disable once InvertIf
        if (currentIndex > 0) {
            // Handle any remaining text before the first match
            ParserDataDto dto = ParserDataDtoPool.Get();
            dto.AsElement(node, origin, input[..currentIndex], MdElement.Content);
            _stack.Push(dto);
        }
    }
    public void PushContentToStack(string content, IMdNode currentNode, ParserOrigin origin) {
        PushElementToStack(content, currentNode, origin, MdElement.Content);
    }
    
    public void PushElementToStack(string? content, IMdNode currentNode, ParserOrigin origin, MdElement element) {
        ParserDataDto dto = ParserDataDtoPool.Get();
        dto.AsElement(currentNode, origin, content, element);
        _stack.Push(dto);
    }
    #endregion

    public bool TryPopDto([NotNullWhen(true)] out ParserDataDto? dto) 
        => _stack.TryPop(out dto);
    
    public void Clear() {
        while (_stack.TryPop(out ParserDataDto? dto)) ParserDataDtoPool.Return(dto); // Makes sure we clean everything
        _stack.Clear();
        RootNode = null!;
    }
}

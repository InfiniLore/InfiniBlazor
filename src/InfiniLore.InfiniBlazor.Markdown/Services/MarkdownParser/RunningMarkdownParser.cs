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
        AddMatchesToStack(matches, input, node, origin);
    }
    
    public void AddSingleLineMatchesToStack(string input, IMdNode node, ParserOrigin origin) {
        MatchCollection matches = MarkdownRegexLib.SinglelineStructuresRegex.Matches(input);
        AddMatchesToStack(matches,input, node, origin);
    }
    
    private void AddMatchesToStack(MatchCollection matches, string input, IMdNode node, ParserOrigin origin) {
        _stack.EnsureCapacity(_stack.Count + matches.Count);
    
        int currentIndex = input.Length;

        // Process matches in reverse order for _stack
        IOrderedEnumerable<Match> matchesList = matches.ToImmutableArray().OrderByDescending(m => m.Index);
        foreach (Match match in matchesList) {
            int matchEnd = match.Index + match.Length;
        
            // If there's text between this match's end and the last position, add it as raw input
            if (matchEnd < currentIndex) {
                node.WithContent(input[matchEnd..currentIndex]);
            }
        
            
            ParserDataDto dto = ParserDataDtoPool.Get();
            dto.AsMatch(node, origin, match);
            _stack.Push(dto);
            currentIndex = match.Index;
        }

        // Handle any remaining text before the first match
        if (currentIndex > 0) {
            node.WithContent(input[..currentIndex]);
        }
    }
    #endregion

    public bool TryPopDto([NotNullWhen(true)] out ParserDataDto? dto) 
        => _stack.TryPop(out dto);

    public void PushMatches(MatchCollection collection) {
        ImmutableArray<Match> matches = collection.ToImmutableArray();
        int length = matches.Length;
        _stack.EnsureCapacity(_stack.Count + length);

        for (int i = length - 1; i >= 0; i--) {
            ParserDataDto dto = ParserDataDtoPool.Get();
            dto.AsMatch(RootNode, ParserOrigin.NotSkipped, matches[i]);
            _stack.Push(dto);
        }
    }
    
    public void CleanupDto(ParserDataDto dto) {
        ParserDataDtoPool.Return(dto);
    }

    public void Clear() {
        _stack.Clear();
        RootNode = null!;
    }
}

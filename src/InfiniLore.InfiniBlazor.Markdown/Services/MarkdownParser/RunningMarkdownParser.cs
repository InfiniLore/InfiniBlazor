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
    private readonly Stack<ParserDataDto> _stack = new();
    public IMdNodeTree NodeTree { get; set; } = null!;

    public bool TryPopDto([NotNullWhen(true)] out ParserDataDto? dto)
        => _stack.TryPop(out dto);

    public void Clear() {
        while (_stack.TryPop(out ParserDataDto? dto)) ParserDataDtoPool.Return(dto);// Makes sure we clean everything
        _stack.Clear();
        NodeTree = null!;
    }

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
            PushMatchToStack(match, node, origin);
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
                preDto.AsElement(input[matchEnd..currentIndex], node, origin, MdElement.Content);
                _stack.Push(preDto);
            }

            PushMatchToStack(match, node, origin);
            currentIndex = match.Index;
        }

        // ReSharper disable once InvertIf
        if (currentIndex > 0) {
            // Handle any remaining text before the first match
            ParserDataDto dto = ParserDataDtoPool.Get();
            dto.AsElement(input[..currentIndex], node, origin, MdElement.Content);
            _stack.Push(dto);
        }
    }

    public void PushContentToStack(string content, IMdNode currentNode, ParserOrigin origin) {
        PushElementToStack(content, currentNode, origin, MdElement.Content);
    }

    public void PushElementToStack(string? content, IMdNode currentNode, ParserOrigin origin, MdElement element) {
        ParserDataDto dto = ParserDataDtoPool.Get();
        dto.AsElement(content, currentNode, origin, element);
        _stack.Push(dto);
    }

    private void PushMatchToStack(Match match, IMdNode currentNode, ParserOrigin origin) {
        ParserDataDto dto = ParserDataDtoPool.Get();
        dto.AsMatch(match, currentNode, origin);
        _stack.Push(dto);
    }
    #endregion
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownSyntaxTreeParser<string>>]
public class StringMarkdownSyntaxTreeParser(IServiceProvider serviceProvider, ILogger<StringMarkdownSyntaxTreeParser> logger) : IMarkdownSyntaxTreeParser<string> {
    private readonly FrozenDictionary<string, IMarkdownElementHandler> _elementHandlers = ToFrozenDictionary(logger, serviceProvider);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Constructor Helpers
    private static FrozenDictionary<string, IMarkdownElementHandler> ToFrozenDictionary(ILogger logger, IServiceProvider serviceProvider) {
        ReadOnlySpan<string> keyNames = MarkdownRegexLib.MarkdownStructureGroupNames.AsSpan();
        var dictionaryBuilder = new Dictionary<string, IMarkdownElementHandler>(keyNames.Length);

        for (int index = keyNames.Length - 1; index >= 0; index--) {
            string groupName = keyNames[index];
            if (serviceProvider.GetKeyedService<IMarkdownElementHandler>(groupName) is not {} service) {
                logger.LogWarning("No MarkdownElementHandler service found for group name '{groupName}'", groupName);
                continue;
            }

            dictionaryBuilder[groupName] = service;
        }

        return dictionaryBuilder.ToFrozenDictionary();
    }
    #endregion
    
    public void ParseToNodeTree(string markdown, IMarkdownSyntaxTree nodeTree) {
        MarkdownParserEngine runningParser = PoolCache.RunningMarkdownParserPool.Get();

        try {
            runningParser.NodeTree = nodeTree;
            runningParser.AddMultiLineMatchesToStack(markdown, nodeTree.RootNode, HandlerOrigin.Undefined);

            while (runningParser.TryPopDto(out MarkdownFragment? fragment)) {
                try {
                    IMarkdownSyntaxNode currentNode = fragment.Node;
                    HandlerOrigin origin = fragment.Origin;

                    if (fragment.IsMatch) {
                        ProcessMatch(fragment.Match, currentNode, origin, runningParser);
                        continue;
                    }

                    // ReSharper disable once InvertIf
                    if (fragment.Element is not MarkdownElement.Undefined) {
                        ProcessElement(fragment.Element, fragment.Content, currentNode);
                    }
                }
                finally {
                    PoolCache.MarkdownFragmentPool.Return(fragment);
                }
            }
        }
        finally {
            PoolCache.RunningMarkdownParserPool.Return(runningParser);
        }
    }

    private void ProcessMatch(Match match, IMarkdownSyntaxNode currentNode, HandlerOrigin origin, IMarkdownParserEngine runningParser) {
        GroupCollection groups = match.Groups;
        for (int i = 0; i < groups.Count; i++) {
            if (groups[i] is not { Success: true, Name: var name } group) continue;
            if (!_elementHandlers.TryGetValue(name, out IMarkdownElementHandler? handler)) continue;

            HandlerOrigin handlerOrigin = handler.SkipOnOrigin;
            if (handlerOrigin is HandlerOrigin.NotSkipped || !origin.HasFlagFast(handlerOrigin)) {
                handler.HandleMatch(runningParser, currentNode, match, group, origin);
            }
        }
    }

    private static void ProcessElement(MarkdownElement element, string? content, IMarkdownSyntaxNode currentNode) {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (element) {
            case MarkdownElement.HtmlContent: currentNode.WithHtmlContent(content);
                break;
            case MarkdownElement.Content: currentNode.WithContent(content);
                break;
            default: currentNode.AddChildNode(element, content);
                break;
        }
    }
}

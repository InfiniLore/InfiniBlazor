// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownSyntaxTreeParser>]
public sealed class MarkdownSyntaxTreeParser(IServiceProvider serviceProvider, ILogger<MarkdownSyntaxTreeParser> logger) : IMarkdownSyntaxTreeParser {
    private readonly FrozenDictionary<string, IMarkdownElementHandler> _elementHandlers = ToFrozenDictionary<IMarkdownElementHandler>(logger, serviceProvider);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Constructor Helpers
    private static FrozenDictionary<string, T> ToFrozenDictionary<T>(ILogger logger, IServiceProvider serviceProvider) {
        ImmutableArray<string> keyNames = MarkdownRegexLib.MarkdownStructureGroupNames;
        var dictionaryBuilder = new Dictionary<string, T>(keyNames.Length);

        foreach (string groupName in keyNames) {
            if (serviceProvider.GetKeyedService<T>(groupName) is not {} service) {
                logger.LogWarning("No service found for group name '{groupName}' for type '{name}'.", groupName, typeof(T).Name);
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

                    if (fragment.TryGetAsMatch(out Match? match)) {
                        ProcessMatch(match, currentNode, origin, runningParser);
                        continue;
                    }
                    if (fragment.TryGetAsElement(out MarkdownElement element, out string? content)) {
                        ProcessElement(element, content, currentNode);
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
            if (groups[i] is not { Success: true, Name: var name }) continue;
            if (!_elementHandlers.TryGetValue(name, out IMarkdownElementHandler? handler)) continue;

            HandlerOrigin handlerOrigin = handler.SkipOnOrigin;
            if (handlerOrigin is HandlerOrigin.NotSkipped || !origin.HasFlagFast(handlerOrigin)) {
                handler.HandleMatch(runningParser, currentNode, match, groups[i], origin);
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

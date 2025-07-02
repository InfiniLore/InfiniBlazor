// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
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
    
    public void ParseToNodeTreeAsync(string markdown, IMarkdownSyntaxTree nodeTree) {
        MarkdownParserEngine runningParser = MarkdownPoolCache.MarkdownParserEnginePool.Get();

        try {
            runningParser.NodeTree = nodeTree;
            runningParser.PushMultiLineMatchesToStack(markdown, nodeTree.RootNode, HandlerOrigin.Undefined);

            while (runningParser.TryPopDto(out SyntaxFragment? fragment)) {
                try {
                    if (fragment.TryGetAsMatch(out Match? match, out IMdSyntaxNode? parentNode, out HandlerOrigin handlerOrigin)) {
                        ProcessMatch(match, parentNode, handlerOrigin, runningParser);
                    }
                    else if (fragment.TryGetAsProcessedNode(out parentNode, out IMdSyntaxNode? childNode)) {
                        parentNode.AddChildNode(childNode);
                    }
                }
                finally {
                    MarkdownPoolCache.MarkdownFragmentPool.Return(fragment);
                }
            }
        }
        finally {
            MarkdownPoolCache.MarkdownParserEnginePool.Return(runningParser);
        }
    }

    private void ProcessMatch(Match match, IMdSyntaxNode currentNode, HandlerOrigin origin, IMarkdownParserEngine runningParser) {
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
}

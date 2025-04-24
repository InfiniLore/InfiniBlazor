// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.MdNodes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdNodeTreeParser>]
public sealed class MdNodeTreeParser(IServiceProvider serviceProvider, ILogger<MdNodeTreeParser> logger) : IMdNodeTreeParser {
    private readonly FrozenDictionary<string, ISectionHandler> _sectionHandlers = ToFrozenDictionary<ISectionHandler>(logger, serviceProvider);

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
    
    public void ParseToNodeTree(string markdown, IMdNodeTree nodeTree) {
        RunningMarkdownParser runningParser = PoolCache.RunningMarkdownParserPool.Get();

        try {
            runningParser.NodeTree = nodeTree;
            runningParser.AddMultiLineMatchesToStack(markdown, nodeTree.RootNode, ParserOrigin.Undefined);

            while (runningParser.TryPopDto(out ParserDataDto? dataDto)) {
                try {
                    IMdNode currentNode = dataDto.Node;
                    ParserOrigin origin = dataDto.Origin;

                    if (dataDto.TryGetAsMatch(out Match? match)) {
                        ProcessMatch(match, currentNode, origin, runningParser);
                        continue;
                    }
                    if (dataDto.TryGetAsElement(out MdElement element, out string? content)) {
                        ProcessElement(element, content, currentNode);
                    }
                }
                finally {
                    PoolCache.ParserDataDtoPool.Return(dataDto);
                }
            }
        }
        finally {
            PoolCache.RunningMarkdownParserPool.Return(runningParser);
        }
    }

    private void ProcessMatch(Match match, IMdNode currentNode, ParserOrigin origin, RunningMarkdownParser runningParser) {
        GroupCollection groups = match.Groups;
        for (int i = 0; i < groups.Count; i++) {
            if (groups[i] is not { Success: true, Name: var name }) continue;
            if (!_sectionHandlers.TryGetValue(name, out ISectionHandler? handler)) continue;

            ParserOrigin handlerOrigin = handler.SkipOnOrigin;
            if (handlerOrigin is ParserOrigin.NotSkipped || (origin & handlerOrigin) != handlerOrigin) {
                handler.HandleMatch(runningParser, currentNode, match, groups[i], origin);
            }
        }
    }

    private static void ProcessElement(MdElement element, string? content, IMdNode currentNode) {
        // ReSharper disable once SwitchStatementHandlesSomeKnownEnumValuesWithDefault
        switch (element) {
            case MdElement.HtmlContent: currentNode.WithHtmlContent(content);
                break;
            case MdElement.Content: currentNode.WithContent(content);
                break;
            default: currentNode.AddChildNode(element, content);
                break;
        }
    }
}

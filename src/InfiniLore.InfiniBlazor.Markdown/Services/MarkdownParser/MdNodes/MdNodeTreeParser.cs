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
public class MdNodeTreeParser(IServiceProvider serviceProvider, ILogger<MdNodeTreeParser> logger) : IMdNodeTreeParser {
    private readonly FrozenDictionary<string, ISectionHandler> _sectionHandlers = ToFrozenDictionary<ISectionHandler>(logger, serviceProvider);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Constructor Helpers
    private static FrozenDictionary<string, T> ToFrozenDictionary<T>(ILogger logger, IServiceProvider serviceProvider) {
        ImmutableArray<string> keyNames = MarkdownRegexLib.MarkdownStructureGroupNames;
        int keyCount = keyNames.Length;
        var dictionaryBuilder = new Dictionary<string, T>(keyCount);

        for (int index = 0; index < keyCount; index++) {
            string groupName = keyNames[index];
            var service = serviceProvider.GetKeyedService<T>(groupName);
            if (service is null) {
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
            
            // Preload matches into the stack
            runningParser.AddMultiLineMatchesToStack(markdown, nodeTree.RootNode, ParserOrigin.Undefined);

            // Process matches
            while (runningParser.TryPopDto(out ParserDataDto? dataDto)) {
                IMdNode currentNode = dataDto.Node;
                ParserOrigin origin = dataDto.Origin;

                switch (dataDto) {
                    // Process the match, which will happen most of the time
                    case { IsMatch: true, Match: var match }: {
                        GroupCollection groups = match.Groups;
                        int count = groups.Count;

                        for (int index = 0; index < count; index++) {
                            Group group = groups[index];
                            if (group is not { Success: true, Name: {} name }) continue;
                            if (!_sectionHandlers.TryGetValue(name, out ISectionHandler? handler)) continue;

                            ParserOrigin handlerOrigin = handler.SkipOnOrigin;
                            if (handlerOrigin is not ParserOrigin.NotSkipped && (origin & handlerOrigin) == handlerOrigin) continue;

                            handler.HandleMatch(runningParser, currentNode, match, group, origin);
                        }
                        break;
                    }

                    // Needed for adding child text content to a node
                    //      Comes from a SingeLine match which had uncaught section and thus needs to be handled to add the text content
                    case {Element: MdElement.HtmlContent, Content: {} newContent}: {
                        currentNode.WithHtmlContent(newContent);
                        break;
                    }

                    case {Element: MdElement.Content, Content: {} newContent }: {
                        currentNode.WithContent(newContent);
                        break;
                    }

                    case {Element: var element, Content: var newContent }: {
                        IMdNode newNode = currentNode.AddChildNode(element);
                        if (newContent is not null) newNode.WithContent(newContent);
                        break;
                    }
                }

                // Remember to clean up the DTO, else it will not return to the pool
                PoolCache.ParserDataDtoPool.Return(dataDto);
            }
        }
        finally {
            // Also handles ParserDataDto cleanup if still present, so no nested try-finally block needed.
            PoolCache.RunningMarkdownParserPool.Return(runningParser);
        }
    }
}

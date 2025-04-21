// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.MdNodes;
using InfiniLore.InfiniBlazor.Markdown.Pools;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Collections.Immutable;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableService<IMarkdownParser>(ServiceLifetime.Singleton)]
public class MarkdownParser(IServiceProvider serviceProvider, ILogger<MarkdownParser> logger) : IMarkdownParser {
    private readonly FrozenDictionary<string, ISectionHandler> _sectionHandlers = ToFrozenDictionary<ISectionHandler>(GroupNames, logger, serviceProvider);

    private static ImmutableArray<string> GroupNames => [
        // Multiline
        "paragraph",
        "heading",
        "codeBlock",
        "headingSimple",
        "listUnordered",
        "listOrdered",
        "table",
        "blockQuote",
        "htmlBody",
        "horizontalRule",

        // Singleline
        "escaped",
        "bold",
        "italic",
        "supScript",
        "subScript",
        "strike",
        "code",
        "linkNested",
        "linkRegular",
        "underline",
        "emote",
        "tag"
    ];

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    #region Constructor Helpers
    private static FrozenDictionary<string, T> ToFrozenDictionary<T>(ImmutableArray<string> keyNames, ILogger<MarkdownParser> logger, IServiceProvider serviceProvider) {
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

    #region Parsing Methods
    public string ParseToString(string markdown) {
        throw new NotImplementedException();
    }

    public void ParseToWriter<T>(string markdown, T writer) where T : TextWriter {
        throw new NotImplementedException();
    }
    
    public IMdNode ParseToNodeTree(string markdown) {
        var rootNode = new MdNode();
        RunningMarkdownParser runningParser = RunningMarkdownParserPool.Get();
        runningParser.RootNode = rootNode;

        try {
            // Preload matches into the stack
            runningParser.AddMultiLineMatchesToStack(markdown, rootNode, ParserOrigin.Undefined);

            // Process matches
            while (runningParser.TryPopDto(out ParserDataDto? dataDto)) {
                IMdNode currentNode = dataDto.Node;
                ParserOrigin origin = dataDto.Origin;

                switch (dataDto) {
                    // Process the match, which will happen most of the time
                    case { IsMatch: true, Match: var match, }: {
                        GroupCollection groups = match.Groups;
                        int count = groups.Count;

                        for (int index = 0; index < count; index++) {
                            Group group = groups[index];
                            if (group is not {Success: true, Name: {} name}) continue;
                            if (!_sectionHandlers.TryGetValue(name, out ISectionHandler? handler)) continue;

                            ParserOrigin handlerOrigin = handler.SkipOnOrigin;
                            if (handlerOrigin is not ParserOrigin.NotSkipped && (origin & handlerOrigin) == handlerOrigin) continue;

                            handler.HandleMatch(runningParser, currentNode, match, group, origin);
                        }
                        break;
                    }
                    
                    // Needed for adding child text content to a node
                    //      Comes from a SingeLine match which had uncaught section and thus needs to be handled to add the text content
                    case { IsElement: true, Content: {} newContent , Element: MdElement.HtmlContent}: {
                        currentNode.WithHtmlContent(newContent);
                        break;
                    }
                    
                    case { IsElement: true, Content: {} newContent, Element: MdElement.Content }: {
                        currentNode.WithContent(newContent);
                        break;
                    }

                    case { IsElement: true, Content: var newContent, Element: var element }: {
                        IMdNode newNode = currentNode.AddChildNode(element);
                        newNode.WithContent(newContent);
                        break;
                    }
                }
                
                // Remember to clean up the DTO, else it will not return to the pool
                ParserDataDtoPool.Return(dataDto);
            }

            return rootNode;
        }
        finally {
            // Also handles ParserDataDto cleanup if still present, so no nested try-finally block needed.
            RunningMarkdownParserPool.Return(runningParser);
        }
    }
    #endregion
}

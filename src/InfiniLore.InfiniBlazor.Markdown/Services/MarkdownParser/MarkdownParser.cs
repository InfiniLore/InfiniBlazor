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
    public string Parse(string markdown) {
        throw new NotImplementedException();
    }

    public void Parse<T>(string markdown, T writer) where T : TextWriter {
        throw new NotImplementedException();
    }
    public IMdNode ParseToNodeTree(string markdown) {
        var rootNode = new MdNode();
        RunningMarkdownParser runningParser = RunningMarkdownParserPool.Get();
        runningParser.RootNode = rootNode;

        try {
            // Preload matches into the stack
            MatchCollection matches = MarkdownRegexLib.MultilineStructuresRegex.Matches(markdown);
            runningParser.PushMatches(matches);

            // Process matches
            while (runningParser.TryPopDto(out ParserDataDto? dataDto)) {
                // Extract what we already have from the stack
                IMdNode currentNode = dataDto.Node;
                ParserOrigin origin = dataDto.Origin;
                Match match = dataDto.Match;
                GroupCollection groups = match.Groups;
                int count = groups.Count;

                for (int index = 0; index < count; index++) {
                    Group group = groups[index];
                    if (!group.Success) continue;
                    if (!_sectionHandlers.TryGetValue(group.Name, out ISectionHandler? handler)) continue;
                    if ((origin & handler.SkipOnOrigin) == handler.SkipOnOrigin) continue;

                    handler.HandleMatch(match, group, origin, currentNode, runningParser);
                }
            }

            return rootNode;
        }
        finally {
            RunningMarkdownParserPool.Return(runningParser);
        }
    }
    #endregion
}

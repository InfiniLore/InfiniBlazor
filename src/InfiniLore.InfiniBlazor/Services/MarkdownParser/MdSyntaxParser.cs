// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown;
using InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxParser>]
public class MdSyntaxParser(IServiceProvider serviceProvider, ILogger<MdSyntaxParser> logger) : IMdSyntaxParser {
    private readonly FrozenDictionary<string, IMdSyntaxHandler> _elementHandlers = ToFrozenDictionary(logger, serviceProvider);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    private static FrozenDictionary<string, IMdSyntaxHandler> ToFrozenDictionary(ILogger logger, IServiceProvider serviceProvider) {
        ReadOnlySpan<string> keyNames = MarkdownRegexLib.MarkdownStructureGroupNames.AsSpan();
        var dictionaryBuilder = new Dictionary<string, IMdSyntaxHandler>(keyNames.Length);

        for (int index = keyNames.Length - 1; index >= 0; index--) {
            string groupName = keyNames[index];
            if (serviceProvider.GetKeyedService<IMdSyntaxHandler>(groupName) is not {} service) {
                logger.LogWarning("No MarkdownElementHandler service found for group name '{groupName}'", groupName);
                continue;
            }

            dictionaryBuilder[groupName] = service;
        }

        return dictionaryBuilder.ToFrozenDictionary();
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMdSyntaxTree ParseToTree(string markdown) {
        var nodeTree = new MdSyntaxTree();
        ParseToTree(markdown, nodeTree);
        return nodeTree;
    }

    public void ParseToTree(string markdown, IMdSyntaxTree nodeTree) {
        nodeTree.RootNode.Depth = 0;
        MdSyntaxParserStack runningParser = MdSyntaxParserStack.Pool.Get();

        try {
            runningParser.NodeTree = nodeTree;
            runningParser.PushMultiLineMatchesToStack(markdown, nodeTree.RootNode, MdSyntaxHandlerOrigin.Undefined);

            while (runningParser.TryPopDto(out MdSyntaxFragment? fragment)) {
                try {
                    if (fragment.TryGetAsMatch(out Match? match, out IMdSyntaxNode? parentNode, out MdSyntaxHandlerOrigin handlerOrigin)) {
                        ProcessMatch(match, parentNode, handlerOrigin, runningParser);
                    }
                    else if (fragment.TryGetAsProcessedNode(out parentNode, out IMdSyntaxNode? childNode)) {
                        parentNode.AddChildNode(childNode);
                    }
                }
                finally {
                    MdSyntaxFragment.Pool.Return(fragment);
                }
            }
        }
        finally {
            MdSyntaxParserStack.Pool.Return(runningParser);
        }
    }

    private void ProcessMatch(Match match, IMdSyntaxNode currentNode, MdSyntaxHandlerOrigin origin, IMdSyntaxParserStack runningParser) {
        GroupCollection groups = match.Groups;
        for (int i = 0; i < groups.Count; i++) {
            if (groups[i] is not { Success: true, Name: var name } group) continue;
            if (!_elementHandlers.TryGetValue(name, out IMdSyntaxHandler? handler)) continue;

            MdSyntaxHandlerOrigin handlerOrigin = handler.SkipOnOrigin;
            if (handlerOrigin is MdSyntaxHandlerOrigin.NotSkipped || !origin.HasFlagFast(handlerOrigin)) {
                handler.HandleMatch(runningParser, currentNode, match, group, origin);
            }
        }
    }
}

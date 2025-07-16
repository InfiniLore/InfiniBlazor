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
public sealed class MdSyntaxParser(IServiceProvider serviceProvider, ILogger<MdSyntaxParser> logger) : IMdSyntaxParser {
    private readonly FrozenDictionary<string, IMdSyntaxHandler> _elementHandlers = ToFrozenDictionary(logger, serviceProvider);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    private static FrozenDictionary<string, IMdSyntaxHandler> ToFrozenDictionary(ILogger logger, IServiceProvider serviceProvider) {
        ReadOnlySpan<string> keyNames = RegexLib.MdRegexLib.MarkdownStructureGroupNames.AsSpan();
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

        MdSyntaxFragment? fragment = null;
        string normalized = markdown.ReplaceLineEndings("\n");

        try {
            runningParser.PushMultiLineMatchesToStack(normalized, nodeTree.RootNode, MdSyntaxHandlerOrigin.Undefined);

            while (runningParser.TryPopDto(out fragment)) {
                if (fragment.TryGetAsMatch(out Match? match, out IMdSyntaxNode? parentNode, out MdSyntaxHandlerOrigin handlerOrigin)) {
                    ProcessMatch(match, parentNode, handlerOrigin, runningParser);
                }
                else if (fragment.TryGetAsProcessedNode(out parentNode, out IMdSyntaxNode? childNode)) {
                    parentNode.AddChildNode(childNode);
                }

                MdSyntaxFragment.Pool.Return(fragment);
                fragment = null;
            }
        }
        catch (Exception ex) {
            logger.Error(ex, "Error parsing Markdown, during tree conversion.");
            throw;
        }
        finally {
            // makes it so we don't have to have a nested try catch
            if (fragment is not null) MdSyntaxFragment.Pool.Return(fragment);
            MdSyntaxParserStack.Pool.Return(runningParser);
        }
    }

    private void ProcessMatch(Match match, IMdSyntaxNode parentNode, MdSyntaxHandlerOrigin parentOrigin, IMdSyntaxParserStack runningParser) {
        GroupCollection groups = match.Groups;
        int length = groups.Count;
        if (length == 0) return;

        for (int index = 0; index < length; index++) {
            Group group = groups[index];
            if (!group.Success) continue;
            if (!_elementHandlers.TryGetValue(group.Name, out IMdSyntaxHandler? handler)) continue;

            MdSyntaxHandlerOrigin handlerOrigin = handler.SkipOnOrigin;
            if (handlerOrigin is not MdSyntaxHandlerOrigin.NotSkipped && parentOrigin.HasFlagFast(handlerOrigin)) continue;

            handler.HandleMatch(runningParser, parentNode, match, parentOrigin);
        }
    }
}

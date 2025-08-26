// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Text.RegularExpressions;
using InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer.RegexLib;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers.MarkdownString.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMarkdownStringMdSyntaxSerializer>]
public sealed class MarkdownStringMdSyntaxSerializer(IServiceProvider serviceProvider, ILogger<MarkdownStringMdSyntaxSerializer> logger) : IMarkdownStringMdSyntaxSerializer {
    private readonly FrozenDictionary<string, IMarkdownStringMdSyntaxNodeSerializer> _elementHandlers = ToFrozenDictionary(logger, serviceProvider);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    private static FrozenDictionary<string, IMarkdownStringMdSyntaxNodeSerializer> ToFrozenDictionary(ILogger logger, IServiceProvider serviceProvider) {
        ReadOnlySpan<string> keyNames = MdRegexLib.MarkdownStructureGroupNames.AsSpan();
        var dictionaryBuilder = new Dictionary<string, IMarkdownStringMdSyntaxNodeSerializer>(keyNames.Length);

        for (int index = keyNames.Length - 1; index >= 0; index--) {
            string groupName = keyNames[index];
            if (serviceProvider.GetKeyedService<IMarkdownStringMdSyntaxNodeSerializer>(groupName) is not {} service) {
                logger.Warning("No MarkdownElementHandler service found for group name '{groupName}'", groupName);
                continue;
            }

            dictionaryBuilder[groupName] = service;
        }

        return dictionaryBuilder.ToFrozenDictionary();
    }
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMdSyntaxTree SerializeToTree(string markdown) {
        MdSyntaxTree nodeTree = MdSyntaxTree.Pool.Get();
        SerializeToTree(markdown, nodeTree);
        return nodeTree;
    }

    public void SerializeToTree(string markdown, IMdSyntaxTree nodeTree) {
        MdSyntaxFragmentStack runningSerializer = MdSyntaxFragmentStack.Pool.Get();

        MdSyntaxFragment? fragment = null;
        string normalized = markdown.ReplaceLineEndings("\n");

        try {
            runningSerializer.PushMultiLineMatchesToStack(normalized, nodeTree.RootNode, MdSyntaxSerializerOrigin.Undefined);

            while (runningSerializer.TryPopDto(out fragment)) {
                if (fragment.TryGetAsProcessedNode(out IMdSyntaxNode? parentNode, out IMdSyntaxNode? childNode)) {
                    parentNode.AddChildNode(childNode);
                }
                else if (fragment.TryGetAsMatch(out Match? match, out parentNode, out MdSyntaxSerializerOrigin handlerOrigin)) {
                    ProcessMatch(match, parentNode, handlerOrigin, runningSerializer);
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
            MdSyntaxFragmentStack.Pool.Return(runningSerializer);
        }
    }

    private void ProcessMatch(Match match, IMdSyntaxNode parentNode, MdSyntaxSerializerOrigin parentOrigin, IMdSyntaxFragmentStack runningParser) {
        GroupCollection groups = match.Groups;
        int length = groups.Count;
        if (length == 0) return;

        for (int index = 0; index < length; index++) {
            Group group = groups[index];
            if (!group.Success) continue;
            if (!_elementHandlers.TryGetValue(group.Name, out IMarkdownStringMdSyntaxNodeSerializer? handler)) continue;

            MdSyntaxSerializerOrigin handlerOrigin = handler.SkipOnOrigin;
            // if (handlerOrigin is not MdSyntaxSerializerOrigin.NotSkipped && parentOrigin.HasFlagFast(handlerOrigin)) continue;

            handler.HandleMatch(runningParser, parentNode, match, parentOrigin);
        }
    }
}

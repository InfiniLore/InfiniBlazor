// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using InfiniLore.InfiniBlazor.Markdown.RegexLib;
using InfiniLore.InfiniBlazor.Markdown.Syntax;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Frozen;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.Markdown.SyntaxSerializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSyntaxSerializer>]
public sealed class MdSyntaxSerializer(IServiceProvider serviceProvider, ILogger<MdSyntaxSerializer> logger) : IMdSyntaxSerializer {
    private readonly FrozenDictionary<string, IMdSyntaxNodeSerializer> _elementHandlers = ToFrozenDictionary(logger, serviceProvider);
    
    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    private static FrozenDictionary<string, IMdSyntaxNodeSerializer> ToFrozenDictionary(ILogger logger, IServiceProvider serviceProvider) {
        ReadOnlySpan<string> keyNames = MdRegexLib.MarkdownStructureGroupNames.AsSpan();
        var dictionaryBuilder = new Dictionary<string, IMdSyntaxNodeSerializer>(keyNames.Length);

        for (int index = keyNames.Length - 1; index >= 0; index--) {
            string groupName = keyNames[index];
            if (serviceProvider.GetKeyedService<IMdSyntaxNodeSerializer>(groupName) is not {} service) {
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
        var nodeTree = new MdSyntaxTree();
        SerializeToTree(markdown, nodeTree);
        return nodeTree;
    }

    public void SerializeToTree(string markdown, IMdSyntaxTree nodeTree) {
        MdSyntaxSerializerStack runningSerializer = MdSyntaxSerializerStack.Pool.Get();

        MdSyntaxFragment? fragment = null;
        string normalized = markdown.ReplaceLineEndings("\n");

        try {
            runningSerializer.PushMultiLineMatchesToStack(normalized, nodeTree.RootNode, MdSyntaxSerializerOrigin.Undefined);

            while (runningSerializer.TryPopDto(out fragment)) {
                if (fragment.TryGetAsMatch(out Match? match, out IMdSyntaxNode? parentNode, out MdSyntaxSerializerOrigin handlerOrigin)) {
                    ProcessMatch(match, parentNode, handlerOrigin, runningSerializer);
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
            MdSyntaxSerializerStack.Pool.Return(runningSerializer);
        }
    }

    private void ProcessMatch(Match match, IMdSyntaxNode parentNode, MdSyntaxSerializerOrigin parentOrigin, IMdSyntaxSerializerStack runningParser) {
        GroupCollection groups = match.Groups;
        int length = groups.Count;
        if (length == 0) return;

        for (int index = 0; index < length; index++) {
            Group group = groups[index];
            if (!group.Success) continue;
            if (!_elementHandlers.TryGetValue(group.Name, out IMdSyntaxNodeSerializer? handler)) continue;

            MdSyntaxSerializerOrigin handlerOrigin = handler.SkipOnOrigin;
            if (handlerOrigin is not MdSyntaxSerializerOrigin.NotSkipped && parentOrigin.HasFlagFast(handlerOrigin)) continue;

            handler.HandleMatch(runningParser, parentNode, match, parentOrigin);
        }
    }
}

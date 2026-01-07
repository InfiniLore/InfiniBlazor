// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Buffers;
using System.Collections.Immutable;

namespace InfiniBlazor.Markdown.Parsers.Markdown.Serializer;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IMdSerializerFactory>]
public class MdStringMdSyntaxSerializerFactory(ILogger<MdStringMdSyntaxSerializer> logger) : IMdSerializerFactory {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMdStringMdSyntaxSerializer Create(MarkdownSerializerOptions options) {
        // ReSharper disable twice UseCollectionExpression
        ImmutableArray<IMdSyntaxNodeSerializer> singleLineSerializers = options.SingleLine.ToImmutableArray();
        ImmutableArray<IMdSyntaxNodeSerializer>[] singleLineLookup = BuildLookup(singleLineSerializers);
        SearchValues<char> singleLineTriggerSearchValues = SearchValues.Create(options.SingleLine
            .SelectMany(s => s.TriggerCharacters)
            .Distinct()
            .ToArray());

        ImmutableArray<IMdSyntaxNodeSerializer> multiLineSerializers = options.MultiLine.ToImmutableArray();
        ImmutableArray<IMdSyntaxNodeSerializer>[] multiLineLookup = BuildLookup(multiLineSerializers);

        return new MdStringMdSyntaxSerializer(logger) {
            SingleLineSerializers = singleLineSerializers,
            SingleLineLookup = singleLineLookup,
            SingleLineTriggerSearchValues = singleLineTriggerSearchValues,

            MultiLineSerializers = multiLineSerializers,
            MultiLineLookup = multiLineLookup,

            FrontMatterSerializer = options.FrontMatter
        };
    }

    private static ImmutableArray<IMdSyntaxNodeSerializer>[] BuildLookup(ImmutableArray<IMdSyntaxNodeSerializer> serializers) {
        var table = new ImmutableArray<IMdSyntaxNodeSerializer>[256];

        // Identify "Global" serializers (those with no triggers)
        IMdSyntaxNodeSerializer[] globals = serializers.Where(s => s.TriggerCharacters.IsEmpty()).ToArray();

        for (int i = 0; i < 256; i++) {
            char c = (char)i;

            // Get serializers specifically triggered by this character
            IEnumerable<IMdSyntaxNodeSerializer> triggers = serializers.Where(s => s.TriggerCharacters.Contains(c));

            // Result is Triggers + Globals
            table[i] = triggers.Concat(globals).ToImmutableArray();
        }

        return table;
    }
}

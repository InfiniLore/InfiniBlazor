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
[InjectableSingleton<IMdStringMdSyntaxSerializerFactory>]
public class MdStringMdSyntaxSerializerFactory(ILogger<MdStringMdSyntaxSerializer> logger) : IMdStringMdSyntaxSerializerFactory {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public IMdStringMdSyntaxSerializer Create(MarkdownSerializerOptions options) {
        // ReSharper disable twice UseCollectionExpression
        ImmutableArray<IMdSyntaxNodeSerializer> singleLineSerializers = options.SingleLine.ToImmutableArray();
        ImmutableArray<IMdSyntaxNodeSerializer> multiLineSerializers = options.MultiLine.ToImmutableArray();

        (ImmutableArray<IMdSyntaxNodeSerializer>[] singleAscii, ImmutableDictionary<char, ImmutableArray<IMdSyntaxNodeSerializer>> singleNonAscii) = BuildLookup(singleLineSerializers);
        (ImmutableArray<IMdSyntaxNodeSerializer>[] multiAscii, ImmutableDictionary<char, ImmutableArray<IMdSyntaxNodeSerializer>> multiNonAscii) = BuildLookup(multiLineSerializers);

        Span<bool> seen = stackalloc bool[256];
        Span<char> buffer = stackalloc char[256];
        HashSet<char>? nonAscii = null;
        int count = 0;

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (IMdSyntaxNodeSerializer serializer in singleLineSerializers) {
            foreach (char ch in serializer.TriggerCharacters) {
                if (ch >= 256) {
                    nonAscii ??= new HashSet<char>();
                    nonAscii.Add(ch);
                    continue;
                }
                
                if (seen[ch]) continue;

                seen[ch] = true;
                buffer[count++] = ch;
            }
        }

        ReadOnlySpan<char> bufferView = buffer[..count];
        
        SearchValues<char> singleLineTriggerSearchValues = nonAscii is null
            ? SearchValues.Create(bufferView)
            : SearchValues.Create(BuildAllArray(ref bufferView, nonAscii));

        return new MdStringMdSyntaxSerializer(logger) {
            SingleLineSerializers = singleLineSerializers,
            SingleLineLookup = singleAscii,
            SingleLineNonAsciiLookup = singleNonAscii,
            SingleLineTriggerSearchValues = singleLineTriggerSearchValues,

            MultiLineSerializers = multiLineSerializers,
            MultiLineLookup = multiAscii,
            MultiLineNonAsciiLookup = multiNonAscii,

            FrontMatterSerializer = options.FrontMatter,
        };
    }

    private static (ImmutableArray<IMdSyntaxNodeSerializer>[] ascii,
        ImmutableDictionary<char, ImmutableArray<IMdSyntaxNodeSerializer>> nonAscii)
        BuildLookup(ImmutableArray<IMdSyntaxNodeSerializer> serializers) {
        var buckets = new List<IMdSyntaxNodeSerializer>?[256];
        var nonAsciiBuckets = new Dictionary<char, List<IMdSyntaxNodeSerializer>>();
        var globals = new List<IMdSyntaxNodeSerializer>();

        Span<bool> localSeen = stackalloc bool[256];
        HashSet<char>? localNonAscii = null;

        foreach (IMdSyntaxNodeSerializer s in serializers) {
            ReadOnlySpan<char> triggers = s.TriggerCharacters;
            if (triggers.IsEmpty) {
                globals.Add(s);
                continue;
            }

            localSeen.Clear();
            localNonAscii?.Clear();

            foreach (char ch in triggers) {
                if (ch < 256) {
                    if (localSeen[ch]) continue;
                    localSeen[ch] = true;

                    List<IMdSyntaxNodeSerializer>? list = buckets[ch];
                    if (list is null) buckets[ch] = list = new List<IMdSyntaxNodeSerializer>();
                    list.Add(s);
                }
                else {
                    localNonAscii ??= new HashSet<char>();
                    if (!localNonAscii.Add(ch)) continue;

                    if (!nonAsciiBuckets.TryGetValue(ch, out var list)) {
                        list = new List<IMdSyntaxNodeSerializer>();
                        nonAsciiBuckets[ch] = list;
                    }
                    list.Add(s);
                }
            }
        }

        var table = new ImmutableArray<IMdSyntaxNodeSerializer>[256];
        for (int c = 0; c < 256; c++) {
            List<IMdSyntaxNodeSerializer>? list = buckets[c];
            int total = (list?.Count ?? 0) + globals.Count;

            if (total == 0) {
                table[c] = ImmutableArray<IMdSyntaxNodeSerializer>.Empty;
                continue;
            }

            var arr = new IMdSyntaxNodeSerializer[total];
            int offset = 0;

            if (list is not null) {
                list.CopyTo(arr, 0);
                offset = list.Count;
            }

            if (globals.Count > 0) {
                globals.CopyTo(arr, offset);
            }

            table[c] = ImmutableArray.Create(arr);
        }

        ImmutableDictionary<char, ImmutableArray<IMdSyntaxNodeSerializer>>.Builder nonAscii = ImmutableDictionary.CreateBuilder<char, ImmutableArray<IMdSyntaxNodeSerializer>>();
        foreach ((char ch, List<IMdSyntaxNodeSerializer> list) in nonAsciiBuckets) {
            int total = list.Count + globals.Count;
            var arr = new IMdSyntaxNodeSerializer[total];
            list.CopyTo(arr, 0);
            if (globals.Count > 0) globals.CopyTo(arr, list.Count);
            nonAscii[ch] = ImmutableArray.Create(arr);
        }

        return (table, nonAscii.ToImmutable());
    }

    private static char[] BuildAllArray(ref ReadOnlySpan<char> ascii, HashSet<char> nonAscii) {
        char[] all = new char[ascii.Length + nonAscii.Count];
        ascii.CopyTo(all);
        int i = ascii.Length;
        foreach (char ch in nonAscii) all[i++] = ch;
        return all;
    }
}

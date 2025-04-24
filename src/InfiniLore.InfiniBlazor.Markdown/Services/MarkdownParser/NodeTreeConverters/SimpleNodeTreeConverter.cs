// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections.Frozen;

namespace InfiniLore.InfiniBlazor.Markdown.NodeTreeConverters;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public abstract class SimpleNodeTreeConverter {
    private static readonly FrozenDictionary<MdElement, HtmlTag> MdElementLookup = new Dictionary<MdElement, HtmlTag> {
        { MdElement.Blockquote, HtmlTag.Create("blockquote") },
        { MdElement.Bold, HtmlTag.Create("strong") },
        { MdElement.CheckboxSelected, HtmlTag.CreateVoid("input type=\"checkbox\" disabled checked/") },
        { MdElement.CheckboxUnselected, HtmlTag.CreateVoid("input type=\"checkbox\" disabled/") },
        { MdElement.CodeBlock, new HtmlTag("<pre><code", "</code></pre>") },
        { MdElement.CodeInline, HtmlTag.Create("code") },
        { MdElement.H1, HtmlTag.Create("h1") },
        { MdElement.H2, HtmlTag.Create("h2") },
        { MdElement.H3, HtmlTag.Create("h3") },
        { MdElement.H4, HtmlTag.Create("h4") },
        { MdElement.H5, HtmlTag.Create("h5") },
        { MdElement.H6, HtmlTag.Create("h6") },
        { MdElement.HorizontalRule, HtmlTag.CreateVoid("hr") },
        { MdElement.Image, HtmlTag.CreateVoid("img") },
        { MdElement.Italic, HtmlTag.Create("em") },
        { MdElement.Link, HtmlTag.Create("a") },
        { MdElement.ListItem, HtmlTag.Create("li") },
        { MdElement.ListOrdered, HtmlTag.Create("ol") },
        { MdElement.ListUnordered, HtmlTag.Create("ul") },
        { MdElement.Paragraph, HtmlTag.Create("p") },
        { MdElement.Strikethrough, HtmlTag.Create("s") },
        { MdElement.Subscript, HtmlTag.Create("sub") },
        { MdElement.Superscript, HtmlTag.Create("sup") },
        { MdElement.Table, HtmlTag.Create("table") },
        { MdElement.TableBody, HtmlTag.Create("tbody") },
        { MdElement.TableCell, HtmlTag.Create("td") },
        { MdElement.TableHead, HtmlTag.Create("thead") },
        { MdElement.TableHeadCell, HtmlTag.Create("th") },
        { MdElement.TableRow, HtmlTag.Create("tr") },
        { MdElement.Tag, HtmlTag.CreateWithClass("span", "tag") },
        { MdElement.Underline, HtmlTag.CreateWithStyle("span", "text-decoration: underline;") }
    }.ToFrozenDictionary();

    private static readonly FrozenSet<MdElement> DefinedElements = MdElementLookup.Keys.ToFrozenSet();

    protected delegate void WriteDelegate<in T>(T writer, ReadOnlySpan<char> content);
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    protected static void ProcessNodeTree<T>(IMdNodeTree tree, T writer, WriteDelegate<T> writeContent) {
        Dictionary<int, MdElement> depthCache = PoolCache.DepthCachePool.Get();
        try {
            int lastKnownDepth = -1;

            foreach (IMdNodeVisitor mdNodeVisitor in tree.VisitNodesBreadthFirst()) {
                int depth = mdNodeVisitor.Depth;
                IMdNode node = mdNodeVisitor.Node;

                if (lastKnownDepth + 1 > depth) 
                    CloseOpenTags(writer, depthCache, depth, writeContent);

                MdElement element = node.Element;
                if (element is MdElement.Content or MdElement.HtmlContent) {
                    ReadOnlySpan<char> content = node.Content;
                    if (content.Length == 0) continue;

                    writeContent(writer, content);
                    continue;
                }

                if (!DefinedElements.Contains(element)) continue;

                HtmlTag htmlTag = MdElementLookup[element];
                writeContent(writer, htmlTag.OpenTagSpan);

                if (!node.Classes.IsEmpty()) {
                    writeContent(writer, " class=\"");
                    foreach (ReadOnlySpan<char> cssClass in node.Classes) {
                        writeContent(writer, " ");
                        writeContent(writer, cssClass);
                    }
                    writeContent(writer, "\"");
                }

                if (!node.Attributes.IsEmpty()) {
                    writeContent(writer, " ");
                    foreach (KeyValuePair<string, string> attribute in node.Attributes) {
                        writeContent(writer, attribute.Key);
                        writeContent(writer, "=\"");
                        writeContent(writer, attribute.Value);
                        writeContent(writer, "\"");
                    }
                }

                writeContent(writer, ">");

                if (htmlTag.HasClosingTag) depthCache.AddOrUpdate(depth, element);
                lastKnownDepth = depth;
            }

            CloseOpenTags(writer, depthCache, -1, writeContent);
        }
        finally {
            PoolCache.DepthCachePool.Return(depthCache);
        }
    }

    private static void CloseOpenTags<T>(T writer, Dictionary<int, MdElement> depthCache, int depth, WriteDelegate<T> writeContent) {
        if (depthCache.Count == 0) return;

        Span<int> keysToRemove = stackalloc int[depthCache.Count];
        int count = 0;

        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (int k in depthCache.Keys) {
            if (k < depth) continue;
            keysToRemove[count++] = k;
        }

        Span<int> slice = keysToRemove[..count];
        slice.Sort();
        for (int i = count - 1; i >= 0; i--) {
            int key = slice[i];
            MdElement closingEl = depthCache[key];
            HtmlTag htmlTag = MdElementLookup[closingEl];
            writeContent(writer, htmlTag.CloseTagSpan);
            depthCache.Remove(key);
        }
    }
}

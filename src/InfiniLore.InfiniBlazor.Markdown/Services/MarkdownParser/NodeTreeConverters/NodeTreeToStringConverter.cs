// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Buffers;
using System.Collections.Frozen;
using System.Text;

namespace InfiniLore.InfiniBlazor.Markdown.NodeTreeConverters;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class NodeTreeToStringConverter : IMdNodeTreeConverter<string> {
    private static readonly FrozenDictionary<MdElement, (string, string)> MdElementLookup = new Dictionary<MdElement, (string, string)> {
        {MdElement.Blockquote, ("<blockquote","</blockquote>")},
        {MdElement.Bold, ("<strong","</strong>")},
        {MdElement.CheckboxSelected, ("<input type=\"checkbox\" disabled checked /",string.Empty)},
        {MdElement.CheckboxUnselected, ("<input type=\"checkbox\" disabled /",string.Empty)},
        {MdElement.CodeBlock, ("<pre><code ","</code></pre>")},
        {MdElement.CodeInline, ("<code","</code>")},
        {MdElement.H1, ("<h1","</h1>")},
        {MdElement.H2, ("<h2","</h2>")},
        {MdElement.H3, ("<h3","</h3>")},
        {MdElement.H4, ("<h4","</h4>")},
        {MdElement.H5, ("<h5","</h5>")},
        {MdElement.H6, ("<h6","</h6>")},
        {MdElement.HorizontalRule, ("<hr",string.Empty)},
        {MdElement.Image, ("<img",string.Empty)},
        {MdElement.Italic, ("<em","</em>")},
        {MdElement.Link, ("<a","</a>")},
        {MdElement.ListItem, ("<li","</li>")},
        {MdElement.ListOrdered, ("<ol","</ol>")},
        {MdElement.ListUnordered, ("<ul","</ul>")},
        {MdElement.Paragraph, ("<p","</p>")},
        {MdElement.Strikethrough, ("<s","</s>")},
        {MdElement.Subscript, ("<sub","</sub>")},
        {MdElement.Superscript, ("<sup","</sup>")},
        {MdElement.Table, ("<table","</table>")},
        {MdElement.TableBody, ("<tbody","</tbody>")},
        {MdElement.TableCell, ("<td","</td>")},
        {MdElement.TableHead, ("<thead","</thead>")},
        {MdElement.TableHeadCell, ("<th","</th>")},
        {MdElement.TableRow, ("<tr","</tr>")},
        {MdElement.Tag, ("<span class=\"tag\"","</span>")},
        {MdElement.Underline, ("<span style=\"text-decoration: underline;\"","</span>")},
    }.ToFrozenDictionary();
    
    private static readonly FrozenSet<MdElement> DefinedElements = MdElementLookup.Keys.ToFrozenSet();
    
    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string Convert(IMdNodeTree tree) {
        StringBuilder builder = PoolCache.StringBuilderPool.Get();
        Dictionary<int, MdElement> depthCache = PoolCache.DepthCachePool.Get();
        try {
            int lastKnownDepth = -1;
            
            foreach (IMdNodeVisitor mdNodeVisitor in tree.VisitNodes()) {
                int depth = mdNodeVisitor.Depth;
                IMdNode node = mdNodeVisitor.Node;

                // write the end tag of a previous sibling
                if (lastKnownDepth + 1 > depth) CloseOpenTags(builder, depthCache, depth);
                
                // write the current HTML tag
                MdElement element = node.Element;
                if ( element is  MdElement.Content or MdElement.HtmlContent) {
                    ReadOnlySpan<char> content = node.Content;
                    if (content.Length == 0) continue;
                    builder.Append(content);
                    continue;
                }
                if (!DefinedElements.Contains(element)) continue; // Our parser doesn't handle component

                // Elements with Children
                (string tagOpen, string tagClose) = MdElementLookup[element];
                ReadOnlySpan<char> tagOpenSpan = tagOpen.AsSpan();
                ReadOnlySpan<char> tagCloseSpan = tagClose.AsSpan();

                builder.Append(tagOpenSpan);
                if (!node.Classes.IsEmpty()) {
                    builder.Append(" class=\"".AsSpan());
                    foreach (ReadOnlySpan<char> cssClass in node.Classes) {
                        builder.Append(' ');
                        builder.Append(cssClass);
                    }
                    builder.Append('"');
                }

                if (!node.Attributes.IsEmpty()) {
                    builder.Append(' ');
                    foreach (KeyValuePair<string, string> attribute in node.Attributes) {
                        ReadOnlySpan<char> keySpan = attribute.Key.AsSpan();
                        ReadOnlySpan<char> valueSpan = attribute.Value.AsSpan();

                        builder.Append(keySpan);
                        builder.Append("=\"".AsSpan());
                        builder.Append(valueSpan);
                        builder.Append('"');
                    }
                }

                builder.Append('>');

                if (tagCloseSpan.Length != 0) depthCache.AddOrUpdate(depth, element);
                lastKnownDepth = depth;
            }
            
            // write the end tags of all remaining siblings
            CloseOpenTags(builder, depthCache, -1);
            
            // write the data
            return builder.ToString();
        }
        finally {
            PoolCache.StringBuilderPool.Return(builder);
            PoolCache.DepthCachePool.Return(depthCache);
        }
    }
    private static void CloseOpenTags(StringBuilder builder, Dictionary<int, MdElement> depthCache, int depth) {
        int[] oldKeysArr = ArrayPool<int>.Shared.Rent(depthCache.Count);
        int count = 0;
                    
        // ReSharper disable once ForeachCanBePartlyConvertedToQueryUsingAnotherGetEnumerator
        foreach (int k in depthCache.Keys) {
            if (k < depth) continue;
            oldKeysArr[count++] = k;
        }

        Array.Sort(oldKeysArr, 0, count);
        Array.Reverse(oldKeysArr,0, count);
                    
        for (int i = 0; i < count; i++) {
            int key = oldKeysArr[i];
            MdElement closingEl = depthCache[key];
            (string _, string closingTag) = MdElementLookup[closingEl];
            ReadOnlySpan<char> closingTagSpan = closingTag.AsSpan();
            builder.Append(closingTagSpan);
            depthCache.Remove(key);
        }
                    
        ArrayPool<int>.Shared.Return(oldKeysArr);
    }
}

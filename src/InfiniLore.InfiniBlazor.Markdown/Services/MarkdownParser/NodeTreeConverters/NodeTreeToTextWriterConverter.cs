// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Markdown.NodeTreeConverters;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class NodeTreeToTextWriterConverter<T> : SimpleNodeTreeConverter, IMdNodeTreeToWriterConverter<T> where T : TextWriter {
    public void Convert(IMdNodeTree tree, T writer) {
        ProcessNodeTree(tree, writer, static (w, content) => w.Write(content));
    }
}

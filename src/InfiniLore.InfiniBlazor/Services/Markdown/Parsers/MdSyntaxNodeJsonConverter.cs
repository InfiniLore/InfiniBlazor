// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace InfiniLore.InfiniBlazor.Markdown.Parsers;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxNodeJsonConverter : JsonConverter<IMdSyntaxNode> {
    public override bool CanConvert(Type typeToConvert) => typeof(IMdSyntaxNode).IsAssignableFrom(typeToConvert);

    public override void Write(Utf8JsonWriter writer, IMdSyntaxNode value, JsonSerializerOptions options) {
        ArgumentNullException.ThrowIfNull(value);

        writer.WriteStartObject();

        // Write the type of the node as metadata
        writer.WriteString("Type", value.GetType().FullName);

        // Serialize the object itself
        foreach (PropertyInfo property in value.GetType().GetProperties()) {
            object? propertyValue = property.GetValue(value);
            if (propertyValue == null) continue;

            writer.WritePropertyName(property.Name);
            JsonSerializer.Serialize(writer, propertyValue, property.PropertyType, options);
        }

        writer.WriteEndObject();
    }

    public override IMdSyntaxNode Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options) {
        using JsonDocument doc = JsonDocument.ParseValue(ref reader);

        JsonElement root = doc.RootElement;

        // Read the type from the metadata
        string? typeName = root.GetProperty("Type").GetString();
        if (typeName == null) throw new InvalidOperationException("Type property not found in JSON.");

        var nodeType = Type.GetType(typeName);
        if (nodeType == null) throw new InvalidOperationException($"Unknown type: {typeName}");

        // Deserialize the node
        return (IMdSyntaxNode)(JsonSerializer.Deserialize(root.GetRawText(), nodeType, options)
            ?? throw new InvalidOperationException("Failed to deserialize the node."));
    }
}

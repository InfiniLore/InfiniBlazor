// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.Blazor.Markdown.Config;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MarkdownTextEditorConfig(IInfiniLoreBlazorConfig config) {
    public HashSet<string> Modifiers { get; } = new();

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    // public MarkdownTextEditorConfig AddModifier(string key, IKeybind? keybind = null) {
    //     IEnumerable<ServiceDescriptor> textModifiers = config.Services.Where(service => service.ServiceType == typeof(ITextModifier));
    //     if (!textModifiers.Any(modifier => ReferenceEquals(modifier.ServiceKey, key))) return this;
    //
    //     Modifiers.Add(key);
    //     return this;
    // }
}

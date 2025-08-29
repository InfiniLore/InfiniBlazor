// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using CodeOfChaos.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.RenderTree;
using System.Diagnostics.CodeAnalysis;

namespace InfiniLore.InfiniBlazor.AutoDocumenting;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[InjectableSingleton<IAttributeValueConverter>]
[SuppressMessage("Usage", "BL0006:Do not use RenderTree types")]// Yes, I am aware that this isn't perfect as the type names are expected to change at some point. For now, it works.
public class AttributeValueConverter : IAttributeValueConverter {

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public string FormatAttributeValue(RenderTreeFrame frame) => frame.AttributeValue switch {
        true => "true",
        false => "false",
        null => string.Empty,
        _ => frame.AttributeValue.ToString() ?? string.Empty
    };
}

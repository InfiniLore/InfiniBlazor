// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.Lucide;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class RenderFragmentHelper {
    // ReSharper disable once InconsistentNaming
    // ReSharper disable once UnusedParameter.Local
    public static RenderFragment Empty() => static __builder => {};

    // ReSharper disable once InconsistentNaming
    // ReSharper disable once RedundantAssignment
    public static RenderFragment AsLucideIcon(string iconName) => __builder => {
        int sequence = 0;
        __builder.OpenComponent<LucideIcon>(sequence++);
        __builder.AddAttribute(sequence++, "Name", iconName);
        __builder.CloseComponent();
    };
}

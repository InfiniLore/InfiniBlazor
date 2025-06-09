// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace InfiniLore.InfiniBlazor.Theming.Generators.Helpers.GeneratorStringBuilder;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ForEachAppendBodyExtensions {
    public static GeneratorStringBuilder ForEachAppendBody(
        this GeneratorStringBuilder builder,
        IEnumerable<string> items
    ) => builder.ForEach(
            items,
            static (g, item) => g.AppendBody(item)
        );

    public static GeneratorStringBuilder ForEachAppendBody<T>(
        this GeneratorStringBuilder builder,
        IEnumerable<T> items,
        Func<T, string> itemFormatter
    ) => builder.ForEach(
            items,
            static (g, item, formatter) => g.AppendBody(formatter(item)),
            itemFormatter
        );

    public static GeneratorStringBuilder ForEachAppendBody<T, T1>(
        this GeneratorStringBuilder builder,
        IEnumerable<T> items,
        Func<T, T1, string> itemFormatter,
        T1 arg
    ) => builder.ForEach(
            items,
            static (g, item, formatter, arg) => g.AppendBody(formatter(item, arg)),
            itemFormatter,
            arg
        );

}

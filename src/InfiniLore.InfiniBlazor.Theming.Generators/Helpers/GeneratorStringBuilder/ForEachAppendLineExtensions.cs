// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace InfiniLore.InfiniBlazor.Theming.Generators.Helpers.GeneratorStringBuilder;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ForEachAppendLineExtensions {
    public static GeneratorStringBuilder ForEachAppendLine(
        this GeneratorStringBuilder builder,
        IEnumerable<string> items
    ) => builder.ForEach(
            items,
            static (g, item) => g.AppendLine(item)
        );

    public static GeneratorStringBuilder ForEachAppendLine<T>(
        this GeneratorStringBuilder builder,
        IEnumerable<T> items,
        Func<T, string> itemFormatter
    ) => builder.ForEach(
            items,
            static (g, item, formatter) => g.AppendLine(formatter(item)),
            itemFormatter
        );

    public static GeneratorStringBuilder ForEachAppendLine<T, T1>(
        this GeneratorStringBuilder builder,
        IEnumerable<T> items,
        Func<T, T1, string> itemFormatter,
        T1 arg
    ) => builder.ForEach(
            items,
            static (g, item, formatter, arg) => g.AppendLine(formatter(item, arg)),
            itemFormatter,
            arg
        );

}

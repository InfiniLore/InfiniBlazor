// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace InfiniLore.InfiniBlazor.Theming.Generators.Helpers.GeneratorStringBuilder;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ForEachAppendBodyIndentedExtensions {
    public static GeneratorStringBuilder ForEachAppendBodyIndented(
        this GeneratorStringBuilder builder, 
        IEnumerable<string> items
    ) => builder.ForEach(
            items,
            static (g, item) => g.AppendBodyIndented(item)
        );

    public static GeneratorStringBuilder ForEachAppendBodyIndented<T>(
        this GeneratorStringBuilder builder,
        IEnumerable<T> items,
        Func<T, string> itemFormatter
    ) => builder.ForEach(
            items,
            static (g, item, formatter) => g.AppendBodyIndented(formatter(item)),
            itemFormatter
        );

    public static GeneratorStringBuilder ForEachAppendBodyIndented<T, T1>(
        this GeneratorStringBuilder builder,
        IEnumerable<T> items, 
        Func<T, T1, string> itemFormatter,
        T1 arg
    ) 
        => builder.ForEach(
            items,
            static (g, item, formatter, arg) => g.AppendBodyIndented(formatter(item, arg)),
            itemFormatter,
            arg
        );

}

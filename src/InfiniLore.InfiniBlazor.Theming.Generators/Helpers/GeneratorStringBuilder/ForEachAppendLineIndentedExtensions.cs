// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace CodeOfChaos.GeneratorTools;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ForEachAppendLineIndentedExtensions {
    public static GeneratorStringBuilder ForEachAppendLineIndented(
        this GeneratorStringBuilder builder, 
        IEnumerable<string> items
    ) => builder.ForEach(
            items,
            static (g, item) => g.AppendLineIndented(item)
        );

    public static GeneratorStringBuilder ForEachAppendLineIndented<T>(
        this GeneratorStringBuilder builder,
        IEnumerable<T> items,
        Func<T, string> itemFormatter
    ) => builder.ForEach(
            items,
            static (g, item, formatter) => g.AppendLineIndented(formatter(item)),
            itemFormatter
        );

    public static GeneratorStringBuilder ForEachAppendLineIndented<T, T1>(
        this GeneratorStringBuilder builder,
        IEnumerable<T> items, 
        Func<T, T1, string> itemFormatter,
        T1 arg
    ) 
        => builder.ForEach(
            items,
            static (g, item, formatter, arg) => g.AppendLineIndented(formatter(item, arg)),
            itemFormatter,
            arg
        );

}

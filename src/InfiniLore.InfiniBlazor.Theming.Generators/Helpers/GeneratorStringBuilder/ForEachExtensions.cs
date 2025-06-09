// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;

namespace InfiniLore.InfiniBlazor.Theming.Generators.Helpers.GeneratorStringBuilder;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public static class ForEachExtensions {
    public static GeneratorStringBuilder ForEach<T>(
        this GeneratorStringBuilder builder, 
        IEnumerable<T> items,
        Action<GeneratorStringBuilder, T> itemFormatter
    ) {
        if (items is ICollection<T> { Count: 0 }) return builder;// Skip iteration if no items

        foreach (T item in items) itemFormatter(builder, item);
        return builder;
    }

    public static GeneratorStringBuilder ForEach<T, T1>(
        this GeneratorStringBuilder builder,
        IEnumerable<T> items,
        Action<GeneratorStringBuilder, T, T1> itemFormatter,
        T1 arg
    ) {
        if (items is ICollection<T> { Count: 0 }) return builder; // Skip iteration if no items

        foreach (T item in items) itemFormatter(builder, item, arg);
        return builder;
    }
    
    public static GeneratorStringBuilder ForEach<T, T1, T2>(
        this GeneratorStringBuilder builder,
        IEnumerable<T> items,
        Action<GeneratorStringBuilder, T, T1, T2> itemFormatter,
        T1 arg1,
        T2 arg2
    ) {
        if (items is ICollection<T> { Count: 0 }) return builder; // Skip iteration if no items

        foreach (T item in items) itemFormatter(builder, item, arg1, arg2);
        return builder;
    }
    
    public static GeneratorStringBuilder ForEachIndented<T>(
        this GeneratorStringBuilder builder, 
        IEnumerable<T> items,
        Action<GeneratorStringBuilder, T> itemFormatter
    ) {
        if (items is ICollection<T> { Count: 0 }) return builder; // Skip iteration if no items

        builder.Indent(b => {
            foreach (T item in items) itemFormatter(b, item);
        });
        
        return builder;
    }

    public static GeneratorStringBuilder ForEachIndented<T, T1>(
        this GeneratorStringBuilder builder,
        IEnumerable<T> items,
        Action<GeneratorStringBuilder, T, T1> itemFormatter,
        T1 arg
    ) {
        if (items is ICollection<T> { Count: 0 }) return builder; // Skip iteration if no items
        
        builder.Indent(b => {
            foreach (T item in items) itemFormatter(b, item, arg);
        });
        
        return builder;
    }
    
    public static GeneratorStringBuilder ForEachIndented<T, T1, T2>(
        this GeneratorStringBuilder builder,
        IEnumerable<T> items,
        Action<GeneratorStringBuilder, T, T1, T2> itemFormatter,
        T1 arg1,
        T2 arg2
    ) {
        if (items is ICollection<T> { Count: 0 }) return builder; // Skip iteration if no items
        
        builder.Indent(b => {
            foreach (T item in items) itemFormatter(b, item, arg1, arg2);
        });

        return builder;
    }
}

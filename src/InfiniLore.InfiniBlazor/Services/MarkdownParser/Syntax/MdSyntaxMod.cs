// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.MarkdownParser.RegexLib;
using InfiniLore.InfiniBlazor.Pooling;
using Microsoft.Extensions.ObjectPool;
using System.Text.RegularExpressions;

namespace InfiniLore.InfiniBlazor.MarkdownParser.Syntax;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class MdSyntaxMod : IResettable {
    public string? Title { get; private set; }
    public string? Style { get; private set; }
    public (int width, int height)? Size { get; private set; }
    public bool Fit { get; private set; }

    public static ObjectPool<MdSyntaxMod> Pool { get; } = PoolingHelpers.CreateResettablePool<MdSyntaxMod>(16);

    private static readonly int ModTitleId = MdRegexLib.GetGroupId(MdRegexGroupNames.ModTitle);
    private static readonly int ModStyleId = MdRegexLib.GetGroupId(MdRegexGroupNames.ModStyle);
    private static readonly int ModSizeId = MdRegexLib.GetGroupId(MdRegexGroupNames.ModSize);
    private static readonly int ModFitId = MdRegexLib.GetGroupId(MdRegexGroupNames.ModFit);

    // -----------------------------------------------------------------------------------------------------------------
    // Constructors
    // -----------------------------------------------------------------------------------------------------------------
    public static MdSyntaxMod FromString(string input) {
        MdSyntaxMod mod = Pool.Get();

        MatchCollection mods = MdRegexLib.ModifierStructuresRegex.Matches(input);
        foreach (Match match in mods) {
            GroupCollection groups = match.Groups;

            if (groups[ModTitleId] is { Success: true, Value: var title }) {
                mod.Title = title;
                continue;
            }

            if (groups[ModStyleId] is { Success: true, Value: var style }) {
                mod.Style = style;
                continue;
            }

            if (groups[ModFitId].Success) {
                mod.Fit = true;
                continue;
            }

            // ReSharper disable once InvertIf
            if (groups[ModSizeId] is { Success: true, Value: var size }) {
                if (size.Contains('x')) {
                    string[] split = size.Split('x');
                    mod.Size = (int.Parse(split[0]), int.Parse(split[1]));
                }
                else if (int.TryParse(size, out int sizeInt)) {
                    mod.Size = (sizeInt, sizeInt);
                }
            }

        }

        return mod;
    }

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool TryReset() {
        Title = null;
        Style = null;
        Size = null;
        Fit = false;

        return true;
    }
}

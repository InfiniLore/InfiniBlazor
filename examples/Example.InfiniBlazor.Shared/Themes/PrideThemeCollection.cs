// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Theming;
using InfiniLore.InfiniBlazor.Theming.CssData;
using JetBrains.Annotations;

namespace Example.InfiniBlazor.Shared.Themes;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[UsedImplicitly]
public class PrideThemeCollection : ThemeCollection {
    public const string Name = "Pride";
    public override string CollectionName => Name;
    
    // Based on: https://cesque.com/gaydient/
    protected override OrderedDictionary<ThemeMode, ICssData> Modes { get; } = new() {
        [Trans] = new EmptyCssData {
            ColorInfiniAccent = "rgb(85, 205, 252)",
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(85, 205, 252), rgb(179, 157, 233), rgb(247, 168, 184), rgb(246, 216, 221), rgb(255, 255, 255) 45%, rgb(255, 255, 255), rgb(255, 255, 255) 55%, rgb(246, 216, 221), rgb(247, 168, 184), rgb(179, 157, 233), rgb(85, 205, 252))",
            ColorInfiniNavHorizontal = "rgb(85, 205, 252)",
            ColorInfiniNavHamburger = "var(--color-infini-base-95)",
            ColorInfiniNavText = "var(--color-infini-base-95)",
            ColorInfiniNavTextHover = "var(--color-infini-base-95)",
        },
        
        [Pride] = new EmptyCssData {
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(237, 34, 36), rgb(243, 91, 34), rgb(249, 150, 33), rgb(245, 193, 30), rgb(241, 235, 27) 27%, rgb(241, 235, 27), rgb(241, 235, 27) 33%, rgb(99, 199, 32), rgb(12, 155, 73), rgb(33, 135, 141), rgb(57, 84, 165), rgb(97, 55, 155), rgb(147, 40, 142))",
            ColorInfiniNavTop = "rgb(237, 34, 36)",
            ColorInfiniNavBottom = "rgb(147, 40, 142)",
            ColorInfiniNavHamburger = "var(--color-infini-base-00)",
            ColorInfiniNavText = "var(--color-infini-base-00)",
            ColorInfiniNavTextHover = "var(--color-infini-base-00)",
        },
        
        // The remaining are inserted alphabetically
        [Agender] = new EmptyCssData {
            ColorInfiniAccent = "rgb(186, 244, 132)",
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(0, 0, 0), rgb(103, 103, 103), rgb(205, 205, 205) 15%, rgb(230, 230, 230), rgb(255, 255, 255) 22%, rgb(255, 255, 255), rgb(255, 255, 255) 28%, rgb(228, 239, 201), rgb(186, 244, 132), rgb(228, 239, 201), rgb(255, 255, 255) 72%, rgb(255, 255, 255), rgb(255, 255, 255) 78%, rgb(230, 230, 230), rgb(205, 205, 205) 85%, rgb(103, 103, 103), rgb(0, 0, 0))",
            ColorInfiniNavHamburger = "var(--color-infini-base-95)",
            ColorInfiniNavText = "var(--color-infini-base-95)",
            ColorInfiniNavTextHover = "var(--color-infini-base-95)",
        },
        
        [Aromantic] = new EmptyCssData {
            ColorInfiniAccent = "rgb(60, 170, 65)",
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(60, 170, 65), rgb(179, 207, 164), rgb(255, 255, 255) 45%, rgb(255, 255, 255), rgb(255, 255, 255) 55%, rgb(128, 128, 128), rgb(0, 0, 0))",
            ColorInfiniNavHamburger = "var(--color-infini-base-95)",
            ColorInfiniNavText = "var(--color-infini-base-95)",
            ColorInfiniNavTextHover = "var(--color-infini-base-95)",
        },
        
        [Asexual] = new EmptyCssData {
            ColorInfiniAccent = "rgb(101, 46, 129)",
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(0, 0, 0), rgb(81, 81, 80), rgb(163, 162, 160), rgb(209, 208, 208), rgb(255, 255, 255) 60%, rgb(255, 255, 255), rgb(255, 255, 255) 70%, rgb(185, 151, 185), rgb(101, 46, 129))",
            ColorInfiniNavHamburger = "var(--color-infini-base-00)",
            ColorInfiniNavText = "var(--color-infini-base-95)",
            ColorInfiniNavTextHover = "var(--color-infini-base-95)",
        },
        
        [Bisexual] = new EmptyCssData {
            ColorInfiniAccent = "rgb(255, 0, 128)",
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(255, 0, 128), rgb(200, 37, 157), rgb(140, 71, 153), rgb(68, 46, 159), rgb(0, 50, 160))",
            ColorInfiniNavHamburger = "var(--color-infini-base-00)",
            ColorInfiniNavText = "var(--color-infini-base-00)",
            ColorInfiniNavTextHover = "var(--color-infini-base-00)",
        },
        
        [GayMan] = new EmptyCssData {
            ColorInfiniAccent = "rgb(7, 141, 111)",
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(7, 141, 111), rgb(152, 202, 153), rgb(255, 255, 255) 45%, rgb(255, 255, 255), rgb(255, 255, 255) 55%, rgb(205, 208, 234), rgb(123, 173, 226) 70%, rgb(69, 76, 189), rgb(63, 26, 121))",
            ColorInfiniNavHamburger = "var(--color-infini-base-95)",
            ColorInfiniNavText = "var(--color-infini-base-95)",
            ColorInfiniNavTextHover = "var(--color-infini-base-95)",
        },
        
        [GenderFluid] = new EmptyCssData {
            ColorInfiniAccent = "rgb(255, 120, 166)",
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(255, 120, 166), rgb(247, 196, 210), rgb(255, 255, 255) 23%, rgb(255, 255, 255), rgb(255, 255, 255) 28%, rgb(219, 148, 213), rgb(190, 20, 215), rgb(89, 22, 85), rgb(0, 0, 0) 73%, rgb(0, 0, 0), rgb(0, 0, 0) 78%, rgb(51, 36, 87), rgb(50, 60, 191))",
            ColorInfiniNavHamburger = "var(--color-infini-base-95)",
            ColorInfiniNavText = "var(--color-infini-base-95)",
            ColorInfiniNavTextHover = "var(--color-infini-base-95)",
        },
        
        [GenderQueer] = new EmptyCssData {
            ColorInfiniAccent = "rgb(185, 154, 222)",
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(185, 154, 222), rgb(225, 209, 232), rgb(255, 255, 255) 45%, rgb(255, 255, 255), rgb(255, 255, 255) 55%, rgb(187, 191, 159), rgb(107, 142, 58))",
            ColorInfiniNavHamburger = "var(--color-infini-base-95)",
            ColorInfiniNavText = "var(--color-infini-base-95)",
            ColorInfiniNavTextHover = "var(--color-infini-base-95)",
        },
        
        [Intersex] = new EmptyCssData {
            ColorInfiniAccent = "rgb(161, 3, 245)",
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(255, 217, 0), rgb(255, 217, 0), rgb(255, 217, 0) 10%, rgb(230, 54, 97), rgb(157, 0, 253), rgb(230, 54, 97), rgb(255, 217, 0) 90%, rgb(255, 217, 0), rgb(255, 217, 0))",
            ColorInfiniNavHamburger = "var(--color-infini-base-95)",
            ColorInfiniNavText = "var(--color-infini-base-95)",
            ColorInfiniNavTextHover = "var(--color-infini-base-95)",
        },
        
        [Lesbian] = new EmptyCssData {
            ColorInfiniAccent = "rgb(213, 44, 0)",
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(213, 44, 0), rgb(226, 150, 136), rgb(255, 255, 255) 45%, rgb(255, 255, 255), rgb(255, 255, 255) 55%, rgb(210, 127, 164), rgb(162, 2, 98))",
            ColorInfiniNavHamburger = "var(--color-infini-base-95)",
            ColorInfiniNavText = "var(--color-infini-base-95)",
            ColorInfiniNavTextHover = "var(--color-infini-base-95)",
        },
        
        [NonBinary] = new EmptyCssData {
            ColorInfiniAccent = "rgb(253, 219, 0)",
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(253, 219, 0), rgb(238, 212, 143), rgb(255, 255, 255) 30%, rgb(255, 255, 255), rgb(255, 255, 255) 36%, rgb(212, 181, 222), rgb(156, 92, 212), rgb(88, 50, 96), rgb(0, 0, 0))",
            ColorInfiniNavHamburger = "var(--color-infini-base-95)",
            ColorInfiniNavText = "var(--color-infini-base-95)",
            ColorInfiniNavTextHover = "var(--color-infini-base-95)",
        },
        
        [PanSexual] = new EmptyCssData {
            ColorInfiniAccent = "rgb(255, 30, 140)",
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(255, 30, 140) 10%, rgb(255, 102, 57), rgb(255, 230, 29) 40%, rgb(255, 230, 29), rgb(255, 230, 29) 60%, rgb(87, 229, 98), rgb(31, 179, 253) 85%, rgb(31, 179, 253), rgb(31, 179, 253))",
            ColorInfiniNavHamburger = "var(--color-infini-base-95)",
            ColorInfiniNavText = "var(--color-infini-base-95)",
            ColorInfiniNavTextHover = "var(--color-infini-base-95)",
        },
        
        [PocInclusive] = new EmptyCssData {
            ColorInfiniNavVertical = "linear-gradient(to bottom, rgb(0, 0, 0), rgb(54, 35, 18), rgb(120, 79, 23), rgb(181, 63, 27), rgb(237, 34, 36), rgb(243, 91, 34), rgb(249, 150, 33), rgb(245, 193, 30), rgb(241, 235, 27) 48%, rgb(241, 235, 27), rgb(241, 235, 27) 52%, rgb(99, 199, 32), rgb(12, 155, 73), rgb(33, 135, 141), rgb(57, 84, 165), rgb(97, 55, 155), rgb(147, 40, 142))",
            ColorInfiniNavHamburger = "var(--color-infini-base-00)",
            ColorInfiniNavText = "var(--color-infini-base-00)",
            ColorInfiniNavTextHover = "var(--color-infini-base-00)",
        },
    };
    
    private static ThemeMode Trans { get; } = ThemeMode.AsCustom(nameof(Trans));
    private static ThemeMode Pride { get; } = ThemeMode.AsCustom(nameof(Pride));

    private static ThemeMode Agender { get; } = ThemeMode.AsCustom(nameof(Agender));
    private static ThemeMode Aromantic { get; } = ThemeMode.AsCustom(nameof(Aromantic));
    private static ThemeMode Asexual { get; } = ThemeMode.AsCustom(nameof(Asexual));
    private static ThemeMode Bisexual { get; } = ThemeMode.AsCustom(nameof(Bisexual));
    private static ThemeMode GayMan { get; } = ThemeMode.AsCustom(nameof(GayMan));
    private static ThemeMode GenderFluid { get; } = ThemeMode.AsCustom(nameof(GenderFluid));
    private static ThemeMode GenderQueer { get; } = ThemeMode.AsCustom(nameof(GenderQueer));
    private static ThemeMode Intersex { get; } = ThemeMode.AsCustom(nameof(Intersex));
    private static ThemeMode Lesbian { get; } = ThemeMode.AsCustom(nameof(Lesbian));
    private static ThemeMode NonBinary { get; } = ThemeMode.AsCustom(nameof(NonBinary));
    private static ThemeMode PanSexual { get; } = ThemeMode.AsCustom(nameof(PanSexual));
    private static ThemeMode PocInclusive { get; } = ThemeMode.AsCustom(nameof(PocInclusive));
}

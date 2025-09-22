// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public readonly record struct ThemeMode(
    string Name,
    ThemeModeVariants Variant
) {
    public const string LigtModeName = "light-mode";
    public const string DarkModeName = "dark-mode";
    public const string VibrantModeName = "vibrant-mode";
    public const string MutedModeName = "muted-mode";
    public const string OledModeName = "oled-mode";
    
    public static ThemeMode Empty { get; } = new ThemeMode(string.Empty, ThemeModeVariants.Undefined);

    public static ThemeMode LightMode { get; } = new(LigtModeName, ThemeModeVariants.Light);
    public static ThemeMode DarkMode { get; } = new(DarkModeName, ThemeModeVariants.Dark);
    public static ThemeMode VibrantMode { get; } = new(VibrantModeName, ThemeModeVariants.Vibrant);
    public static ThemeMode MutedMode { get; } = new(MutedModeName, ThemeModeVariants.Muted);
    public static ThemeMode OledMode { get; } = new(OledModeName, ThemeModeVariants.Oled);

    public static ThemeMode AsCustom(string name) => new(name, ThemeModeVariants.Custom);
    public static ThemeMode AsUndefined(string name) => new(name, ThemeModeVariants.Undefined);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public bool Equals(ThemeMode other) {
        return Name == other.Name;
    }

    public override int GetHashCode() => Name.GetHashCode();
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record ThemeMode(
    string Name,
    bool IsDark,
    bool IsLight
) : IThemeMode {
    private readonly int _hashCode = Name.GetHashCode();
    
    public static IThemeMode LightMode { get; } = new ThemeMode("light-mode", false, true);
    public static IThemeMode DarkMode { get; } = new ThemeMode("dark-mode", true, false);

    // -----------------------------------------------------------------------------------------------------------------
    // Methods
    // -----------------------------------------------------------------------------------------------------------------
    public virtual bool Equals(ThemeMode? other) {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return Name == other.Name;
    }

    public override int GetHashCode() => _hashCode;
}

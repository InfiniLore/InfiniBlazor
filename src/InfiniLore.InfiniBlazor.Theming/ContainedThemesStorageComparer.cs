// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Theming;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public class ContainedThemesStorageComparer : IEqualityComparer<IThemeMode>,
    IAlternateEqualityComparer<string, IThemeMode>,
    IAlternateEqualityComparer<ReadOnlySpan<char>, IThemeMode> {
    public static readonly ContainedThemesStorageComparer Instance = new();
    
    public bool Equals(IThemeMode? x, IThemeMode? y) 
        => ReferenceEquals(x, y) || x is not null && y is not null && x.Equals(y);
    
    public int GetHashCode(IThemeMode obj) 
        => obj.Name.GetHashCode();
    
    public bool Equals(string alternate, IThemeMode other) 
        => alternate.Equals(other.Name, StringComparison.Ordinal);
    
    public int GetHashCode(string alternate) 
        => alternate.GetHashCode();
    
    public bool Equals(ReadOnlySpan<char> alternate, IThemeMode other) 
        => alternate.Equals(other.Name.AsSpan(), StringComparison.Ordinal);
    
    public int GetHashCode(ReadOnlySpan<char> alternate) 
        =>  string.GetHashCode(alternate);
    
    // DO NOT DO THIS
    public IThemeMode Create(string alternate) => throw new NotImplementedException();
    public IThemeMode Create(ReadOnlySpan<char> alternate) => throw new NotImplementedException();
}

// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.MarkdownParser.EmoteSystem;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public sealed class EmoteKeyComparer : IEqualityComparer<EmoteKey>,
    IAlternateEqualityComparer<string, EmoteKey>,
    IAlternateEqualityComparer<ReadOnlySpan<char>, EmoteKey> 
{
    public static EmoteKeyComparer Instance { get; } = new();

    #region EmoteKey
    public bool Equals(EmoteKey? x, EmoteKey? y)
        => x is not null && y is not null && x.Equals(y);

    public int GetHashCode(EmoteKey obj)
        => obj.GetHashCode();
    #endregion

    #region String
    public bool Equals(string alternate, EmoteKey other)
        => other.ContainsKey(alternate);

    public int GetHashCode(string alternate)
        => alternate.GetHashCode();

    public EmoteKey Create(string alternate) 
        => throw new NotSupportedException("Cannot create registration based on partial key");
    #endregion

    #region ReadOnlySpan<char>
    public bool Equals(ReadOnlySpan<char> alternate, EmoteKey other)
        => other.ContainsKey(alternate);
    
    public int GetHashCode(ReadOnlySpan<char> alternate)
        => alternate.GetHashCode();
    
    public EmoteKey Create(ReadOnlySpan<char> alternate) 
        => throw new NotSupportedException("Cannot create registration based on partial key");
    #endregion
}

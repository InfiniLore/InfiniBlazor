// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record struct AvatarStatusData(
    AvatarStatus Status,
    string? CustomStatus
) {
    public static AvatarStatusData Empty { get; } = new(AvatarStatus.Undefined, null);
    public static AvatarStatusData Online { get; } = new(AvatarStatus.Online, null);
    public static AvatarStatusData Offline { get; } = new(AvatarStatus.Offline, null);
    public static AvatarStatusData Busy { get; } = new(AvatarStatus.Busy, null);
    public static AvatarStatusData DoNotDisturb { get; } = new(AvatarStatus.DoNotDisturb, null);
    public static AvatarStatusData Custom(string customStatus) => new(AvatarStatus.Custom, customStatus);
}

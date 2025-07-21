// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using InfiniLore.InfiniBlazor.Dialogs;
using Microsoft.AspNetCore.Components;

namespace InfiniLore.InfiniBlazor.Components;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public record InfiniDialogBasicDto(
    string Title,
    string Body,
    EventCallback OnReturn = default,
    EventCallback OnExit = default,
    EventCallback OnContinue = default
    ) : DialogData<InfiniDialogBasic> {

    public override IDictionary<string, object?> AsDynamicParameters() {
        return new Dictionary<string, object?> {
            [nameof(Title)] = Title,
            [nameof(Body)] = Body,
            [nameof(OnReturn)] = OnReturn,
            [nameof(OnExit)] = OnExit,
            [nameof(OnContinue)] = OnContinue,
            ["DialogData"] = this,
        };
    }
}

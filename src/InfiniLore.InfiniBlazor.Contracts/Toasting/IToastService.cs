// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using System.Collections;

namespace InfiniLore.InfiniBlazor.Toasting;

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
public interface IToastService {
    IEnumerable<IToastMessage> Messages { get; }
    event Func<Task>? OnChangeAsync;

    Task ShowToastAsync(string title, string message, int durationSeconds = 5);
}

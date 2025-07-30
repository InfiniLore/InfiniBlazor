// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
using Android.App;
using Android.Runtime;

// ReSharper disable once CheckNamespace
namespace Example.InfiniBlazor.Maui;
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
[Application]
public class MainApplication(IntPtr handle, JniHandleOwnership ownership) : MauiApplication(handle, ownership) {
    protected override MauiApp CreateMauiApp() => MauiProgram.CreateMauiApp();
}

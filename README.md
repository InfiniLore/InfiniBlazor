# 🎨 InfiniLore.InfiniBlazor
A modern Blazor component library made for InfiniLore, but can be adap.

## 📋 Prerequisites

- .NET 9.0 or later
- ASP.NET Core

## 📦 Installation

Install the package via NuGet:
```shell
dotnet add package InfiniLore.InfiniBlazor
```
## 🚀 Quick Start

### 1. Add Required Imports
Add to your `App.razor`:
```html
<link rel="stylesheet" href="_content/InfiniLore.InfiniBlazor/InfiniBlazor.css" />
<script src="_content/InfiniLore.InfiniBlazor/InfiniBlazor.js"></script>
```

Add to your `_Imports.razor`:
```razor
@using InfiniLore.InfiniBlazor
@using InfiniLore.InfiniBlazor.Components
```

In your `MainLayout.razor` or main layout:
```razor
<InfiniThemeManager/>
```

In your `Program.cs`:
```csharp
builder.Services.AddInfiniBlazor();
```

## 🤝 Contributing
Contributions are welcome! Please feel free to submit a Pull Request.

## 💬 Support

For issues and feature requests, please use the GitHub Issues section.

---

Made with ❤️ by AnnaSasDev and the contributors to InfiniLore
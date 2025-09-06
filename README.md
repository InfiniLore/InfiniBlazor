# InfiniLore.InfiniBlazor

A comprehensive Blazor component library built for modern web applications.
`InfiniLore.InfiniBlazor` provides a rich set of UI components with advanced features including theming, markdown processing, and optional packages for auto-documentation, and cross-platform MAUI support.

> Originally this project was created as a simple interactive Markdown editor that could be used within any framework. 
> However, as the project grew, it became clear that the library would benefit from a more comprehensive set of components and features. 
> As a result, the project was rewritten from scratch to provide a more comprehensive component library and a more modern development experience.

## Features

### Core Components
- **UI Components**: Buttons, checkboxes, radio buttons, dividers, tabs, layouts
- **Advanced Controls**: Emote system with Lucide icon integration
- **Interactive Elements**: Dialog system, callouts, query parameter management
- **Theming System**: Dynamic theme management with CSS custom properties
- **Markdown Processing**: Advanced Markdown syntax parsing and allowing developers to attach interactive rendering with custom Blazor integration
- **JavaScript Interop**: Seamless JavaScript integration layer
- **Toast Notifications**: Built-in notification system

### Extensions
- **Auto Documentation**: Automatic component documentation generation with source generators
- **Extra Components**: Extended component library for specialized use cases
- **MAUI Integration**: Cross-platform mobile and desktop application support

## Technology Stack

- **.NET 9.0**: Latest .NET Core 
- **C# 13.0**: Modern C# language features
- **TUnit**: Modern testing framework

## Prerequisites

- .NET 9.0 SDK or later
- Node.js (for TypeScript and TailwindCSS compilation)
- Visual Studio 2022 or JetBrains Rider (recommended)

## Installation

### NuGet Package
```bash
dotnet add package InfiniLore.InfiniBlazor
```

### From Source
```bash
git clone https://github.com/InfiniLore/InfiniBlazor
cd InfiniLore.InfiniBlazor
dotnet restore
```

## Quick Start

### 1. Service Registration
Add to your `Program.cs`:
```csharp
builder.Services.AddInfiniBlazor();

// ...

app.UseInfiniBlazor(); // Used for importing static data, like emote libs, at startup

```
### 2. Import Statements
Add to your `_Imports.razor`:
```razor
@using InfiniLore.InfiniBlazor
@using InfiniLore.InfiniBlazor.Components
```
### 3. CSS and JavaScript Resources
Add to your `App.razor` or main HTML file:
```html
<link rel="stylesheet" href="_content/InfiniLore.InfiniBlazor/InfiniBlazor.css" />
<script src="_content/InfiniLore.InfiniBlazor/InfiniBlazor.js"></script>
```
### 4. Theme Manager
Add to your main layout:
```htmlinblazor
<InfiniThemeManager/>
<InfiniDialogManager/>

<InfiniLayout Layout="NavLayout.LeftPrimary">
    <Left>
        <NavBarLeft/>
    </Left>
    
    @* Other Layout locations if you want to ... *@
    
    <Body>
        <InfiniToastManager/>
        @Body
    </Body>
</InfiniLayout>
```

### 5. Basic Component Usage
```htmlinblazor
<InfiniButton Color="Color.Primary" Size="Size.M" OnClick="HandleClick">
    Click Me
</InfiniButton>

<InfiniCheckbox @bind-Value="isChecked" Label="Enable feature" />

@*...*@
```

## Extension Modules

### Auto Documentation
Automatically generates documentation for components using source generators:
```razor
<InfiniAutoDocument Id="my-component">
    <MyComponent />
</InfiniAutoDocument>
```

### MAUI Integration
Cross-platform support for mobile and desktop applications with specialized components and services.

## Theming System
Although the library is built with using TailwindCSS for styling, it is not required to style your application using TailwindCSS.

The library includes a comprehensive theming system with:
- CSS custom property-based themes
- Dynamic theme switching
- Component-specific styling hooks
- TailwindCSS integration for utility classes

## Contributing

### Development Setup
1. Clone the repository
2. Install .NET 9.0 SDK
3. Install Node.js dependencies: `npm install`
4. Build the solution: `dotnet build`

### Code Standards
- Follow established C# conventions
- Use nullable reference types
- Include comprehensive tests for new features
- Update documentation for public APIs

## License
This project is licensed under the GNU Lesser General Public License v3.0. See the LICENSE file for details.

## Support and Community
- **Issues**: Report bugs and request features via GitHub Issues
- **Discussions**: Join community discussions for questions and ideas
- **Contributing**: See CONTRIBUTING.md for development guidelines

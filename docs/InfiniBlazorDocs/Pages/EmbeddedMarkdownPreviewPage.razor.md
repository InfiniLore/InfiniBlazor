# Embedded Markdown Preview Component
This component allows for the preview of Markdown content embedded within the application.
It leverages the `RazorMarkdownFileExtractor` service to fetch and render markdown files directly from the application's resources.

## Usage
To use this component, include it in your Razor page and provide the path to the embedded Markdown resource. 
The component will handle the extraction and rendering of the Markdown content.

```htmlinblazor
<EmbeddedMarkdownPreview Path="Resources.Markdown.GettingStarted.md" />
```
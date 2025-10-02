// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// noinspection JSUnusedGlobalSymbols
export class HighlightLib {
    private isHighlightJsLoaded(): this is this & { __hljsAvailable: true } {
        return typeof window !== 'undefined' && window.hljs !== undefined;
    }

    public highlightAll(): void {
        if (this.isHighlightJsLoaded()) {
            window.hljs!.highlightAll();
        }
    }

    public highlightElement(element: HTMLElement): void {
        if (!element) return;

        if (this.isHighlightJsLoaded()) {
            window.hljs!.highlightElement(element);
        }
    }

    public configure(options: any): void {
        if (this.isHighlightJsLoaded()) {
            window.hljs!.configure(options);
        }
    }

    public getAvailableLanguages(): string[] {
        if (this.isHighlightJsLoaded()) {
            return window.hljs!.listLanguages();
        }
        return [];
    }
}
// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// noinspection JSUnusedGlobalSymbols
export class HighlightLib {
    private isHighlightJsAvailable(): boolean {
        return typeof window !== 'undefined' && window.hljs !== undefined;
    }
    
    public highlightAll(): void {
        if (!this.isHighlightJsAvailable()) return;
        window.hljs!.highlightAll();
    }

    public highlightElement(element: HTMLElement): void {
        if (!this.isHighlightJsAvailable()) return;
        if (!element) return;

        if (element.dataset.highlighted) delete element.dataset.highlighted;
        window.hljs!.highlightElement(element);
    }
    
    public setContentAndHighlight(element: HTMLElement, content: string): void {
        if (!this.isHighlightJsAvailable()) return;
        if (!element) return;
        if (!content) return;
        
        element.textContent = content;
        this.highlightElement(element);
    }

    public configure(options: any): void {
        if (!this.isHighlightJsAvailable()) return;
        window.hljs!.configure(options);
    }

    public getAvailableLanguages(): string[] {
        if (!this.isHighlightJsAvailable()) return [];
        return window.hljs!.listLanguages();
    }
}
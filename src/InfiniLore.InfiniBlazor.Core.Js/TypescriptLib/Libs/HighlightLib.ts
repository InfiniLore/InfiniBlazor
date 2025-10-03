// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
import hljs from 'highlight.js';
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// noinspection JSUnusedGlobalSymbols
export class HighlightLib {
    public highlightAll(): void {
        hljs.highlightAll();
    }

    public highlightElement(element: HTMLElement): void {
        if (!element) return;

        if (element.dataset.highlighted) delete element.dataset.highlighted;
        hljs.highlightElement(element);
    }
    
    public setContentAndHighlight(element: HTMLElement, content: string): void {
        if (!element) return;
        if (!content) return;
        
        element.textContent = content;
        this.highlightElement(element);
    }

    public configure(options: any): void {
        hljs.configure(options);
    }

    public getAvailableLanguages(): string[] {
        return hljs.listLanguages();
    }
}
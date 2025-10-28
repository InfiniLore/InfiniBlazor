// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
export class MermaidLib {
    public isMermaidJsAvailable(): boolean {
        return typeof window !== 'undefined' && window.mermaid !== undefined;
    }
    
    public async renderMermaidAsync(element: HTMLElement): Promise<void> {
        if (!element) return;
        
        if (!this.isMermaidJsAvailable()) {
            throw new Error("Mermaid is not available");
        }
        
        window.mermaid?.run({
            nodes: [element],
        });
    }
    
    public async renderMermaidWithContentAsync(element:HTMLElement, content: string): Promise<void> {
        if (!element) return;
        if (!content) return;
        
        if (!this.isMermaidJsAvailable()) {
            throw new Error("Mermaid is not available");
        }
        
        element.textContent = content;
        window.mermaid?.run({
            nodes: [element],
        });
    }
}
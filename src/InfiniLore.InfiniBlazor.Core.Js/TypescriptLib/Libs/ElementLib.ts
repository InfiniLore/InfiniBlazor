// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// noinspection JSUnusedGlobalSymbols
export class ElementLib {
    public setTextContent(element: HTMLElement, text: string): void {
        if (!element) return;
        if (!text) return;
        
        element.textContent = text;
    }

    public getTextContent(element: HTMLElement): string {
        if (!element) return "";
        if (!element.textContent) return "";
        
        return element.textContent;
    }

    public addHorizontalScroll(element: HTMLElement, i: number) : void {
        if (!element) return;
        if (!i) return;
        
        element.scrollBy({ left: i, behavior: 'smooth' });
    }

    public clickElement(element: HTMLElement) : void {
        if (!element) return;
        
        element.click();
    }

    public clickElementById(elementId:string) : void {
        if (!elementId) return;
        
        const element = document.getElementById(elementId);
        if (element === null) return;
        
        element.click();
    }
}
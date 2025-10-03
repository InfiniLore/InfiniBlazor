// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
import {IInputElement} from "../Contracts/IInputElement";

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// noinspection JSUnusedGlobalSymbols
export class ElementLib {
    public setValue(element: IInputElement, value: string): void {
        if (!element) return;
        if (!value) return;
        
        element.value = value;
    }
    
    public setTextContent(element: HTMLElement, text: string): void {
        if (!element) return;
        if (!text) return;
        
        element.textContent = text;
    }
    
    public setTextContentSelectionAware(element: IInputElement, text: string): void {
        if (!element) return;
        if (!text) return;

        const start = element.selectionStart || 0;
        const end = element.selectionEnd || 0;
        element.textContent = text;
        element.setSelectionRange(start, end);
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
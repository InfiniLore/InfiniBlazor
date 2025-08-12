// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
import {IInputElement} from "../Contracts/IInputElement";
import {CSharpTuple} from "../Contracts/CSharpTuple";

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// noinspection JSUnusedGlobalSymbols
export class InputElementLib {
    private isValidInputElement(element: IInputElement): boolean {
        if (!element) return false;
        return element.tagName.toLowerCase() === "input" || element.tagName.toLowerCase() === "textarea";
    }

    public getSelectionStart(element: IInputElement): number {
        if (!this.isValidInputElement(element)) return -1;
        return element.selectionStart || 0;
    }

    public getSelectionEnd(element: IInputElement): number {
        if (!this.isValidInputElement(element)) return -1;
        return element.selectionEnd || 0;
    }

    public getSelection(element: IInputElement): CSharpTuple<number, number> {
        if (!this.isValidInputElement(element)) return {
            Item1: -1,
            Item2: -1
        }
        return {
            Item1: element.selectionStart || 0,
            Item2: element.selectionEnd || 0
        };
    }

    public setSelectionRange(element: IInputElement, start: number, end: number): void {
        if (!start) return;
        if (!end) return;
        if (!this.isValidInputElement(element)) return;
        
        element.focus();
        element.setSelectionRange(start, end);
    }
}
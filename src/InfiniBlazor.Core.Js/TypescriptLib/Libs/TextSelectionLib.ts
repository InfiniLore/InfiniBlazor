// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
import {IInputElement} from "../Contracts/IInputElement";
import {CSharpTuple} from "../Contracts/CSharpTuple";

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// noinspection JSUnusedGlobalSymbols
export class TextSelectionLib {
    private isValidInputElement(element: IInputElement): boolean {
        if (!element) return false;
        return element.tagName.toLowerCase() === "input" || element.tagName.toLowerCase() === "textarea";
    }

    public getStartIndex(element: IInputElement): number {
        if (!this.isValidInputElement(element)) return -1;
        return element.selectionStart || 0;
    }

    public getEndIndex(element: IInputElement): number {
        if (!this.isValidInputElement(element)) return -1;
        return element.selectionEnd || 0;
    }

    public getAsTuple(element: IInputElement): CSharpTuple<number, number> {
        if (!this.isValidInputElement(element)) return {
            Item1: -1,
            Item2: -1
        }
        return {
            Item1: element.selectionStart || 0,
            Item2: element.selectionEnd || 0
        };
    }

    public setRange(element: IInputElement, start: number, end: number): void {
        if (!this.isValidInputElement(element)) {
            console.warn("invalid element")
            return;
        }
        
        if (!Number.isInteger(start) || start < 0) return;
        if (!Number.isInteger(end) || end < 0) return;
        if (start > end) return;
        
        element.focus();
        element.setSelectionRange(start, end);
    }
}
// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
import { CSharpTuple } from "./Contracts/CSharpTuple";
import { IInputElement } from "./Contracts/IInputElement";
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
function checkInputElement(element: IInputElement): boolean {
    if (!element) return false;
    return element.tagName.toLowerCase() === "input" || element.tagName.toLowerCase() === "textarea";
}

export function getInputSelectionStart(element: IInputElement): number {
    if (!checkInputElement(element)) return -1;
    return element.selectionStart || 0;
}

export function getInputSelectionEnd(element: IInputElement): number {
    if (!checkInputElement(element)) return -1;
    return element.selectionEnd || 0;
}

export function getInputSelection(element: IInputElement): CSharpTuple<number, number> {
    if (!checkInputElement(element)) return {
        Item1: -1,
        Item2: -1
    }
    return {
        Item1: element.selectionStart || 0,
        Item2: element.selectionEnd || 0
    };
}

export function setInputSelectionRange(element: IInputElement, start: number, end: number): void {
    if (!checkInputElement(element)) return;
    element.focus();
    element.setSelectionRange(start, end);
}
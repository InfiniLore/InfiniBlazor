// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
import {IInputElement} from "./Contracts";
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
function getSelection(element: IInputElement, callback: (element: IInputElement) => number): number {
    if (element.tagName.toLowerCase() !== "input" && element.tagName.toLowerCase() !== "textarea") return -1;
    return callback(element) ?? -1;
}

export function getSelectionStart(element: IInputElement): number {
    if (!element) return -1;
    return getSelection(element, (el) => el.selectionStart || 0);
}

export function getSelectionEnd(element: IInputElement): number {
    if (!element) return -1;
    return getSelection(element, (el) => el.selectionEnd || 0);
}

export function setSelectionRange(element: IInputElement, start: number, end: number): void {
    if (!element) return;
    element.focus();
    element.setSelectionRange(start, end);
}
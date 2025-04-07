import {getSelection} from "./selection";

export {};
declare global {
    // noinspection JSUnusedGlobalSymbols
    interface Window {
        getSelectionStart: (element: HTMLInputElement | HTMLTextAreaElement) => number;
        getSelectionEnd: (element: HTMLInputElement | HTMLTextAreaElement) => number;
        preventDefault: (event: Event) => void;
        setSelectionRange: (element: HTMLInputElement | HTMLTextAreaElement, start: number, end: number) => void;
    }
}

window.getSelectionStart = (element: HTMLInputElement | HTMLTextAreaElement): number =>
    getSelection(element, (el) => el.selectionStart || 0);

window.getSelectionEnd = (element: HTMLInputElement | HTMLTextAreaElement): number =>
    getSelection(element, (el) => el.selectionEnd || 0);

window.preventDefault = (event: Event): void => {
    event.preventDefault();
};

window.setSelectionRange = (element: HTMLInputElement | HTMLTextAreaElement, start: number, end: number): void => {
    if (!element) return;
    element.focus();
    element.setSelectionRange(start, end);
};

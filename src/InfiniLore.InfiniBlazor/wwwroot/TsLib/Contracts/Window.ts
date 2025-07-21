// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
import {IInputElement} from "./IInputElement";
import {CSharpTuple} from "./CSharpTuple";
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
declare global {
    // noinspection JSUnusedGlobalSymbols
    interface Window {
        getInputSelectionStart: (element: IInputElement) => number;
        getInputSelectionEnd: (element: IInputElement) => number;
        getInputSelection: (element: IInputElement) => CSharpTuple<number, number>;

        setInputSelectionRange: (element: IInputElement, start: number, end: number) => void;

        addPreventDefaultListener: () => void;
        removePreventDefaultListener: () => void;

        setTextContent: (element: HTMLElement, text: string) => void;
        getTextContent: (element: HTMLElement) => string;

        addOrUpdateStyleElementAtHead : (idName: string, css: string) => void;

        getBoundingClientRect : (element: Element) => DOMRect | null;

        addHorizontalScroll: (element: Element, i: number) => void;
    }
}
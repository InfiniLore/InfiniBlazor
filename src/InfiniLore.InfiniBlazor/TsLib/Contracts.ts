// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
export type IInputElement = HTMLInputElement | HTMLTextAreaElement
export type CSharpTuple<T1, T2> = { "Item1": T1, "Item2": T2 };

export type KeyCondition = (event: KeyboardEvent, key: string) => boolean;

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
    }
}
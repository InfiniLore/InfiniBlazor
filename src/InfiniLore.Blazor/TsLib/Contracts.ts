// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
export type IInputElement = HTMLInputElement | HTMLTextAreaElement

declare global {
    // noinspection JSUnusedGlobalSymbols
    interface Window {
        getSelectionStart: (element: IInputElement) => number;
        getSelectionEnd: (element: IInputElement) => number;
        setSelectionRange: (element: IInputElement, start: number, end: number) => void;

        addPreventDefaultListener: () => void;
        removePreventDefaultListener: () => void;
    }
}
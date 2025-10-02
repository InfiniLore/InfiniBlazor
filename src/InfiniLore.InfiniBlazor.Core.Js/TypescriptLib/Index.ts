// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
// noinspection JSUnusedGlobalSymbols

import {ElementLib} from "./Libs/ElementLib";
import {DocumentLib} from "./Libs/DocumentLib";
import {TextSelectionLib} from "./Libs/TextSelectionLib";
import {KeyListenerLib} from "./Libs/KeyListenerLib";
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
export class InfiniBlazor {
    public document : DocumentLib = new DocumentLib();
    public elements : ElementLib = new ElementLib();
    public textSelection : TextSelectionLib = new TextSelectionLib();
    public keyListener : KeyListenerLib = new KeyListenerLib();
}

export const infiniBlazor = new InfiniBlazor();
export default infiniBlazor;

declare global {
    interface Window {
        infiniBlazor: InfiniBlazor;
    }
}

if (typeof window !== 'undefined') {
    window.infiniBlazor = infiniBlazor;
}
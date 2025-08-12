// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
// noinspection JSUnusedGlobalSymbols

import {ElementLib} from "./Libs/ElementLib";
import {DocumentLib} from "./Libs/DocumentLib";
import {InputElementLib} from "./Libs/InputElementLib";
import {KeyListenerLib} from "./Libs/KeyDownListener";
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// noinspection JSUnusedGlobalSymbols
export class InfiniBlazor {
    public document : DocumentLib = new DocumentLib();
    public element : ElementLib = new ElementLib();
    public inputElement : InputElementLib = new InputElementLib();
    public keyListener : KeyListenerLib = new KeyListenerLib();
}

// noinspection JSUnusedGlobalSymbols
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
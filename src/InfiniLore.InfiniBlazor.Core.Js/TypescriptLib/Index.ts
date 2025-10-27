// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
// noinspection JSUnusedGlobalSymbols

import ElementLib from "./Libs/ElementLib";
import {DocumentLib} from "./Libs/DocumentLib";
import {TextSelectionLib} from "./Libs/TextSelectionLib";
import {KeyListenerLib} from "./Libs/KeyListenerLib";
import {HighlightLib} from "./Libs/HighlightLib";
import {MermaidLib} from "./Libs/MermaidLib";
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
export class InfiniBlazor {
    public document : DocumentLib = new DocumentLib();
    public elements : ElementLib = new ElementLib();
    public textSelection : TextSelectionLib = new TextSelectionLib();
    public keyListener : KeyListenerLib = new KeyListenerLib();
    public highlight : HighlightLib = new HighlightLib();
    public mermaid: MermaidLib = new MermaidLib();
}

export const infiniBlazor = new InfiniBlazor();
export default infiniBlazor;

declare global {
    interface Window {
        infiniBlazor: InfiniBlazor;
        hljs?: {
            highlightAll(): void;
            highlightElement(element: HTMLElement): void;
            configure(options: any): void;
            listLanguages(): string[];
            getLanguage(name: string): any;
        }
        mermaid?: {
            initialize(config: any): void;
        }
    }
}


if (typeof window !== 'undefined') {
    window.infiniBlazor = infiniBlazor;
    
    if (infiniBlazor.mermaid.isMermaidJsAvailable()) {
        window.mermaid?.initialize({startOnLoad: true});
    }
}
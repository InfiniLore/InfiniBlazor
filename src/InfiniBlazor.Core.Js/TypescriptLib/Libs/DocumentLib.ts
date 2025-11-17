// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
// noinspection JSUnusedGlobalSymbols
export class DocumentLib {
    
    // noinspection JSUnusedGlobalSymbols
    public addOrUpdateElementAtHead(elementId: string, textContent: string): void {
        if (!elementId) return;
        if (!textContent) return;
        
        const possibleElement = document.head.querySelector(`#${elementId}`);
        if (possibleElement && possibleElement instanceof HTMLStyleElement) {
            possibleElement.textContent = textContent;
            return;
        } else if (possibleElement) {
            document.head.removeChild(possibleElement);
        }

        const styleElement = document.createElement("style");
        styleElement.id = elementId;
        styleElement.textContent = textContent;
        document.head.appendChild(styleElement);
    }    
}

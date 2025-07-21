// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
export function setTextContent(element: HTMLElement, text: string): void {
    if (!element) return;
    if (!text) return;
    element.textContent = text;
}

export function getTextContent(element: HTMLElement): string {
    if (!element) return "";
    if (!element.textContent) return "";
    return element.textContent;
}
// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
export function getBoundingClientRect(element: Element): DOMRect | null {
    if (!element) return null;
    return element.getBoundingClientRect();
}

export function addHorizontalScroll(element: Element, i: number) : void {
    element.scrollBy({ left: i, behavior: 'smooth' });
}

export function clickElementById(elementId:string) : void {
    const element = document.getElementById(elementId);
    if (element === null) return;
    element.click();
}
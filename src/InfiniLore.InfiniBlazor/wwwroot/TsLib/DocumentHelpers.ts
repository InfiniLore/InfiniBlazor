// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
export function addOrUpdateStyleElementAtHead(idName: string, css: string): void {
    if (!idName) return;
    if (!css) return;
    
    const head = document.head;

    const possibleStyle = head.querySelector(`#${idName}`);
    if (possibleStyle && possibleStyle instanceof HTMLStyleElement) {
        possibleStyle.textContent = css;  // Using textContent instead of innerHTML
        return;
    } else if (possibleStyle) {
        head.removeChild(possibleStyle);
    }

    const style = document.createElement("style");
    style.id = idName;
    style.textContent = css;  // Using textContent instead of innerHTML
    head.appendChild(style);
}

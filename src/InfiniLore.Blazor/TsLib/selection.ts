export const getSelection: (element: (HTMLInputElement | HTMLTextAreaElement), callback: (element: (HTMLInputElement | HTMLTextAreaElement)) => (number | null)) => number = (
    element: HTMLInputElement | HTMLTextAreaElement,
    callback: (element: HTMLInputElement | HTMLTextAreaElement) => number | null
): number => {
    if (!element || (element.tagName.toLowerCase() !== "input" && element.tagName.toLowerCase() !== "textarea")) {
        console.error("Element must be an input or textarea.");
        return -1;
    }
    return callback(element) ?? -1;
};

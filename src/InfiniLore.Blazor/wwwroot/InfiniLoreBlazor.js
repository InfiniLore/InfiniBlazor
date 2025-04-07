window.getSelectionStart = (element) => _getSelection(
    element,
    (element) => element.selectionStart
)

window.getSelectionEnd = (element) => _getSelection(
    element,
    (element) => element.selectionEnd
);

const _getSelection = (element, callback) => {
    if (!(element && (element.tagName.toLowerCase() === "input" || element.tagName.toLowerCase() === "textarea"))) {
        console.error("Element must be an input or textarea.");
        return -1;
    } 
    return callback(element);
}

window.preventDefault = (event) => event.preventDefault();

window.setSelectionRangeBlazor = (element, start, end) => {
    if (!element) return;
    element.focus();
    element.setSelectionRange(start, end);
};

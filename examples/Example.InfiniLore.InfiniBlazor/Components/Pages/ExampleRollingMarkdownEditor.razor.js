window.getInnerText = (element) => element.innerText;
window.focusElement = (element) => {
    element.focus();
    const range = document.createRange();
    const selection = window.getSelection();
    range.selectNodeContents(element);
    range.collapse(false);
    selection.removeAllRanges();
    selection.addRange(range);
}
window.setElementText = (element, text) => {
    element.innerText = text;
    window.focusElement(element);
}
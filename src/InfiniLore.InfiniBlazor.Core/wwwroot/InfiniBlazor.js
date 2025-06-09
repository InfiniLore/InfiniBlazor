/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/InfiniLore.InfiniBlazor/TsLib/DocumentHelpers.ts":
/*!**************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/TsLib/DocumentHelpers.ts ***!
  \**************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.addOrUpdateStyleElementAtHead = addOrUpdateStyleElementAtHead;
function addOrUpdateStyleElementAtHead(idName, css) {
    if (!idName)
        return;
    if (!css)
        return;
    const head = document.head;
    const possibleStyle = head.querySelector(`#${idName}`);
    if (possibleStyle && possibleStyle instanceof HTMLStyleElement) {
        possibleStyle.textContent = css;
        return;
    }
    else if (possibleStyle) {
        head.removeChild(possibleStyle);
    }
    const style = document.createElement("style");
    style.id = idName;
    style.textContent = css;
    head.appendChild(style);
}


/***/ }),

/***/ "./src/InfiniLore.InfiniBlazor/TsLib/HtmlElementHelpers.ts":
/*!*****************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/TsLib/HtmlElementHelpers.ts ***!
  \*****************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.setTextContent = setTextContent;
exports.getTextContent = getTextContent;
function setTextContent(element, text) {
    if (!element)
        return;
    if (!text)
        return;
    element.textContent = text;
}
function getTextContent(element) {
    if (!element)
        return "";
    if (!element.textContent)
        return "";
    return element.textContent;
}


/***/ }),

/***/ "./src/InfiniLore.InfiniBlazor/TsLib/InputSelection.ts":
/*!*************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/TsLib/InputSelection.ts ***!
  \*************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.getInputSelectionStart = getInputSelectionStart;
exports.getInputSelectionEnd = getInputSelectionEnd;
exports.getInputSelection = getInputSelection;
exports.setInputSelectionRange = setInputSelectionRange;
function checkInputElement(element) {
    if (!element)
        return false;
    return element.tagName.toLowerCase() === "input" || element.tagName.toLowerCase() === "textarea";
}
function getInputSelectionStart(element) {
    if (!checkInputElement(element))
        return -1;
    return element.selectionStart || 0;
}
function getInputSelectionEnd(element) {
    if (!checkInputElement(element))
        return -1;
    return element.selectionEnd || 0;
}
function getInputSelection(element) {
    if (!checkInputElement(element))
        return {
            Item1: -1,
            Item2: -1
        };
    return {
        Item1: element.selectionStart || 0,
        Item2: element.selectionEnd || 0
    };
}
function setInputSelectionRange(element, start, end) {
    if (!checkInputElement(element))
        return;
    element.focus();
    element.setSelectionRange(start, end);
}


/***/ }),

/***/ "./src/InfiniLore.InfiniBlazor/TsLib/KeyDownListener.ts":
/*!**************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/TsLib/KeyDownListener.ts ***!
  \**************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.addPreventDefaultListener = addPreventDefaultListener;
exports.removePreventDefaultListener = removePreventDefaultListener;
const keysToSkip = new Set(["u", "b", "i", "a"]);
const allowSpecialConditions = [
    (event, key) => event.ctrlKey && event.shiftKey && key === "i",
];
function preventKeyDefault(event) {
    if (!event)
        return;
    if (!event.ctrlKey)
        return;
    const key = event.key.toLowerCase();
    if (allowSpecialConditions.some(condition => condition(event, key)))
        return;
    if (!keysToSkip.has(key))
        return;
    event.preventDefault();
}
function addPreventDefaultListener() {
    document.addEventListener("keydown", preventKeyDefault);
}
function removePreventDefaultListener() {
    document.removeEventListener("keydown", preventKeyDefault);
}


/***/ })

/******/ 	});
/************************************************************************/
/******/ 	// The module cache
/******/ 	var __webpack_module_cache__ = {};
/******/ 	
/******/ 	// The require function
/******/ 	function __webpack_require__(moduleId) {
/******/ 		// Check if module is in cache
/******/ 		var cachedModule = __webpack_module_cache__[moduleId];
/******/ 		if (cachedModule !== undefined) {
/******/ 			return cachedModule.exports;
/******/ 		}
/******/ 		// Create a new module (and put it into the cache)
/******/ 		var module = __webpack_module_cache__[moduleId] = {
/******/ 			// no module.id needed
/******/ 			// no module.loaded needed
/******/ 			exports: {}
/******/ 		};
/******/ 	
/******/ 		// Execute the module function
/******/ 		__webpack_modules__[moduleId](module, module.exports, __webpack_require__);
/******/ 	
/******/ 		// Return the exports of the module
/******/ 		return module.exports;
/******/ 	}
/******/ 	
/************************************************************************/
var __webpack_exports__ = {};
// This entry needs to be wrapped in an IIFE because it needs to be isolated against other modules in the chunk.
(() => {
var exports = __webpack_exports__;
/*!****************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/TsLib/WindowDefinitions.ts ***!
  \****************************************************************/

Object.defineProperty(exports, "__esModule", ({ value: true }));
const InputSelection_1 = __webpack_require__(/*! ./InputSelection */ "./src/InfiniLore.InfiniBlazor/TsLib/InputSelection.ts");
const KeyDownListener_1 = __webpack_require__(/*! ./KeyDownListener */ "./src/InfiniLore.InfiniBlazor/TsLib/KeyDownListener.ts");
const HtmlElementHelpers_1 = __webpack_require__(/*! ./HtmlElementHelpers */ "./src/InfiniLore.InfiniBlazor/TsLib/HtmlElementHelpers.ts");
const DocumentHelpers_1 = __webpack_require__(/*! ./DocumentHelpers */ "./src/InfiniLore.InfiniBlazor/TsLib/DocumentHelpers.ts");
window.getInputSelectionStart = InputSelection_1.getInputSelectionStart;
window.getInputSelectionEnd = InputSelection_1.getInputSelectionEnd;
window.getInputSelection = InputSelection_1.getInputSelection;
window.setInputSelectionRange = InputSelection_1.setInputSelectionRange;
window.addPreventDefaultListener = KeyDownListener_1.addPreventDefaultListener;
window.removePreventDefaultListener = KeyDownListener_1.removePreventDefaultListener;
window.setTextContent = HtmlElementHelpers_1.setTextContent;
window.getTextContent = HtmlElementHelpers_1.getTextContent;
window.addOrUpdateStyleElementAtHead = DocumentHelpers_1.addOrUpdateStyleElementAtHead;

})();

/******/ })()
;
//# sourceMappingURL=data:application/json;charset=utf-8;base64,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QUFDO0FBQzlELENBQUM7Ozs7Ozs7VUNoQ0Q7VUFDQTs7VUFFQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTs7VUFFQTtVQUNBOztVQUVBO1VBQ0E7VUFDQTs7Ozs7Ozs7Ozs7O0FDbkJBLDhIQUF5SDtBQUN6SCxpSUFBMEY7QUFDMUYsMElBQW9FO0FBQ3BFLGlJQUFnRTtBQUloRSxNQUFNLENBQUMsc0JBQXNCLEdBQUcsdUNBQXNCLENBQUM7QUFDdkQsTUFBTSxDQUFDLG9CQUFvQixHQUFHLHFDQUFvQixDQUFDO0FBQ25ELE1BQU0sQ0FBQyxpQkFBaUIsR0FBRyxrQ0FBaUIsQ0FBQztBQUM3QyxNQUFNLENBQUMsc0JBQXNCLEdBQUcsdUNBQXNCLENBQUM7QUFFdkQsTUFBTSxDQUFDLHlCQUF5QixHQUFHLDJDQUF5QixDQUFDO0FBQzdELE1BQU0sQ0FBQyw0QkFBNEIsR0FBRyw4Q0FBNEIsQ0FBQztBQUVuRSxNQUFNLENBQUMsY0FBYyxHQUFHLG1DQUFjLENBQUM7QUFDdkMsTUFBTSxDQUFDLGNBQWMsR0FBRyxtQ0FBYyxDQUFDO0FBRXZDLE1BQU0sQ0FBQyw2QkFBNkIsR0FBRywrQ0FBNkIsQ0FBQyIsInNvdXJjZXMiOlsid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3IvVHNMaWIvRG9jdW1lbnRIZWxwZXJzLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3IvVHNMaWIvSHRtbEVsZW1lbnRIZWxwZXJzLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3IvVHNMaWIvSW5wdXRTZWxlY3Rpb24udHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci9Uc0xpYi9LZXlEb3duTGlzdGVuZXIudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2Jvb3RzdHJhcCIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yL1RzTGliL1dpbmRvd0RlZmluaXRpb25zLnRzIl0sInNvdXJjZXNDb250ZW50IjpbIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmV4cG9ydCBmdW5jdGlvbiBhZGRPclVwZGF0ZVN0eWxlRWxlbWVudEF0SGVhZChpZE5hbWU6IHN0cmluZywgY3NzOiBzdHJpbmcpOiB2b2lkIHtcclxuICAgIGlmICghaWROYW1lKSByZXR1cm47XHJcbiAgICBpZiAoIWNzcykgcmV0dXJuO1xyXG4gICAgXHJcbiAgICBjb25zdCBoZWFkID0gZG9jdW1lbnQuaGVhZDtcclxuXHJcbiAgICBjb25zdCBwb3NzaWJsZVN0eWxlID0gaGVhZC5xdWVyeVNlbGVjdG9yKGAjJHtpZE5hbWV9YCk7XHJcbiAgICBpZiAocG9zc2libGVTdHlsZSAmJiBwb3NzaWJsZVN0eWxlIGluc3RhbmNlb2YgSFRNTFN0eWxlRWxlbWVudCkge1xyXG4gICAgICAgIHBvc3NpYmxlU3R5bGUudGV4dENvbnRlbnQgPSBjc3M7ICAvLyBVc2luZyB0ZXh0Q29udGVudCBpbnN0ZWFkIG9mIGlubmVySFRNTFxyXG4gICAgICAgIHJldHVybjtcclxuICAgIH0gZWxzZSBpZiAocG9zc2libGVTdHlsZSkge1xyXG4gICAgICAgIGhlYWQucmVtb3ZlQ2hpbGQocG9zc2libGVTdHlsZSk7XHJcbiAgICB9XHJcblxyXG4gICAgY29uc3Qgc3R5bGUgPSBkb2N1bWVudC5jcmVhdGVFbGVtZW50KFwic3R5bGVcIik7XHJcbiAgICBzdHlsZS5pZCA9IGlkTmFtZTtcclxuICAgIHN0eWxlLnRleHRDb250ZW50ID0gY3NzOyAgLy8gVXNpbmcgdGV4dENvbnRlbnQgaW5zdGVhZCBvZiBpbm5lckhUTUxcclxuICAgIGhlYWQuYXBwZW5kQ2hpbGQoc3R5bGUpO1xyXG59XHJcbiIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmV4cG9ydCBmdW5jdGlvbiBzZXRUZXh0Q29udGVudChlbGVtZW50OiBIVE1MRWxlbWVudCwgdGV4dDogc3RyaW5nKTogdm9pZCB7XHJcbiAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgIGlmICghdGV4dCkgcmV0dXJuO1xyXG4gICAgZWxlbWVudC50ZXh0Q29udGVudCA9IHRleHQ7XHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBnZXRUZXh0Q29udGVudChlbGVtZW50OiBIVE1MRWxlbWVudCk6IHN0cmluZyB7XHJcbiAgICBpZiAoIWVsZW1lbnQpIHJldHVybiBcIlwiO1xyXG4gICAgaWYgKCFlbGVtZW50LnRleHRDb250ZW50KSByZXR1cm4gXCJcIjtcclxuICAgIHJldHVybiBlbGVtZW50LnRleHRDb250ZW50O1xyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmltcG9ydCB7IENTaGFycFR1cGxlIH0gZnJvbSBcIi4vQ29udHJhY3RzL0NTaGFycFR1cGxlXCI7XHJcbmltcG9ydCB7IElJbnB1dEVsZW1lbnQgfSBmcm9tIFwiLi9Db250cmFjdHMvSUlucHV0RWxlbWVudFwiO1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuZnVuY3Rpb24gY2hlY2tJbnB1dEVsZW1lbnQoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IGJvb2xlYW4ge1xyXG4gICAgaWYgKCFlbGVtZW50KSByZXR1cm4gZmFsc2U7XHJcbiAgICByZXR1cm4gZWxlbWVudC50YWdOYW1lLnRvTG93ZXJDYXNlKCkgPT09IFwiaW5wdXRcIiB8fCBlbGVtZW50LnRhZ05hbWUudG9Mb3dlckNhc2UoKSA9PT0gXCJ0ZXh0YXJlYVwiO1xyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gZ2V0SW5wdXRTZWxlY3Rpb25TdGFydChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogbnVtYmVyIHtcclxuICAgIGlmICghY2hlY2tJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybiAtMTtcclxuICAgIHJldHVybiBlbGVtZW50LnNlbGVjdGlvblN0YXJ0IHx8IDA7XHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBnZXRJbnB1dFNlbGVjdGlvbkVuZChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogbnVtYmVyIHtcclxuICAgIGlmICghY2hlY2tJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybiAtMTtcclxuICAgIHJldHVybiBlbGVtZW50LnNlbGVjdGlvbkVuZCB8fCAwO1xyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gZ2V0SW5wdXRTZWxlY3Rpb24oZWxlbWVudDogSUlucHV0RWxlbWVudCk6IENTaGFycFR1cGxlPG51bWJlciwgbnVtYmVyPiB7XHJcbiAgICBpZiAoIWNoZWNrSW5wdXRFbGVtZW50KGVsZW1lbnQpKSByZXR1cm4ge1xyXG4gICAgICAgIEl0ZW0xOiAtMSxcclxuICAgICAgICBJdGVtMjogLTFcclxuICAgIH1cclxuICAgIHJldHVybiB7XHJcbiAgICAgICAgSXRlbTE6IGVsZW1lbnQuc2VsZWN0aW9uU3RhcnQgfHwgMCxcclxuICAgICAgICBJdGVtMjogZWxlbWVudC5zZWxlY3Rpb25FbmQgfHwgMFxyXG4gICAgfTtcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIHNldElucHV0U2VsZWN0aW9uUmFuZ2UoZWxlbWVudDogSUlucHV0RWxlbWVudCwgc3RhcnQ6IG51bWJlciwgZW5kOiBudW1iZXIpOiB2b2lkIHtcclxuICAgIGlmICghY2hlY2tJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybjtcclxuICAgIGVsZW1lbnQuZm9jdXMoKTtcclxuICAgIGVsZW1lbnQuc2V0U2VsZWN0aW9uUmFuZ2Uoc3RhcnQsIGVuZCk7XHJcbn0iLCIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuaW1wb3J0IHtLZXlDb25kaXRpb259IGZyb20gXCIuL0NvbnRyYWN0cy9LZXlDb25kaXRpb25cIjtcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmNvbnN0IGtleXNUb1NraXA6IFNldDxzdHJpbmc+ID0gbmV3IFNldChbXCJ1XCIsIFwiYlwiLCBcImlcIiwgXCJhXCJdKTtcclxuY29uc3QgYWxsb3dTcGVjaWFsQ29uZGl0aW9uczogS2V5Q29uZGl0aW9uW10gPSBbXHJcbiAgICAoZXZlbnQsIGtleSkgPT4gZXZlbnQuY3RybEtleSAmJiBldmVudC5zaGlmdEtleSAmJiBrZXkgPT09IFwiaVwiLCAvLyBTa2lwIGBDdHJsK1NoaWZ0K0lgXHJcbl07XHJcblxyXG4vLyBNYWluIGZ1bmN0aW9uIHRvIHByZXZlbnQgZGVmYXVsdCBiZWhhdmlvclxyXG5mdW5jdGlvbiBwcmV2ZW50S2V5RGVmYXVsdChldmVudDogS2V5Ym9hcmRFdmVudCk6IHZvaWQge1xyXG4gICAgaWYgKCFldmVudCkgcmV0dXJuO1xyXG4gICAgaWYgKCFldmVudC5jdHJsS2V5KSByZXR1cm47XHJcblxyXG4gICAgY29uc3Qga2V5ID0gZXZlbnQua2V5LnRvTG93ZXJDYXNlKCk7XHJcbiAgICBpZiAoYWxsb3dTcGVjaWFsQ29uZGl0aW9ucy5zb21lKGNvbmRpdGlvbiA9PiBjb25kaXRpb24oZXZlbnQsIGtleSkpKSByZXR1cm47XHJcblxyXG4gICAgLy8gQmxvY2sgZGVmYXVsdCBiZWhhdmlvciBmb3Iga2V5cyBpbiB0aGUga2V5c1RvU2tpcCBzZXRcclxuICAgIGlmICgha2V5c1RvU2tpcC5oYXMoa2V5KSkgcmV0dXJuO1xyXG4gICAgZXZlbnQucHJldmVudERlZmF1bHQoKTtcclxufVxyXG5cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBhZGRQcmV2ZW50RGVmYXVsdExpc3RlbmVyKCkgOiB2b2lkIHtcclxuICAgIGRvY3VtZW50LmFkZEV2ZW50TGlzdGVuZXIoXCJrZXlkb3duXCIscHJldmVudEtleURlZmF1bHQpXHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiByZW1vdmVQcmV2ZW50RGVmYXVsdExpc3RlbmVyKCkgOiB2b2lkIHtcclxuICAgIGRvY3VtZW50LnJlbW92ZUV2ZW50TGlzdGVuZXIoXCJrZXlkb3duXCIsIHByZXZlbnRLZXlEZWZhdWx0KVxyXG59IiwiLy8gVGhlIG1vZHVsZSBjYWNoZVxudmFyIF9fd2VicGFja19tb2R1bGVfY2FjaGVfXyA9IHt9O1xuXG4vLyBUaGUgcmVxdWlyZSBmdW5jdGlvblxuZnVuY3Rpb24gX193ZWJwYWNrX3JlcXVpcmVfXyhtb2R1bGVJZCkge1xuXHQvLyBDaGVjayBpZiBtb2R1bGUgaXMgaW4gY2FjaGVcblx0dmFyIGNhY2hlZE1vZHVsZSA9IF9fd2VicGFja19tb2R1bGVfY2FjaGVfX1ttb2R1bGVJZF07XG5cdGlmIChjYWNoZWRNb2R1bGUgIT09IHVuZGVmaW5lZCkge1xuXHRcdHJldHVybiBjYWNoZWRNb2R1bGUuZXhwb3J0cztcblx0fVxuXHQvLyBDcmVhdGUgYSBuZXcgbW9kdWxlIChhbmQgcHV0IGl0IGludG8gdGhlIGNhY2hlKVxuXHR2YXIgbW9kdWxlID0gX193ZWJwYWNrX21vZHVsZV9jYWNoZV9fW21vZHVsZUlkXSA9IHtcblx0XHQvLyBubyBtb2R1bGUuaWQgbmVlZGVkXG5cdFx0Ly8gbm8gbW9kdWxlLmxvYWRlZCBuZWVkZWRcblx0XHRleHBvcnRzOiB7fVxuXHR9O1xuXG5cdC8vIEV4ZWN1dGUgdGhlIG1vZHVsZSBmdW5jdGlvblxuXHRfX3dlYnBhY2tfbW9kdWxlc19fW21vZHVsZUlkXShtb2R1bGUsIG1vZHVsZS5leHBvcnRzLCBfX3dlYnBhY2tfcmVxdWlyZV9fKTtcblxuXHQvLyBSZXR1cm4gdGhlIGV4cG9ydHMgb2YgdGhlIG1vZHVsZVxuXHRyZXR1cm4gbW9kdWxlLmV4cG9ydHM7XG59XG5cbiIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5pbXBvcnQge2dldElucHV0U2VsZWN0aW9uU3RhcnQsIGdldElucHV0U2VsZWN0aW9uRW5kLCBzZXRJbnB1dFNlbGVjdGlvblJhbmdlLCBnZXRJbnB1dFNlbGVjdGlvbn0gZnJvbSBcIi4vSW5wdXRTZWxlY3Rpb25cIjtcclxuaW1wb3J0IHthZGRQcmV2ZW50RGVmYXVsdExpc3RlbmVyLCByZW1vdmVQcmV2ZW50RGVmYXVsdExpc3RlbmVyfSBmcm9tIFwiLi9LZXlEb3duTGlzdGVuZXJcIjtcclxuaW1wb3J0IHtzZXRUZXh0Q29udGVudCwgZ2V0VGV4dENvbnRlbnR9IGZyb20gXCIuL0h0bWxFbGVtZW50SGVscGVyc1wiO1xyXG5pbXBvcnQge2FkZE9yVXBkYXRlU3R5bGVFbGVtZW50QXRIZWFkfSBmcm9tIFwiLi9Eb2N1bWVudEhlbHBlcnNcIjtcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbndpbmRvdy5nZXRJbnB1dFNlbGVjdGlvblN0YXJ0ID0gZ2V0SW5wdXRTZWxlY3Rpb25TdGFydDtcclxud2luZG93LmdldElucHV0U2VsZWN0aW9uRW5kID0gZ2V0SW5wdXRTZWxlY3Rpb25FbmQ7XHJcbndpbmRvdy5nZXRJbnB1dFNlbGVjdGlvbiA9IGdldElucHV0U2VsZWN0aW9uO1xyXG53aW5kb3cuc2V0SW5wdXRTZWxlY3Rpb25SYW5nZSA9IHNldElucHV0U2VsZWN0aW9uUmFuZ2U7XHJcblxyXG53aW5kb3cuYWRkUHJldmVudERlZmF1bHRMaXN0ZW5lciA9IGFkZFByZXZlbnREZWZhdWx0TGlzdGVuZXI7XHJcbndpbmRvdy5yZW1vdmVQcmV2ZW50RGVmYXVsdExpc3RlbmVyID0gcmVtb3ZlUHJldmVudERlZmF1bHRMaXN0ZW5lcjtcclxuXHJcbndpbmRvdy5zZXRUZXh0Q29udGVudCA9IHNldFRleHRDb250ZW50O1xyXG53aW5kb3cuZ2V0VGV4dENvbnRlbnQgPSBnZXRUZXh0Q29udGVudDtcclxuXHJcbndpbmRvdy5hZGRPclVwZGF0ZVN0eWxlRWxlbWVudEF0SGVhZCA9IGFkZE9yVXBkYXRlU3R5bGVFbGVtZW50QXRIZWFkOyJdLCJuYW1lcyI6W10sInNvdXJjZVJvb3QiOiIifQ==
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

/***/ "./src/InfiniLore.InfiniBlazor/TsLib/ElementHelpers.ts":
/*!*************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/TsLib/ElementHelpers.ts ***!
  \*************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.getBoundingClientRect = getBoundingClientRect;
exports.addHorizontalScroll = addHorizontalScroll;
function getBoundingClientRect(element) {
    if (!element)
        return null;
    return element.getBoundingClientRect();
}
function addHorizontalScroll(element, i) {
    element.scrollBy({ left: i, behavior: 'smooth' });
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
const ElementHelpers_1 = __webpack_require__(/*! ./ElementHelpers */ "./src/InfiniLore.InfiniBlazor/TsLib/ElementHelpers.ts");
window.getInputSelectionStart = InputSelection_1.getInputSelectionStart;
window.getInputSelectionEnd = InputSelection_1.getInputSelectionEnd;
window.getInputSelection = InputSelection_1.getInputSelection;
window.setInputSelectionRange = InputSelection_1.setInputSelectionRange;
window.addPreventDefaultListener = KeyDownListener_1.addPreventDefaultListener;
window.removePreventDefaultListener = KeyDownListener_1.removePreventDefaultListener;
window.setTextContent = HtmlElementHelpers_1.setTextContent;
window.getTextContent = HtmlElementHelpers_1.getTextContent;
window.addOrUpdateStyleElementAtHead = DocumentHelpers_1.addOrUpdateStyleElementAtHead;
window.getBoundingClientRect = ElementHelpers_1.getBoundingClientRect;
window.addHorizontalScroll = ElementHelpers_1.addHorizontalScroll;

})();

/******/ })()
;
//# sourceMappingURL=data:application/json;charset=utf-8;base64,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QixHQUFHLG9DQUFtQixDQUFDIiwic291cmNlcyI6WyJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci9Uc0xpYi9Eb2N1bWVudEhlbHBlcnMudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci9Uc0xpYi9FbGVtZW50SGVscGVycy50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yL1RzTGliL0h0bWxFbGVtZW50SGVscGVycy50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yL1RzTGliL0lucHV0U2VsZWN0aW9uLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3IvVHNMaWIvS2V5RG93bkxpc3RlbmVyLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvd2VicGFjay9ib290c3RyYXAiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci9Uc0xpYi9XaW5kb3dEZWZpbml0aW9ucy50cyJdLCJzb3VyY2VzQ29udGVudCI6WyIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5leHBvcnQgZnVuY3Rpb24gYWRkT3JVcGRhdGVTdHlsZUVsZW1lbnRBdEhlYWQoaWROYW1lOiBzdHJpbmcsIGNzczogc3RyaW5nKTogdm9pZCB7XHJcbiAgICBpZiAoIWlkTmFtZSkgcmV0dXJuO1xyXG4gICAgaWYgKCFjc3MpIHJldHVybjtcclxuICAgIFxyXG4gICAgY29uc3QgaGVhZCA9IGRvY3VtZW50LmhlYWQ7XHJcblxyXG4gICAgY29uc3QgcG9zc2libGVTdHlsZSA9IGhlYWQucXVlcnlTZWxlY3RvcihgIyR7aWROYW1lfWApO1xyXG4gICAgaWYgKHBvc3NpYmxlU3R5bGUgJiYgcG9zc2libGVTdHlsZSBpbnN0YW5jZW9mIEhUTUxTdHlsZUVsZW1lbnQpIHtcclxuICAgICAgICBwb3NzaWJsZVN0eWxlLnRleHRDb250ZW50ID0gY3NzOyAgLy8gVXNpbmcgdGV4dENvbnRlbnQgaW5zdGVhZCBvZiBpbm5lckhUTUxcclxuICAgICAgICByZXR1cm47XHJcbiAgICB9IGVsc2UgaWYgKHBvc3NpYmxlU3R5bGUpIHtcclxuICAgICAgICBoZWFkLnJlbW92ZUNoaWxkKHBvc3NpYmxlU3R5bGUpO1xyXG4gICAgfVxyXG5cclxuICAgIGNvbnN0IHN0eWxlID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudChcInN0eWxlXCIpO1xyXG4gICAgc3R5bGUuaWQgPSBpZE5hbWU7XHJcbiAgICBzdHlsZS50ZXh0Q29udGVudCA9IGNzczsgIC8vIFVzaW5nIHRleHRDb250ZW50IGluc3RlYWQgb2YgaW5uZXJIVE1MXHJcbiAgICBoZWFkLmFwcGVuZENoaWxkKHN0eWxlKTtcclxufVxyXG4iLCIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5leHBvcnQgZnVuY3Rpb24gZ2V0Qm91bmRpbmdDbGllbnRSZWN0KGVsZW1lbnQ6IEVsZW1lbnQpOiBET01SZWN0IHwgbnVsbCB7XHJcbiAgICBpZiAoIWVsZW1lbnQpIHJldHVybiBudWxsO1xyXG4gICAgcmV0dXJuIGVsZW1lbnQuZ2V0Qm91bmRpbmdDbGllbnRSZWN0KCk7XHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBhZGRIb3Jpem9udGFsU2Nyb2xsKGVsZW1lbnQ6IEVsZW1lbnQsIGk6IG51bWJlcikgOiB2b2lkIHtcclxuICAgIGVsZW1lbnQuc2Nyb2xsQnkoeyBsZWZ0OiBpLCBiZWhhdmlvcjogJ3Ntb290aCcgfSk7XHJcbn0iLCIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5leHBvcnQgZnVuY3Rpb24gc2V0VGV4dENvbnRlbnQoZWxlbWVudDogSFRNTEVsZW1lbnQsIHRleHQ6IHN0cmluZyk6IHZvaWQge1xyXG4gICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcbiAgICBpZiAoIXRleHQpIHJldHVybjtcclxuICAgIGVsZW1lbnQudGV4dENvbnRlbnQgPSB0ZXh0O1xyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gZ2V0VGV4dENvbnRlbnQoZWxlbWVudDogSFRNTEVsZW1lbnQpOiBzdHJpbmcge1xyXG4gICAgaWYgKCFlbGVtZW50KSByZXR1cm4gXCJcIjtcclxuICAgIGlmICghZWxlbWVudC50ZXh0Q29udGVudCkgcmV0dXJuIFwiXCI7XHJcbiAgICByZXR1cm4gZWxlbWVudC50ZXh0Q29udGVudDtcclxufSIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5pbXBvcnQgeyBDU2hhcnBUdXBsZSB9IGZyb20gXCIuL0NvbnRyYWN0cy9DU2hhcnBUdXBsZVwiO1xyXG5pbXBvcnQgeyBJSW5wdXRFbGVtZW50IH0gZnJvbSBcIi4vQ29udHJhY3RzL0lJbnB1dEVsZW1lbnRcIjtcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmZ1bmN0aW9uIGNoZWNrSW5wdXRFbGVtZW50KGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiBib29sZWFuIHtcclxuICAgIGlmICghZWxlbWVudCkgcmV0dXJuIGZhbHNlO1xyXG4gICAgcmV0dXJuIGVsZW1lbnQudGFnTmFtZS50b0xvd2VyQ2FzZSgpID09PSBcImlucHV0XCIgfHwgZWxlbWVudC50YWdOYW1lLnRvTG93ZXJDYXNlKCkgPT09IFwidGV4dGFyZWFcIjtcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIGdldElucHV0U2VsZWN0aW9uU3RhcnQoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IG51bWJlciB7XHJcbiAgICBpZiAoIWNoZWNrSW5wdXRFbGVtZW50KGVsZW1lbnQpKSByZXR1cm4gLTE7XHJcbiAgICByZXR1cm4gZWxlbWVudC5zZWxlY3Rpb25TdGFydCB8fCAwO1xyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gZ2V0SW5wdXRTZWxlY3Rpb25FbmQoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IG51bWJlciB7XHJcbiAgICBpZiAoIWNoZWNrSW5wdXRFbGVtZW50KGVsZW1lbnQpKSByZXR1cm4gLTE7XHJcbiAgICByZXR1cm4gZWxlbWVudC5zZWxlY3Rpb25FbmQgfHwgMDtcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIGdldElucHV0U2VsZWN0aW9uKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiBDU2hhcnBUdXBsZTxudW1iZXIsIG51bWJlcj4ge1xyXG4gICAgaWYgKCFjaGVja0lucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuIHtcclxuICAgICAgICBJdGVtMTogLTEsXHJcbiAgICAgICAgSXRlbTI6IC0xXHJcbiAgICB9XHJcbiAgICByZXR1cm4ge1xyXG4gICAgICAgIEl0ZW0xOiBlbGVtZW50LnNlbGVjdGlvblN0YXJ0IHx8IDAsXHJcbiAgICAgICAgSXRlbTI6IGVsZW1lbnQuc2VsZWN0aW9uRW5kIHx8IDBcclxuICAgIH07XHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBzZXRJbnB1dFNlbGVjdGlvblJhbmdlKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQsIHN0YXJ0OiBudW1iZXIsIGVuZDogbnVtYmVyKTogdm9pZCB7XHJcbiAgICBpZiAoIWNoZWNrSW5wdXRFbGVtZW50KGVsZW1lbnQpKSByZXR1cm47XHJcbiAgICBlbGVtZW50LmZvY3VzKCk7XHJcbiAgICBlbGVtZW50LnNldFNlbGVjdGlvblJhbmdlKHN0YXJ0LCBlbmQpO1xyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmltcG9ydCB7S2V5Q29uZGl0aW9ufSBmcm9tIFwiLi9Db250cmFjdHMvS2V5Q29uZGl0aW9uXCI7XHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5jb25zdCBrZXlzVG9Ta2lwOiBTZXQ8c3RyaW5nPiA9IG5ldyBTZXQoW1widVwiLCBcImJcIiwgXCJpXCIsIFwiYVwiXSk7XHJcbmNvbnN0IGFsbG93U3BlY2lhbENvbmRpdGlvbnM6IEtleUNvbmRpdGlvbltdID0gW1xyXG4gICAgKGV2ZW50LCBrZXkpID0+IGV2ZW50LmN0cmxLZXkgJiYgZXZlbnQuc2hpZnRLZXkgJiYga2V5ID09PSBcImlcIiwgLy8gU2tpcCBgQ3RybCtTaGlmdCtJYFxyXG5dO1xyXG5cclxuLy8gTWFpbiBmdW5jdGlvbiB0byBwcmV2ZW50IGRlZmF1bHQgYmVoYXZpb3JcclxuZnVuY3Rpb24gcHJldmVudEtleURlZmF1bHQoZXZlbnQ6IEtleWJvYXJkRXZlbnQpOiB2b2lkIHtcclxuICAgIGlmICghZXZlbnQpIHJldHVybjtcclxuICAgIGlmICghZXZlbnQuY3RybEtleSkgcmV0dXJuO1xyXG5cclxuICAgIGNvbnN0IGtleSA9IGV2ZW50LmtleS50b0xvd2VyQ2FzZSgpO1xyXG4gICAgaWYgKGFsbG93U3BlY2lhbENvbmRpdGlvbnMuc29tZShjb25kaXRpb24gPT4gY29uZGl0aW9uKGV2ZW50LCBrZXkpKSkgcmV0dXJuO1xyXG5cclxuICAgIC8vIEJsb2NrIGRlZmF1bHQgYmVoYXZpb3IgZm9yIGtleXMgaW4gdGhlIGtleXNUb1NraXAgc2V0XHJcbiAgICBpZiAoIWtleXNUb1NraXAuaGFzKGtleSkpIHJldHVybjtcclxuICAgIGV2ZW50LnByZXZlbnREZWZhdWx0KCk7XHJcbn1cclxuXHJcblxyXG5leHBvcnQgZnVuY3Rpb24gYWRkUHJldmVudERlZmF1bHRMaXN0ZW5lcigpIDogdm9pZCB7XHJcbiAgICBkb2N1bWVudC5hZGRFdmVudExpc3RlbmVyKFwia2V5ZG93blwiLHByZXZlbnRLZXlEZWZhdWx0KVxyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gcmVtb3ZlUHJldmVudERlZmF1bHRMaXN0ZW5lcigpIDogdm9pZCB7XHJcbiAgICBkb2N1bWVudC5yZW1vdmVFdmVudExpc3RlbmVyKFwia2V5ZG93blwiLCBwcmV2ZW50S2V5RGVmYXVsdClcclxufSIsIi8vIFRoZSBtb2R1bGUgY2FjaGVcbnZhciBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX18gPSB7fTtcblxuLy8gVGhlIHJlcXVpcmUgZnVuY3Rpb25cbmZ1bmN0aW9uIF9fd2VicGFja19yZXF1aXJlX18obW9kdWxlSWQpIHtcblx0Ly8gQ2hlY2sgaWYgbW9kdWxlIGlzIGluIGNhY2hlXG5cdHZhciBjYWNoZWRNb2R1bGUgPSBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX19bbW9kdWxlSWRdO1xuXHRpZiAoY2FjaGVkTW9kdWxlICE9PSB1bmRlZmluZWQpIHtcblx0XHRyZXR1cm4gY2FjaGVkTW9kdWxlLmV4cG9ydHM7XG5cdH1cblx0Ly8gQ3JlYXRlIGEgbmV3IG1vZHVsZSAoYW5kIHB1dCBpdCBpbnRvIHRoZSBjYWNoZSlcblx0dmFyIG1vZHVsZSA9IF9fd2VicGFja19tb2R1bGVfY2FjaGVfX1ttb2R1bGVJZF0gPSB7XG5cdFx0Ly8gbm8gbW9kdWxlLmlkIG5lZWRlZFxuXHRcdC8vIG5vIG1vZHVsZS5sb2FkZWQgbmVlZGVkXG5cdFx0ZXhwb3J0czoge31cblx0fTtcblxuXHQvLyBFeGVjdXRlIHRoZSBtb2R1bGUgZnVuY3Rpb25cblx0X193ZWJwYWNrX21vZHVsZXNfX1ttb2R1bGVJZF0obW9kdWxlLCBtb2R1bGUuZXhwb3J0cywgX193ZWJwYWNrX3JlcXVpcmVfXyk7XG5cblx0Ly8gUmV0dXJuIHRoZSBleHBvcnRzIG9mIHRoZSBtb2R1bGVcblx0cmV0dXJuIG1vZHVsZS5leHBvcnRzO1xufVxuXG4iLCIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuaW1wb3J0IHtnZXRJbnB1dFNlbGVjdGlvblN0YXJ0LCBnZXRJbnB1dFNlbGVjdGlvbkVuZCwgc2V0SW5wdXRTZWxlY3Rpb25SYW5nZSwgZ2V0SW5wdXRTZWxlY3Rpb259IGZyb20gXCIuL0lucHV0U2VsZWN0aW9uXCI7XHJcbmltcG9ydCB7YWRkUHJldmVudERlZmF1bHRMaXN0ZW5lciwgcmVtb3ZlUHJldmVudERlZmF1bHRMaXN0ZW5lcn0gZnJvbSBcIi4vS2V5RG93bkxpc3RlbmVyXCI7XHJcbmltcG9ydCB7c2V0VGV4dENvbnRlbnQsIGdldFRleHRDb250ZW50fSBmcm9tIFwiLi9IdG1sRWxlbWVudEhlbHBlcnNcIjtcclxuaW1wb3J0IHthZGRPclVwZGF0ZVN0eWxlRWxlbWVudEF0SGVhZH0gZnJvbSBcIi4vRG9jdW1lbnRIZWxwZXJzXCI7XHJcbmltcG9ydCB7YWRkSG9yaXpvbnRhbFNjcm9sbCwgZ2V0Qm91bmRpbmdDbGllbnRSZWN0fSBmcm9tIFwiLi9FbGVtZW50SGVscGVyc1wiO1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxud2luZG93LmdldElucHV0U2VsZWN0aW9uU3RhcnQgPSBnZXRJbnB1dFNlbGVjdGlvblN0YXJ0O1xyXG53aW5kb3cuZ2V0SW5wdXRTZWxlY3Rpb25FbmQgPSBnZXRJbnB1dFNlbGVjdGlvbkVuZDtcclxud2luZG93LmdldElucHV0U2VsZWN0aW9uID0gZ2V0SW5wdXRTZWxlY3Rpb247XHJcbndpbmRvdy5zZXRJbnB1dFNlbGVjdGlvblJhbmdlID0gc2V0SW5wdXRTZWxlY3Rpb25SYW5nZTtcclxuXHJcbndpbmRvdy5hZGRQcmV2ZW50RGVmYXVsdExpc3RlbmVyID0gYWRkUHJldmVudERlZmF1bHRMaXN0ZW5lcjtcclxud2luZG93LnJlbW92ZVByZXZlbnREZWZhdWx0TGlzdGVuZXIgPSByZW1vdmVQcmV2ZW50RGVmYXVsdExpc3RlbmVyO1xyXG5cclxud2luZG93LnNldFRleHRDb250ZW50ID0gc2V0VGV4dENvbnRlbnQ7XHJcbndpbmRvdy5nZXRUZXh0Q29udGVudCA9IGdldFRleHRDb250ZW50O1xyXG5cclxud2luZG93LmFkZE9yVXBkYXRlU3R5bGVFbGVtZW50QXRIZWFkID0gYWRkT3JVcGRhdGVTdHlsZUVsZW1lbnRBdEhlYWQ7XHJcblxyXG53aW5kb3cuZ2V0Qm91bmRpbmdDbGllbnRSZWN0ID0gZ2V0Qm91bmRpbmdDbGllbnRSZWN0O1xyXG53aW5kb3cuYWRkSG9yaXpvbnRhbFNjcm9sbCA9IGFkZEhvcml6b250YWxTY3JvbGw7Il0sIm5hbWVzIjpbXSwic291cmNlUm9vdCI6IiJ9
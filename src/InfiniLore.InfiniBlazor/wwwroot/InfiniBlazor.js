/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/DocumentHelpers.ts":
/*!**********************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/DocumentHelpers.ts ***!
  \**********************************************************************/
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

/***/ "./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/ElementHelpers.ts":
/*!*********************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/ElementHelpers.ts ***!
  \*********************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.getBoundingClientRect = getBoundingClientRect;
exports.addHorizontalScroll = addHorizontalScroll;
exports.clickElementById = clickElementById;
function getBoundingClientRect(element) {
    if (!element)
        return null;
    return element.getBoundingClientRect();
}
function addHorizontalScroll(element, i) {
    element.scrollBy({ left: i, behavior: 'smooth' });
}
function clickElementById(elementId) {
    const element = document.getElementById(elementId);
    if (element === null)
        return;
    element.click();
}


/***/ }),

/***/ "./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/HtmlElementHelpers.ts":
/*!*************************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/HtmlElementHelpers.ts ***!
  \*************************************************************************/
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

/***/ "./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/InputSelection.ts":
/*!*********************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/InputSelection.ts ***!
  \*********************************************************************/
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

/***/ "./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/KeyDownListener.ts":
/*!**********************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/KeyDownListener.ts ***!
  \**********************************************************************/
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
/*!************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/Index.ts ***!
  \************************************************************/

Object.defineProperty(exports, "__esModule", ({ value: true }));
const InputSelection_1 = __webpack_require__(/*! ./InputSelection */ "./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/InputSelection.ts");
const KeyDownListener_1 = __webpack_require__(/*! ./KeyDownListener */ "./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/KeyDownListener.ts");
const HtmlElementHelpers_1 = __webpack_require__(/*! ./HtmlElementHelpers */ "./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/HtmlElementHelpers.ts");
const DocumentHelpers_1 = __webpack_require__(/*! ./DocumentHelpers */ "./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/DocumentHelpers.ts");
const ElementHelpers_1 = __webpack_require__(/*! ./ElementHelpers */ "./src/InfiniLore.InfiniBlazor/wwwroot/TsLib/ElementHelpers.ts");
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
window.clickElementById = ElementHelpers_1.clickElementById;

})();

/******/ })()
;
//# sourceMappingURL=data:application/json;charset=utf-8;base64,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QUVuRSxNQUFNLENBQUMsY0FBYyxHQUFHLG1DQUFjLENBQUM7QUFDdkMsTUFBTSxDQUFDLGNBQWMsR0FBRyxtQ0FBYyxDQUFDO0FBRXZDLE1BQU0sQ0FBQyw2QkFBNkIsR0FBRywrQ0FBNkIsQ0FBQztBQUVyRSxNQUFNLENBQUMscUJBQXFCLEdBQUcsc0NBQXFCLENBQUM7QUFDckQsTUFBTSxDQUFDLG1CQUFtQixHQUFHLG9DQUFtQixDQUFDO0FBRWpELE1BQU0sQ0FBQyxnQkFBZ0IsR0FBRyxpQ0FBZ0IsQ0FBQyIsInNvdXJjZXMiOlsid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3Ivd3d3cm9vdC9Uc0xpYi9Eb2N1bWVudEhlbHBlcnMudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci93d3dyb290L1RzTGliL0VsZW1lbnRIZWxwZXJzLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3Ivd3d3cm9vdC9Uc0xpYi9IdG1sRWxlbWVudEhlbHBlcnMudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci93d3dyb290L1RzTGliL0lucHV0U2VsZWN0aW9uLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3Ivd3d3cm9vdC9Uc0xpYi9LZXlEb3duTGlzdGVuZXIudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2Jvb3RzdHJhcCIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yL3d3d3Jvb3QvVHNMaWIvSW5kZXgudHMiXSwic291cmNlc0NvbnRlbnQiOlsiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcblxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuZXhwb3J0IGZ1bmN0aW9uIGFkZE9yVXBkYXRlU3R5bGVFbGVtZW50QXRIZWFkKGlkTmFtZTogc3RyaW5nLCBjc3M6IHN0cmluZyk6IHZvaWQge1xyXG4gICAgaWYgKCFpZE5hbWUpIHJldHVybjtcclxuICAgIGlmICghY3NzKSByZXR1cm47XHJcbiAgICBcclxuICAgIGNvbnN0IGhlYWQgPSBkb2N1bWVudC5oZWFkO1xyXG5cclxuICAgIGNvbnN0IHBvc3NpYmxlU3R5bGUgPSBoZWFkLnF1ZXJ5U2VsZWN0b3IoYCMke2lkTmFtZX1gKTtcclxuICAgIGlmIChwb3NzaWJsZVN0eWxlICYmIHBvc3NpYmxlU3R5bGUgaW5zdGFuY2VvZiBIVE1MU3R5bGVFbGVtZW50KSB7XHJcbiAgICAgICAgcG9zc2libGVTdHlsZS50ZXh0Q29udGVudCA9IGNzczsgIC8vIFVzaW5nIHRleHRDb250ZW50IGluc3RlYWQgb2YgaW5uZXJIVE1MXHJcbiAgICAgICAgcmV0dXJuO1xyXG4gICAgfSBlbHNlIGlmIChwb3NzaWJsZVN0eWxlKSB7XHJcbiAgICAgICAgaGVhZC5yZW1vdmVDaGlsZChwb3NzaWJsZVN0eWxlKTtcclxuICAgIH1cclxuXHJcbiAgICBjb25zdCBzdHlsZSA9IGRvY3VtZW50LmNyZWF0ZUVsZW1lbnQoXCJzdHlsZVwiKTtcclxuICAgIHN0eWxlLmlkID0gaWROYW1lO1xyXG4gICAgc3R5bGUudGV4dENvbnRlbnQgPSBjc3M7ICAvLyBVc2luZyB0ZXh0Q29udGVudCBpbnN0ZWFkIG9mIGlubmVySFRNTFxyXG4gICAgaGVhZC5hcHBlbmRDaGlsZChzdHlsZSk7XHJcbn1cclxuIiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcblxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuZXhwb3J0IGZ1bmN0aW9uIGdldEJvdW5kaW5nQ2xpZW50UmVjdChlbGVtZW50OiBFbGVtZW50KTogRE9NUmVjdCB8IG51bGwge1xyXG4gICAgaWYgKCFlbGVtZW50KSByZXR1cm4gbnVsbDtcclxuICAgIHJldHVybiBlbGVtZW50LmdldEJvdW5kaW5nQ2xpZW50UmVjdCgpO1xyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gYWRkSG9yaXpvbnRhbFNjcm9sbChlbGVtZW50OiBFbGVtZW50LCBpOiBudW1iZXIpIDogdm9pZCB7XHJcbiAgICBlbGVtZW50LnNjcm9sbEJ5KHsgbGVmdDogaSwgYmVoYXZpb3I6ICdzbW9vdGgnIH0pO1xyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gY2xpY2tFbGVtZW50QnlJZChlbGVtZW50SWQ6c3RyaW5nKSA6IHZvaWQge1xyXG4gICAgY29uc3QgZWxlbWVudCA9IGRvY3VtZW50LmdldEVsZW1lbnRCeUlkKGVsZW1lbnRJZCk7XHJcbiAgICBpZiAoZWxlbWVudCA9PT0gbnVsbCkgcmV0dXJuO1xyXG4gICAgZWxlbWVudC5jbGljaygpO1xyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcblxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuZXhwb3J0IGZ1bmN0aW9uIHNldFRleHRDb250ZW50KGVsZW1lbnQ6IEhUTUxFbGVtZW50LCB0ZXh0OiBzdHJpbmcpOiB2b2lkIHtcclxuICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgaWYgKCF0ZXh0KSByZXR1cm47XHJcbiAgICBlbGVtZW50LnRleHRDb250ZW50ID0gdGV4dDtcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIGdldFRleHRDb250ZW50KGVsZW1lbnQ6IEhUTUxFbGVtZW50KTogc3RyaW5nIHtcclxuICAgIGlmICghZWxlbWVudCkgcmV0dXJuIFwiXCI7XHJcbiAgICBpZiAoIWVsZW1lbnQudGV4dENvbnRlbnQpIHJldHVybiBcIlwiO1xyXG4gICAgcmV0dXJuIGVsZW1lbnQudGV4dENvbnRlbnQ7XHJcbn0iLCIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuaW1wb3J0IHsgQ1NoYXJwVHVwbGUgfSBmcm9tIFwiLi9Db250cmFjdHMvQ1NoYXJwVHVwbGVcIjtcclxuaW1wb3J0IHsgSUlucHV0RWxlbWVudCB9IGZyb20gXCIuL0NvbnRyYWN0cy9JSW5wdXRFbGVtZW50XCI7XHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5mdW5jdGlvbiBjaGVja0lucHV0RWxlbWVudChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogYm9vbGVhbiB7XHJcbiAgICBpZiAoIWVsZW1lbnQpIHJldHVybiBmYWxzZTtcclxuICAgIHJldHVybiBlbGVtZW50LnRhZ05hbWUudG9Mb3dlckNhc2UoKSA9PT0gXCJpbnB1dFwiIHx8IGVsZW1lbnQudGFnTmFtZS50b0xvd2VyQ2FzZSgpID09PSBcInRleHRhcmVhXCI7XHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBnZXRJbnB1dFNlbGVjdGlvblN0YXJ0KGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiBudW1iZXIge1xyXG4gICAgaWYgKCFjaGVja0lucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuIC0xO1xyXG4gICAgcmV0dXJuIGVsZW1lbnQuc2VsZWN0aW9uU3RhcnQgfHwgMDtcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIGdldElucHV0U2VsZWN0aW9uRW5kKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiBudW1iZXIge1xyXG4gICAgaWYgKCFjaGVja0lucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuIC0xO1xyXG4gICAgcmV0dXJuIGVsZW1lbnQuc2VsZWN0aW9uRW5kIHx8IDA7XHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBnZXRJbnB1dFNlbGVjdGlvbihlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogQ1NoYXJwVHVwbGU8bnVtYmVyLCBudW1iZXI+IHtcclxuICAgIGlmICghY2hlY2tJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybiB7XHJcbiAgICAgICAgSXRlbTE6IC0xLFxyXG4gICAgICAgIEl0ZW0yOiAtMVxyXG4gICAgfVxyXG4gICAgcmV0dXJuIHtcclxuICAgICAgICBJdGVtMTogZWxlbWVudC5zZWxlY3Rpb25TdGFydCB8fCAwLFxyXG4gICAgICAgIEl0ZW0yOiBlbGVtZW50LnNlbGVjdGlvbkVuZCB8fCAwXHJcbiAgICB9O1xyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gc2V0SW5wdXRTZWxlY3Rpb25SYW5nZShlbGVtZW50OiBJSW5wdXRFbGVtZW50LCBzdGFydDogbnVtYmVyLCBlbmQ6IG51bWJlcik6IHZvaWQge1xyXG4gICAgaWYgKCFjaGVja0lucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuO1xyXG4gICAgZWxlbWVudC5mb2N1cygpO1xyXG4gICAgZWxlbWVudC5zZXRTZWxlY3Rpb25SYW5nZShzdGFydCwgZW5kKTtcclxufSIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5pbXBvcnQge0tleUNvbmRpdGlvbn0gZnJvbSBcIi4vQ29udHJhY3RzL0tleUNvbmRpdGlvblwiO1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuY29uc3Qga2V5c1RvU2tpcDogU2V0PHN0cmluZz4gPSBuZXcgU2V0KFtcInVcIiwgXCJiXCIsIFwiaVwiLCBcImFcIl0pO1xyXG5jb25zdCBhbGxvd1NwZWNpYWxDb25kaXRpb25zOiBLZXlDb25kaXRpb25bXSA9IFtcclxuICAgIChldmVudCwga2V5KSA9PiBldmVudC5jdHJsS2V5ICYmIGV2ZW50LnNoaWZ0S2V5ICYmIGtleSA9PT0gXCJpXCIsIC8vIFNraXAgYEN0cmwrU2hpZnQrSWBcclxuXTtcclxuXHJcbi8vIE1haW4gZnVuY3Rpb24gdG8gcHJldmVudCBkZWZhdWx0IGJlaGF2aW9yXHJcbmZ1bmN0aW9uIHByZXZlbnRLZXlEZWZhdWx0KGV2ZW50OiBLZXlib2FyZEV2ZW50KTogdm9pZCB7XHJcbiAgICBpZiAoIWV2ZW50KSByZXR1cm47XHJcbiAgICBpZiAoIWV2ZW50LmN0cmxLZXkpIHJldHVybjtcclxuXHJcbiAgICBjb25zdCBrZXkgPSBldmVudC5rZXkudG9Mb3dlckNhc2UoKTtcclxuICAgIGlmIChhbGxvd1NwZWNpYWxDb25kaXRpb25zLnNvbWUoY29uZGl0aW9uID0+IGNvbmRpdGlvbihldmVudCwga2V5KSkpIHJldHVybjtcclxuXHJcbiAgICAvLyBCbG9jayBkZWZhdWx0IGJlaGF2aW9yIGZvciBrZXlzIGluIHRoZSBrZXlzVG9Ta2lwIHNldFxyXG4gICAgaWYgKCFrZXlzVG9Ta2lwLmhhcyhrZXkpKSByZXR1cm47XHJcbiAgICBldmVudC5wcmV2ZW50RGVmYXVsdCgpO1xyXG59XHJcblxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIGFkZFByZXZlbnREZWZhdWx0TGlzdGVuZXIoKSA6IHZvaWQge1xyXG4gICAgZG9jdW1lbnQuYWRkRXZlbnRMaXN0ZW5lcihcImtleWRvd25cIixwcmV2ZW50S2V5RGVmYXVsdClcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIHJlbW92ZVByZXZlbnREZWZhdWx0TGlzdGVuZXIoKSA6IHZvaWQge1xyXG4gICAgZG9jdW1lbnQucmVtb3ZlRXZlbnRMaXN0ZW5lcihcImtleWRvd25cIiwgcHJldmVudEtleURlZmF1bHQpXHJcbn0iLCIvLyBUaGUgbW9kdWxlIGNhY2hlXG52YXIgX193ZWJwYWNrX21vZHVsZV9jYWNoZV9fID0ge307XG5cbi8vIFRoZSByZXF1aXJlIGZ1bmN0aW9uXG5mdW5jdGlvbiBfX3dlYnBhY2tfcmVxdWlyZV9fKG1vZHVsZUlkKSB7XG5cdC8vIENoZWNrIGlmIG1vZHVsZSBpcyBpbiBjYWNoZVxuXHR2YXIgY2FjaGVkTW9kdWxlID0gX193ZWJwYWNrX21vZHVsZV9jYWNoZV9fW21vZHVsZUlkXTtcblx0aWYgKGNhY2hlZE1vZHVsZSAhPT0gdW5kZWZpbmVkKSB7XG5cdFx0cmV0dXJuIGNhY2hlZE1vZHVsZS5leHBvcnRzO1xuXHR9XG5cdC8vIENyZWF0ZSBhIG5ldyBtb2R1bGUgKGFuZCBwdXQgaXQgaW50byB0aGUgY2FjaGUpXG5cdHZhciBtb2R1bGUgPSBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX19bbW9kdWxlSWRdID0ge1xuXHRcdC8vIG5vIG1vZHVsZS5pZCBuZWVkZWRcblx0XHQvLyBubyBtb2R1bGUubG9hZGVkIG5lZWRlZFxuXHRcdGV4cG9ydHM6IHt9XG5cdH07XG5cblx0Ly8gRXhlY3V0ZSB0aGUgbW9kdWxlIGZ1bmN0aW9uXG5cdF9fd2VicGFja19tb2R1bGVzX19bbW9kdWxlSWRdKG1vZHVsZSwgbW9kdWxlLmV4cG9ydHMsIF9fd2VicGFja19yZXF1aXJlX18pO1xuXG5cdC8vIFJldHVybiB0aGUgZXhwb3J0cyBvZiB0aGUgbW9kdWxlXG5cdHJldHVybiBtb2R1bGUuZXhwb3J0cztcbn1cblxuIiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmltcG9ydCB7Z2V0SW5wdXRTZWxlY3Rpb25TdGFydCwgZ2V0SW5wdXRTZWxlY3Rpb25FbmQsIHNldElucHV0U2VsZWN0aW9uUmFuZ2UsIGdldElucHV0U2VsZWN0aW9ufSBmcm9tIFwiLi9JbnB1dFNlbGVjdGlvblwiO1xyXG5pbXBvcnQge2FkZFByZXZlbnREZWZhdWx0TGlzdGVuZXIsIHJlbW92ZVByZXZlbnREZWZhdWx0TGlzdGVuZXJ9IGZyb20gXCIuL0tleURvd25MaXN0ZW5lclwiO1xyXG5pbXBvcnQge3NldFRleHRDb250ZW50LCBnZXRUZXh0Q29udGVudH0gZnJvbSBcIi4vSHRtbEVsZW1lbnRIZWxwZXJzXCI7XHJcbmltcG9ydCB7YWRkT3JVcGRhdGVTdHlsZUVsZW1lbnRBdEhlYWR9IGZyb20gXCIuL0RvY3VtZW50SGVscGVyc1wiO1xyXG5pbXBvcnQge2FkZEhvcml6b250YWxTY3JvbGwsIGNsaWNrRWxlbWVudEJ5SWQsIGdldEJvdW5kaW5nQ2xpZW50UmVjdH0gZnJvbSBcIi4vRWxlbWVudEhlbHBlcnNcIjtcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbndpbmRvdy5nZXRJbnB1dFNlbGVjdGlvblN0YXJ0ID0gZ2V0SW5wdXRTZWxlY3Rpb25TdGFydDtcclxud2luZG93LmdldElucHV0U2VsZWN0aW9uRW5kID0gZ2V0SW5wdXRTZWxlY3Rpb25FbmQ7XHJcbndpbmRvdy5nZXRJbnB1dFNlbGVjdGlvbiA9IGdldElucHV0U2VsZWN0aW9uO1xyXG53aW5kb3cuc2V0SW5wdXRTZWxlY3Rpb25SYW5nZSA9IHNldElucHV0U2VsZWN0aW9uUmFuZ2U7XHJcblxyXG53aW5kb3cuYWRkUHJldmVudERlZmF1bHRMaXN0ZW5lciA9IGFkZFByZXZlbnREZWZhdWx0TGlzdGVuZXI7XHJcbndpbmRvdy5yZW1vdmVQcmV2ZW50RGVmYXVsdExpc3RlbmVyID0gcmVtb3ZlUHJldmVudERlZmF1bHRMaXN0ZW5lcjtcclxuXHJcbndpbmRvdy5zZXRUZXh0Q29udGVudCA9IHNldFRleHRDb250ZW50O1xyXG53aW5kb3cuZ2V0VGV4dENvbnRlbnQgPSBnZXRUZXh0Q29udGVudDtcclxuXHJcbndpbmRvdy5hZGRPclVwZGF0ZVN0eWxlRWxlbWVudEF0SGVhZCA9IGFkZE9yVXBkYXRlU3R5bGVFbGVtZW50QXRIZWFkO1xyXG5cclxud2luZG93LmdldEJvdW5kaW5nQ2xpZW50UmVjdCA9IGdldEJvdW5kaW5nQ2xpZW50UmVjdDtcclxud2luZG93LmFkZEhvcml6b250YWxTY3JvbGwgPSBhZGRIb3Jpem9udGFsU2Nyb2xsO1xyXG5cclxud2luZG93LmNsaWNrRWxlbWVudEJ5SWQgPSBjbGlja0VsZW1lbnRCeUlkOyJdLCJuYW1lcyI6W10sInNvdXJjZVJvb3QiOiIifQ==
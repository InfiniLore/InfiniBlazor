/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/InfiniLore.Blazor/TsLib/InputSelection.ts":
/*!*******************************************************!*\
  !*** ./src/InfiniLore.Blazor/TsLib/InputSelection.ts ***!
  \*******************************************************/
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

/***/ "./src/InfiniLore.Blazor/TsLib/KeyDownListener.ts":
/*!********************************************************!*\
  !*** ./src/InfiniLore.Blazor/TsLib/KeyDownListener.ts ***!
  \********************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.addPreventDefaultListener = addPreventDefaultListener;
exports.removePreventDefaultListener = removePreventDefaultListener;
function preventKeyDefault(event) {
    if (!event)
        return;
    if (!(event.ctrlKey))
        return;
    if (event.key === "F5" || event.key === "F12")
        return;
    if (event.ctrlKey && event.shiftKey && event.key.toLowerCase() === "i")
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
/*!*****************************************************!*\
  !*** ./src/InfiniLore.Blazor/TsLib/WindowHelper.ts ***!
  \*****************************************************/

Object.defineProperty(exports, "__esModule", ({ value: true }));
const InputSelection_1 = __webpack_require__(/*! ./InputSelection */ "./src/InfiniLore.Blazor/TsLib/InputSelection.ts");
const KeyDownListener_1 = __webpack_require__(/*! ./KeyDownListener */ "./src/InfiniLore.Blazor/TsLib/KeyDownListener.ts");
window.getInputSelectionStart = InputSelection_1.getInputSelectionStart;
window.getInputSelectionEnd = InputSelection_1.getInputSelectionEnd;
window.getInputSelection = InputSelection_1.getInputSelection;
window.setInputSelectionRange = InputSelection_1.setInputSelectionRange;
window.addPreventDefaultListener = KeyDownListener_1.addPreventDefaultListener;
window.removePreventDefaultListener = KeyDownListener_1.removePreventDefaultListener;

})();

/******/ })()
;
//# sourceMappingURL=data:application/json;charset=utf-8;base64,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LENBQUM7QUFDN0MsTUFBTSxDQUFDLHNCQUFzQixHQUFHLHVDQUFzQixDQUFDO0FBRXZELE1BQU0sQ0FBQyx5QkFBeUIsR0FBRywyQ0FBeUIsQ0FBQztBQUM3RCxNQUFNLENBQUMsNEJBQTRCLEdBQUcsOENBQTRCLENBQUMiLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuQmxhem9yL1RzTGliL0lucHV0U2VsZWN0aW9uLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5CbGF6b3IvVHNMaWIvS2V5RG93bkxpc3RlbmVyLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvd2VicGFjay9ib290c3RyYXAiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkJsYXpvci9Uc0xpYi9XaW5kb3dIZWxwZXIudHMiXSwic291cmNlc0NvbnRlbnQiOlsiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmltcG9ydCB7Q1NoYXJwVHVwbGUsIElJbnB1dEVsZW1lbnR9IGZyb20gXCIuL0NvbnRyYWN0c1wiO1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuZnVuY3Rpb24gY2hlY2tJbnB1dEVsZW1lbnQoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IGJvb2xlYW4ge1xyXG4gICAgaWYgKCFlbGVtZW50KSByZXR1cm4gZmFsc2U7XHJcbiAgICByZXR1cm4gZWxlbWVudC50YWdOYW1lLnRvTG93ZXJDYXNlKCkgPT09IFwiaW5wdXRcIiB8fCBlbGVtZW50LnRhZ05hbWUudG9Mb3dlckNhc2UoKSA9PT0gXCJ0ZXh0YXJlYVwiO1xyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gZ2V0SW5wdXRTZWxlY3Rpb25TdGFydChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogbnVtYmVyIHtcclxuICAgIGlmICghY2hlY2tJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybiAtMTtcclxuICAgIHJldHVybiBlbGVtZW50LnNlbGVjdGlvblN0YXJ0IHx8IDA7XHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBnZXRJbnB1dFNlbGVjdGlvbkVuZChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogbnVtYmVyIHtcclxuICAgIGlmICghY2hlY2tJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybiAtMTtcclxuICAgIHJldHVybiBlbGVtZW50LnNlbGVjdGlvbkVuZCB8fCAwO1xyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gZ2V0SW5wdXRTZWxlY3Rpb24oZWxlbWVudDogSUlucHV0RWxlbWVudCk6IENTaGFycFR1cGxlPG51bWJlciwgbnVtYmVyPiB7XHJcbiAgICBpZiAoIWNoZWNrSW5wdXRFbGVtZW50KGVsZW1lbnQpKSByZXR1cm4ge1xyXG4gICAgICAgIEl0ZW0xOiAtMSxcclxuICAgICAgICBJdGVtMjogLTFcclxuICAgIH1cclxuICAgIHJldHVybiB7XHJcbiAgICAgICAgSXRlbTE6IGVsZW1lbnQuc2VsZWN0aW9uU3RhcnQgfHwgMCxcclxuICAgICAgICBJdGVtMjogZWxlbWVudC5zZWxlY3Rpb25FbmQgfHwgMFxyXG4gICAgfTtcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIHNldElucHV0U2VsZWN0aW9uUmFuZ2UoZWxlbWVudDogSUlucHV0RWxlbWVudCwgc3RhcnQ6IG51bWJlciwgZW5kOiBudW1iZXIpOiB2b2lkIHtcclxuICAgIGlmICghY2hlY2tJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybjtcclxuICAgIGVsZW1lbnQuZm9jdXMoKTtcclxuICAgIGVsZW1lbnQuc2V0U2VsZWN0aW9uUmFuZ2Uoc3RhcnQsIGVuZCk7XHJcbn0iLCIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5mdW5jdGlvbiBwcmV2ZW50S2V5RGVmYXVsdChldmVudDpLZXlib2FyZEV2ZW50KSA6IHZvaWQge1xyXG4gICAgaWYgKCFldmVudCkgcmV0dXJuO1xyXG4gICAgaWYgKCEoZXZlbnQuY3RybEtleSkpIHJldHVybjtcclxuICAgIGlmIChldmVudC5rZXkgPT09IFwiRjVcIiB8fCBldmVudC5rZXkgPT09IFwiRjEyXCIpIHJldHVybjtcclxuICAgIGlmIChldmVudC5jdHJsS2V5ICYmIGV2ZW50LnNoaWZ0S2V5ICYmIGV2ZW50LmtleS50b0xvd2VyQ2FzZSgpID09PSBcImlcIiApIHJldHVybjtcclxuICAgIGV2ZW50LnByZXZlbnREZWZhdWx0KCk7XHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBhZGRQcmV2ZW50RGVmYXVsdExpc3RlbmVyKCkgOiB2b2lkIHtcclxuICAgIGRvY3VtZW50LmFkZEV2ZW50TGlzdGVuZXIoXCJrZXlkb3duXCIscHJldmVudEtleURlZmF1bHQpXHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiByZW1vdmVQcmV2ZW50RGVmYXVsdExpc3RlbmVyKCkgOiB2b2lkIHtcclxuICAgIGRvY3VtZW50LnJlbW92ZUV2ZW50TGlzdGVuZXIoXCJrZXlkb3duXCIsIHByZXZlbnRLZXlEZWZhdWx0KVxyXG59IiwiLy8gVGhlIG1vZHVsZSBjYWNoZVxudmFyIF9fd2VicGFja19tb2R1bGVfY2FjaGVfXyA9IHt9O1xuXG4vLyBUaGUgcmVxdWlyZSBmdW5jdGlvblxuZnVuY3Rpb24gX193ZWJwYWNrX3JlcXVpcmVfXyhtb2R1bGVJZCkge1xuXHQvLyBDaGVjayBpZiBtb2R1bGUgaXMgaW4gY2FjaGVcblx0dmFyIGNhY2hlZE1vZHVsZSA9IF9fd2VicGFja19tb2R1bGVfY2FjaGVfX1ttb2R1bGVJZF07XG5cdGlmIChjYWNoZWRNb2R1bGUgIT09IHVuZGVmaW5lZCkge1xuXHRcdHJldHVybiBjYWNoZWRNb2R1bGUuZXhwb3J0cztcblx0fVxuXHQvLyBDcmVhdGUgYSBuZXcgbW9kdWxlIChhbmQgcHV0IGl0IGludG8gdGhlIGNhY2hlKVxuXHR2YXIgbW9kdWxlID0gX193ZWJwYWNrX21vZHVsZV9jYWNoZV9fW21vZHVsZUlkXSA9IHtcblx0XHQvLyBubyBtb2R1bGUuaWQgbmVlZGVkXG5cdFx0Ly8gbm8gbW9kdWxlLmxvYWRlZCBuZWVkZWRcblx0XHRleHBvcnRzOiB7fVxuXHR9O1xuXG5cdC8vIEV4ZWN1dGUgdGhlIG1vZHVsZSBmdW5jdGlvblxuXHRfX3dlYnBhY2tfbW9kdWxlc19fW21vZHVsZUlkXShtb2R1bGUsIG1vZHVsZS5leHBvcnRzLCBfX3dlYnBhY2tfcmVxdWlyZV9fKTtcblxuXHQvLyBSZXR1cm4gdGhlIGV4cG9ydHMgb2YgdGhlIG1vZHVsZVxuXHRyZXR1cm4gbW9kdWxlLmV4cG9ydHM7XG59XG5cbiIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxuLy8gSW1wb3J0c1xuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXG5pbXBvcnQge2dldElucHV0U2VsZWN0aW9uU3RhcnQsIGdldElucHV0U2VsZWN0aW9uRW5kLCBzZXRJbnB1dFNlbGVjdGlvblJhbmdlLCBnZXRJbnB1dFNlbGVjdGlvbn0gZnJvbSBcIi4vSW5wdXRTZWxlY3Rpb25cIjtcbmltcG9ydCB7YWRkUHJldmVudERlZmF1bHRMaXN0ZW5lciwgcmVtb3ZlUHJldmVudERlZmF1bHRMaXN0ZW5lcn0gZnJvbSBcIi4vS2V5RG93bkxpc3RlbmVyXCI7XG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cbi8vIENvZGVcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxud2luZG93LmdldElucHV0U2VsZWN0aW9uU3RhcnQgPSBnZXRJbnB1dFNlbGVjdGlvblN0YXJ0O1xud2luZG93LmdldElucHV0U2VsZWN0aW9uRW5kID0gZ2V0SW5wdXRTZWxlY3Rpb25FbmQ7XG53aW5kb3cuZ2V0SW5wdXRTZWxlY3Rpb24gPSBnZXRJbnB1dFNlbGVjdGlvbjtcbndpbmRvdy5zZXRJbnB1dFNlbGVjdGlvblJhbmdlID0gc2V0SW5wdXRTZWxlY3Rpb25SYW5nZTtcblxud2luZG93LmFkZFByZXZlbnREZWZhdWx0TGlzdGVuZXIgPSBhZGRQcmV2ZW50RGVmYXVsdExpc3RlbmVyO1xud2luZG93LnJlbW92ZVByZXZlbnREZWZhdWx0TGlzdGVuZXIgPSByZW1vdmVQcmV2ZW50RGVmYXVsdExpc3RlbmVyOyJdLCJuYW1lcyI6W10sInNvdXJjZVJvb3QiOiIifQ==
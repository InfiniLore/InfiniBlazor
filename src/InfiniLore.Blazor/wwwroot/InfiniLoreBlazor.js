/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

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
    if (event.key === "F5" || event.key === "F12")
        return;
    if (event.ctrlKey && event.shiftKey && event.key.toLowerCase() === "i")
        return;
    if (!(event.ctrlKey))
        return;
    event.preventDefault();
}
function addPreventDefaultListener() {
    document.addEventListener("keydown", preventKeyDefault);
}
function removePreventDefaultListener() {
    document.removeEventListener("keydown", preventKeyDefault);
}


/***/ }),

/***/ "./src/InfiniLore.Blazor/TsLib/Selection.ts":
/*!**************************************************!*\
  !*** ./src/InfiniLore.Blazor/TsLib/Selection.ts ***!
  \**************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.getSelectionStart = getSelectionStart;
exports.getSelectionEnd = getSelectionEnd;
exports.setSelectionRange = setSelectionRange;
function getSelection(element, callback) {
    var _a;
    if (element.tagName.toLowerCase() !== "input" && element.tagName.toLowerCase() !== "textarea")
        return -1;
    return (_a = callback(element)) !== null && _a !== void 0 ? _a : -1;
}
function getSelectionStart(element) {
    if (!element)
        return -1;
    return getSelection(element, (el) => el.selectionStart || 0);
}
function getSelectionEnd(element) {
    if (!element)
        return -1;
    return getSelection(element, (el) => el.selectionEnd || 0);
}
function setSelectionRange(element, start, end) {
    if (!element)
        return;
    element.focus();
    element.setSelectionRange(start, end);
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
const Selection_1 = __webpack_require__(/*! ./Selection */ "./src/InfiniLore.Blazor/TsLib/Selection.ts");
const KeyDownListener_1 = __webpack_require__(/*! ./KeyDownListener */ "./src/InfiniLore.Blazor/TsLib/KeyDownListener.ts");
window.getSelectionStart = Selection_1.getSelectionStart;
window.getSelectionEnd = Selection_1.getSelectionEnd;
window.setSelectionRange = Selection_1.setSelectionRange;
window.addPreventDefaultListener = KeyDownListener_1.addPreventDefaultListener;
window.removePreventDefaultListener = KeyDownListener_1.removePreventDefaultListener;

})();

/******/ })()
;
//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiSW5maW5pTG9yZUJsYXpvci5qcyIsIm1hcHBpbmdzIjoiOzs7Ozs7Ozs7Ozs7QUFlQSw4REFFQztBQUVELG9FQUVDO0FBZEQsU0FBUyxpQkFBaUIsQ0FBQyxLQUFtQjtJQUMxQyxJQUFJLENBQUMsS0FBSztRQUFFLE9BQU87SUFDbkIsSUFBSSxLQUFLLENBQUMsR0FBRyxLQUFLLElBQUksSUFBSSxLQUFLLENBQUMsR0FBRyxLQUFLLEtBQUs7UUFBRSxPQUFPO0lBQ3RELElBQUksS0FBSyxDQUFDLE9BQU8sSUFBSSxLQUFLLENBQUMsUUFBUSxJQUFJLEtBQUssQ0FBQyxHQUFHLENBQUMsV0FBVyxFQUFFLEtBQUssR0FBRztRQUFHLE9BQU87SUFDaEYsSUFBSSxDQUFDLENBQUMsS0FBSyxDQUFDLE9BQU8sQ0FBQztRQUFFLE9BQU87SUFDN0IsS0FBSyxDQUFDLGNBQWMsRUFBRSxDQUFDO0FBQzNCLENBQUM7QUFFRCxTQUFnQix5QkFBeUI7SUFDckMsUUFBUSxDQUFDLGdCQUFnQixDQUFDLFNBQVMsRUFBQyxpQkFBaUIsQ0FBQztBQUMxRCxDQUFDO0FBRUQsU0FBZ0IsNEJBQTRCO0lBQ3hDLFFBQVEsQ0FBQyxtQkFBbUIsQ0FBQyxTQUFTLEVBQUUsaUJBQWlCLENBQUM7QUFDOUQsQ0FBQzs7Ozs7Ozs7Ozs7OztBQ1RELDhDQUdDO0FBRUQsMENBR0M7QUFFRCw4Q0FJQztBQW5CRCxTQUFTLFlBQVksQ0FBQyxPQUFzQixFQUFFLFFBQTRDOztJQUN0RixJQUFJLE9BQU8sQ0FBQyxPQUFPLENBQUMsV0FBVyxFQUFFLEtBQUssT0FBTyxJQUFJLE9BQU8sQ0FBQyxPQUFPLENBQUMsV0FBVyxFQUFFLEtBQUssVUFBVTtRQUFFLE9BQU8sQ0FBQyxDQUFDLENBQUM7SUFDekcsT0FBTyxjQUFRLENBQUMsT0FBTyxDQUFDLG1DQUFJLENBQUMsQ0FBQyxDQUFDO0FBQ25DLENBQUM7QUFFRCxTQUFnQixpQkFBaUIsQ0FBQyxPQUFzQjtJQUNwRCxJQUFJLENBQUMsT0FBTztRQUFFLE9BQU8sQ0FBQyxDQUFDLENBQUM7SUFDeEIsT0FBTyxZQUFZLENBQUMsT0FBTyxFQUFFLENBQUMsRUFBRSxFQUFFLEVBQUUsQ0FBQyxFQUFFLENBQUMsY0FBYyxJQUFJLENBQUMsQ0FBQyxDQUFDO0FBQ2pFLENBQUM7QUFFRCxTQUFnQixlQUFlLENBQUMsT0FBc0I7SUFDbEQsSUFBSSxDQUFDLE9BQU87UUFBRSxPQUFPLENBQUMsQ0FBQyxDQUFDO0lBQ3hCLE9BQU8sWUFBWSxDQUFDLE9BQU8sRUFBRSxDQUFDLEVBQUUsRUFBRSxFQUFFLENBQUMsRUFBRSxDQUFDLFlBQVksSUFBSSxDQUFDLENBQUMsQ0FBQztBQUMvRCxDQUFDO0FBRUQsU0FBZ0IsaUJBQWlCLENBQUMsT0FBc0IsRUFBRSxLQUFhLEVBQUUsR0FBVztJQUNoRixJQUFJLENBQUMsT0FBTztRQUFFLE9BQU87SUFDckIsT0FBTyxDQUFDLEtBQUssRUFBRSxDQUFDO0lBQ2hCLE9BQU8sQ0FBQyxpQkFBaUIsQ0FBQyxLQUFLLEVBQUUsR0FBRyxDQUFDLENBQUM7QUFDMUMsQ0FBQzs7Ozs7OztVQzFCRDtVQUNBOztVQUVBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBOztVQUVBO1VBQ0E7O1VBRUE7VUFDQTtVQUNBOzs7Ozs7Ozs7Ozs7QUNuQkEseUdBQWtGO0FBQ2xGLDJIQUEwRjtBQUkxRixNQUFNLENBQUMsaUJBQWlCLEdBQUcsNkJBQWlCLENBQUM7QUFDN0MsTUFBTSxDQUFDLGVBQWUsR0FBRywyQkFBZSxDQUFDO0FBQ3pDLE1BQU0sQ0FBQyxpQkFBaUIsR0FBRyw2QkFBaUIsQ0FBQztBQUU3QyxNQUFNLENBQUMseUJBQXlCLEdBQUcsMkNBQXlCLENBQUM7QUFDN0QsTUFBTSxDQUFDLDRCQUE0QixHQUFHLDhDQUE0QixDQUFDIiwic291cmNlcyI6WyJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkJsYXpvci9Uc0xpYi9LZXlEb3duTGlzdGVuZXIudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkJsYXpvci9Uc0xpYi9TZWxlY3Rpb24udHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2Jvb3RzdHJhcCIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuQmxhem9yL1RzTGliL1dpbmRvd0hlbHBlci50cyJdLCJzb3VyY2VzQ29udGVudCI6WyIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5mdW5jdGlvbiBwcmV2ZW50S2V5RGVmYXVsdChldmVudDpLZXlib2FyZEV2ZW50KSA6IHZvaWQge1xyXG4gICAgaWYgKCFldmVudCkgcmV0dXJuO1xyXG4gICAgaWYgKGV2ZW50LmtleSA9PT0gXCJGNVwiIHx8IGV2ZW50LmtleSA9PT0gXCJGMTJcIikgcmV0dXJuO1xyXG4gICAgaWYgKGV2ZW50LmN0cmxLZXkgJiYgZXZlbnQuc2hpZnRLZXkgJiYgZXZlbnQua2V5LnRvTG93ZXJDYXNlKCkgPT09IFwiaVwiICkgcmV0dXJuO1xyXG4gICAgaWYgKCEoZXZlbnQuY3RybEtleSkpIHJldHVybjtcclxuICAgIGV2ZW50LnByZXZlbnREZWZhdWx0KCk7XHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBhZGRQcmV2ZW50RGVmYXVsdExpc3RlbmVyKCkgOiB2b2lkIHtcclxuICAgIGRvY3VtZW50LmFkZEV2ZW50TGlzdGVuZXIoXCJrZXlkb3duXCIscHJldmVudEtleURlZmF1bHQpXHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiByZW1vdmVQcmV2ZW50RGVmYXVsdExpc3RlbmVyKCkgOiB2b2lkIHtcclxuICAgIGRvY3VtZW50LnJlbW92ZUV2ZW50TGlzdGVuZXIoXCJrZXlkb3duXCIsIHByZXZlbnRLZXlEZWZhdWx0KVxyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmltcG9ydCB7SUlucHV0RWxlbWVudH0gZnJvbSBcIi4vQ29udHJhY3RzXCI7XHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5mdW5jdGlvbiBnZXRTZWxlY3Rpb24oZWxlbWVudDogSUlucHV0RWxlbWVudCwgY2FsbGJhY2s6IChlbGVtZW50OiBJSW5wdXRFbGVtZW50KSA9PiBudW1iZXIpOiBudW1iZXIge1xyXG4gICAgaWYgKGVsZW1lbnQudGFnTmFtZS50b0xvd2VyQ2FzZSgpICE9PSBcImlucHV0XCIgJiYgZWxlbWVudC50YWdOYW1lLnRvTG93ZXJDYXNlKCkgIT09IFwidGV4dGFyZWFcIikgcmV0dXJuIC0xO1xyXG4gICAgcmV0dXJuIGNhbGxiYWNrKGVsZW1lbnQpID8/IC0xO1xyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gZ2V0U2VsZWN0aW9uU3RhcnQoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IG51bWJlciB7XHJcbiAgICBpZiAoIWVsZW1lbnQpIHJldHVybiAtMTtcclxuICAgIHJldHVybiBnZXRTZWxlY3Rpb24oZWxlbWVudCwgKGVsKSA9PiBlbC5zZWxlY3Rpb25TdGFydCB8fCAwKTtcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIGdldFNlbGVjdGlvbkVuZChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogbnVtYmVyIHtcclxuICAgIGlmICghZWxlbWVudCkgcmV0dXJuIC0xO1xyXG4gICAgcmV0dXJuIGdldFNlbGVjdGlvbihlbGVtZW50LCAoZWwpID0+IGVsLnNlbGVjdGlvbkVuZCB8fCAwKTtcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIHNldFNlbGVjdGlvblJhbmdlKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQsIHN0YXJ0OiBudW1iZXIsIGVuZDogbnVtYmVyKTogdm9pZCB7XHJcbiAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgIGVsZW1lbnQuZm9jdXMoKTtcclxuICAgIGVsZW1lbnQuc2V0U2VsZWN0aW9uUmFuZ2Uoc3RhcnQsIGVuZCk7XHJcbn0iLCIvLyBUaGUgbW9kdWxlIGNhY2hlXG52YXIgX193ZWJwYWNrX21vZHVsZV9jYWNoZV9fID0ge307XG5cbi8vIFRoZSByZXF1aXJlIGZ1bmN0aW9uXG5mdW5jdGlvbiBfX3dlYnBhY2tfcmVxdWlyZV9fKG1vZHVsZUlkKSB7XG5cdC8vIENoZWNrIGlmIG1vZHVsZSBpcyBpbiBjYWNoZVxuXHR2YXIgY2FjaGVkTW9kdWxlID0gX193ZWJwYWNrX21vZHVsZV9jYWNoZV9fW21vZHVsZUlkXTtcblx0aWYgKGNhY2hlZE1vZHVsZSAhPT0gdW5kZWZpbmVkKSB7XG5cdFx0cmV0dXJuIGNhY2hlZE1vZHVsZS5leHBvcnRzO1xuXHR9XG5cdC8vIENyZWF0ZSBhIG5ldyBtb2R1bGUgKGFuZCBwdXQgaXQgaW50byB0aGUgY2FjaGUpXG5cdHZhciBtb2R1bGUgPSBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX19bbW9kdWxlSWRdID0ge1xuXHRcdC8vIG5vIG1vZHVsZS5pZCBuZWVkZWRcblx0XHQvLyBubyBtb2R1bGUubG9hZGVkIG5lZWRlZFxuXHRcdGV4cG9ydHM6IHt9XG5cdH07XG5cblx0Ly8gRXhlY3V0ZSB0aGUgbW9kdWxlIGZ1bmN0aW9uXG5cdF9fd2VicGFja19tb2R1bGVzX19bbW9kdWxlSWRdKG1vZHVsZSwgbW9kdWxlLmV4cG9ydHMsIF9fd2VicGFja19yZXF1aXJlX18pO1xuXG5cdC8vIFJldHVybiB0aGUgZXhwb3J0cyBvZiB0aGUgbW9kdWxlXG5cdHJldHVybiBtb2R1bGUuZXhwb3J0cztcbn1cblxuIiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXG4vLyBJbXBvcnRzXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cbmltcG9ydCB7Z2V0U2VsZWN0aW9uU3RhcnQsIGdldFNlbGVjdGlvbkVuZCwgc2V0U2VsZWN0aW9uUmFuZ2V9IGZyb20gXCIuL1NlbGVjdGlvblwiO1xuaW1wb3J0IHthZGRQcmV2ZW50RGVmYXVsdExpc3RlbmVyLCByZW1vdmVQcmV2ZW50RGVmYXVsdExpc3RlbmVyfSBmcm9tIFwiLi9LZXlEb3duTGlzdGVuZXJcIjtcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxuLy8gQ29kZVxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXG53aW5kb3cuZ2V0U2VsZWN0aW9uU3RhcnQgPSBnZXRTZWxlY3Rpb25TdGFydDtcbndpbmRvdy5nZXRTZWxlY3Rpb25FbmQgPSBnZXRTZWxlY3Rpb25FbmQ7XG53aW5kb3cuc2V0U2VsZWN0aW9uUmFuZ2UgPSBzZXRTZWxlY3Rpb25SYW5nZTtcblxud2luZG93LmFkZFByZXZlbnREZWZhdWx0TGlzdGVuZXIgPSBhZGRQcmV2ZW50RGVmYXVsdExpc3RlbmVyO1xud2luZG93LnJlbW92ZVByZXZlbnREZWZhdWx0TGlzdGVuZXIgPSByZW1vdmVQcmV2ZW50RGVmYXVsdExpc3RlbmVyOyJdLCJuYW1lcyI6W10sInNvdXJjZVJvb3QiOiIifQ==
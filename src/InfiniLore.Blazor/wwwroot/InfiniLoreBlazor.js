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
//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiSW5maW5pTG9yZUJsYXpvci5qcyIsIm1hcHBpbmdzIjoiOzs7Ozs7Ozs7Ozs7QUFZQSx3REFHQztBQUVELG9EQUdDO0FBRUQsOENBU0M7QUFFRCx3REFJQztBQTlCRCxTQUFTLGlCQUFpQixDQUFDLE9BQXNCO0lBQzdDLElBQUksQ0FBQyxPQUFPO1FBQUUsT0FBTyxLQUFLLENBQUM7SUFDM0IsT0FBTyxPQUFPLENBQUMsT0FBTyxDQUFDLFdBQVcsRUFBRSxLQUFLLE9BQU8sSUFBSSxPQUFPLENBQUMsT0FBTyxDQUFDLFdBQVcsRUFBRSxLQUFLLFVBQVUsQ0FBQztBQUNyRyxDQUFDO0FBRUQsU0FBZ0Isc0JBQXNCLENBQUMsT0FBc0I7SUFDekQsSUFBSSxDQUFDLGlCQUFpQixDQUFDLE9BQU8sQ0FBQztRQUFFLE9BQU8sQ0FBQyxDQUFDLENBQUM7SUFDM0MsT0FBTyxPQUFPLENBQUMsY0FBYyxJQUFJLENBQUMsQ0FBQztBQUN2QyxDQUFDO0FBRUQsU0FBZ0Isb0JBQW9CLENBQUMsT0FBc0I7SUFDdkQsSUFBSSxDQUFDLGlCQUFpQixDQUFDLE9BQU8sQ0FBQztRQUFFLE9BQU8sQ0FBQyxDQUFDLENBQUM7SUFDM0MsT0FBTyxPQUFPLENBQUMsWUFBWSxJQUFJLENBQUMsQ0FBQztBQUNyQyxDQUFDO0FBRUQsU0FBZ0IsaUJBQWlCLENBQUMsT0FBc0I7SUFDcEQsSUFBSSxDQUFDLGlCQUFpQixDQUFDLE9BQU8sQ0FBQztRQUFFLE9BQU87WUFDcEMsS0FBSyxFQUFFLENBQUMsQ0FBQztZQUNULEtBQUssRUFBRSxDQUFDLENBQUM7U0FDWjtJQUNELE9BQU87UUFDSCxLQUFLLEVBQUUsT0FBTyxDQUFDLGNBQWMsSUFBSSxDQUFDO1FBQ2xDLEtBQUssRUFBRSxPQUFPLENBQUMsWUFBWSxJQUFJLENBQUM7S0FDbkMsQ0FBQztBQUNOLENBQUM7QUFFRCxTQUFnQixzQkFBc0IsQ0FBQyxPQUFzQixFQUFFLEtBQWEsRUFBRSxHQUFXO0lBQ3JGLElBQUksQ0FBQyxpQkFBaUIsQ0FBQyxPQUFPLENBQUM7UUFBRSxPQUFPO0lBQ3hDLE9BQU8sQ0FBQyxLQUFLLEVBQUUsQ0FBQztJQUNoQixPQUFPLENBQUMsaUJBQWlCLENBQUMsS0FBSyxFQUFFLEdBQUcsQ0FBQyxDQUFDO0FBQzFDLENBQUM7Ozs7Ozs7Ozs7Ozs7QUNYRCw4REFFQztBQUVELG9FQUVDO0FBekJELE1BQU0sVUFBVSxHQUFnQixJQUFJLEdBQUcsQ0FBQyxDQUFDLEdBQUcsRUFBRSxHQUFHLEVBQUUsR0FBRyxFQUFFLEdBQUcsQ0FBQyxDQUFDLENBQUM7QUFDOUQsTUFBTSxzQkFBc0IsR0FBbUI7SUFDM0MsQ0FBQyxLQUFLLEVBQUUsR0FBRyxFQUFFLEVBQUUsQ0FBQyxLQUFLLENBQUMsT0FBTyxJQUFJLEtBQUssQ0FBQyxRQUFRLElBQUksR0FBRyxLQUFLLEdBQUc7Q0FDakUsQ0FBQztBQUdGLFNBQVMsaUJBQWlCLENBQUMsS0FBb0I7SUFDM0MsSUFBSSxDQUFDLEtBQUs7UUFBRSxPQUFPO0lBQ25CLElBQUksQ0FBQyxLQUFLLENBQUMsT0FBTztRQUFFLE9BQU87SUFFM0IsTUFBTSxHQUFHLEdBQUcsS0FBSyxDQUFDLEdBQUcsQ0FBQyxXQUFXLEVBQUUsQ0FBQztJQUNwQyxJQUFJLHNCQUFzQixDQUFDLElBQUksQ0FBQyxTQUFTLENBQUMsRUFBRSxDQUFDLFNBQVMsQ0FBQyxLQUFLLEVBQUUsR0FBRyxDQUFDLENBQUM7UUFBRSxPQUFPO0lBRzVFLElBQUksQ0FBQyxVQUFVLENBQUMsR0FBRyxDQUFDLEdBQUcsQ0FBQztRQUFFLE9BQU87SUFDakMsS0FBSyxDQUFDLGNBQWMsRUFBRSxDQUFDO0FBQzNCLENBQUM7QUFHRCxTQUFnQix5QkFBeUI7SUFDckMsUUFBUSxDQUFDLGdCQUFnQixDQUFDLFNBQVMsRUFBQyxpQkFBaUIsQ0FBQztBQUMxRCxDQUFDO0FBRUQsU0FBZ0IsNEJBQTRCO0lBQ3hDLFFBQVEsQ0FBQyxtQkFBbUIsQ0FBQyxTQUFTLEVBQUUsaUJBQWlCLENBQUM7QUFDOUQsQ0FBQzs7Ozs7OztVQ2hDRDtVQUNBOztVQUVBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBOztVQUVBO1VBQ0E7O1VBRUE7VUFDQTtVQUNBOzs7Ozs7Ozs7Ozs7QUNuQkEsd0hBQXlIO0FBQ3pILDJIQUEwRjtBQUkxRixNQUFNLENBQUMsc0JBQXNCLEdBQUcsdUNBQXNCLENBQUM7QUFDdkQsTUFBTSxDQUFDLG9CQUFvQixHQUFHLHFDQUFvQixDQUFDO0FBQ25ELE1BQU0sQ0FBQyxpQkFBaUIsR0FBRyxrQ0FBaUIsQ0FBQztBQUM3QyxNQUFNLENBQUMsc0JBQXNCLEdBQUcsdUNBQXNCLENBQUM7QUFFdkQsTUFBTSxDQUFDLHlCQUF5QixHQUFHLDJDQUF5QixDQUFDO0FBQzdELE1BQU0sQ0FBQyw0QkFBNEIsR0FBRyw4Q0FBNEIsQ0FBQyIsInNvdXJjZXMiOlsid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5CbGF6b3IvVHNMaWIvSW5wdXRTZWxlY3Rpb24udHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkJsYXpvci9Uc0xpYi9LZXlEb3duTGlzdGVuZXIudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2Jvb3RzdHJhcCIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuQmxhem9yL1RzTGliL1dpbmRvd0hlbHBlci50cyJdLCJzb3VyY2VzQ29udGVudCI6WyIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuaW1wb3J0IHtDU2hhcnBUdXBsZSwgSUlucHV0RWxlbWVudH0gZnJvbSBcIi4vQ29udHJhY3RzXCI7XHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5mdW5jdGlvbiBjaGVja0lucHV0RWxlbWVudChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogYm9vbGVhbiB7XHJcbiAgICBpZiAoIWVsZW1lbnQpIHJldHVybiBmYWxzZTtcclxuICAgIHJldHVybiBlbGVtZW50LnRhZ05hbWUudG9Mb3dlckNhc2UoKSA9PT0gXCJpbnB1dFwiIHx8IGVsZW1lbnQudGFnTmFtZS50b0xvd2VyQ2FzZSgpID09PSBcInRleHRhcmVhXCI7XHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBnZXRJbnB1dFNlbGVjdGlvblN0YXJ0KGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiBudW1iZXIge1xyXG4gICAgaWYgKCFjaGVja0lucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuIC0xO1xyXG4gICAgcmV0dXJuIGVsZW1lbnQuc2VsZWN0aW9uU3RhcnQgfHwgMDtcclxufVxyXG5cclxuZXhwb3J0IGZ1bmN0aW9uIGdldElucHV0U2VsZWN0aW9uRW5kKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiBudW1iZXIge1xyXG4gICAgaWYgKCFjaGVja0lucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuIC0xO1xyXG4gICAgcmV0dXJuIGVsZW1lbnQuc2VsZWN0aW9uRW5kIHx8IDA7XHJcbn1cclxuXHJcbmV4cG9ydCBmdW5jdGlvbiBnZXRJbnB1dFNlbGVjdGlvbihlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogQ1NoYXJwVHVwbGU8bnVtYmVyLCBudW1iZXI+IHtcclxuICAgIGlmICghY2hlY2tJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybiB7XHJcbiAgICAgICAgSXRlbTE6IC0xLFxyXG4gICAgICAgIEl0ZW0yOiAtMVxyXG4gICAgfVxyXG4gICAgcmV0dXJuIHtcclxuICAgICAgICBJdGVtMTogZWxlbWVudC5zZWxlY3Rpb25TdGFydCB8fCAwLFxyXG4gICAgICAgIEl0ZW0yOiBlbGVtZW50LnNlbGVjdGlvbkVuZCB8fCAwXHJcbiAgICB9O1xyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gc2V0SW5wdXRTZWxlY3Rpb25SYW5nZShlbGVtZW50OiBJSW5wdXRFbGVtZW50LCBzdGFydDogbnVtYmVyLCBlbmQ6IG51bWJlcik6IHZvaWQge1xyXG4gICAgaWYgKCFjaGVja0lucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuO1xyXG4gICAgZWxlbWVudC5mb2N1cygpO1xyXG4gICAgZWxlbWVudC5zZXRTZWxlY3Rpb25SYW5nZShzdGFydCwgZW5kKTtcclxufSIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5pbXBvcnQge0tleUNvbmRpdGlvbn0gZnJvbSBcIi4vQ29udHJhY3RzXCI7XHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5jb25zdCBrZXlzVG9Ta2lwOiBTZXQ8c3RyaW5nPiA9IG5ldyBTZXQoW1widVwiLCBcImJcIiwgXCJpXCIsIFwiYVwiXSk7XHJcbmNvbnN0IGFsbG93U3BlY2lhbENvbmRpdGlvbnM6IEtleUNvbmRpdGlvbltdID0gW1xyXG4gICAgKGV2ZW50LCBrZXkpID0+IGV2ZW50LmN0cmxLZXkgJiYgZXZlbnQuc2hpZnRLZXkgJiYga2V5ID09PSBcImlcIiwgLy8gU2tpcCBgQ3RybCtTaGlmdCtJYFxyXG5dO1xyXG5cclxuLy8gTWFpbiBmdW5jdGlvbiB0byBwcmV2ZW50IGRlZmF1bHQgYmVoYXZpb3JcclxuZnVuY3Rpb24gcHJldmVudEtleURlZmF1bHQoZXZlbnQ6IEtleWJvYXJkRXZlbnQpOiB2b2lkIHtcclxuICAgIGlmICghZXZlbnQpIHJldHVybjtcclxuICAgIGlmICghZXZlbnQuY3RybEtleSkgcmV0dXJuO1xyXG5cclxuICAgIGNvbnN0IGtleSA9IGV2ZW50LmtleS50b0xvd2VyQ2FzZSgpO1xyXG4gICAgaWYgKGFsbG93U3BlY2lhbENvbmRpdGlvbnMuc29tZShjb25kaXRpb24gPT4gY29uZGl0aW9uKGV2ZW50LCBrZXkpKSkgcmV0dXJuO1xyXG5cclxuICAgIC8vIEJsb2NrIGRlZmF1bHQgYmVoYXZpb3IgZm9yIGtleXMgaW4gdGhlIGtleXNUb1NraXAgc2V0XHJcbiAgICBpZiAoIWtleXNUb1NraXAuaGFzKGtleSkpIHJldHVybjtcclxuICAgIGV2ZW50LnByZXZlbnREZWZhdWx0KCk7XHJcbn1cclxuXHJcblxyXG5leHBvcnQgZnVuY3Rpb24gYWRkUHJldmVudERlZmF1bHRMaXN0ZW5lcigpIDogdm9pZCB7XHJcbiAgICBkb2N1bWVudC5hZGRFdmVudExpc3RlbmVyKFwia2V5ZG93blwiLHByZXZlbnRLZXlEZWZhdWx0KVxyXG59XHJcblxyXG5leHBvcnQgZnVuY3Rpb24gcmVtb3ZlUHJldmVudERlZmF1bHRMaXN0ZW5lcigpIDogdm9pZCB7XHJcbiAgICBkb2N1bWVudC5yZW1vdmVFdmVudExpc3RlbmVyKFwia2V5ZG93blwiLCBwcmV2ZW50S2V5RGVmYXVsdClcclxufSIsIi8vIFRoZSBtb2R1bGUgY2FjaGVcbnZhciBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX18gPSB7fTtcblxuLy8gVGhlIHJlcXVpcmUgZnVuY3Rpb25cbmZ1bmN0aW9uIF9fd2VicGFja19yZXF1aXJlX18obW9kdWxlSWQpIHtcblx0Ly8gQ2hlY2sgaWYgbW9kdWxlIGlzIGluIGNhY2hlXG5cdHZhciBjYWNoZWRNb2R1bGUgPSBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX19bbW9kdWxlSWRdO1xuXHRpZiAoY2FjaGVkTW9kdWxlICE9PSB1bmRlZmluZWQpIHtcblx0XHRyZXR1cm4gY2FjaGVkTW9kdWxlLmV4cG9ydHM7XG5cdH1cblx0Ly8gQ3JlYXRlIGEgbmV3IG1vZHVsZSAoYW5kIHB1dCBpdCBpbnRvIHRoZSBjYWNoZSlcblx0dmFyIG1vZHVsZSA9IF9fd2VicGFja19tb2R1bGVfY2FjaGVfX1ttb2R1bGVJZF0gPSB7XG5cdFx0Ly8gbm8gbW9kdWxlLmlkIG5lZWRlZFxuXHRcdC8vIG5vIG1vZHVsZS5sb2FkZWQgbmVlZGVkXG5cdFx0ZXhwb3J0czoge31cblx0fTtcblxuXHQvLyBFeGVjdXRlIHRoZSBtb2R1bGUgZnVuY3Rpb25cblx0X193ZWJwYWNrX21vZHVsZXNfX1ttb2R1bGVJZF0obW9kdWxlLCBtb2R1bGUuZXhwb3J0cywgX193ZWJwYWNrX3JlcXVpcmVfXyk7XG5cblx0Ly8gUmV0dXJuIHRoZSBleHBvcnRzIG9mIHRoZSBtb2R1bGVcblx0cmV0dXJuIG1vZHVsZS5leHBvcnRzO1xufVxuXG4iLCIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cbi8vIEltcG9ydHNcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxuaW1wb3J0IHtnZXRJbnB1dFNlbGVjdGlvblN0YXJ0LCBnZXRJbnB1dFNlbGVjdGlvbkVuZCwgc2V0SW5wdXRTZWxlY3Rpb25SYW5nZSwgZ2V0SW5wdXRTZWxlY3Rpb259IGZyb20gXCIuL0lucHV0U2VsZWN0aW9uXCI7XG5pbXBvcnQge2FkZFByZXZlbnREZWZhdWx0TGlzdGVuZXIsIHJlbW92ZVByZXZlbnREZWZhdWx0TGlzdGVuZXJ9IGZyb20gXCIuL0tleURvd25MaXN0ZW5lclwiO1xuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXG4vLyBDb2RlXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cbndpbmRvdy5nZXRJbnB1dFNlbGVjdGlvblN0YXJ0ID0gZ2V0SW5wdXRTZWxlY3Rpb25TdGFydDtcbndpbmRvdy5nZXRJbnB1dFNlbGVjdGlvbkVuZCA9IGdldElucHV0U2VsZWN0aW9uRW5kO1xud2luZG93LmdldElucHV0U2VsZWN0aW9uID0gZ2V0SW5wdXRTZWxlY3Rpb247XG53aW5kb3cuc2V0SW5wdXRTZWxlY3Rpb25SYW5nZSA9IHNldElucHV0U2VsZWN0aW9uUmFuZ2U7XG5cbndpbmRvdy5hZGRQcmV2ZW50RGVmYXVsdExpc3RlbmVyID0gYWRkUHJldmVudERlZmF1bHRMaXN0ZW5lcjtcbndpbmRvdy5yZW1vdmVQcmV2ZW50RGVmYXVsdExpc3RlbmVyID0gcmVtb3ZlUHJldmVudERlZmF1bHRMaXN0ZW5lcjsiXSwibmFtZXMiOltdLCJzb3VyY2VSb290IjoiIn0=
/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/InfiniLore.Blazor/TsLib/selection.ts":
/*!**************************************************!*\
  !*** ./src/InfiniLore.Blazor/TsLib/selection.ts ***!
  \**************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.getSelection = void 0;
const getSelection = (element, callback) => {
    var _a;
    if (!element || (element.tagName.toLowerCase() !== "input" && element.tagName.toLowerCase() !== "textarea")) {
        console.error("Element must be an input or textarea.");
        return -1;
    }
    return (_a = callback(element)) !== null && _a !== void 0 ? _a : -1;
};
exports.getSelection = getSelection;


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
  !*** ./src/InfiniLore.Blazor/TsLib/windowHelper.ts ***!
  \*****************************************************/

Object.defineProperty(exports, "__esModule", ({ value: true }));
const selection_1 = __webpack_require__(/*! ./selection */ "./src/InfiniLore.Blazor/TsLib/selection.ts");
window.getSelectionStart = (element) => (0, selection_1.getSelection)(element, (el) => el.selectionStart || 0);
window.getSelectionEnd = (element) => (0, selection_1.getSelection)(element, (el) => el.selectionEnd || 0);
window.preventDefault = (event) => {
    event.preventDefault();
};
window.setSelectionRange = (element, start, end) => {
    if (!element)
        return;
    element.focus();
    element.setSelectionRange(start, end);
};

})();

/******/ })()
;
//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiSW5maW5pTG9yZUJsYXpvci5qcyIsIm1hcHBpbmdzIjoiOzs7Ozs7Ozs7Ozs7O0FBQU8sTUFBTSxZQUFZLEdBQW9KLENBQ3pLLE9BQStDLEVBQy9DLFFBQTRFLEVBQ3RFLEVBQUU7O0lBQ1IsSUFBSSxDQUFDLE9BQU8sSUFBSSxDQUFDLE9BQU8sQ0FBQyxPQUFPLENBQUMsV0FBVyxFQUFFLEtBQUssT0FBTyxJQUFJLE9BQU8sQ0FBQyxPQUFPLENBQUMsV0FBVyxFQUFFLEtBQUssVUFBVSxDQUFDLEVBQUUsQ0FBQztRQUMxRyxPQUFPLENBQUMsS0FBSyxDQUFDLHVDQUF1QyxDQUFDLENBQUM7UUFDdkQsT0FBTyxDQUFDLENBQUMsQ0FBQztJQUNkLENBQUM7SUFDRCxPQUFPLGNBQVEsQ0FBQyxPQUFPLENBQUMsbUNBQUksQ0FBQyxDQUFDLENBQUM7QUFDbkMsQ0FBQyxDQUFDO0FBVFcsb0JBQVksZ0JBU3ZCOzs7Ozs7O1VDVEY7VUFDQTs7VUFFQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTs7VUFFQTtVQUNBOztVQUVBO1VBQ0E7VUFDQTs7Ozs7Ozs7Ozs7O0FDdEJBLHlHQUF5QztBQWF6QyxNQUFNLENBQUMsaUJBQWlCLEdBQUcsQ0FBQyxPQUErQyxFQUFVLEVBQUUsQ0FDbkYsNEJBQVksRUFBQyxPQUFPLEVBQUUsQ0FBQyxFQUFFLEVBQUUsRUFBRSxDQUFDLEVBQUUsQ0FBQyxjQUFjLElBQUksQ0FBQyxDQUFDLENBQUM7QUFFMUQsTUFBTSxDQUFDLGVBQWUsR0FBRyxDQUFDLE9BQStDLEVBQVUsRUFBRSxDQUNqRiw0QkFBWSxFQUFDLE9BQU8sRUFBRSxDQUFDLEVBQUUsRUFBRSxFQUFFLENBQUMsRUFBRSxDQUFDLFlBQVksSUFBSSxDQUFDLENBQUMsQ0FBQztBQUV4RCxNQUFNLENBQUMsY0FBYyxHQUFHLENBQUMsS0FBWSxFQUFRLEVBQUU7SUFDM0MsS0FBSyxDQUFDLGNBQWMsRUFBRSxDQUFDO0FBQzNCLENBQUMsQ0FBQztBQUVGLE1BQU0sQ0FBQyxpQkFBaUIsR0FBRyxDQUFDLE9BQStDLEVBQUUsS0FBYSxFQUFFLEdBQVcsRUFBUSxFQUFFO0lBQzdHLElBQUksQ0FBQyxPQUFPO1FBQUUsT0FBTztJQUNyQixPQUFPLENBQUMsS0FBSyxFQUFFLENBQUM7SUFDaEIsT0FBTyxDQUFDLGlCQUFpQixDQUFDLEtBQUssRUFBRSxHQUFHLENBQUMsQ0FBQztBQUMxQyxDQUFDLENBQUMiLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuQmxhem9yL1RzTGliL3NlbGVjdGlvbi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlL3dlYnBhY2svYm9vdHN0cmFwIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5CbGF6b3IvVHNMaWIvd2luZG93SGVscGVyLnRzIl0sInNvdXJjZXNDb250ZW50IjpbImV4cG9ydCBjb25zdCBnZXRTZWxlY3Rpb246IChlbGVtZW50OiAoSFRNTElucHV0RWxlbWVudCB8IEhUTUxUZXh0QXJlYUVsZW1lbnQpLCBjYWxsYmFjazogKGVsZW1lbnQ6IChIVE1MSW5wdXRFbGVtZW50IHwgSFRNTFRleHRBcmVhRWxlbWVudCkpID0+IChudW1iZXIgfCBudWxsKSkgPT4gbnVtYmVyID0gKFxyXG4gICAgZWxlbWVudDogSFRNTElucHV0RWxlbWVudCB8IEhUTUxUZXh0QXJlYUVsZW1lbnQsXHJcbiAgICBjYWxsYmFjazogKGVsZW1lbnQ6IEhUTUxJbnB1dEVsZW1lbnQgfCBIVE1MVGV4dEFyZWFFbGVtZW50KSA9PiBudW1iZXIgfCBudWxsXHJcbik6IG51bWJlciA9PiB7XHJcbiAgICBpZiAoIWVsZW1lbnQgfHwgKGVsZW1lbnQudGFnTmFtZS50b0xvd2VyQ2FzZSgpICE9PSBcImlucHV0XCIgJiYgZWxlbWVudC50YWdOYW1lLnRvTG93ZXJDYXNlKCkgIT09IFwidGV4dGFyZWFcIikpIHtcclxuICAgICAgICBjb25zb2xlLmVycm9yKFwiRWxlbWVudCBtdXN0IGJlIGFuIGlucHV0IG9yIHRleHRhcmVhLlwiKTtcclxuICAgICAgICByZXR1cm4gLTE7XHJcbiAgICB9XHJcbiAgICByZXR1cm4gY2FsbGJhY2soZWxlbWVudCkgPz8gLTE7XHJcbn07XHJcbiIsIi8vIFRoZSBtb2R1bGUgY2FjaGVcbnZhciBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX18gPSB7fTtcblxuLy8gVGhlIHJlcXVpcmUgZnVuY3Rpb25cbmZ1bmN0aW9uIF9fd2VicGFja19yZXF1aXJlX18obW9kdWxlSWQpIHtcblx0Ly8gQ2hlY2sgaWYgbW9kdWxlIGlzIGluIGNhY2hlXG5cdHZhciBjYWNoZWRNb2R1bGUgPSBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX19bbW9kdWxlSWRdO1xuXHRpZiAoY2FjaGVkTW9kdWxlICE9PSB1bmRlZmluZWQpIHtcblx0XHRyZXR1cm4gY2FjaGVkTW9kdWxlLmV4cG9ydHM7XG5cdH1cblx0Ly8gQ3JlYXRlIGEgbmV3IG1vZHVsZSAoYW5kIHB1dCBpdCBpbnRvIHRoZSBjYWNoZSlcblx0dmFyIG1vZHVsZSA9IF9fd2VicGFja19tb2R1bGVfY2FjaGVfX1ttb2R1bGVJZF0gPSB7XG5cdFx0Ly8gbm8gbW9kdWxlLmlkIG5lZWRlZFxuXHRcdC8vIG5vIG1vZHVsZS5sb2FkZWQgbmVlZGVkXG5cdFx0ZXhwb3J0czoge31cblx0fTtcblxuXHQvLyBFeGVjdXRlIHRoZSBtb2R1bGUgZnVuY3Rpb25cblx0X193ZWJwYWNrX21vZHVsZXNfX1ttb2R1bGVJZF0obW9kdWxlLCBtb2R1bGUuZXhwb3J0cywgX193ZWJwYWNrX3JlcXVpcmVfXyk7XG5cblx0Ly8gUmV0dXJuIHRoZSBleHBvcnRzIG9mIHRoZSBtb2R1bGVcblx0cmV0dXJuIG1vZHVsZS5leHBvcnRzO1xufVxuXG4iLCJpbXBvcnQge2dldFNlbGVjdGlvbn0gZnJvbSBcIi4vc2VsZWN0aW9uXCI7XG5cbmV4cG9ydCB7fTtcbmRlY2xhcmUgZ2xvYmFsIHtcbiAgICAvLyBub2luc3BlY3Rpb24gSlNVbnVzZWRHbG9iYWxTeW1ib2xzXG4gICAgaW50ZXJmYWNlIFdpbmRvdyB7XG4gICAgICAgIGdldFNlbGVjdGlvblN0YXJ0OiAoZWxlbWVudDogSFRNTElucHV0RWxlbWVudCB8IEhUTUxUZXh0QXJlYUVsZW1lbnQpID0+IG51bWJlcjtcbiAgICAgICAgZ2V0U2VsZWN0aW9uRW5kOiAoZWxlbWVudDogSFRNTElucHV0RWxlbWVudCB8IEhUTUxUZXh0QXJlYUVsZW1lbnQpID0+IG51bWJlcjtcbiAgICAgICAgcHJldmVudERlZmF1bHQ6IChldmVudDogRXZlbnQpID0+IHZvaWQ7XG4gICAgICAgIHNldFNlbGVjdGlvblJhbmdlOiAoZWxlbWVudDogSFRNTElucHV0RWxlbWVudCB8IEhUTUxUZXh0QXJlYUVsZW1lbnQsIHN0YXJ0OiBudW1iZXIsIGVuZDogbnVtYmVyKSA9PiB2b2lkO1xuICAgIH1cbn1cblxud2luZG93LmdldFNlbGVjdGlvblN0YXJ0ID0gKGVsZW1lbnQ6IEhUTUxJbnB1dEVsZW1lbnQgfCBIVE1MVGV4dEFyZWFFbGVtZW50KTogbnVtYmVyID0+XG4gICAgZ2V0U2VsZWN0aW9uKGVsZW1lbnQsIChlbCkgPT4gZWwuc2VsZWN0aW9uU3RhcnQgfHwgMCk7XG5cbndpbmRvdy5nZXRTZWxlY3Rpb25FbmQgPSAoZWxlbWVudDogSFRNTElucHV0RWxlbWVudCB8IEhUTUxUZXh0QXJlYUVsZW1lbnQpOiBudW1iZXIgPT5cbiAgICBnZXRTZWxlY3Rpb24oZWxlbWVudCwgKGVsKSA9PiBlbC5zZWxlY3Rpb25FbmQgfHwgMCk7XG5cbndpbmRvdy5wcmV2ZW50RGVmYXVsdCA9IChldmVudDogRXZlbnQpOiB2b2lkID0+IHtcbiAgICBldmVudC5wcmV2ZW50RGVmYXVsdCgpO1xufTtcblxud2luZG93LnNldFNlbGVjdGlvblJhbmdlID0gKGVsZW1lbnQ6IEhUTUxJbnB1dEVsZW1lbnQgfCBIVE1MVGV4dEFyZWFFbGVtZW50LCBzdGFydDogbnVtYmVyLCBlbmQ6IG51bWJlcik6IHZvaWQgPT4ge1xuICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xuICAgIGVsZW1lbnQuZm9jdXMoKTtcbiAgICBlbGVtZW50LnNldFNlbGVjdGlvblJhbmdlKHN0YXJ0LCBlbmQpO1xufTtcbiJdLCJuYW1lcyI6W10sInNvdXJjZVJvb3QiOiIifQ==
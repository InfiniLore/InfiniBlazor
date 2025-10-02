/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Index.ts":
/*!********************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Index.ts ***!
  \********************************************************************/
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.infiniBlazor = exports.InfiniBlazor = void 0;
const ElementLib_1 = __webpack_require__(/*! ./Libs/ElementLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/ElementLib.ts");
const DocumentLib_1 = __webpack_require__(/*! ./Libs/DocumentLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/DocumentLib.ts");
const TextSelectionLib_1 = __webpack_require__(/*! ./Libs/TextSelectionLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/TextSelectionLib.ts");
const KeyListenerLib_1 = __webpack_require__(/*! ./Libs/KeyListenerLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/KeyListenerLib.ts");
class InfiniBlazor {
    constructor() {
        this.document = new DocumentLib_1.DocumentLib();
        this.element = new ElementLib_1.ElementLib();
        this.textSelection = new TextSelectionLib_1.TextSelectionLib();
        this.keyListener = new KeyListenerLib_1.KeyListenerLib();
    }
}
exports.InfiniBlazor = InfiniBlazor;
exports.infiniBlazor = new InfiniBlazor();
exports["default"] = exports.infiniBlazor;
if (typeof window !== 'undefined') {
    window.infiniBlazor = exports.infiniBlazor;
}


/***/ }),

/***/ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/DocumentLib.ts":
/*!*******************************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/DocumentLib.ts ***!
  \*******************************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.DocumentLib = void 0;
class DocumentLib {
    addOrUpdateElementAtHead(elementId, textContent) {
        if (!elementId)
            return;
        if (!textContent)
            return;
        const possibleElement = document.head.querySelector(`#${elementId}`);
        if (possibleElement && possibleElement instanceof HTMLStyleElement) {
            possibleElement.textContent = textContent;
            return;
        }
        else if (possibleElement) {
            document.head.removeChild(possibleElement);
        }
        const styleElement = document.createElement("style");
        styleElement.id = elementId;
        styleElement.textContent = textContent;
        document.head.appendChild(styleElement);
    }
}
exports.DocumentLib = DocumentLib;


/***/ }),

/***/ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/ElementLib.ts":
/*!******************************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/ElementLib.ts ***!
  \******************************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.ElementLib = void 0;
class ElementLib {
    setValue(element, value) {
        if (!element)
            return;
        if (!value)
            return;
        element.value = value;
    }
    setTextContent(element, text) {
        if (!element)
            return;
        if (!text)
            return;
        element.textContent = text;
    }
    getTextContent(element) {
        if (!element)
            return "";
        if (!element.textContent)
            return "";
        return element.textContent;
    }
    addHorizontalScroll(element, i) {
        if (!element)
            return;
        if (!i)
            return;
        element.scrollBy({ left: i, behavior: 'smooth' });
    }
    clickElement(element) {
        if (!element)
            return;
        element.click();
    }
    clickElementById(elementId) {
        if (!elementId)
            return;
        const element = document.getElementById(elementId);
        if (element === null)
            return;
        element.click();
    }
}
exports.ElementLib = ElementLib;


/***/ }),

/***/ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/KeyListenerLib.ts":
/*!**********************************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/KeyListenerLib.ts ***!
  \**********************************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.KeyListenerLib = void 0;
const keysToSkip = new Set(["u", "b", "i", "a"]);
const allowSpecialConditions = [
    (event, key) => event.ctrlKey && event.shiftKey && key === "i",
];
class KeyListenerLib {
    preventKeyDefault(event) {
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
    addPreventDefaultListener() {
        document.addEventListener("keydown", this.preventKeyDefault);
    }
    removePreventDefaultListener() {
        document.removeEventListener("keydown", this.preventKeyDefault);
    }
}
exports.KeyListenerLib = KeyListenerLib;


/***/ }),

/***/ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/TextSelectionLib.ts":
/*!************************************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/TextSelectionLib.ts ***!
  \************************************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.TextSelectionLib = void 0;
class TextSelectionLib {
    isValidInputElement(element) {
        if (!element)
            return false;
        return element.tagName.toLowerCase() === "input" || element.tagName.toLowerCase() === "textarea";
    }
    getStartIndex(element) {
        if (!this.isValidInputElement(element))
            return -1;
        return element.selectionStart || 0;
    }
    getEndIndex(element) {
        if (!this.isValidInputElement(element))
            return -1;
        return element.selectionEnd || 0;
    }
    getAsTuple(element) {
        if (!this.isValidInputElement(element))
            return {
                Item1: -1,
                Item2: -1
            };
        return {
            Item1: element.selectionStart || 0,
            Item2: element.selectionEnd || 0
        };
    }
    setRange(element, start, end) {
        if (!this.isValidInputElement(element)) {
            console.warn("invalid element");
            return;
        }
        if (!Number.isInteger(start) || start < 0)
            return;
        if (!Number.isInteger(end) || end < 0)
            return;
        if (start > end)
            return;
        element.focus();
        element.setSelectionRange(start, end);
    }
}
exports.TextSelectionLib = TextSelectionLib;


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
/******/ 	
/******/ 	// startup
/******/ 	// Load entry module and return exports
/******/ 	// This entry module is referenced by other modules so it can't be inlined
/******/ 	var __webpack_exports__ = __webpack_require__("./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Index.ts");
/******/ 	
/******/ })()
;
//# sourceMappingURL=data:application/json;charset=utf-8;base64,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QUMsSUFBSSxDQUFDLG1CQUFtQixDQUFDLE9BQU8sQ0FBQyxFQUFFLENBQUM7WUFDckMsT0FBTyxDQUFDLElBQUksQ0FBQyxpQkFBaUIsQ0FBQztZQUMvQixPQUFPO1FBQ1gsQ0FBQztRQUVELElBQUksQ0FBQyxNQUFNLENBQUMsU0FBUyxDQUFDLEtBQUssQ0FBQyxJQUFJLEtBQUssR0FBRyxDQUFDO1lBQUUsT0FBTztRQUNsRCxJQUFJLENBQUMsTUFBTSxDQUFDLFNBQVMsQ0FBQyxHQUFHLENBQUMsSUFBSSxHQUFHLEdBQUcsQ0FBQztZQUFFLE9BQU87UUFDOUMsSUFBSSxLQUFLLEdBQUcsR0FBRztZQUFFLE9BQU87UUFFeEIsT0FBTyxDQUFDLEtBQUssRUFBRSxDQUFDO1FBQ2hCLE9BQU8sQ0FBQyxpQkFBaUIsQ0FBQyxLQUFLLEVBQUUsR0FBRyxDQUFDLENBQUM7SUFDMUMsQ0FBQztDQUNKO0FBeENELDRDQXdDQzs7Ozs7OztVQ2xERDtVQUNBOztVQUVBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBOztVQUVBO1VBQ0E7O1VBRUE7VUFDQTtVQUNBOzs7O1VFdEJBO1VBQ0E7VUFDQTtVQUNBIiwic291cmNlcyI6WyJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci5Db3JlLkpzL1R5cGVzY3JpcHRMaWIvSW5kZXgudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci5Db3JlLkpzL1R5cGVzY3JpcHRMaWIvTGlicy9Eb2N1bWVudExpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9MaWJzL0VsZW1lbnRMaWIudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci5Db3JlLkpzL1R5cGVzY3JpcHRMaWIvTGlicy9LZXlMaXN0ZW5lckxpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9MaWJzL1RleHRTZWxlY3Rpb25MaWIudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2Jvb3RzdHJhcCIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlL3dlYnBhY2svYmVmb3JlLXN0YXJ0dXAiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL3N0YXJ0dXAiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2FmdGVyLXN0YXJ0dXAiXSwic291cmNlc0NvbnRlbnQiOlsiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuXHJcbmltcG9ydCB7RWxlbWVudExpYn0gZnJvbSBcIi4vTGlicy9FbGVtZW50TGliXCI7XHJcbmltcG9ydCB7RG9jdW1lbnRMaWJ9IGZyb20gXCIuL0xpYnMvRG9jdW1lbnRMaWJcIjtcclxuaW1wb3J0IHtUZXh0U2VsZWN0aW9uTGlifSBmcm9tIFwiLi9MaWJzL1RleHRTZWxlY3Rpb25MaWJcIjtcclxuaW1wb3J0IHtLZXlMaXN0ZW5lckxpYn0gZnJvbSBcIi4vTGlicy9LZXlMaXN0ZW5lckxpYlwiO1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG5leHBvcnQgY2xhc3MgSW5maW5pQmxhem9yIHtcclxuICAgIHB1YmxpYyBkb2N1bWVudCA6IERvY3VtZW50TGliID0gbmV3IERvY3VtZW50TGliKCk7XHJcbiAgICBwdWJsaWMgZWxlbWVudCA6IEVsZW1lbnRMaWIgPSBuZXcgRWxlbWVudExpYigpO1xyXG4gICAgcHVibGljIHRleHRTZWxlY3Rpb24gOiBUZXh0U2VsZWN0aW9uTGliID0gbmV3IFRleHRTZWxlY3Rpb25MaWIoKTtcclxuICAgIHB1YmxpYyBrZXlMaXN0ZW5lciA6IEtleUxpc3RlbmVyTGliID0gbmV3IEtleUxpc3RlbmVyTGliKCk7XHJcbn1cclxuXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuZXhwb3J0IGNvbnN0IGluZmluaUJsYXpvciA9IG5ldyBJbmZpbmlCbGF6b3IoKTtcclxuZXhwb3J0IGRlZmF1bHQgaW5maW5pQmxhem9yO1xyXG5cclxuZGVjbGFyZSBnbG9iYWwge1xyXG4gICAgaW50ZXJmYWNlIFdpbmRvdyB7XHJcbiAgICAgICAgaW5maW5pQmxhem9yOiBJbmZpbmlCbGF6b3I7XHJcbiAgICB9XHJcbn1cclxuXHJcbmlmICh0eXBlb2Ygd2luZG93ICE9PSAndW5kZWZpbmVkJykge1xyXG4gICAgd2luZG93LmluZmluaUJsYXpvciA9IGluZmluaUJsYXpvcjtcclxufSIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuZXhwb3J0IGNsYXNzIERvY3VtZW50TGliIHtcclxuICAgIFxyXG4gICAgLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG4gICAgcHVibGljIGFkZE9yVXBkYXRlRWxlbWVudEF0SGVhZChlbGVtZW50SWQ6IHN0cmluZywgdGV4dENvbnRlbnQ6IHN0cmluZyk6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudElkKSByZXR1cm47XHJcbiAgICAgICAgaWYgKCF0ZXh0Q29udGVudCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGNvbnN0IHBvc3NpYmxlRWxlbWVudCA9IGRvY3VtZW50LmhlYWQucXVlcnlTZWxlY3RvcihgIyR7ZWxlbWVudElkfWApO1xyXG4gICAgICAgIGlmIChwb3NzaWJsZUVsZW1lbnQgJiYgcG9zc2libGVFbGVtZW50IGluc3RhbmNlb2YgSFRNTFN0eWxlRWxlbWVudCkge1xyXG4gICAgICAgICAgICBwb3NzaWJsZUVsZW1lbnQudGV4dENvbnRlbnQgPSB0ZXh0Q29udGVudDtcclxuICAgICAgICAgICAgcmV0dXJuO1xyXG4gICAgICAgIH0gZWxzZSBpZiAocG9zc2libGVFbGVtZW50KSB7XHJcbiAgICAgICAgICAgIGRvY3VtZW50LmhlYWQucmVtb3ZlQ2hpbGQocG9zc2libGVFbGVtZW50KTtcclxuICAgICAgICB9XHJcblxyXG4gICAgICAgIGNvbnN0IHN0eWxlRWxlbWVudCA9IGRvY3VtZW50LmNyZWF0ZUVsZW1lbnQoXCJzdHlsZVwiKTtcclxuICAgICAgICBzdHlsZUVsZW1lbnQuaWQgPSBlbGVtZW50SWQ7XHJcbiAgICAgICAgc3R5bGVFbGVtZW50LnRleHRDb250ZW50ID0gdGV4dENvbnRlbnQ7XHJcbiAgICAgICAgZG9jdW1lbnQuaGVhZC5hcHBlbmRDaGlsZChzdHlsZUVsZW1lbnQpO1xyXG4gICAgfSAgICBcclxufVxyXG4iLCIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBub2luc3BlY3Rpb24gSlNVbnVzZWRHbG9iYWxTeW1ib2xzXHJcbmV4cG9ydCBjbGFzcyBFbGVtZW50TGliIHtcclxuICAgIHB1YmxpYyBzZXRWYWx1ZShlbGVtZW50OiBIVE1MSW5wdXRFbGVtZW50LCB2YWx1ZTogc3RyaW5nKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcbiAgICAgICAgaWYgKCF2YWx1ZSkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQudmFsdWUgPSB2YWx1ZTtcclxuICAgIH1cclxuICAgIFxyXG4gICAgcHVibGljIHNldFRleHRDb250ZW50KGVsZW1lbnQ6IEhUTUxFbGVtZW50LCB0ZXh0OiBzdHJpbmcpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgICAgICBpZiAoIXRleHQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LnRleHRDb250ZW50ID0gdGV4dDtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgZ2V0VGV4dENvbnRlbnQoZWxlbWVudDogSFRNTEVsZW1lbnQpOiBzdHJpbmcge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuIFwiXCI7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50LnRleHRDb250ZW50KSByZXR1cm4gXCJcIjtcclxuICAgICAgICBcclxuICAgICAgICByZXR1cm4gZWxlbWVudC50ZXh0Q29udGVudDtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgYWRkSG9yaXpvbnRhbFNjcm9sbChlbGVtZW50OiBIVE1MRWxlbWVudCwgaTogbnVtYmVyKSA6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghaSkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQuc2Nyb2xsQnkoeyBsZWZ0OiBpLCBiZWhhdmlvcjogJ3Ntb290aCcgfSk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGNsaWNrRWxlbWVudChlbGVtZW50OiBIVE1MRWxlbWVudCkgOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LmNsaWNrKCk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGNsaWNrRWxlbWVudEJ5SWQoZWxlbWVudElkOnN0cmluZykgOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnRJZCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGNvbnN0IGVsZW1lbnQgPSBkb2N1bWVudC5nZXRFbGVtZW50QnlJZChlbGVtZW50SWQpO1xyXG4gICAgICAgIGlmIChlbGVtZW50ID09PSBudWxsKSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC5jbGljaygpO1xyXG4gICAgfVxyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmltcG9ydCB7S2V5Q29uZGl0aW9ufSBmcm9tIFwiLi4vQ29udHJhY3RzL0tleUNvbmRpdGlvblwiO1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuY29uc3Qga2V5c1RvU2tpcDogU2V0PHN0cmluZz4gPSBuZXcgU2V0KFtcInVcIiwgXCJiXCIsIFwiaVwiLCBcImFcIl0pO1xyXG5jb25zdCBhbGxvd1NwZWNpYWxDb25kaXRpb25zOiBLZXlDb25kaXRpb25bXSA9IFtcclxuICAgIChldmVudCwga2V5KSA9PiBldmVudC5jdHJsS2V5ICYmIGV2ZW50LnNoaWZ0S2V5ICYmIGtleSA9PT0gXCJpXCIsIC8vIFNraXAgYEN0cmwrU2hpZnQrSWBcclxuXTtcclxuXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuZXhwb3J0IGNsYXNzIEtleUxpc3RlbmVyTGliIHtcclxuICAgIC8vIE1haW4gZnVuY3Rpb24gdG8gcHJldmVudCBkZWZhdWx0IGJlaGF2aW9yXHJcbiAgICBwcml2YXRlIHByZXZlbnRLZXlEZWZhdWx0KGV2ZW50OiBLZXlib2FyZEV2ZW50KTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCFldmVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghZXZlbnQuY3RybEtleSkgcmV0dXJuO1xyXG5cclxuICAgICAgICBjb25zdCBrZXkgPSBldmVudC5rZXkudG9Mb3dlckNhc2UoKTtcclxuICAgICAgICBpZiAoYWxsb3dTcGVjaWFsQ29uZGl0aW9ucy5zb21lKGNvbmRpdGlvbiA9PiBjb25kaXRpb24oZXZlbnQsIGtleSkpKSByZXR1cm47XHJcblxyXG4gICAgICAgIC8vIEJsb2NrIGRlZmF1bHQgYmVoYXZpb3IgZm9yIGtleXMgaW4gdGhlIGtleXNUb1NraXAgc2V0XHJcbiAgICAgICAgaWYgKCFrZXlzVG9Ta2lwLmhhcyhrZXkpKSByZXR1cm47XHJcbiAgICAgICAgZXZlbnQucHJldmVudERlZmF1bHQoKTtcclxuICAgIH1cclxuXHJcblxyXG4gICAgcHVibGljIGFkZFByZXZlbnREZWZhdWx0TGlzdGVuZXIoKSA6IHZvaWQge1xyXG4gICAgICAgIGRvY3VtZW50LmFkZEV2ZW50TGlzdGVuZXIoXCJrZXlkb3duXCIsdGhpcy5wcmV2ZW50S2V5RGVmYXVsdClcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgcmVtb3ZlUHJldmVudERlZmF1bHRMaXN0ZW5lcigpIDogdm9pZCB7XHJcbiAgICAgICAgZG9jdW1lbnQucmVtb3ZlRXZlbnRMaXN0ZW5lcihcImtleWRvd25cIiwgdGhpcy5wcmV2ZW50S2V5RGVmYXVsdClcclxuICAgIH1cclxufSIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5pbXBvcnQge0lJbnB1dEVsZW1lbnR9IGZyb20gXCIuLi9Db250cmFjdHMvSUlucHV0RWxlbWVudFwiO1xyXG5pbXBvcnQge0NTaGFycFR1cGxlfSBmcm9tIFwiLi4vQ29udHJhY3RzL0NTaGFycFR1cGxlXCI7XHJcblxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG5leHBvcnQgY2xhc3MgVGV4dFNlbGVjdGlvbkxpYiB7XHJcbiAgICBwcml2YXRlIGlzVmFsaWRJbnB1dEVsZW1lbnQoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IGJvb2xlYW4ge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuIGZhbHNlO1xyXG4gICAgICAgIHJldHVybiBlbGVtZW50LnRhZ05hbWUudG9Mb3dlckNhc2UoKSA9PT0gXCJpbnB1dFwiIHx8IGVsZW1lbnQudGFnTmFtZS50b0xvd2VyQ2FzZSgpID09PSBcInRleHRhcmVhXCI7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldFN0YXJ0SW5kZXgoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IG51bWJlciB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzVmFsaWRJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybiAtMTtcclxuICAgICAgICByZXR1cm4gZWxlbWVudC5zZWxlY3Rpb25TdGFydCB8fCAwO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBnZXRFbmRJbmRleChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogbnVtYmVyIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNWYWxpZElucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuIC0xO1xyXG4gICAgICAgIHJldHVybiBlbGVtZW50LnNlbGVjdGlvbkVuZCB8fCAwO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBnZXRBc1R1cGxlKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiBDU2hhcnBUdXBsZTxudW1iZXIsIG51bWJlcj4ge1xyXG4gICAgICAgIGlmICghdGhpcy5pc1ZhbGlkSW5wdXRFbGVtZW50KGVsZW1lbnQpKSByZXR1cm4ge1xyXG4gICAgICAgICAgICBJdGVtMTogLTEsXHJcbiAgICAgICAgICAgIEl0ZW0yOiAtMVxyXG4gICAgICAgIH1cclxuICAgICAgICByZXR1cm4ge1xyXG4gICAgICAgICAgICBJdGVtMTogZWxlbWVudC5zZWxlY3Rpb25TdGFydCB8fCAwLFxyXG4gICAgICAgICAgICBJdGVtMjogZWxlbWVudC5zZWxlY3Rpb25FbmQgfHwgMFxyXG4gICAgICAgIH07XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIHNldFJhbmdlKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQsIHN0YXJ0OiBudW1iZXIsIGVuZDogbnVtYmVyKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzVmFsaWRJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHtcclxuICAgICAgICAgICAgY29uc29sZS53YXJuKFwiaW52YWxpZCBlbGVtZW50XCIpXHJcbiAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICB9XHJcbiAgICAgICAgXHJcbiAgICAgICAgaWYgKCFOdW1iZXIuaXNJbnRlZ2VyKHN0YXJ0KSB8fCBzdGFydCA8IDApIHJldHVybjtcclxuICAgICAgICBpZiAoIU51bWJlci5pc0ludGVnZXIoZW5kKSB8fCBlbmQgPCAwKSByZXR1cm47XHJcbiAgICAgICAgaWYgKHN0YXJ0ID4gZW5kKSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC5mb2N1cygpO1xyXG4gICAgICAgIGVsZW1lbnQuc2V0U2VsZWN0aW9uUmFuZ2Uoc3RhcnQsIGVuZCk7XHJcbiAgICB9XHJcbn0iLCIvLyBUaGUgbW9kdWxlIGNhY2hlXG52YXIgX193ZWJwYWNrX21vZHVsZV9jYWNoZV9fID0ge307XG5cbi8vIFRoZSByZXF1aXJlIGZ1bmN0aW9uXG5mdW5jdGlvbiBfX3dlYnBhY2tfcmVxdWlyZV9fKG1vZHVsZUlkKSB7XG5cdC8vIENoZWNrIGlmIG1vZHVsZSBpcyBpbiBjYWNoZVxuXHR2YXIgY2FjaGVkTW9kdWxlID0gX193ZWJwYWNrX21vZHVsZV9jYWNoZV9fW21vZHVsZUlkXTtcblx0aWYgKGNhY2hlZE1vZHVsZSAhPT0gdW5kZWZpbmVkKSB7XG5cdFx0cmV0dXJuIGNhY2hlZE1vZHVsZS5leHBvcnRzO1xuXHR9XG5cdC8vIENyZWF0ZSBhIG5ldyBtb2R1bGUgKGFuZCBwdXQgaXQgaW50byB0aGUgY2FjaGUpXG5cdHZhciBtb2R1bGUgPSBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX19bbW9kdWxlSWRdID0ge1xuXHRcdC8vIG5vIG1vZHVsZS5pZCBuZWVkZWRcblx0XHQvLyBubyBtb2R1bGUubG9hZGVkIG5lZWRlZFxuXHRcdGV4cG9ydHM6IHt9XG5cdH07XG5cblx0Ly8gRXhlY3V0ZSB0aGUgbW9kdWxlIGZ1bmN0aW9uXG5cdF9fd2VicGFja19tb2R1bGVzX19bbW9kdWxlSWRdKG1vZHVsZSwgbW9kdWxlLmV4cG9ydHMsIF9fd2VicGFja19yZXF1aXJlX18pO1xuXG5cdC8vIFJldHVybiB0aGUgZXhwb3J0cyBvZiB0aGUgbW9kdWxlXG5cdHJldHVybiBtb2R1bGUuZXhwb3J0cztcbn1cblxuIiwiIiwiLy8gc3RhcnR1cFxuLy8gTG9hZCBlbnRyeSBtb2R1bGUgYW5kIHJldHVybiBleHBvcnRzXG4vLyBUaGlzIGVudHJ5IG1vZHVsZSBpcyByZWZlcmVuY2VkIGJ5IG90aGVyIG1vZHVsZXMgc28gaXQgY2FuJ3QgYmUgaW5saW5lZFxudmFyIF9fd2VicGFja19leHBvcnRzX18gPSBfX3dlYnBhY2tfcmVxdWlyZV9fKFwiLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3IuQ29yZS5Kcy9UeXBlc2NyaXB0TGliL0luZGV4LnRzXCIpO1xuIiwiIl0sIm5hbWVzIjpbXSwic291cmNlUm9vdCI6IiJ9
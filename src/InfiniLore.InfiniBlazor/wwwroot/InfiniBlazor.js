/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Index.ts":
/*!********************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Index.ts ***!
  \********************************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.infiniBlazor = exports.InfiniBlazor = void 0;
const ElementLib_1 = __importDefault(__webpack_require__(/*! ./Libs/ElementLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/ElementLib.ts"));
const DocumentLib_1 = __webpack_require__(/*! ./Libs/DocumentLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/DocumentLib.ts");
const TextSelectionLib_1 = __webpack_require__(/*! ./Libs/TextSelectionLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/TextSelectionLib.ts");
const KeyListenerLib_1 = __webpack_require__(/*! ./Libs/KeyListenerLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/KeyListenerLib.ts");
const HighlightLib_1 = __webpack_require__(/*! ./Libs/HighlightLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/HighlightLib.ts");
class InfiniBlazor {
    constructor() {
        this.document = new DocumentLib_1.DocumentLib();
        this.elements = new ElementLib_1.default();
        this.textSelection = new TextSelectionLib_1.TextSelectionLib();
        this.keyListener = new KeyListenerLib_1.KeyListenerLib();
        this.highlight = new HighlightLib_1.HighlightLib();
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
class ElementLib {
    setValue(element, value) {
        if (!element)
            return;
        if (!value)
            return;
        element.value = value;
    }
    setValueSelectionAware(element, text) {
        console.log(text);
        if (!element)
            return;
        if (!text)
            return;
        const start = element.selectionStart || 0;
        const end = element.selectionEnd || 0;
        element.value = text;
        element.setSelectionRange(start, end);
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
exports["default"] = ElementLib;


/***/ }),

/***/ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/HighlightLib.ts":
/*!********************************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/HighlightLib.ts ***!
  \********************************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.HighlightLib = void 0;
class HighlightLib {
    isHighlightJsAvailable() {
        return typeof window !== 'undefined' && window.hljs !== undefined;
    }
    ensureElementDataset(element) {
        if (element.dataset)
            return;
        Object.defineProperty(element, 'dataset', {
            value: {},
            writable: true,
            enumerable: true,
            configurable: true
        });
    }
    highlightAll() {
        if (!this.isHighlightJsAvailable())
            return;
        window.hljs.highlightAll();
    }
    highlightElement(element) {
        if (!this.isHighlightJsAvailable())
            return;
        if (!element)
            return;
        this.ensureElementDataset(element);
        if (element.dataset && element.dataset.highlighted) {
            delete element.dataset.highlighted;
        }
        window.hljs.highlightElement(element);
    }
    setContentAndHighlight(element, content) {
        if (!this.isHighlightJsAvailable())
            return;
        if (!element)
            return;
        if (!content)
            return;
        element.textContent = content;
        this.highlightElement(element);
    }
    configure(options) {
        if (!this.isHighlightJsAvailable())
            return;
        window.hljs.configure(options);
    }
    getAvailableLanguages() {
        if (!this.isHighlightJsAvailable())
            return [];
        return window.hljs.listLanguages();
    }
}
exports.HighlightLib = HighlightLib;


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
/******/ 		__webpack_modules__[moduleId].call(module.exports, module, module.exports, __webpack_require__);
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
//# sourceMappingURL=data:application/json;charset=utf-8;base64,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QUFTLENBQUMsS0FBSyxDQUFDLElBQUksS0FBSyxHQUFHLENBQUM7WUFBRSxPQUFPO1FBQ2xELElBQUksQ0FBQyxNQUFNLENBQUMsU0FBUyxDQUFDLEdBQUcsQ0FBQyxJQUFJLEdBQUcsR0FBRyxDQUFDO1lBQUUsT0FBTztRQUM5QyxJQUFJLEtBQUssR0FBRyxHQUFHO1lBQUUsT0FBTztRQUV4QixPQUFPLENBQUMsS0FBSyxFQUFFLENBQUM7UUFDaEIsT0FBTyxDQUFDLGlCQUFpQixDQUFDLEtBQUssRUFBRSxHQUFHLENBQUMsQ0FBQztJQUMxQyxDQUFDO0NBQ0o7QUF4Q0QsNENBd0NDOzs7Ozs7O1VDbEREO1VBQ0E7O1VBRUE7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7O1VBRUE7VUFDQTs7VUFFQTtVQUNBO1VBQ0E7Ozs7VUV0QkE7VUFDQTtVQUNBO1VBQ0EiLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9JbmRleC50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9MaWJzL0RvY3VtZW50TGliLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3IuQ29yZS5Kcy9UeXBlc2NyaXB0TGliL0xpYnMvRWxlbWVudExpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9MaWJzL0hpZ2hsaWdodExpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9MaWJzL0tleUxpc3RlbmVyTGliLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3IuQ29yZS5Kcy9UeXBlc2NyaXB0TGliL0xpYnMvVGV4dFNlbGVjdGlvbkxpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlL3dlYnBhY2svYm9vdHN0cmFwIiwid2VicGFjazovL2luZmluaWxvcmUvd2VicGFjay9iZWZvcmUtc3RhcnR1cCIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlL3dlYnBhY2svc3RhcnR1cCIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlL3dlYnBhY2svYWZ0ZXItc3RhcnR1cCJdLCJzb3VyY2VzQ29udGVudCI6WyIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG5cclxuaW1wb3J0IEVsZW1lbnRMaWIgZnJvbSBcIi4vTGlicy9FbGVtZW50TGliXCI7XHJcbmltcG9ydCB7RG9jdW1lbnRMaWJ9IGZyb20gXCIuL0xpYnMvRG9jdW1lbnRMaWJcIjtcclxuaW1wb3J0IHtUZXh0U2VsZWN0aW9uTGlifSBmcm9tIFwiLi9MaWJzL1RleHRTZWxlY3Rpb25MaWJcIjtcclxuaW1wb3J0IHtLZXlMaXN0ZW5lckxpYn0gZnJvbSBcIi4vTGlicy9LZXlMaXN0ZW5lckxpYlwiO1xyXG5pbXBvcnQge0hpZ2hsaWdodExpYn0gZnJvbSBcIi4vTGlicy9IaWdobGlnaHRMaWJcIjtcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmV4cG9ydCBjbGFzcyBJbmZpbmlCbGF6b3Ige1xyXG4gICAgcHVibGljIGRvY3VtZW50IDogRG9jdW1lbnRMaWIgPSBuZXcgRG9jdW1lbnRMaWIoKTtcclxuICAgIHB1YmxpYyBlbGVtZW50cyA6IEVsZW1lbnRMaWIgPSBuZXcgRWxlbWVudExpYigpO1xyXG4gICAgcHVibGljIHRleHRTZWxlY3Rpb24gOiBUZXh0U2VsZWN0aW9uTGliID0gbmV3IFRleHRTZWxlY3Rpb25MaWIoKTtcclxuICAgIHB1YmxpYyBrZXlMaXN0ZW5lciA6IEtleUxpc3RlbmVyTGliID0gbmV3IEtleUxpc3RlbmVyTGliKCk7XHJcbiAgICBwdWJsaWMgaGlnaGxpZ2h0IDogSGlnaGxpZ2h0TGliID0gbmV3IEhpZ2hsaWdodExpYigpO1xyXG59XHJcblxyXG5leHBvcnQgY29uc3QgaW5maW5pQmxhem9yID0gbmV3IEluZmluaUJsYXpvcigpO1xyXG5leHBvcnQgZGVmYXVsdCBpbmZpbmlCbGF6b3I7XHJcblxyXG5kZWNsYXJlIGdsb2JhbCB7XHJcbiAgICBpbnRlcmZhY2UgV2luZG93IHtcclxuICAgICAgICBpbmZpbmlCbGF6b3I6IEluZmluaUJsYXpvcjtcclxuICAgICAgICBobGpzPzoge1xyXG4gICAgICAgICAgICBoaWdobGlnaHRBbGwoKTogdm9pZDtcclxuICAgICAgICAgICAgaGlnaGxpZ2h0RWxlbWVudChlbGVtZW50OiBIVE1MRWxlbWVudCk6IHZvaWQ7XHJcbiAgICAgICAgICAgIGNvbmZpZ3VyZShvcHRpb25zOiBhbnkpOiB2b2lkO1xyXG4gICAgICAgICAgICBsaXN0TGFuZ3VhZ2VzKCk6IHN0cmluZ1tdO1xyXG4gICAgICAgICAgICBnZXRMYW5ndWFnZShuYW1lOiBzdHJpbmcpOiBhbnk7XHJcbiAgICAgICAgfVxyXG4gICAgfVxyXG59XHJcblxyXG5cclxuaWYgKHR5cGVvZiB3aW5kb3cgIT09ICd1bmRlZmluZWQnKSB7XHJcbiAgICB3aW5kb3cuaW5maW5pQmxhem9yID0gaW5maW5pQmxhem9yO1xyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcblxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG5leHBvcnQgY2xhc3MgRG9jdW1lbnRMaWIge1xyXG4gICAgXHJcbiAgICAvLyBub2luc3BlY3Rpb24gSlNVbnVzZWRHbG9iYWxTeW1ib2xzXHJcbiAgICBwdWJsaWMgYWRkT3JVcGRhdGVFbGVtZW50QXRIZWFkKGVsZW1lbnRJZDogc3RyaW5nLCB0ZXh0Q29udGVudDogc3RyaW5nKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50SWQpIHJldHVybjtcclxuICAgICAgICBpZiAoIXRleHRDb250ZW50KSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgY29uc3QgcG9zc2libGVFbGVtZW50ID0gZG9jdW1lbnQuaGVhZC5xdWVyeVNlbGVjdG9yKGAjJHtlbGVtZW50SWR9YCk7XHJcbiAgICAgICAgaWYgKHBvc3NpYmxlRWxlbWVudCAmJiBwb3NzaWJsZUVsZW1lbnQgaW5zdGFuY2VvZiBIVE1MU3R5bGVFbGVtZW50KSB7XHJcbiAgICAgICAgICAgIHBvc3NpYmxlRWxlbWVudC50ZXh0Q29udGVudCA9IHRleHRDb250ZW50O1xyXG4gICAgICAgICAgICByZXR1cm47XHJcbiAgICAgICAgfSBlbHNlIGlmIChwb3NzaWJsZUVsZW1lbnQpIHtcclxuICAgICAgICAgICAgZG9jdW1lbnQuaGVhZC5yZW1vdmVDaGlsZChwb3NzaWJsZUVsZW1lbnQpO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgY29uc3Qgc3R5bGVFbGVtZW50ID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudChcInN0eWxlXCIpO1xyXG4gICAgICAgIHN0eWxlRWxlbWVudC5pZCA9IGVsZW1lbnRJZDtcclxuICAgICAgICBzdHlsZUVsZW1lbnQudGV4dENvbnRlbnQgPSB0ZXh0Q29udGVudDtcclxuICAgICAgICBkb2N1bWVudC5oZWFkLmFwcGVuZENoaWxkKHN0eWxlRWxlbWVudCk7XHJcbiAgICB9ICAgIFxyXG59XHJcbiIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5pbXBvcnQge0lJbnB1dEVsZW1lbnR9IGZyb20gXCIuLi9Db250cmFjdHMvSUlucHV0RWxlbWVudFwiO1xyXG5cclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuY2xhc3MgRWxlbWVudExpYiB7XHJcbiAgICBwdWJsaWMgc2V0VmFsdWUoZWxlbWVudDogSUlucHV0RWxlbWVudCwgdmFsdWU6IHN0cmluZyk6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghdmFsdWUpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LnZhbHVlID0gdmFsdWU7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIHNldFZhbHVlU2VsZWN0aW9uQXdhcmUoZWxlbWVudDogSUlucHV0RWxlbWVudCwgdGV4dDogc3RyaW5nKTogdm9pZCB7XHJcbiAgICAgICAgY29uc29sZS5sb2codGV4dCk7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcbiAgICAgICAgaWYgKCF0ZXh0KSByZXR1cm47XHJcblxyXG4gICAgICAgIGNvbnN0IHN0YXJ0ID0gZWxlbWVudC5zZWxlY3Rpb25TdGFydCB8fCAwO1xyXG4gICAgICAgIGNvbnN0IGVuZCA9IGVsZW1lbnQuc2VsZWN0aW9uRW5kIHx8IDA7XHJcbiAgICAgICAgZWxlbWVudC52YWx1ZSA9IHRleHQ7XHJcbiAgICAgICAgZWxlbWVudC5zZXRTZWxlY3Rpb25SYW5nZShzdGFydCwgZW5kKTtcclxuICAgIH1cclxuICAgIFxyXG4gICAgcHVibGljIHNldFRleHRDb250ZW50KGVsZW1lbnQ6IEhUTUxFbGVtZW50LCB0ZXh0OiBzdHJpbmcpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgICAgICBpZiAoIXRleHQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LnRleHRDb250ZW50ID0gdGV4dDtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgZ2V0VGV4dENvbnRlbnQoZWxlbWVudDogSFRNTEVsZW1lbnQpOiBzdHJpbmcge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuIFwiXCI7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50LnRleHRDb250ZW50KSByZXR1cm4gXCJcIjtcclxuICAgICAgICBcclxuICAgICAgICByZXR1cm4gZWxlbWVudC50ZXh0Q29udGVudDtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgYWRkSG9yaXpvbnRhbFNjcm9sbChlbGVtZW50OiBIVE1MRWxlbWVudCwgaTogbnVtYmVyKSA6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghaSkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQuc2Nyb2xsQnkoeyBsZWZ0OiBpLCBiZWhhdmlvcjogJ3Ntb290aCcgfSk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGNsaWNrRWxlbWVudChlbGVtZW50OiBIVE1MRWxlbWVudCkgOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LmNsaWNrKCk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGNsaWNrRWxlbWVudEJ5SWQoZWxlbWVudElkOnN0cmluZykgOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnRJZCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGNvbnN0IGVsZW1lbnQgPSBkb2N1bWVudC5nZXRFbGVtZW50QnlJZChlbGVtZW50SWQpO1xyXG4gICAgICAgIGlmIChlbGVtZW50ID09PSBudWxsKSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC5jbGljaygpO1xyXG4gICAgfVxyXG59XHJcblxyXG5leHBvcnQgZGVmYXVsdCBFbGVtZW50TGliIiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcblxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG5leHBvcnQgY2xhc3MgSGlnaGxpZ2h0TGliIHtcclxuICAgIHByaXZhdGUgaXNIaWdobGlnaHRKc0F2YWlsYWJsZSgpOiBib29sZWFuIHtcclxuICAgICAgICByZXR1cm4gdHlwZW9mIHdpbmRvdyAhPT0gJ3VuZGVmaW5lZCcgJiYgd2luZG93LmhsanMgIT09IHVuZGVmaW5lZDtcclxuICAgIH1cclxuICAgIFxyXG4gICAgcHJpdmF0ZSBlbnN1cmVFbGVtZW50RGF0YXNldChlbGVtZW50OiBIVE1MRWxlbWVudCk6IHZvaWQge1xyXG4gICAgICAgIGlmIChlbGVtZW50LmRhdGFzZXQpIHJldHVybjtcclxuICAgICAgICBPYmplY3QuZGVmaW5lUHJvcGVydHkoZWxlbWVudCwgJ2RhdGFzZXQnLCB7XHJcbiAgICAgICAgICAgIHZhbHVlOiB7fSxcclxuICAgICAgICAgICAgd3JpdGFibGU6IHRydWUsXHJcbiAgICAgICAgICAgIGVudW1lcmFibGU6IHRydWUsXHJcbiAgICAgICAgICAgIGNvbmZpZ3VyYWJsZTogdHJ1ZVxyXG4gICAgICAgIH0pO1xyXG4gICAgfVxyXG4gICAgXHJcbiAgICBwdWJsaWMgaGlnaGxpZ2h0QWxsKCk6IHZvaWQge1xyXG4gICAgICAgIGlmICghdGhpcy5pc0hpZ2hsaWdodEpzQXZhaWxhYmxlKCkpIHJldHVybjtcclxuICAgICAgICB3aW5kb3cuaGxqcyEuaGlnaGxpZ2h0QWxsKCk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGhpZ2hsaWdodEVsZW1lbnQoZWxlbWVudDogSFRNTEVsZW1lbnQpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNIaWdobGlnaHRKc0F2YWlsYWJsZSgpKSByZXR1cm47XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcblxyXG4gICAgICAgIHRoaXMuZW5zdXJlRWxlbWVudERhdGFzZXQoZWxlbWVudCk7XHJcbiAgICAgICAgaWYgKGVsZW1lbnQuZGF0YXNldCAmJiBlbGVtZW50LmRhdGFzZXQuaGlnaGxpZ2h0ZWQpIHtcclxuICAgICAgICAgICAgZGVsZXRlIGVsZW1lbnQuZGF0YXNldC5oaWdobGlnaHRlZDtcclxuICAgICAgICB9XHJcbiAgICAgICAgd2luZG93LmhsanMhLmhpZ2hsaWdodEVsZW1lbnQoZWxlbWVudCk7XHJcbiAgICB9XHJcbiAgICBcclxuICAgIHB1YmxpYyBzZXRDb250ZW50QW5kSGlnaGxpZ2h0KGVsZW1lbnQ6IEhUTUxFbGVtZW50LCBjb250ZW50OiBzdHJpbmcpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNIaWdobGlnaHRKc0F2YWlsYWJsZSgpKSByZXR1cm47XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcbiAgICAgICAgaWYgKCFjb250ZW50KSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC50ZXh0Q29udGVudCA9IGNvbnRlbnQ7XHJcbiAgICAgICAgdGhpcy5oaWdobGlnaHRFbGVtZW50KGVsZW1lbnQpO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBjb25maWd1cmUob3B0aW9uczogYW55KTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzSGlnaGxpZ2h0SnNBdmFpbGFibGUoKSkgcmV0dXJuO1xyXG4gICAgICAgIHdpbmRvdy5obGpzIS5jb25maWd1cmUob3B0aW9ucyk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldEF2YWlsYWJsZUxhbmd1YWdlcygpOiBzdHJpbmdbXSB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzSGlnaGxpZ2h0SnNBdmFpbGFibGUoKSkgcmV0dXJuIFtdO1xyXG4gICAgICAgIHJldHVybiB3aW5kb3cuaGxqcyEubGlzdExhbmd1YWdlcygpO1xyXG4gICAgfVxyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmltcG9ydCB7S2V5Q29uZGl0aW9ufSBmcm9tIFwiLi4vQ29udHJhY3RzL0tleUNvbmRpdGlvblwiO1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuY29uc3Qga2V5c1RvU2tpcDogU2V0PHN0cmluZz4gPSBuZXcgU2V0KFtcInVcIiwgXCJiXCIsIFwiaVwiLCBcImFcIl0pO1xyXG5jb25zdCBhbGxvd1NwZWNpYWxDb25kaXRpb25zOiBLZXlDb25kaXRpb25bXSA9IFtcclxuICAgIChldmVudCwga2V5KSA9PiBldmVudC5jdHJsS2V5ICYmIGV2ZW50LnNoaWZ0S2V5ICYmIGtleSA9PT0gXCJpXCIsIC8vIFNraXAgYEN0cmwrU2hpZnQrSWBcclxuXTtcclxuXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuZXhwb3J0IGNsYXNzIEtleUxpc3RlbmVyTGliIHtcclxuICAgIC8vIE1haW4gZnVuY3Rpb24gdG8gcHJldmVudCBkZWZhdWx0IGJlaGF2aW9yXHJcbiAgICBwcml2YXRlIHByZXZlbnRLZXlEZWZhdWx0KGV2ZW50OiBLZXlib2FyZEV2ZW50KTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCFldmVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghZXZlbnQuY3RybEtleSkgcmV0dXJuO1xyXG5cclxuICAgICAgICBjb25zdCBrZXkgPSBldmVudC5rZXkudG9Mb3dlckNhc2UoKTtcclxuICAgICAgICBpZiAoYWxsb3dTcGVjaWFsQ29uZGl0aW9ucy5zb21lKGNvbmRpdGlvbiA9PiBjb25kaXRpb24oZXZlbnQsIGtleSkpKSByZXR1cm47XHJcblxyXG4gICAgICAgIC8vIEJsb2NrIGRlZmF1bHQgYmVoYXZpb3IgZm9yIGtleXMgaW4gdGhlIGtleXNUb1NraXAgc2V0XHJcbiAgICAgICAgaWYgKCFrZXlzVG9Ta2lwLmhhcyhrZXkpKSByZXR1cm47XHJcbiAgICAgICAgZXZlbnQucHJldmVudERlZmF1bHQoKTtcclxuICAgIH1cclxuXHJcblxyXG4gICAgcHVibGljIGFkZFByZXZlbnREZWZhdWx0TGlzdGVuZXIoKSA6IHZvaWQge1xyXG4gICAgICAgIGRvY3VtZW50LmFkZEV2ZW50TGlzdGVuZXIoXCJrZXlkb3duXCIsdGhpcy5wcmV2ZW50S2V5RGVmYXVsdClcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgcmVtb3ZlUHJldmVudERlZmF1bHRMaXN0ZW5lcigpIDogdm9pZCB7XHJcbiAgICAgICAgZG9jdW1lbnQucmVtb3ZlRXZlbnRMaXN0ZW5lcihcImtleWRvd25cIiwgdGhpcy5wcmV2ZW50S2V5RGVmYXVsdClcclxuICAgIH1cclxufSIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5pbXBvcnQge0lJbnB1dEVsZW1lbnR9IGZyb20gXCIuLi9Db250cmFjdHMvSUlucHV0RWxlbWVudFwiO1xyXG5pbXBvcnQge0NTaGFycFR1cGxlfSBmcm9tIFwiLi4vQ29udHJhY3RzL0NTaGFycFR1cGxlXCI7XHJcblxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG5leHBvcnQgY2xhc3MgVGV4dFNlbGVjdGlvbkxpYiB7XHJcbiAgICBwcml2YXRlIGlzVmFsaWRJbnB1dEVsZW1lbnQoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IGJvb2xlYW4ge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuIGZhbHNlO1xyXG4gICAgICAgIHJldHVybiBlbGVtZW50LnRhZ05hbWUudG9Mb3dlckNhc2UoKSA9PT0gXCJpbnB1dFwiIHx8IGVsZW1lbnQudGFnTmFtZS50b0xvd2VyQ2FzZSgpID09PSBcInRleHRhcmVhXCI7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldFN0YXJ0SW5kZXgoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IG51bWJlciB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzVmFsaWRJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybiAtMTtcclxuICAgICAgICByZXR1cm4gZWxlbWVudC5zZWxlY3Rpb25TdGFydCB8fCAwO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBnZXRFbmRJbmRleChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogbnVtYmVyIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNWYWxpZElucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuIC0xO1xyXG4gICAgICAgIHJldHVybiBlbGVtZW50LnNlbGVjdGlvbkVuZCB8fCAwO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBnZXRBc1R1cGxlKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiBDU2hhcnBUdXBsZTxudW1iZXIsIG51bWJlcj4ge1xyXG4gICAgICAgIGlmICghdGhpcy5pc1ZhbGlkSW5wdXRFbGVtZW50KGVsZW1lbnQpKSByZXR1cm4ge1xyXG4gICAgICAgICAgICBJdGVtMTogLTEsXHJcbiAgICAgICAgICAgIEl0ZW0yOiAtMVxyXG4gICAgICAgIH1cclxuICAgICAgICByZXR1cm4ge1xyXG4gICAgICAgICAgICBJdGVtMTogZWxlbWVudC5zZWxlY3Rpb25TdGFydCB8fCAwLFxyXG4gICAgICAgICAgICBJdGVtMjogZWxlbWVudC5zZWxlY3Rpb25FbmQgfHwgMFxyXG4gICAgICAgIH07XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIHNldFJhbmdlKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQsIHN0YXJ0OiBudW1iZXIsIGVuZDogbnVtYmVyKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzVmFsaWRJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHtcclxuICAgICAgICAgICAgY29uc29sZS53YXJuKFwiaW52YWxpZCBlbGVtZW50XCIpXHJcbiAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICB9XHJcbiAgICAgICAgXHJcbiAgICAgICAgaWYgKCFOdW1iZXIuaXNJbnRlZ2VyKHN0YXJ0KSB8fCBzdGFydCA8IDApIHJldHVybjtcclxuICAgICAgICBpZiAoIU51bWJlci5pc0ludGVnZXIoZW5kKSB8fCBlbmQgPCAwKSByZXR1cm47XHJcbiAgICAgICAgaWYgKHN0YXJ0ID4gZW5kKSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC5mb2N1cygpO1xyXG4gICAgICAgIGVsZW1lbnQuc2V0U2VsZWN0aW9uUmFuZ2Uoc3RhcnQsIGVuZCk7XHJcbiAgICB9XHJcbn0iLCIvLyBUaGUgbW9kdWxlIGNhY2hlXG52YXIgX193ZWJwYWNrX21vZHVsZV9jYWNoZV9fID0ge307XG5cbi8vIFRoZSByZXF1aXJlIGZ1bmN0aW9uXG5mdW5jdGlvbiBfX3dlYnBhY2tfcmVxdWlyZV9fKG1vZHVsZUlkKSB7XG5cdC8vIENoZWNrIGlmIG1vZHVsZSBpcyBpbiBjYWNoZVxuXHR2YXIgY2FjaGVkTW9kdWxlID0gX193ZWJwYWNrX21vZHVsZV9jYWNoZV9fW21vZHVsZUlkXTtcblx0aWYgKGNhY2hlZE1vZHVsZSAhPT0gdW5kZWZpbmVkKSB7XG5cdFx0cmV0dXJuIGNhY2hlZE1vZHVsZS5leHBvcnRzO1xuXHR9XG5cdC8vIENyZWF0ZSBhIG5ldyBtb2R1bGUgKGFuZCBwdXQgaXQgaW50byB0aGUgY2FjaGUpXG5cdHZhciBtb2R1bGUgPSBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX19bbW9kdWxlSWRdID0ge1xuXHRcdC8vIG5vIG1vZHVsZS5pZCBuZWVkZWRcblx0XHQvLyBubyBtb2R1bGUubG9hZGVkIG5lZWRlZFxuXHRcdGV4cG9ydHM6IHt9XG5cdH07XG5cblx0Ly8gRXhlY3V0ZSB0aGUgbW9kdWxlIGZ1bmN0aW9uXG5cdF9fd2VicGFja19tb2R1bGVzX19bbW9kdWxlSWRdLmNhbGwobW9kdWxlLmV4cG9ydHMsIG1vZHVsZSwgbW9kdWxlLmV4cG9ydHMsIF9fd2VicGFja19yZXF1aXJlX18pO1xuXG5cdC8vIFJldHVybiB0aGUgZXhwb3J0cyBvZiB0aGUgbW9kdWxlXG5cdHJldHVybiBtb2R1bGUuZXhwb3J0cztcbn1cblxuIiwiIiwiLy8gc3RhcnR1cFxuLy8gTG9hZCBlbnRyeSBtb2R1bGUgYW5kIHJldHVybiBleHBvcnRzXG4vLyBUaGlzIGVudHJ5IG1vZHVsZSBpcyByZWZlcmVuY2VkIGJ5IG90aGVyIG1vZHVsZXMgc28gaXQgY2FuJ3QgYmUgaW5saW5lZFxudmFyIF9fd2VicGFja19leHBvcnRzX18gPSBfX3dlYnBhY2tfcmVxdWlyZV9fKFwiLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3IuQ29yZS5Kcy9UeXBlc2NyaXB0TGliL0luZGV4LnRzXCIpO1xuIiwiIl0sIm5hbWVzIjpbXSwic291cmNlUm9vdCI6IiJ9
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
var _a;
Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.infiniBlazor = exports.InfiniBlazor = void 0;
const ElementLib_1 = __importDefault(__webpack_require__(/*! ./Libs/ElementLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/ElementLib.ts"));
const DocumentLib_1 = __webpack_require__(/*! ./Libs/DocumentLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/DocumentLib.ts");
const TextSelectionLib_1 = __webpack_require__(/*! ./Libs/TextSelectionLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/TextSelectionLib.ts");
const KeyListenerLib_1 = __webpack_require__(/*! ./Libs/KeyListenerLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/KeyListenerLib.ts");
const HighlightLib_1 = __webpack_require__(/*! ./Libs/HighlightLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/HighlightLib.ts");
const MermaidLib_1 = __webpack_require__(/*! ./Libs/MermaidLib */ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/MermaidLib.ts");
class InfiniBlazor {
    constructor() {
        this.document = new DocumentLib_1.DocumentLib();
        this.elements = new ElementLib_1.default();
        this.textSelection = new TextSelectionLib_1.TextSelectionLib();
        this.keyListener = new KeyListenerLib_1.KeyListenerLib();
        this.highlight = new HighlightLib_1.HighlightLib();
        this.mermaid = new MermaidLib_1.MermaidLib();
    }
}
exports.InfiniBlazor = InfiniBlazor;
exports.infiniBlazor = new InfiniBlazor();
exports["default"] = exports.infiniBlazor;
if (typeof window !== 'undefined') {
    window.infiniBlazor = exports.infiniBlazor;
    if (exports.infiniBlazor.mermaid.isMermaidJsAvailable()) {
        (_a = window.mermaid) === null || _a === void 0 ? void 0 : _a.initialize({ startOnLoad: true });
    }
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
    clearValue(element) {
        if (!element)
            return;
        element.value = "";
    }
    setValue(element, value) {
        if (!element)
            return;
        if (!value)
            return;
        element.value = value;
    }
    setValueSelectionAware(element, text) {
        if (!element)
            return;
        if (!text)
            return;
        const start = element.selectionStart || 0;
        const end = element.selectionEnd || 0;
        if (element.value === text)
            return;
        const oldLength = element.value.length;
        const newLength = text.length;
        const diff = newLength - oldLength;
        element.value = text;
        element.setSelectionRange(start + diff, end + diff);
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
        const key = event.key.toLowerCase();
        if (key == "tab" && !event.shiftKey && !event.ctrlKey) {
            event.preventDefault();
            return;
        }
        if (!event.ctrlKey)
            return;
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

/***/ "./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/MermaidLib.ts":
/*!******************************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor.Core.Js/TypescriptLib/Libs/MermaidLib.ts ***!
  \******************************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.MermaidLib = void 0;
class MermaidLib {
    isMermaidJsAvailable() {
        return typeof window !== 'undefined' && window.mermaid !== undefined;
    }
}
exports.MermaidLib = MermaidLib;


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
//# sourceMappingURL=data:application/json;charset=utf-8;base64,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LENBQUM7UUFDaEIsT0FBTyxDQUFDLGlCQUFpQixDQUFDLEtBQUssRUFBRSxHQUFHLENBQUMsQ0FBQztJQUMxQyxDQUFDO0NBQ0o7QUF4Q0QsNENBd0NDOzs7Ozs7O1VDbEREO1VBQ0E7O1VBRUE7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7O1VBRUE7VUFDQTs7VUFFQTtVQUNBO1VBQ0E7Ozs7VUV0QkE7VUFDQTtVQUNBO1VBQ0EiLCJzb3VyY2VzIjpbIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9JbmRleC50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9MaWJzL0RvY3VtZW50TGliLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3IuQ29yZS5Kcy9UeXBlc2NyaXB0TGliL0xpYnMvRWxlbWVudExpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9MaWJzL0hpZ2hsaWdodExpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9MaWJzL0tleUxpc3RlbmVyTGliLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3IuQ29yZS5Kcy9UeXBlc2NyaXB0TGliL0xpYnMvTWVybWFpZExpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9MaWJzL1RleHRTZWxlY3Rpb25MaWIudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2Jvb3RzdHJhcCIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlL3dlYnBhY2svYmVmb3JlLXN0YXJ0dXAiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL3N0YXJ0dXAiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2FmdGVyLXN0YXJ0dXAiXSwic291cmNlc0NvbnRlbnQiOlsiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuXHJcbmltcG9ydCBFbGVtZW50TGliIGZyb20gXCIuL0xpYnMvRWxlbWVudExpYlwiO1xyXG5pbXBvcnQge0RvY3VtZW50TGlifSBmcm9tIFwiLi9MaWJzL0RvY3VtZW50TGliXCI7XHJcbmltcG9ydCB7VGV4dFNlbGVjdGlvbkxpYn0gZnJvbSBcIi4vTGlicy9UZXh0U2VsZWN0aW9uTGliXCI7XHJcbmltcG9ydCB7S2V5TGlzdGVuZXJMaWJ9IGZyb20gXCIuL0xpYnMvS2V5TGlzdGVuZXJMaWJcIjtcclxuaW1wb3J0IHtIaWdobGlnaHRMaWJ9IGZyb20gXCIuL0xpYnMvSGlnaGxpZ2h0TGliXCI7XHJcbmltcG9ydCB7TWVybWFpZExpYn0gZnJvbSBcIi4vTGlicy9NZXJtYWlkTGliXCI7XHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5leHBvcnQgY2xhc3MgSW5maW5pQmxhem9yIHtcclxuICAgIHB1YmxpYyBkb2N1bWVudCA6IERvY3VtZW50TGliID0gbmV3IERvY3VtZW50TGliKCk7XHJcbiAgICBwdWJsaWMgZWxlbWVudHMgOiBFbGVtZW50TGliID0gbmV3IEVsZW1lbnRMaWIoKTtcclxuICAgIHB1YmxpYyB0ZXh0U2VsZWN0aW9uIDogVGV4dFNlbGVjdGlvbkxpYiA9IG5ldyBUZXh0U2VsZWN0aW9uTGliKCk7XHJcbiAgICBwdWJsaWMga2V5TGlzdGVuZXIgOiBLZXlMaXN0ZW5lckxpYiA9IG5ldyBLZXlMaXN0ZW5lckxpYigpO1xyXG4gICAgcHVibGljIGhpZ2hsaWdodCA6IEhpZ2hsaWdodExpYiA9IG5ldyBIaWdobGlnaHRMaWIoKTtcclxuICAgIHB1YmxpYyBtZXJtYWlkOiBNZXJtYWlkTGliID0gbmV3IE1lcm1haWRMaWIoKTtcclxufVxyXG5cclxuZXhwb3J0IGNvbnN0IGluZmluaUJsYXpvciA9IG5ldyBJbmZpbmlCbGF6b3IoKTtcclxuZXhwb3J0IGRlZmF1bHQgaW5maW5pQmxhem9yO1xyXG5cclxuZGVjbGFyZSBnbG9iYWwge1xyXG4gICAgaW50ZXJmYWNlIFdpbmRvdyB7XHJcbiAgICAgICAgaW5maW5pQmxhem9yOiBJbmZpbmlCbGF6b3I7XHJcbiAgICAgICAgaGxqcz86IHtcclxuICAgICAgICAgICAgaGlnaGxpZ2h0QWxsKCk6IHZvaWQ7XHJcbiAgICAgICAgICAgIGhpZ2hsaWdodEVsZW1lbnQoZWxlbWVudDogSFRNTEVsZW1lbnQpOiB2b2lkO1xyXG4gICAgICAgICAgICBjb25maWd1cmUob3B0aW9uczogYW55KTogdm9pZDtcclxuICAgICAgICAgICAgbGlzdExhbmd1YWdlcygpOiBzdHJpbmdbXTtcclxuICAgICAgICAgICAgZ2V0TGFuZ3VhZ2UobmFtZTogc3RyaW5nKTogYW55O1xyXG4gICAgICAgIH1cclxuICAgICAgICBtZXJtYWlkPzoge1xyXG4gICAgICAgICAgICBpbml0aWFsaXplKGNvbmZpZzogYW55KTogdm9pZDtcclxuICAgICAgICB9XHJcbiAgICB9XHJcbn1cclxuXHJcblxyXG5pZiAodHlwZW9mIHdpbmRvdyAhPT0gJ3VuZGVmaW5lZCcpIHtcclxuICAgIHdpbmRvdy5pbmZpbmlCbGF6b3IgPSBpbmZpbmlCbGF6b3I7XHJcbiAgICBcclxuICAgIGlmIChpbmZpbmlCbGF6b3IubWVybWFpZC5pc01lcm1haWRKc0F2YWlsYWJsZSgpKSB7XHJcbiAgICAgICAgd2luZG93Lm1lcm1haWQ/LmluaXRpYWxpemUoe3N0YXJ0T25Mb2FkOiB0cnVlfSk7XHJcbiAgICB9XHJcbn0iLCIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBub2luc3BlY3Rpb24gSlNVbnVzZWRHbG9iYWxTeW1ib2xzXHJcbmV4cG9ydCBjbGFzcyBEb2N1bWVudExpYiB7XHJcbiAgICBcclxuICAgIC8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuICAgIHB1YmxpYyBhZGRPclVwZGF0ZUVsZW1lbnRBdEhlYWQoZWxlbWVudElkOiBzdHJpbmcsIHRleHRDb250ZW50OiBzdHJpbmcpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnRJZCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghdGV4dENvbnRlbnQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBjb25zdCBwb3NzaWJsZUVsZW1lbnQgPSBkb2N1bWVudC5oZWFkLnF1ZXJ5U2VsZWN0b3IoYCMke2VsZW1lbnRJZH1gKTtcclxuICAgICAgICBpZiAocG9zc2libGVFbGVtZW50ICYmIHBvc3NpYmxlRWxlbWVudCBpbnN0YW5jZW9mIEhUTUxTdHlsZUVsZW1lbnQpIHtcclxuICAgICAgICAgICAgcG9zc2libGVFbGVtZW50LnRleHRDb250ZW50ID0gdGV4dENvbnRlbnQ7XHJcbiAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICB9IGVsc2UgaWYgKHBvc3NpYmxlRWxlbWVudCkge1xyXG4gICAgICAgICAgICBkb2N1bWVudC5oZWFkLnJlbW92ZUNoaWxkKHBvc3NpYmxlRWxlbWVudCk7XHJcbiAgICAgICAgfVxyXG5cclxuICAgICAgICBjb25zdCBzdHlsZUVsZW1lbnQgPSBkb2N1bWVudC5jcmVhdGVFbGVtZW50KFwic3R5bGVcIik7XHJcbiAgICAgICAgc3R5bGVFbGVtZW50LmlkID0gZWxlbWVudElkO1xyXG4gICAgICAgIHN0eWxlRWxlbWVudC50ZXh0Q29udGVudCA9IHRleHRDb250ZW50O1xyXG4gICAgICAgIGRvY3VtZW50LmhlYWQuYXBwZW5kQ2hpbGQoc3R5bGVFbGVtZW50KTtcclxuICAgIH0gICAgXHJcbn1cclxuIiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmltcG9ydCB7SUlucHV0RWxlbWVudH0gZnJvbSBcIi4uL0NvbnRyYWN0cy9JSW5wdXRFbGVtZW50XCI7XHJcblxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG5jbGFzcyBFbGVtZW50TGliIHtcclxuICAgIHB1YmxpYyBjbGVhclZhbHVlKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LnZhbHVlID0gXCJcIjtcclxuICAgIH1cclxuICAgIFxyXG4gICAgcHVibGljIHNldFZhbHVlKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQsIHZhbHVlOiBzdHJpbmcpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgICAgICBpZiAoIXZhbHVlKSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC52YWx1ZSA9IHZhbHVlO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBzZXRWYWx1ZVNlbGVjdGlvbkF3YXJlKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQsIHRleHQ6IHN0cmluZyk6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghdGV4dCkgcmV0dXJuO1xyXG5cclxuICAgICAgICBjb25zdCBzdGFydCA9IGVsZW1lbnQuc2VsZWN0aW9uU3RhcnQgfHwgMDtcclxuICAgICAgICBjb25zdCBlbmQgPSBlbGVtZW50LnNlbGVjdGlvbkVuZCB8fCAwO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGlmIChlbGVtZW50LnZhbHVlID09PSB0ZXh0KSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgY29uc3Qgb2xkTGVuZ3RoID0gZWxlbWVudC52YWx1ZS5sZW5ndGg7XHJcbiAgICAgICAgY29uc3QgbmV3TGVuZ3RoID0gdGV4dC5sZW5ndGg7XHJcbiAgICAgICAgY29uc3QgZGlmZiA9IG5ld0xlbmd0aCAtIG9sZExlbmd0aDtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LnZhbHVlID0gdGV4dDtcclxuICAgICAgICBlbGVtZW50LnNldFNlbGVjdGlvblJhbmdlKHN0YXJ0ICsgZGlmZiwgZW5kICsgZGlmZik7XHJcbiAgICB9XHJcbiAgICBcclxuICAgIHB1YmxpYyBzZXRUZXh0Q29udGVudChlbGVtZW50OiBIVE1MRWxlbWVudCwgdGV4dDogc3RyaW5nKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcbiAgICAgICAgaWYgKCF0ZXh0KSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC50ZXh0Q29udGVudCA9IHRleHQ7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldFRleHRDb250ZW50KGVsZW1lbnQ6IEhUTUxFbGVtZW50KTogc3RyaW5nIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybiBcIlwiO1xyXG4gICAgICAgIGlmICghZWxlbWVudC50ZXh0Q29udGVudCkgcmV0dXJuIFwiXCI7XHJcbiAgICAgICAgXHJcbiAgICAgICAgcmV0dXJuIGVsZW1lbnQudGV4dENvbnRlbnQ7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGFkZEhvcml6b250YWxTY3JvbGwoZWxlbWVudDogSFRNTEVsZW1lbnQsIGk6IG51bWJlcikgOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgICAgICBpZiAoIWkpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LnNjcm9sbEJ5KHsgbGVmdDogaSwgYmVoYXZpb3I6ICdzbW9vdGgnIH0pO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBjbGlja0VsZW1lbnQoZWxlbWVudDogSFRNTEVsZW1lbnQpIDogdm9pZCB7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC5jbGljaygpO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBjbGlja0VsZW1lbnRCeUlkKGVsZW1lbnRJZDpzdHJpbmcpIDogdm9pZCB7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50SWQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBjb25zdCBlbGVtZW50ID0gZG9jdW1lbnQuZ2V0RWxlbWVudEJ5SWQoZWxlbWVudElkKTtcclxuICAgICAgICBpZiAoZWxlbWVudCA9PT0gbnVsbCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQuY2xpY2soKTtcclxuICAgIH1cclxufVxyXG5cclxuZXhwb3J0IGRlZmF1bHQgRWxlbWVudExpYiIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuZXhwb3J0IGNsYXNzIEhpZ2hsaWdodExpYiB7XHJcbiAgICBwcml2YXRlIGlzSGlnaGxpZ2h0SnNBdmFpbGFibGUoKTogYm9vbGVhbiB7XHJcbiAgICAgICAgcmV0dXJuIHR5cGVvZiB3aW5kb3cgIT09ICd1bmRlZmluZWQnICYmIHdpbmRvdy5obGpzICE9PSB1bmRlZmluZWQ7XHJcbiAgICB9XHJcbiAgICBcclxuICAgIHByaXZhdGUgZW5zdXJlRWxlbWVudERhdGFzZXQoZWxlbWVudDogSFRNTEVsZW1lbnQpOiB2b2lkIHtcclxuICAgICAgICBpZiAoZWxlbWVudC5kYXRhc2V0KSByZXR1cm47XHJcbiAgICAgICAgT2JqZWN0LmRlZmluZVByb3BlcnR5KGVsZW1lbnQsICdkYXRhc2V0Jywge1xyXG4gICAgICAgICAgICB2YWx1ZToge30sXHJcbiAgICAgICAgICAgIHdyaXRhYmxlOiB0cnVlLFxyXG4gICAgICAgICAgICBlbnVtZXJhYmxlOiB0cnVlLFxyXG4gICAgICAgICAgICBjb25maWd1cmFibGU6IHRydWVcclxuICAgICAgICB9KTtcclxuICAgIH1cclxuICAgIFxyXG4gICAgcHVibGljIGhpZ2hsaWdodEFsbCgpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNIaWdobGlnaHRKc0F2YWlsYWJsZSgpKSByZXR1cm47XHJcbiAgICAgICAgd2luZG93LmhsanMhLmhpZ2hsaWdodEFsbCgpO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBoaWdobGlnaHRFbGVtZW50KGVsZW1lbnQ6IEhUTUxFbGVtZW50KTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzSGlnaGxpZ2h0SnNBdmFpbGFibGUoKSkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG5cclxuICAgICAgICB0aGlzLmVuc3VyZUVsZW1lbnREYXRhc2V0KGVsZW1lbnQpO1xyXG4gICAgICAgIGlmIChlbGVtZW50LmRhdGFzZXQgJiYgZWxlbWVudC5kYXRhc2V0LmhpZ2hsaWdodGVkKSB7XHJcbiAgICAgICAgICAgIGRlbGV0ZSBlbGVtZW50LmRhdGFzZXQuaGlnaGxpZ2h0ZWQ7XHJcbiAgICAgICAgfVxyXG4gICAgICAgIHdpbmRvdy5obGpzIS5oaWdobGlnaHRFbGVtZW50KGVsZW1lbnQpO1xyXG4gICAgfVxyXG4gICAgXHJcbiAgICBwdWJsaWMgc2V0Q29udGVudEFuZEhpZ2hsaWdodChlbGVtZW50OiBIVE1MRWxlbWVudCwgY29udGVudDogc3RyaW5nKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzSGlnaGxpZ2h0SnNBdmFpbGFibGUoKSkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghY29udGVudCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQudGV4dENvbnRlbnQgPSBjb250ZW50O1xyXG4gICAgICAgIHRoaXMuaGlnaGxpZ2h0RWxlbWVudChlbGVtZW50KTtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgY29uZmlndXJlKG9wdGlvbnM6IGFueSk6IHZvaWQge1xyXG4gICAgICAgIGlmICghdGhpcy5pc0hpZ2hsaWdodEpzQXZhaWxhYmxlKCkpIHJldHVybjtcclxuICAgICAgICB3aW5kb3cuaGxqcyEuY29uZmlndXJlKG9wdGlvbnMpO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBnZXRBdmFpbGFibGVMYW5ndWFnZXMoKTogc3RyaW5nW10ge1xyXG4gICAgICAgIGlmICghdGhpcy5pc0hpZ2hsaWdodEpzQXZhaWxhYmxlKCkpIHJldHVybiBbXTtcclxuICAgICAgICByZXR1cm4gd2luZG93LmhsanMhLmxpc3RMYW5ndWFnZXMoKTtcclxuICAgIH1cclxufSIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5pbXBvcnQge0tleUNvbmRpdGlvbn0gZnJvbSBcIi4uL0NvbnRyYWN0cy9LZXlDb25kaXRpb25cIjtcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmNvbnN0IGtleXNUb1NraXA6IFNldDxzdHJpbmc+ID0gbmV3IFNldChbXCJ1XCIsIFwiYlwiLCBcImlcIiwgXCJhXCJdKTtcclxuY29uc3QgYWxsb3dTcGVjaWFsQ29uZGl0aW9uczogS2V5Q29uZGl0aW9uW10gPSBbXHJcbiAgICAoZXZlbnQsIGtleSkgPT4gZXZlbnQuY3RybEtleSAmJiBldmVudC5zaGlmdEtleSAmJiBrZXkgPT09IFwiaVwiLCAvLyBTa2lwIGBDdHJsK1NoaWZ0K0lgXHJcbl07XHJcblxyXG4vLyBub2luc3BlY3Rpb24gSlNVbnVzZWRHbG9iYWxTeW1ib2xzXHJcbmV4cG9ydCBjbGFzcyBLZXlMaXN0ZW5lckxpYiB7XHJcbiAgICAvLyBNYWluIGZ1bmN0aW9uIHRvIHByZXZlbnQgZGVmYXVsdCBiZWhhdmlvclxyXG4gICAgcHJpdmF0ZSBwcmV2ZW50S2V5RGVmYXVsdChldmVudDogS2V5Ym9hcmRFdmVudCk6IHZvaWQge1xyXG4gICAgICAgIGlmICghZXZlbnQpIHJldHVybjtcclxuICAgICAgICBjb25zdCBrZXkgPSBldmVudC5rZXkudG9Mb3dlckNhc2UoKTtcclxuXHJcbiAgICAgICAgLy8gU3BlY2lhbCB0YWIgYmVoYXZpb3VyXHJcbiAgICAgICAgaWYgKGtleSA9PSBcInRhYlwiICYmICFldmVudC5zaGlmdEtleSAmJiAhZXZlbnQuY3RybEtleSkge1xyXG4gICAgICAgICAgICBldmVudC5wcmV2ZW50RGVmYXVsdCgpO1xyXG4gICAgICAgICAgICByZXR1cm47XHJcbiAgICAgICAgfVxyXG5cclxuICAgICAgICAvLyBCbG9jayBkZWZhdWx0IGJlaGF2aW9yIGZvciBrZXlzIHRoYXQgYXJlIHJlZ2lzdGVyZWRcclxuICAgICAgICBpZiAoIWV2ZW50LmN0cmxLZXkpIHJldHVybjtcclxuICAgICAgICBpZiAoYWxsb3dTcGVjaWFsQ29uZGl0aW9ucy5zb21lKGNvbmRpdGlvbiA9PiBjb25kaXRpb24oZXZlbnQsIGtleSkpKSByZXR1cm47XHJcbiAgICAgICAgaWYgKCFrZXlzVG9Ta2lwLmhhcyhrZXkpKSByZXR1cm47XHJcbiAgICAgICAgZXZlbnQucHJldmVudERlZmF1bHQoKTtcclxuICAgIH1cclxuXHJcblxyXG4gICAgcHVibGljIGFkZFByZXZlbnREZWZhdWx0TGlzdGVuZXIoKSA6IHZvaWQge1xyXG4gICAgICAgIGRvY3VtZW50LmFkZEV2ZW50TGlzdGVuZXIoXCJrZXlkb3duXCIsdGhpcy5wcmV2ZW50S2V5RGVmYXVsdClcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgcmVtb3ZlUHJldmVudERlZmF1bHRMaXN0ZW5lcigpIDogdm9pZCB7XHJcbiAgICAgICAgZG9jdW1lbnQucmVtb3ZlRXZlbnRMaXN0ZW5lcihcImtleWRvd25cIiwgdGhpcy5wcmV2ZW50S2V5RGVmYXVsdClcclxuICAgIH1cclxufSIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmV4cG9ydCBjbGFzcyBNZXJtYWlkTGliIHtcclxuICAgIHB1YmxpYyBpc01lcm1haWRKc0F2YWlsYWJsZSgpOiBib29sZWFuIHtcclxuICAgICAgICByZXR1cm4gdHlwZW9mIHdpbmRvdyAhPT0gJ3VuZGVmaW5lZCcgJiYgd2luZG93Lm1lcm1haWQgIT09IHVuZGVmaW5lZDtcclxuICAgIH1cclxufSIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5pbXBvcnQge0lJbnB1dEVsZW1lbnR9IGZyb20gXCIuLi9Db250cmFjdHMvSUlucHV0RWxlbWVudFwiO1xyXG5pbXBvcnQge0NTaGFycFR1cGxlfSBmcm9tIFwiLi4vQ29udHJhY3RzL0NTaGFycFR1cGxlXCI7XHJcblxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG5leHBvcnQgY2xhc3MgVGV4dFNlbGVjdGlvbkxpYiB7XHJcbiAgICBwcml2YXRlIGlzVmFsaWRJbnB1dEVsZW1lbnQoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IGJvb2xlYW4ge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuIGZhbHNlO1xyXG4gICAgICAgIHJldHVybiBlbGVtZW50LnRhZ05hbWUudG9Mb3dlckNhc2UoKSA9PT0gXCJpbnB1dFwiIHx8IGVsZW1lbnQudGFnTmFtZS50b0xvd2VyQ2FzZSgpID09PSBcInRleHRhcmVhXCI7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldFN0YXJ0SW5kZXgoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IG51bWJlciB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzVmFsaWRJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybiAtMTtcclxuICAgICAgICByZXR1cm4gZWxlbWVudC5zZWxlY3Rpb25TdGFydCB8fCAwO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBnZXRFbmRJbmRleChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogbnVtYmVyIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNWYWxpZElucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuIC0xO1xyXG4gICAgICAgIHJldHVybiBlbGVtZW50LnNlbGVjdGlvbkVuZCB8fCAwO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBnZXRBc1R1cGxlKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiBDU2hhcnBUdXBsZTxudW1iZXIsIG51bWJlcj4ge1xyXG4gICAgICAgIGlmICghdGhpcy5pc1ZhbGlkSW5wdXRFbGVtZW50KGVsZW1lbnQpKSByZXR1cm4ge1xyXG4gICAgICAgICAgICBJdGVtMTogLTEsXHJcbiAgICAgICAgICAgIEl0ZW0yOiAtMVxyXG4gICAgICAgIH1cclxuICAgICAgICByZXR1cm4ge1xyXG4gICAgICAgICAgICBJdGVtMTogZWxlbWVudC5zZWxlY3Rpb25TdGFydCB8fCAwLFxyXG4gICAgICAgICAgICBJdGVtMjogZWxlbWVudC5zZWxlY3Rpb25FbmQgfHwgMFxyXG4gICAgICAgIH07XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIHNldFJhbmdlKGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQsIHN0YXJ0OiBudW1iZXIsIGVuZDogbnVtYmVyKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzVmFsaWRJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHtcclxuICAgICAgICAgICAgY29uc29sZS53YXJuKFwiaW52YWxpZCBlbGVtZW50XCIpXHJcbiAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICB9XHJcbiAgICAgICAgXHJcbiAgICAgICAgaWYgKCFOdW1iZXIuaXNJbnRlZ2VyKHN0YXJ0KSB8fCBzdGFydCA8IDApIHJldHVybjtcclxuICAgICAgICBpZiAoIU51bWJlci5pc0ludGVnZXIoZW5kKSB8fCBlbmQgPCAwKSByZXR1cm47XHJcbiAgICAgICAgaWYgKHN0YXJ0ID4gZW5kKSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC5mb2N1cygpO1xyXG4gICAgICAgIGVsZW1lbnQuc2V0U2VsZWN0aW9uUmFuZ2Uoc3RhcnQsIGVuZCk7XHJcbiAgICB9XHJcbn0iLCIvLyBUaGUgbW9kdWxlIGNhY2hlXG52YXIgX193ZWJwYWNrX21vZHVsZV9jYWNoZV9fID0ge307XG5cbi8vIFRoZSByZXF1aXJlIGZ1bmN0aW9uXG5mdW5jdGlvbiBfX3dlYnBhY2tfcmVxdWlyZV9fKG1vZHVsZUlkKSB7XG5cdC8vIENoZWNrIGlmIG1vZHVsZSBpcyBpbiBjYWNoZVxuXHR2YXIgY2FjaGVkTW9kdWxlID0gX193ZWJwYWNrX21vZHVsZV9jYWNoZV9fW21vZHVsZUlkXTtcblx0aWYgKGNhY2hlZE1vZHVsZSAhPT0gdW5kZWZpbmVkKSB7XG5cdFx0cmV0dXJuIGNhY2hlZE1vZHVsZS5leHBvcnRzO1xuXHR9XG5cdC8vIENyZWF0ZSBhIG5ldyBtb2R1bGUgKGFuZCBwdXQgaXQgaW50byB0aGUgY2FjaGUpXG5cdHZhciBtb2R1bGUgPSBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX19bbW9kdWxlSWRdID0ge1xuXHRcdC8vIG5vIG1vZHVsZS5pZCBuZWVkZWRcblx0XHQvLyBubyBtb2R1bGUubG9hZGVkIG5lZWRlZFxuXHRcdGV4cG9ydHM6IHt9XG5cdH07XG5cblx0Ly8gRXhlY3V0ZSB0aGUgbW9kdWxlIGZ1bmN0aW9uXG5cdF9fd2VicGFja19tb2R1bGVzX19bbW9kdWxlSWRdLmNhbGwobW9kdWxlLmV4cG9ydHMsIG1vZHVsZSwgbW9kdWxlLmV4cG9ydHMsIF9fd2VicGFja19yZXF1aXJlX18pO1xuXG5cdC8vIFJldHVybiB0aGUgZXhwb3J0cyBvZiB0aGUgbW9kdWxlXG5cdHJldHVybiBtb2R1bGUuZXhwb3J0cztcbn1cblxuIiwiIiwiLy8gc3RhcnR1cFxuLy8gTG9hZCBlbnRyeSBtb2R1bGUgYW5kIHJldHVybiBleHBvcnRzXG4vLyBUaGlzIGVudHJ5IG1vZHVsZSBpcyByZWZlcmVuY2VkIGJ5IG90aGVyIG1vZHVsZXMgc28gaXQgY2FuJ3QgYmUgaW5saW5lZFxudmFyIF9fd2VicGFja19leHBvcnRzX18gPSBfX3dlYnBhY2tfcmVxdWlyZV9fKFwiLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3IuQ29yZS5Kcy9UeXBlc2NyaXB0TGliL0luZGV4LnRzXCIpO1xuIiwiIl0sIm5hbWVzIjpbXSwic291cmNlUm9vdCI6IiJ9
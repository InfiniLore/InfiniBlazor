/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/InfiniBlazor.Core.Js/TypescriptLib/Index.ts":
/*!*********************************************************!*\
  !*** ./src/InfiniBlazor.Core.Js/TypescriptLib/Index.ts ***!
  \*********************************************************/
/***/ (function(__unused_webpack_module, exports, __webpack_require__) {


var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
var _a;
Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.infiniBlazor = exports.InfiniBlazor = void 0;
const ElementLib_1 = __importDefault(__webpack_require__(/*! ./Libs/ElementLib */ "./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/ElementLib.ts"));
const DocumentLib_1 = __webpack_require__(/*! ./Libs/DocumentLib */ "./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/DocumentLib.ts");
const TextSelectionLib_1 = __webpack_require__(/*! ./Libs/TextSelectionLib */ "./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/TextSelectionLib.ts");
const KeyListenerLib_1 = __webpack_require__(/*! ./Libs/KeyListenerLib */ "./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/KeyListenerLib.ts");
const HighlightLib_1 = __webpack_require__(/*! ./Libs/HighlightLib */ "./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/HighlightLib.ts");
const MermaidLib_1 = __webpack_require__(/*! ./Libs/MermaidLib */ "./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/MermaidLib.ts");
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

/***/ "./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/DocumentLib.ts":
/*!********************************************************************!*\
  !*** ./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/DocumentLib.ts ***!
  \********************************************************************/
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

/***/ "./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/ElementLib.ts":
/*!*******************************************************************!*\
  !*** ./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/ElementLib.ts ***!
  \*******************************************************************/
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

/***/ "./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/HighlightLib.ts":
/*!*********************************************************************!*\
  !*** ./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/HighlightLib.ts ***!
  \*********************************************************************/
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

/***/ "./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/KeyListenerLib.ts":
/*!***********************************************************************!*\
  !*** ./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/KeyListenerLib.ts ***!
  \***********************************************************************/
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

/***/ "./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/MermaidLib.ts":
/*!*******************************************************************!*\
  !*** ./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/MermaidLib.ts ***!
  \*******************************************************************/
/***/ (function(__unused_webpack_module, exports) {


var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.MermaidLib = void 0;
class MermaidLib {
    isMermaidJsAvailable() {
        return typeof window !== 'undefined' && window.mermaid !== undefined;
    }
    renderMermaidAsync(element) {
        return __awaiter(this, void 0, void 0, function* () {
            var _a;
            if (!element)
                return;
            if (!this.isMermaidJsAvailable()) {
                throw new Error("Mermaid is not available");
            }
            (_a = window.mermaid) === null || _a === void 0 ? void 0 : _a.run({
                nodes: [element],
            });
        });
    }
    renderMermaidWithContentAsync(element, content) {
        return __awaiter(this, void 0, void 0, function* () {
            var _a;
            if (!element)
                return;
            if (!content)
                return;
            if (!this.isMermaidJsAvailable()) {
                throw new Error("Mermaid is not available");
            }
            element.textContent = content;
            (_a = window.mermaid) === null || _a === void 0 ? void 0 : _a.run({
                nodes: [element],
            });
        });
    }
}
exports.MermaidLib = MermaidLib;


/***/ }),

/***/ "./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/TextSelectionLib.ts":
/*!*************************************************************************!*\
  !*** ./src/InfiniBlazor.Core.Js/TypescriptLib/Libs/TextSelectionLib.ts ***!
  \*************************************************************************/
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
/******/ 	var __webpack_exports__ = __webpack_require__("./src/InfiniBlazor.Core.Js/TypescriptLib/Index.ts");
/******/ 	
/******/ })()
;
//# sourceMappingURL=data:application/json;charset=utf-8;base64,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QUFDLE9BQXNCO1FBQ3BDLElBQUksQ0FBQyxJQUFJLENBQUMsbUJBQW1CLENBQUMsT0FBTyxDQUFDO1lBQUUsT0FBTztnQkFDM0MsS0FBSyxFQUFFLENBQUMsQ0FBQztnQkFDVCxLQUFLLEVBQUUsQ0FBQyxDQUFDO2FBQ1o7UUFDRCxPQUFPO1lBQ0gsS0FBSyxFQUFFLE9BQU8sQ0FBQyxjQUFjLElBQUksQ0FBQztZQUNsQyxLQUFLLEVBQUUsT0FBTyxDQUFDLFlBQVksSUFBSSxDQUFDO1NBQ25DLENBQUM7SUFDTixDQUFDO0lBRU0sUUFBUSxDQUFDLE9BQXNCLEVBQUUsS0FBYSxFQUFFLEdBQVc7UUFDOUQsSUFBSSxDQUFDLElBQUksQ0FBQyxtQkFBbUIsQ0FBQyxPQUFPLENBQUMsRUFBRSxDQUFDO1lBQ3JDLE9BQU8sQ0FBQyxJQUFJLENBQUMsaUJBQWlCLENBQUM7WUFDL0IsT0FBTztRQUNYLENBQUM7UUFFRCxJQUFJLENBQUMsTUFBTSxDQUFDLFNBQVMsQ0FBQyxLQUFLLENBQUMsSUFBSSxLQUFLLEdBQUcsQ0FBQztZQUFFLE9BQU87UUFDbEQsSUFBSSxDQUFDLE1BQU0sQ0FBQyxTQUFTLENBQUMsR0FBRyxDQUFDLElBQUksR0FBRyxHQUFHLENBQUM7WUFBRSxPQUFPO1FBQzlDLElBQUksS0FBSyxHQUFHLEdBQUc7WUFBRSxPQUFPO1FBRXhCLE9BQU8sQ0FBQyxLQUFLLEVBQUUsQ0FBQztRQUNoQixPQUFPLENBQUMsaUJBQWlCLENBQUMsS0FBSyxFQUFFLEdBQUcsQ0FBQyxDQUFDO0lBQzFDLENBQUM7Q0FDSjtBQXhDRCw0Q0F3Q0M7Ozs7Ozs7VUNsREQ7VUFDQTs7VUFFQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTs7VUFFQTtVQUNBOztVQUVBO1VBQ0E7VUFDQTs7OztVRXRCQTtVQUNBO1VBQ0E7VUFDQSIsInNvdXJjZXMiOlsid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9JbmRleC50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUJsYXpvci5Db3JlLkpzL1R5cGVzY3JpcHRMaWIvTGlicy9Eb2N1bWVudExpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUJsYXpvci5Db3JlLkpzL1R5cGVzY3JpcHRMaWIvTGlicy9FbGVtZW50TGliLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9MaWJzL0hpZ2hsaWdodExpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUJsYXpvci5Db3JlLkpzL1R5cGVzY3JpcHRMaWIvTGlicy9LZXlMaXN0ZW5lckxpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUJsYXpvci5Db3JlLkpzL1R5cGVzY3JpcHRMaWIvTGlicy9NZXJtYWlkTGliLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9MaWJzL1RleHRTZWxlY3Rpb25MaWIudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2Jvb3RzdHJhcCIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlL3dlYnBhY2svYmVmb3JlLXN0YXJ0dXAiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL3N0YXJ0dXAiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2FmdGVyLXN0YXJ0dXAiXSwic291cmNlc0NvbnRlbnQiOlsiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuXHJcbmltcG9ydCBFbGVtZW50TGliIGZyb20gXCIuL0xpYnMvRWxlbWVudExpYlwiO1xyXG5pbXBvcnQge0RvY3VtZW50TGlifSBmcm9tIFwiLi9MaWJzL0RvY3VtZW50TGliXCI7XHJcbmltcG9ydCB7VGV4dFNlbGVjdGlvbkxpYn0gZnJvbSBcIi4vTGlicy9UZXh0U2VsZWN0aW9uTGliXCI7XHJcbmltcG9ydCB7S2V5TGlzdGVuZXJMaWJ9IGZyb20gXCIuL0xpYnMvS2V5TGlzdGVuZXJMaWJcIjtcclxuaW1wb3J0IHtIaWdobGlnaHRMaWJ9IGZyb20gXCIuL0xpYnMvSGlnaGxpZ2h0TGliXCI7XHJcbmltcG9ydCB7TWVybWFpZExpYn0gZnJvbSBcIi4vTGlicy9NZXJtYWlkTGliXCI7XHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5leHBvcnQgY2xhc3MgSW5maW5pQmxhem9yIHtcclxuICAgIHB1YmxpYyBkb2N1bWVudCA6IERvY3VtZW50TGliID0gbmV3IERvY3VtZW50TGliKCk7XHJcbiAgICBwdWJsaWMgZWxlbWVudHMgOiBFbGVtZW50TGliID0gbmV3IEVsZW1lbnRMaWIoKTtcclxuICAgIHB1YmxpYyB0ZXh0U2VsZWN0aW9uIDogVGV4dFNlbGVjdGlvbkxpYiA9IG5ldyBUZXh0U2VsZWN0aW9uTGliKCk7XHJcbiAgICBwdWJsaWMga2V5TGlzdGVuZXIgOiBLZXlMaXN0ZW5lckxpYiA9IG5ldyBLZXlMaXN0ZW5lckxpYigpO1xyXG4gICAgcHVibGljIGhpZ2hsaWdodCA6IEhpZ2hsaWdodExpYiA9IG5ldyBIaWdobGlnaHRMaWIoKTtcclxuICAgIHB1YmxpYyBtZXJtYWlkOiBNZXJtYWlkTGliID0gbmV3IE1lcm1haWRMaWIoKTtcclxufVxyXG5cclxuZXhwb3J0IGNvbnN0IGluZmluaUJsYXpvciA9IG5ldyBJbmZpbmlCbGF6b3IoKTtcclxuZXhwb3J0IGRlZmF1bHQgaW5maW5pQmxhem9yO1xyXG5cclxuZGVjbGFyZSBnbG9iYWwge1xyXG4gICAgaW50ZXJmYWNlIFdpbmRvdyB7XHJcbiAgICAgICAgaW5maW5pQmxhem9yOiBJbmZpbmlCbGF6b3I7XHJcbiAgICAgICAgaGxqcz86IHtcclxuICAgICAgICAgICAgaGlnaGxpZ2h0QWxsKCk6IHZvaWQ7XHJcbiAgICAgICAgICAgIGhpZ2hsaWdodEVsZW1lbnQoZWxlbWVudDogSFRNTEVsZW1lbnQpOiB2b2lkO1xyXG4gICAgICAgICAgICBjb25maWd1cmUob3B0aW9uczogYW55KTogdm9pZDtcclxuICAgICAgICAgICAgbGlzdExhbmd1YWdlcygpOiBzdHJpbmdbXTtcclxuICAgICAgICAgICAgZ2V0TGFuZ3VhZ2UobmFtZTogc3RyaW5nKTogYW55O1xyXG4gICAgICAgIH1cclxuICAgICAgICBtZXJtYWlkPzoge1xyXG4gICAgICAgICAgICBpbml0aWFsaXplKGNvbmZpZzogYW55KTogdm9pZDtcclxuICAgICAgICAgICAgcnVuKHBhcmFtOiBhbnkpOiB2b2lkO1xyXG4gICAgICAgIH1cclxuICAgIH1cclxufVxyXG5cclxuXHJcbmlmICh0eXBlb2Ygd2luZG93ICE9PSAndW5kZWZpbmVkJykge1xyXG4gICAgd2luZG93LmluZmluaUJsYXpvciA9IGluZmluaUJsYXpvcjtcclxuICAgIFxyXG4gICAgaWYgKGluZmluaUJsYXpvci5tZXJtYWlkLmlzTWVybWFpZEpzQXZhaWxhYmxlKCkpIHtcclxuICAgICAgICB3aW5kb3cubWVybWFpZD8uaW5pdGlhbGl6ZSh7c3RhcnRPbkxvYWQ6IHRydWV9KTtcclxuICAgIH1cclxufSIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuZXhwb3J0IGNsYXNzIERvY3VtZW50TGliIHtcclxuICAgIFxyXG4gICAgLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG4gICAgcHVibGljIGFkZE9yVXBkYXRlRWxlbWVudEF0SGVhZChlbGVtZW50SWQ6IHN0cmluZywgdGV4dENvbnRlbnQ6IHN0cmluZyk6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudElkKSByZXR1cm47XHJcbiAgICAgICAgaWYgKCF0ZXh0Q29udGVudCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGNvbnN0IHBvc3NpYmxlRWxlbWVudCA9IGRvY3VtZW50LmhlYWQucXVlcnlTZWxlY3RvcihgIyR7ZWxlbWVudElkfWApO1xyXG4gICAgICAgIGlmIChwb3NzaWJsZUVsZW1lbnQgJiYgcG9zc2libGVFbGVtZW50IGluc3RhbmNlb2YgSFRNTFN0eWxlRWxlbWVudCkge1xyXG4gICAgICAgICAgICBwb3NzaWJsZUVsZW1lbnQudGV4dENvbnRlbnQgPSB0ZXh0Q29udGVudDtcclxuICAgICAgICAgICAgcmV0dXJuO1xyXG4gICAgICAgIH0gZWxzZSBpZiAocG9zc2libGVFbGVtZW50KSB7XHJcbiAgICAgICAgICAgIGRvY3VtZW50LmhlYWQucmVtb3ZlQ2hpbGQocG9zc2libGVFbGVtZW50KTtcclxuICAgICAgICB9XHJcblxyXG4gICAgICAgIGNvbnN0IHN0eWxlRWxlbWVudCA9IGRvY3VtZW50LmNyZWF0ZUVsZW1lbnQoXCJzdHlsZVwiKTtcclxuICAgICAgICBzdHlsZUVsZW1lbnQuaWQgPSBlbGVtZW50SWQ7XHJcbiAgICAgICAgc3R5bGVFbGVtZW50LnRleHRDb250ZW50ID0gdGV4dENvbnRlbnQ7XHJcbiAgICAgICAgZG9jdW1lbnQuaGVhZC5hcHBlbmRDaGlsZChzdHlsZUVsZW1lbnQpO1xyXG4gICAgfSAgICBcclxufVxyXG4iLCIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuaW1wb3J0IHtJSW5wdXRFbGVtZW50fSBmcm9tIFwiLi4vQ29udHJhY3RzL0lJbnB1dEVsZW1lbnRcIjtcclxuXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBub2luc3BlY3Rpb24gSlNVbnVzZWRHbG9iYWxTeW1ib2xzXHJcbmNsYXNzIEVsZW1lbnRMaWIge1xyXG4gICAgcHVibGljIGNsZWFyVmFsdWUoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQudmFsdWUgPSBcIlwiO1xyXG4gICAgfVxyXG4gICAgXHJcbiAgICBwdWJsaWMgc2V0VmFsdWUoZWxlbWVudDogSUlucHV0RWxlbWVudCwgdmFsdWU6IHN0cmluZyk6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghdmFsdWUpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LnZhbHVlID0gdmFsdWU7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIHNldFZhbHVlU2VsZWN0aW9uQXdhcmUoZWxlbWVudDogSUlucHV0RWxlbWVudCwgdGV4dDogc3RyaW5nKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcbiAgICAgICAgaWYgKCF0ZXh0KSByZXR1cm47XHJcblxyXG4gICAgICAgIGNvbnN0IHN0YXJ0ID0gZWxlbWVudC5zZWxlY3Rpb25TdGFydCB8fCAwO1xyXG4gICAgICAgIGNvbnN0IGVuZCA9IGVsZW1lbnQuc2VsZWN0aW9uRW5kIHx8IDA7XHJcbiAgICAgICAgXHJcbiAgICAgICAgaWYgKGVsZW1lbnQudmFsdWUgPT09IHRleHQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBjb25zdCBvbGRMZW5ndGggPSBlbGVtZW50LnZhbHVlLmxlbmd0aDtcclxuICAgICAgICBjb25zdCBuZXdMZW5ndGggPSB0ZXh0Lmxlbmd0aDtcclxuICAgICAgICBjb25zdCBkaWZmID0gbmV3TGVuZ3RoIC0gb2xkTGVuZ3RoO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQudmFsdWUgPSB0ZXh0O1xyXG4gICAgICAgIGVsZW1lbnQuc2V0U2VsZWN0aW9uUmFuZ2Uoc3RhcnQgKyBkaWZmLCBlbmQgKyBkaWZmKTtcclxuICAgIH1cclxuICAgIFxyXG4gICAgcHVibGljIHNldFRleHRDb250ZW50KGVsZW1lbnQ6IEhUTUxFbGVtZW50LCB0ZXh0OiBzdHJpbmcpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgICAgICBpZiAoIXRleHQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LnRleHRDb250ZW50ID0gdGV4dDtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgZ2V0VGV4dENvbnRlbnQoZWxlbWVudDogSFRNTEVsZW1lbnQpOiBzdHJpbmcge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuIFwiXCI7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50LnRleHRDb250ZW50KSByZXR1cm4gXCJcIjtcclxuICAgICAgICBcclxuICAgICAgICByZXR1cm4gZWxlbWVudC50ZXh0Q29udGVudDtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgYWRkSG9yaXpvbnRhbFNjcm9sbChlbGVtZW50OiBIVE1MRWxlbWVudCwgaTogbnVtYmVyKSA6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghaSkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQuc2Nyb2xsQnkoeyBsZWZ0OiBpLCBiZWhhdmlvcjogJ3Ntb290aCcgfSk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGNsaWNrRWxlbWVudChlbGVtZW50OiBIVE1MRWxlbWVudCkgOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LmNsaWNrKCk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGNsaWNrRWxlbWVudEJ5SWQoZWxlbWVudElkOnN0cmluZykgOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnRJZCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGNvbnN0IGVsZW1lbnQgPSBkb2N1bWVudC5nZXRFbGVtZW50QnlJZChlbGVtZW50SWQpO1xyXG4gICAgICAgIGlmIChlbGVtZW50ID09PSBudWxsKSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC5jbGljaygpO1xyXG4gICAgfVxyXG59XHJcblxyXG5leHBvcnQgZGVmYXVsdCBFbGVtZW50TGliIiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcblxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG5leHBvcnQgY2xhc3MgSGlnaGxpZ2h0TGliIHtcclxuICAgIHByaXZhdGUgaXNIaWdobGlnaHRKc0F2YWlsYWJsZSgpOiBib29sZWFuIHtcclxuICAgICAgICByZXR1cm4gdHlwZW9mIHdpbmRvdyAhPT0gJ3VuZGVmaW5lZCcgJiYgd2luZG93LmhsanMgIT09IHVuZGVmaW5lZDtcclxuICAgIH1cclxuICAgIFxyXG4gICAgcHJpdmF0ZSBlbnN1cmVFbGVtZW50RGF0YXNldChlbGVtZW50OiBIVE1MRWxlbWVudCk6IHZvaWQge1xyXG4gICAgICAgIGlmIChlbGVtZW50LmRhdGFzZXQpIHJldHVybjtcclxuICAgICAgICBPYmplY3QuZGVmaW5lUHJvcGVydHkoZWxlbWVudCwgJ2RhdGFzZXQnLCB7XHJcbiAgICAgICAgICAgIHZhbHVlOiB7fSxcclxuICAgICAgICAgICAgd3JpdGFibGU6IHRydWUsXHJcbiAgICAgICAgICAgIGVudW1lcmFibGU6IHRydWUsXHJcbiAgICAgICAgICAgIGNvbmZpZ3VyYWJsZTogdHJ1ZVxyXG4gICAgICAgIH0pO1xyXG4gICAgfVxyXG4gICAgXHJcbiAgICBwdWJsaWMgaGlnaGxpZ2h0QWxsKCk6IHZvaWQge1xyXG4gICAgICAgIGlmICghdGhpcy5pc0hpZ2hsaWdodEpzQXZhaWxhYmxlKCkpIHJldHVybjtcclxuICAgICAgICB3aW5kb3cuaGxqcyEuaGlnaGxpZ2h0QWxsKCk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGhpZ2hsaWdodEVsZW1lbnQoZWxlbWVudDogSFRNTEVsZW1lbnQpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNIaWdobGlnaHRKc0F2YWlsYWJsZSgpKSByZXR1cm47XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcblxyXG4gICAgICAgIHRoaXMuZW5zdXJlRWxlbWVudERhdGFzZXQoZWxlbWVudCk7XHJcbiAgICAgICAgaWYgKGVsZW1lbnQuZGF0YXNldCAmJiBlbGVtZW50LmRhdGFzZXQuaGlnaGxpZ2h0ZWQpIHtcclxuICAgICAgICAgICAgZGVsZXRlIGVsZW1lbnQuZGF0YXNldC5oaWdobGlnaHRlZDtcclxuICAgICAgICB9XHJcbiAgICAgICAgd2luZG93LmhsanMhLmhpZ2hsaWdodEVsZW1lbnQoZWxlbWVudCk7XHJcbiAgICB9XHJcbiAgICBcclxuICAgIHB1YmxpYyBzZXRDb250ZW50QW5kSGlnaGxpZ2h0KGVsZW1lbnQ6IEhUTUxFbGVtZW50LCBjb250ZW50OiBzdHJpbmcpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNIaWdobGlnaHRKc0F2YWlsYWJsZSgpKSByZXR1cm47XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcbiAgICAgICAgaWYgKCFjb250ZW50KSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC50ZXh0Q29udGVudCA9IGNvbnRlbnQ7XHJcbiAgICAgICAgdGhpcy5oaWdobGlnaHRFbGVtZW50KGVsZW1lbnQpO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBjb25maWd1cmUob3B0aW9uczogYW55KTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzSGlnaGxpZ2h0SnNBdmFpbGFibGUoKSkgcmV0dXJuO1xyXG4gICAgICAgIHdpbmRvdy5obGpzIS5jb25maWd1cmUob3B0aW9ucyk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldEF2YWlsYWJsZUxhbmd1YWdlcygpOiBzdHJpbmdbXSB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzSGlnaGxpZ2h0SnNBdmFpbGFibGUoKSkgcmV0dXJuIFtdO1xyXG4gICAgICAgIHJldHVybiB3aW5kb3cuaGxqcyEubGlzdExhbmd1YWdlcygpO1xyXG4gICAgfVxyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmltcG9ydCB7S2V5Q29uZGl0aW9ufSBmcm9tIFwiLi4vQ29udHJhY3RzL0tleUNvbmRpdGlvblwiO1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuY29uc3Qga2V5c1RvU2tpcDogU2V0PHN0cmluZz4gPSBuZXcgU2V0KFtcInVcIiwgXCJiXCIsIFwiaVwiLCBcImFcIl0pO1xyXG5jb25zdCBhbGxvd1NwZWNpYWxDb25kaXRpb25zOiBLZXlDb25kaXRpb25bXSA9IFtcclxuICAgIChldmVudCwga2V5KSA9PiBldmVudC5jdHJsS2V5ICYmIGV2ZW50LnNoaWZ0S2V5ICYmIGtleSA9PT0gXCJpXCIsIC8vIFNraXAgYEN0cmwrU2hpZnQrSWBcclxuXTtcclxuXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuZXhwb3J0IGNsYXNzIEtleUxpc3RlbmVyTGliIHtcclxuICAgIC8vIE1haW4gZnVuY3Rpb24gdG8gcHJldmVudCBkZWZhdWx0IGJlaGF2aW9yXHJcbiAgICBwcml2YXRlIHByZXZlbnRLZXlEZWZhdWx0KGV2ZW50OiBLZXlib2FyZEV2ZW50KTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCFldmVudCkgcmV0dXJuO1xyXG4gICAgICAgIGNvbnN0IGtleSA9IGV2ZW50LmtleS50b0xvd2VyQ2FzZSgpO1xyXG5cclxuICAgICAgICAvLyBTcGVjaWFsIHRhYiBiZWhhdmlvdXJcclxuICAgICAgICBpZiAoa2V5ID09IFwidGFiXCIgJiYgIWV2ZW50LnNoaWZ0S2V5ICYmICFldmVudC5jdHJsS2V5KSB7XHJcbiAgICAgICAgICAgIGV2ZW50LnByZXZlbnREZWZhdWx0KCk7XHJcbiAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICB9XHJcblxyXG4gICAgICAgIC8vIEJsb2NrIGRlZmF1bHQgYmVoYXZpb3IgZm9yIGtleXMgdGhhdCBhcmUgcmVnaXN0ZXJlZFxyXG4gICAgICAgIGlmICghZXZlbnQuY3RybEtleSkgcmV0dXJuO1xyXG4gICAgICAgIGlmIChhbGxvd1NwZWNpYWxDb25kaXRpb25zLnNvbWUoY29uZGl0aW9uID0+IGNvbmRpdGlvbihldmVudCwga2V5KSkpIHJldHVybjtcclxuICAgICAgICBpZiAoIWtleXNUb1NraXAuaGFzKGtleSkpIHJldHVybjtcclxuICAgICAgICBldmVudC5wcmV2ZW50RGVmYXVsdCgpO1xyXG4gICAgfVxyXG5cclxuXHJcbiAgICBwdWJsaWMgYWRkUHJldmVudERlZmF1bHRMaXN0ZW5lcigpIDogdm9pZCB7XHJcbiAgICAgICAgZG9jdW1lbnQuYWRkRXZlbnRMaXN0ZW5lcihcImtleWRvd25cIix0aGlzLnByZXZlbnRLZXlEZWZhdWx0KVxyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyByZW1vdmVQcmV2ZW50RGVmYXVsdExpc3RlbmVyKCkgOiB2b2lkIHtcclxuICAgICAgICBkb2N1bWVudC5yZW1vdmVFdmVudExpc3RlbmVyKFwia2V5ZG93blwiLCB0aGlzLnByZXZlbnRLZXlEZWZhdWx0KVxyXG4gICAgfVxyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcblxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuZXhwb3J0IGNsYXNzIE1lcm1haWRMaWIge1xyXG4gICAgcHVibGljIGlzTWVybWFpZEpzQXZhaWxhYmxlKCk6IGJvb2xlYW4ge1xyXG4gICAgICAgIHJldHVybiB0eXBlb2Ygd2luZG93ICE9PSAndW5kZWZpbmVkJyAmJiB3aW5kb3cubWVybWFpZCAhPT0gdW5kZWZpbmVkO1xyXG4gICAgfVxyXG4gICAgXHJcbiAgICBwdWJsaWMgYXN5bmMgcmVuZGVyTWVybWFpZEFzeW5jKGVsZW1lbnQ6IEhUTUxFbGVtZW50KTogUHJvbWlzZTx2b2lkPiB7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgaWYgKCF0aGlzLmlzTWVybWFpZEpzQXZhaWxhYmxlKCkpIHtcclxuICAgICAgICAgICAgdGhyb3cgbmV3IEVycm9yKFwiTWVybWFpZCBpcyBub3QgYXZhaWxhYmxlXCIpO1xyXG4gICAgICAgIH1cclxuICAgICAgICBcclxuICAgICAgICB3aW5kb3cubWVybWFpZD8ucnVuKHtcclxuICAgICAgICAgICAgbm9kZXM6IFtlbGVtZW50XSxcclxuICAgICAgICB9KTtcclxuICAgIH1cclxuICAgIFxyXG4gICAgcHVibGljIGFzeW5jIHJlbmRlck1lcm1haWRXaXRoQ29udGVudEFzeW5jKGVsZW1lbnQ6SFRNTEVsZW1lbnQsIGNvbnRlbnQ6IHN0cmluZyk6IFByb21pc2U8dm9pZD4ge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghY29udGVudCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGlmICghdGhpcy5pc01lcm1haWRKc0F2YWlsYWJsZSgpKSB7XHJcbiAgICAgICAgICAgIHRocm93IG5ldyBFcnJvcihcIk1lcm1haWQgaXMgbm90IGF2YWlsYWJsZVwiKTtcclxuICAgICAgICB9XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC50ZXh0Q29udGVudCA9IGNvbnRlbnQ7XHJcbiAgICAgICAgd2luZG93Lm1lcm1haWQ/LnJ1bih7XHJcbiAgICAgICAgICAgIG5vZGVzOiBbZWxlbWVudF0sXHJcbiAgICAgICAgfSk7XHJcbiAgICB9XHJcbn0iLCIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuaW1wb3J0IHtJSW5wdXRFbGVtZW50fSBmcm9tIFwiLi4vQ29udHJhY3RzL0lJbnB1dEVsZW1lbnRcIjtcclxuaW1wb3J0IHtDU2hhcnBUdXBsZX0gZnJvbSBcIi4uL0NvbnRyYWN0cy9DU2hhcnBUdXBsZVwiO1xyXG5cclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuZXhwb3J0IGNsYXNzIFRleHRTZWxlY3Rpb25MaWIge1xyXG4gICAgcHJpdmF0ZSBpc1ZhbGlkSW5wdXRFbGVtZW50KGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiBib29sZWFuIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybiBmYWxzZTtcclxuICAgICAgICByZXR1cm4gZWxlbWVudC50YWdOYW1lLnRvTG93ZXJDYXNlKCkgPT09IFwiaW5wdXRcIiB8fCBlbGVtZW50LnRhZ05hbWUudG9Mb3dlckNhc2UoKSA9PT0gXCJ0ZXh0YXJlYVwiO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBnZXRTdGFydEluZGV4KGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiBudW1iZXIge1xyXG4gICAgICAgIGlmICghdGhpcy5pc1ZhbGlkSW5wdXRFbGVtZW50KGVsZW1lbnQpKSByZXR1cm4gLTE7XHJcbiAgICAgICAgcmV0dXJuIGVsZW1lbnQuc2VsZWN0aW9uU3RhcnQgfHwgMDtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgZ2V0RW5kSW5kZXgoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IG51bWJlciB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzVmFsaWRJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybiAtMTtcclxuICAgICAgICByZXR1cm4gZWxlbWVudC5zZWxlY3Rpb25FbmQgfHwgMDtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgZ2V0QXNUdXBsZShlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogQ1NoYXJwVHVwbGU8bnVtYmVyLCBudW1iZXI+IHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNWYWxpZElucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuIHtcclxuICAgICAgICAgICAgSXRlbTE6IC0xLFxyXG4gICAgICAgICAgICBJdGVtMjogLTFcclxuICAgICAgICB9XHJcbiAgICAgICAgcmV0dXJuIHtcclxuICAgICAgICAgICAgSXRlbTE6IGVsZW1lbnQuc2VsZWN0aW9uU3RhcnQgfHwgMCxcclxuICAgICAgICAgICAgSXRlbTI6IGVsZW1lbnQuc2VsZWN0aW9uRW5kIHx8IDBcclxuICAgICAgICB9O1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBzZXRSYW5nZShlbGVtZW50OiBJSW5wdXRFbGVtZW50LCBzdGFydDogbnVtYmVyLCBlbmQ6IG51bWJlcik6IHZvaWQge1xyXG4gICAgICAgIGlmICghdGhpcy5pc1ZhbGlkSW5wdXRFbGVtZW50KGVsZW1lbnQpKSB7XHJcbiAgICAgICAgICAgIGNvbnNvbGUud2FybihcImludmFsaWQgZWxlbWVudFwiKVxyXG4gICAgICAgICAgICByZXR1cm47XHJcbiAgICAgICAgfVxyXG4gICAgICAgIFxyXG4gICAgICAgIGlmICghTnVtYmVyLmlzSW50ZWdlcihzdGFydCkgfHwgc3RhcnQgPCAwKSByZXR1cm47XHJcbiAgICAgICAgaWYgKCFOdW1iZXIuaXNJbnRlZ2VyKGVuZCkgfHwgZW5kIDwgMCkgcmV0dXJuO1xyXG4gICAgICAgIGlmIChzdGFydCA+IGVuZCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQuZm9jdXMoKTtcclxuICAgICAgICBlbGVtZW50LnNldFNlbGVjdGlvblJhbmdlKHN0YXJ0LCBlbmQpO1xyXG4gICAgfVxyXG59IiwiLy8gVGhlIG1vZHVsZSBjYWNoZVxudmFyIF9fd2VicGFja19tb2R1bGVfY2FjaGVfXyA9IHt9O1xuXG4vLyBUaGUgcmVxdWlyZSBmdW5jdGlvblxuZnVuY3Rpb24gX193ZWJwYWNrX3JlcXVpcmVfXyhtb2R1bGVJZCkge1xuXHQvLyBDaGVjayBpZiBtb2R1bGUgaXMgaW4gY2FjaGVcblx0dmFyIGNhY2hlZE1vZHVsZSA9IF9fd2VicGFja19tb2R1bGVfY2FjaGVfX1ttb2R1bGVJZF07XG5cdGlmIChjYWNoZWRNb2R1bGUgIT09IHVuZGVmaW5lZCkge1xuXHRcdHJldHVybiBjYWNoZWRNb2R1bGUuZXhwb3J0cztcblx0fVxuXHQvLyBDcmVhdGUgYSBuZXcgbW9kdWxlIChhbmQgcHV0IGl0IGludG8gdGhlIGNhY2hlKVxuXHR2YXIgbW9kdWxlID0gX193ZWJwYWNrX21vZHVsZV9jYWNoZV9fW21vZHVsZUlkXSA9IHtcblx0XHQvLyBubyBtb2R1bGUuaWQgbmVlZGVkXG5cdFx0Ly8gbm8gbW9kdWxlLmxvYWRlZCBuZWVkZWRcblx0XHRleHBvcnRzOiB7fVxuXHR9O1xuXG5cdC8vIEV4ZWN1dGUgdGhlIG1vZHVsZSBmdW5jdGlvblxuXHRfX3dlYnBhY2tfbW9kdWxlc19fW21vZHVsZUlkXS5jYWxsKG1vZHVsZS5leHBvcnRzLCBtb2R1bGUsIG1vZHVsZS5leHBvcnRzLCBfX3dlYnBhY2tfcmVxdWlyZV9fKTtcblxuXHQvLyBSZXR1cm4gdGhlIGV4cG9ydHMgb2YgdGhlIG1vZHVsZVxuXHRyZXR1cm4gbW9kdWxlLmV4cG9ydHM7XG59XG5cbiIsIiIsIi8vIHN0YXJ0dXBcbi8vIExvYWQgZW50cnkgbW9kdWxlIGFuZCByZXR1cm4gZXhwb3J0c1xuLy8gVGhpcyBlbnRyeSBtb2R1bGUgaXMgcmVmZXJlbmNlZCBieSBvdGhlciBtb2R1bGVzIHNvIGl0IGNhbid0IGJlIGlubGluZWRcbnZhciBfX3dlYnBhY2tfZXhwb3J0c19fID0gX193ZWJwYWNrX3JlcXVpcmVfXyhcIi4vc3JjL0luZmluaUJsYXpvci5Db3JlLkpzL1R5cGVzY3JpcHRMaWIvSW5kZXgudHNcIik7XG4iLCIiXSwibmFtZXMiOltdLCJzb3VyY2VSb290IjoiIn0=
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
//# sourceMappingURL=data:application/json;charset=utf-8;base64,eyJ2ZXJzaW9uIjozLCJmaWxlIjoiSW5maW5pQmxhem9yLmpzIiwibWFwcGluZ3MiOiI7Ozs7Ozs7Ozs7Ozs7Ozs7QUFLQSw2SkFBMkM7QUFDM0MsK0lBQStDO0FBQy9DLDhKQUF5RDtBQUN6RCx3SkFBcUQ7QUFDckQsa0pBQWlEO0FBSWpELE1BQWEsWUFBWTtJQUF6QjtRQUNXLGFBQVEsR0FBaUIsSUFBSSx5QkFBVyxFQUFFLENBQUM7UUFDM0MsYUFBUSxHQUFnQixJQUFJLG9CQUFVLEVBQUUsQ0FBQztRQUN6QyxrQkFBYSxHQUFzQixJQUFJLG1DQUFnQixFQUFFLENBQUM7UUFDMUQsZ0JBQVcsR0FBb0IsSUFBSSwrQkFBYyxFQUFFLENBQUM7UUFDcEQsY0FBUyxHQUFrQixJQUFJLDJCQUFZLEVBQUUsQ0FBQztJQUN6RCxDQUFDO0NBQUE7QUFORCxvQ0FNQztBQUVZLG9CQUFZLEdBQUcsSUFBSSxZQUFZLEVBQUUsQ0FBQztBQUMvQyxxQkFBZSxvQkFBWSxDQUFDO0FBZ0I1QixJQUFJLE9BQU8sTUFBTSxLQUFLLFdBQVcsRUFBRSxDQUFDO0lBQ2hDLE1BQU0sQ0FBQyxZQUFZLEdBQUcsb0JBQVksQ0FBQztBQUN2QyxDQUFDOzs7Ozs7Ozs7Ozs7OztBQ2hDRCxNQUFhLFdBQVc7SUFHYix3QkFBd0IsQ0FBQyxTQUFpQixFQUFFLFdBQW1CO1FBQ2xFLElBQUksQ0FBQyxTQUFTO1lBQUUsT0FBTztRQUN2QixJQUFJLENBQUMsV0FBVztZQUFFLE9BQU87UUFFekIsTUFBTSxlQUFlLEdBQUcsUUFBUSxDQUFDLElBQUksQ0FBQyxhQUFhLENBQUMsSUFBSSxTQUFTLEVBQUUsQ0FBQyxDQUFDO1FBQ3JFLElBQUksZUFBZSxJQUFJLGVBQWUsWUFBWSxnQkFBZ0IsRUFBRSxDQUFDO1lBQ2pFLGVBQWUsQ0FBQyxXQUFXLEdBQUcsV0FBVyxDQUFDO1lBQzFDLE9BQU87UUFDWCxDQUFDO2FBQU0sSUFBSSxlQUFlLEVBQUUsQ0FBQztZQUN6QixRQUFRLENBQUMsSUFBSSxDQUFDLFdBQVcsQ0FBQyxlQUFlLENBQUMsQ0FBQztRQUMvQyxDQUFDO1FBRUQsTUFBTSxZQUFZLEdBQUcsUUFBUSxDQUFDLGFBQWEsQ0FBQyxPQUFPLENBQUMsQ0FBQztRQUNyRCxZQUFZLENBQUMsRUFBRSxHQUFHLFNBQVMsQ0FBQztRQUM1QixZQUFZLENBQUMsV0FBVyxHQUFHLFdBQVcsQ0FBQztRQUN2QyxRQUFRLENBQUMsSUFBSSxDQUFDLFdBQVcsQ0FBQyxZQUFZLENBQUMsQ0FBQztJQUM1QyxDQUFDO0NBQ0o7QUFwQkQsa0NBb0JDOzs7Ozs7Ozs7Ozs7O0FDbkJELE1BQU0sVUFBVTtJQUNMLFVBQVUsQ0FBQyxPQUFzQjtRQUNwQyxJQUFJLENBQUMsT0FBTztZQUFFLE9BQU87UUFFckIsT0FBTyxDQUFDLEtBQUssR0FBRyxFQUFFLENBQUM7SUFDdkIsQ0FBQztJQUVNLFFBQVEsQ0FBQyxPQUFzQixFQUFFLEtBQWE7UUFDakQsSUFBSSxDQUFDLE9BQU87WUFBRSxPQUFPO1FBQ3JCLElBQUksQ0FBQyxLQUFLO1lBQUUsT0FBTztRQUVuQixPQUFPLENBQUMsS0FBSyxHQUFHLEtBQUssQ0FBQztJQUMxQixDQUFDO0lBRU0sc0JBQXNCLENBQUMsT0FBc0IsRUFBRSxJQUFZO1FBQzlELElBQUksQ0FBQyxPQUFPO1lBQUUsT0FBTztRQUNyQixJQUFJLENBQUMsSUFBSTtZQUFFLE9BQU87UUFFbEIsTUFBTSxLQUFLLEdBQUcsT0FBTyxDQUFDLGNBQWMsSUFBSSxDQUFDLENBQUM7UUFDMUMsTUFBTSxHQUFHLEdBQUcsT0FBTyxDQUFDLFlBQVksSUFBSSxDQUFDLENBQUM7UUFFdEMsSUFBSSxPQUFPLENBQUMsS0FBSyxLQUFLLElBQUk7WUFBRSxPQUFPO1FBRW5DLE1BQU0sU0FBUyxHQUFHLE9BQU8sQ0FBQyxLQUFLLENBQUMsTUFBTSxDQUFDO1FBQ3ZDLE1BQU0sU0FBUyxHQUFHLElBQUksQ0FBQyxNQUFNLENBQUM7UUFDOUIsTUFBTSxJQUFJLEdBQUcsU0FBUyxHQUFHLFNBQVMsQ0FBQztRQUVuQyxPQUFPLENBQUMsS0FBSyxHQUFHLElBQUksQ0FBQztRQUNyQixPQUFPLENBQUMsaUJBQWlCLENBQUMsS0FBSyxHQUFHLElBQUksRUFBRSxHQUFHLEdBQUcsSUFBSSxDQUFDLENBQUM7SUFDeEQsQ0FBQztJQUVNLGNBQWMsQ0FBQyxPQUFvQixFQUFFLElBQVk7UUFDcEQsSUFBSSxDQUFDLE9BQU87WUFBRSxPQUFPO1FBQ3JCLElBQUksQ0FBQyxJQUFJO1lBQUUsT0FBTztRQUVsQixPQUFPLENBQUMsV0FBVyxHQUFHLElBQUksQ0FBQztJQUMvQixDQUFDO0lBRU0sY0FBYyxDQUFDLE9BQW9CO1FBQ3RDLElBQUksQ0FBQyxPQUFPO1lBQUUsT0FBTyxFQUFFLENBQUM7UUFDeEIsSUFBSSxDQUFDLE9BQU8sQ0FBQyxXQUFXO1lBQUUsT0FBTyxFQUFFLENBQUM7UUFFcEMsT0FBTyxPQUFPLENBQUMsV0FBVyxDQUFDO0lBQy9CLENBQUM7SUFFTSxtQkFBbUIsQ0FBQyxPQUFvQixFQUFFLENBQVM7UUFDdEQsSUFBSSxDQUFDLE9BQU87WUFBRSxPQUFPO1FBQ3JCLElBQUksQ0FBQyxDQUFDO1lBQUUsT0FBTztRQUVmLE9BQU8sQ0FBQyxRQUFRLENBQUMsRUFBRSxJQUFJLEVBQUUsQ0FBQyxFQUFFLFFBQVEsRUFBRSxRQUFRLEVBQUUsQ0FBQyxDQUFDO0lBQ3RELENBQUM7SUFFTSxZQUFZLENBQUMsT0FBb0I7UUFDcEMsSUFBSSxDQUFDLE9BQU87WUFBRSxPQUFPO1FBRXJCLE9BQU8sQ0FBQyxLQUFLLEVBQUUsQ0FBQztJQUNwQixDQUFDO0lBRU0sZ0JBQWdCLENBQUMsU0FBZ0I7UUFDcEMsSUFBSSxDQUFDLFNBQVM7WUFBRSxPQUFPO1FBRXZCLE1BQU0sT0FBTyxHQUFHLFFBQVEsQ0FBQyxjQUFjLENBQUMsU0FBUyxDQUFDLENBQUM7UUFDbkQsSUFBSSxPQUFPLEtBQUssSUFBSTtZQUFFLE9BQU87UUFFN0IsT0FBTyxDQUFDLEtBQUssRUFBRSxDQUFDO0lBQ3BCLENBQUM7Q0FDSjtBQUVELHFCQUFlLFVBQVU7Ozs7Ozs7Ozs7Ozs7O0FDckV6QixNQUFhLFlBQVk7SUFDYixzQkFBc0I7UUFDMUIsT0FBTyxPQUFPLE1BQU0sS0FBSyxXQUFXLElBQUksTUFBTSxDQUFDLElBQUksS0FBSyxTQUFTLENBQUM7SUFDdEUsQ0FBQztJQUVPLG9CQUFvQixDQUFDLE9BQW9CO1FBQzdDLElBQUksT0FBTyxDQUFDLE9BQU87WUFBRSxPQUFPO1FBQzVCLE1BQU0sQ0FBQyxjQUFjLENBQUMsT0FBTyxFQUFFLFNBQVMsRUFBRTtZQUN0QyxLQUFLLEVBQUUsRUFBRTtZQUNULFFBQVEsRUFBRSxJQUFJO1lBQ2QsVUFBVSxFQUFFLElBQUk7WUFDaEIsWUFBWSxFQUFFLElBQUk7U0FDckIsQ0FBQyxDQUFDO0lBQ1AsQ0FBQztJQUVNLFlBQVk7UUFDZixJQUFJLENBQUMsSUFBSSxDQUFDLHNCQUFzQixFQUFFO1lBQUUsT0FBTztRQUMzQyxNQUFNLENBQUMsSUFBSyxDQUFDLFlBQVksRUFBRSxDQUFDO0lBQ2hDLENBQUM7SUFFTSxnQkFBZ0IsQ0FBQyxPQUFvQjtRQUN4QyxJQUFJLENBQUMsSUFBSSxDQUFDLHNCQUFzQixFQUFFO1lBQUUsT0FBTztRQUMzQyxJQUFJLENBQUMsT0FBTztZQUFFLE9BQU87UUFFckIsSUFBSSxDQUFDLG9CQUFvQixDQUFDLE9BQU8sQ0FBQyxDQUFDO1FBQ25DLElBQUksT0FBTyxDQUFDLE9BQU8sSUFBSSxPQUFPLENBQUMsT0FBTyxDQUFDLFdBQVcsRUFBRSxDQUFDO1lBQ2pELE9BQU8sT0FBTyxDQUFDLE9BQU8sQ0FBQyxXQUFXLENBQUM7UUFDdkMsQ0FBQztRQUNELE1BQU0sQ0FBQyxJQUFLLENBQUMsZ0JBQWdCLENBQUMsT0FBTyxDQUFDLENBQUM7SUFDM0MsQ0FBQztJQUVNLHNCQUFzQixDQUFDLE9BQW9CLEVBQUUsT0FBZTtRQUMvRCxJQUFJLENBQUMsSUFBSSxDQUFDLHNCQUFzQixFQUFFO1lBQUUsT0FBTztRQUMzQyxJQUFJLENBQUMsT0FBTztZQUFFLE9BQU87UUFDckIsSUFBSSxDQUFDLE9BQU87WUFBRSxPQUFPO1FBRXJCLE9BQU8sQ0FBQyxXQUFXLEdBQUcsT0FBTyxDQUFDO1FBQzlCLElBQUksQ0FBQyxnQkFBZ0IsQ0FBQyxPQUFPLENBQUMsQ0FBQztJQUNuQyxDQUFDO0lBRU0sU0FBUyxDQUFDLE9BQVk7UUFDekIsSUFBSSxDQUFDLElBQUksQ0FBQyxzQkFBc0IsRUFBRTtZQUFFLE9BQU87UUFDM0MsTUFBTSxDQUFDLElBQUssQ0FBQyxTQUFTLENBQUMsT0FBTyxDQUFDLENBQUM7SUFDcEMsQ0FBQztJQUVNLHFCQUFxQjtRQUN4QixJQUFJLENBQUMsSUFBSSxDQUFDLHNCQUFzQixFQUFFO1lBQUUsT0FBTyxFQUFFLENBQUM7UUFDOUMsT0FBTyxNQUFNLENBQUMsSUFBSyxDQUFDLGFBQWEsRUFBRSxDQUFDO0lBQ3hDLENBQUM7Q0FDSjtBQWpERCxvQ0FpREM7Ozs7Ozs7Ozs7Ozs7O0FDbERELE1BQU0sVUFBVSxHQUFnQixJQUFJLEdBQUcsQ0FBQyxDQUFDLEdBQUcsRUFBRSxHQUFHLEVBQUUsR0FBRyxFQUFFLEdBQUcsQ0FBQyxDQUFDLENBQUM7QUFDOUQsTUFBTSxzQkFBc0IsR0FBbUI7SUFDM0MsQ0FBQyxLQUFLLEVBQUUsR0FBRyxFQUFFLEVBQUUsQ0FBQyxLQUFLLENBQUMsT0FBTyxJQUFJLEtBQUssQ0FBQyxRQUFRLElBQUksR0FBRyxLQUFLLEdBQUc7Q0FDakUsQ0FBQztBQUdGLE1BQWEsY0FBYztJQUVmLGlCQUFpQixDQUFDLEtBQW9CO1FBQzFDLElBQUksQ0FBQyxLQUFLO1lBQUUsT0FBTztRQUNuQixNQUFNLEdBQUcsR0FBRyxLQUFLLENBQUMsR0FBRyxDQUFDLFdBQVcsRUFBRSxDQUFDO1FBR3BDLElBQUksR0FBRyxJQUFJLEtBQUssSUFBSSxDQUFDLEtBQUssQ0FBQyxRQUFRLElBQUksQ0FBQyxLQUFLLENBQUMsT0FBTyxFQUFFLENBQUM7WUFDcEQsS0FBSyxDQUFDLGNBQWMsRUFBRSxDQUFDO1lBQ3ZCLE9BQU87UUFDWCxDQUFDO1FBR0QsSUFBSSxDQUFDLEtBQUssQ0FBQyxPQUFPO1lBQUUsT0FBTztRQUMzQixJQUFJLHNCQUFzQixDQUFDLElBQUksQ0FBQyxTQUFTLENBQUMsRUFBRSxDQUFDLFNBQVMsQ0FBQyxLQUFLLEVBQUUsR0FBRyxDQUFDLENBQUM7WUFBRSxPQUFPO1FBQzVFLElBQUksQ0FBQyxVQUFVLENBQUMsR0FBRyxDQUFDLEdBQUcsQ0FBQztZQUFFLE9BQU87UUFDakMsS0FBSyxDQUFDLGNBQWMsRUFBRSxDQUFDO0lBQzNCLENBQUM7SUFHTSx5QkFBeUI7UUFDNUIsUUFBUSxDQUFDLGdCQUFnQixDQUFDLFNBQVMsRUFBQyxJQUFJLENBQUMsaUJBQWlCLENBQUM7SUFDL0QsQ0FBQztJQUVNLDRCQUE0QjtRQUMvQixRQUFRLENBQUMsbUJBQW1CLENBQUMsU0FBUyxFQUFFLElBQUksQ0FBQyxpQkFBaUIsQ0FBQztJQUNuRSxDQUFDO0NBQ0o7QUEzQkQsd0NBMkJDOzs7Ozs7Ozs7Ozs7OztBQzlCRCxNQUFhLGdCQUFnQjtJQUNqQixtQkFBbUIsQ0FBQyxPQUFzQjtRQUM5QyxJQUFJLENBQUMsT0FBTztZQUFFLE9BQU8sS0FBSyxDQUFDO1FBQzNCLE9BQU8sT0FBTyxDQUFDLE9BQU8sQ0FBQyxXQUFXLEVBQUUsS0FBSyxPQUFPLElBQUksT0FBTyxDQUFDLE9BQU8sQ0FBQyxXQUFXLEVBQUUsS0FBSyxVQUFVLENBQUM7SUFDckcsQ0FBQztJQUVNLGFBQWEsQ0FBQyxPQUFzQjtRQUN2QyxJQUFJLENBQUMsSUFBSSxDQUFDLG1CQUFtQixDQUFDLE9BQU8sQ0FBQztZQUFFLE9BQU8sQ0FBQyxDQUFDLENBQUM7UUFDbEQsT0FBTyxPQUFPLENBQUMsY0FBYyxJQUFJLENBQUMsQ0FBQztJQUN2QyxDQUFDO0lBRU0sV0FBVyxDQUFDLE9BQXNCO1FBQ3JDLElBQUksQ0FBQyxJQUFJLENBQUMsbUJBQW1CLENBQUMsT0FBTyxDQUFDO1lBQUUsT0FBTyxDQUFDLENBQUMsQ0FBQztRQUNsRCxPQUFPLE9BQU8sQ0FBQyxZQUFZLElBQUksQ0FBQyxDQUFDO0lBQ3JDLENBQUM7SUFFTSxVQUFVLENBQUMsT0FBc0I7UUFDcEMsSUFBSSxDQUFDLElBQUksQ0FBQyxtQkFBbUIsQ0FBQyxPQUFPLENBQUM7WUFBRSxPQUFPO2dCQUMzQyxLQUFLLEVBQUUsQ0FBQyxDQUFDO2dCQUNULEtBQUssRUFBRSxDQUFDLENBQUM7YUFDWjtRQUNELE9BQU87WUFDSCxLQUFLLEVBQUUsT0FBTyxDQUFDLGNBQWMsSUFBSSxDQUFDO1lBQ2xDLEtBQUssRUFBRSxPQUFPLENBQUMsWUFBWSxJQUFJLENBQUM7U0FDbkMsQ0FBQztJQUNOLENBQUM7SUFFTSxRQUFRLENBQUMsT0FBc0IsRUFBRSxLQUFhLEVBQUUsR0FBVztRQUM5RCxJQUFJLENBQUMsSUFBSSxDQUFDLG1CQUFtQixDQUFDLE9BQU8sQ0FBQyxFQUFFLENBQUM7WUFDckMsT0FBTyxDQUFDLElBQUksQ0FBQyxpQkFBaUIsQ0FBQztZQUMvQixPQUFPO1FBQ1gsQ0FBQztRQUVELElBQUksQ0FBQyxNQUFNLENBQUMsU0FBUyxDQUFDLEtBQUssQ0FBQyxJQUFJLEtBQUssR0FBRyxDQUFDO1lBQUUsT0FBTztRQUNsRCxJQUFJLENBQUMsTUFBTSxDQUFDLFNBQVMsQ0FBQyxHQUFHLENBQUMsSUFBSSxHQUFHLEdBQUcsQ0FBQztZQUFFLE9BQU87UUFDOUMsSUFBSSxLQUFLLEdBQUcsR0FBRztZQUFFLE9BQU87UUFFeEIsT0FBTyxDQUFDLEtBQUssRUFBRSxDQUFDO1FBQ2hCLE9BQU8sQ0FBQyxpQkFBaUIsQ0FBQyxLQUFLLEVBQUUsR0FBRyxDQUFDLENBQUM7SUFDMUMsQ0FBQztDQUNKO0FBeENELDRDQXdDQzs7Ozs7OztVQ2xERDtVQUNBOztVQUVBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBOztVQUVBO1VBQ0E7O1VBRUE7VUFDQTtVQUNBOzs7O1VFdEJBO1VBQ0E7VUFDQTtVQUNBIiwic291cmNlcyI6WyJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci5Db3JlLkpzL1R5cGVzY3JpcHRMaWIvSW5kZXgudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci5Db3JlLkpzL1R5cGVzY3JpcHRMaWIvTGlicy9Eb2N1bWVudExpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9MaWJzL0VsZW1lbnRMaWIudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci5Db3JlLkpzL1R5cGVzY3JpcHRMaWIvTGlicy9IaWdobGlnaHRMaWIudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS8uL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci5Db3JlLkpzL1R5cGVzY3JpcHRMaWIvTGlicy9LZXlMaXN0ZW5lckxpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yLkNvcmUuSnMvVHlwZXNjcmlwdExpYi9MaWJzL1RleHRTZWxlY3Rpb25MaWIudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2Jvb3RzdHJhcCIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlL3dlYnBhY2svYmVmb3JlLXN0YXJ0dXAiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL3N0YXJ0dXAiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2FmdGVyLXN0YXJ0dXAiXSwic291cmNlc0NvbnRlbnQiOlsiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuXHJcbmltcG9ydCBFbGVtZW50TGliIGZyb20gXCIuL0xpYnMvRWxlbWVudExpYlwiO1xyXG5pbXBvcnQge0RvY3VtZW50TGlifSBmcm9tIFwiLi9MaWJzL0RvY3VtZW50TGliXCI7XHJcbmltcG9ydCB7VGV4dFNlbGVjdGlvbkxpYn0gZnJvbSBcIi4vTGlicy9UZXh0U2VsZWN0aW9uTGliXCI7XHJcbmltcG9ydCB7S2V5TGlzdGVuZXJMaWJ9IGZyb20gXCIuL0xpYnMvS2V5TGlzdGVuZXJMaWJcIjtcclxuaW1wb3J0IHtIaWdobGlnaHRMaWJ9IGZyb20gXCIuL0xpYnMvSGlnaGxpZ2h0TGliXCI7XHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5leHBvcnQgY2xhc3MgSW5maW5pQmxhem9yIHtcclxuICAgIHB1YmxpYyBkb2N1bWVudCA6IERvY3VtZW50TGliID0gbmV3IERvY3VtZW50TGliKCk7XHJcbiAgICBwdWJsaWMgZWxlbWVudHMgOiBFbGVtZW50TGliID0gbmV3IEVsZW1lbnRMaWIoKTtcclxuICAgIHB1YmxpYyB0ZXh0U2VsZWN0aW9uIDogVGV4dFNlbGVjdGlvbkxpYiA9IG5ldyBUZXh0U2VsZWN0aW9uTGliKCk7XHJcbiAgICBwdWJsaWMga2V5TGlzdGVuZXIgOiBLZXlMaXN0ZW5lckxpYiA9IG5ldyBLZXlMaXN0ZW5lckxpYigpO1xyXG4gICAgcHVibGljIGhpZ2hsaWdodCA6IEhpZ2hsaWdodExpYiA9IG5ldyBIaWdobGlnaHRMaWIoKTtcclxufVxyXG5cclxuZXhwb3J0IGNvbnN0IGluZmluaUJsYXpvciA9IG5ldyBJbmZpbmlCbGF6b3IoKTtcclxuZXhwb3J0IGRlZmF1bHQgaW5maW5pQmxhem9yO1xyXG5cclxuZGVjbGFyZSBnbG9iYWwge1xyXG4gICAgaW50ZXJmYWNlIFdpbmRvdyB7XHJcbiAgICAgICAgaW5maW5pQmxhem9yOiBJbmZpbmlCbGF6b3I7XHJcbiAgICAgICAgaGxqcz86IHtcclxuICAgICAgICAgICAgaGlnaGxpZ2h0QWxsKCk6IHZvaWQ7XHJcbiAgICAgICAgICAgIGhpZ2hsaWdodEVsZW1lbnQoZWxlbWVudDogSFRNTEVsZW1lbnQpOiB2b2lkO1xyXG4gICAgICAgICAgICBjb25maWd1cmUob3B0aW9uczogYW55KTogdm9pZDtcclxuICAgICAgICAgICAgbGlzdExhbmd1YWdlcygpOiBzdHJpbmdbXTtcclxuICAgICAgICAgICAgZ2V0TGFuZ3VhZ2UobmFtZTogc3RyaW5nKTogYW55O1xyXG4gICAgICAgIH1cclxuICAgIH1cclxufVxyXG5cclxuXHJcbmlmICh0eXBlb2Ygd2luZG93ICE9PSAndW5kZWZpbmVkJykge1xyXG4gICAgd2luZG93LmluZmluaUJsYXpvciA9IGluZmluaUJsYXpvcjtcclxufSIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuZXhwb3J0IGNsYXNzIERvY3VtZW50TGliIHtcclxuICAgIFxyXG4gICAgLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG4gICAgcHVibGljIGFkZE9yVXBkYXRlRWxlbWVudEF0SGVhZChlbGVtZW50SWQ6IHN0cmluZywgdGV4dENvbnRlbnQ6IHN0cmluZyk6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudElkKSByZXR1cm47XHJcbiAgICAgICAgaWYgKCF0ZXh0Q29udGVudCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGNvbnN0IHBvc3NpYmxlRWxlbWVudCA9IGRvY3VtZW50LmhlYWQucXVlcnlTZWxlY3RvcihgIyR7ZWxlbWVudElkfWApO1xyXG4gICAgICAgIGlmIChwb3NzaWJsZUVsZW1lbnQgJiYgcG9zc2libGVFbGVtZW50IGluc3RhbmNlb2YgSFRNTFN0eWxlRWxlbWVudCkge1xyXG4gICAgICAgICAgICBwb3NzaWJsZUVsZW1lbnQudGV4dENvbnRlbnQgPSB0ZXh0Q29udGVudDtcclxuICAgICAgICAgICAgcmV0dXJuO1xyXG4gICAgICAgIH0gZWxzZSBpZiAocG9zc2libGVFbGVtZW50KSB7XHJcbiAgICAgICAgICAgIGRvY3VtZW50LmhlYWQucmVtb3ZlQ2hpbGQocG9zc2libGVFbGVtZW50KTtcclxuICAgICAgICB9XHJcblxyXG4gICAgICAgIGNvbnN0IHN0eWxlRWxlbWVudCA9IGRvY3VtZW50LmNyZWF0ZUVsZW1lbnQoXCJzdHlsZVwiKTtcclxuICAgICAgICBzdHlsZUVsZW1lbnQuaWQgPSBlbGVtZW50SWQ7XHJcbiAgICAgICAgc3R5bGVFbGVtZW50LnRleHRDb250ZW50ID0gdGV4dENvbnRlbnQ7XHJcbiAgICAgICAgZG9jdW1lbnQuaGVhZC5hcHBlbmRDaGlsZChzdHlsZUVsZW1lbnQpO1xyXG4gICAgfSAgICBcclxufVxyXG4iLCIvLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gSW1wb3J0c1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuaW1wb3J0IHtJSW5wdXRFbGVtZW50fSBmcm9tIFwiLi4vQ29udHJhY3RzL0lJbnB1dEVsZW1lbnRcIjtcclxuXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBub2luc3BlY3Rpb24gSlNVbnVzZWRHbG9iYWxTeW1ib2xzXHJcbmNsYXNzIEVsZW1lbnRMaWIge1xyXG4gICAgcHVibGljIGNsZWFyVmFsdWUoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQudmFsdWUgPSBcIlwiO1xyXG4gICAgfVxyXG4gICAgXHJcbiAgICBwdWJsaWMgc2V0VmFsdWUoZWxlbWVudDogSUlucHV0RWxlbWVudCwgdmFsdWU6IHN0cmluZyk6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghdmFsdWUpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LnZhbHVlID0gdmFsdWU7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIHNldFZhbHVlU2VsZWN0aW9uQXdhcmUoZWxlbWVudDogSUlucHV0RWxlbWVudCwgdGV4dDogc3RyaW5nKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcbiAgICAgICAgaWYgKCF0ZXh0KSByZXR1cm47XHJcblxyXG4gICAgICAgIGNvbnN0IHN0YXJ0ID0gZWxlbWVudC5zZWxlY3Rpb25TdGFydCB8fCAwO1xyXG4gICAgICAgIGNvbnN0IGVuZCA9IGVsZW1lbnQuc2VsZWN0aW9uRW5kIHx8IDA7XHJcbiAgICAgICAgXHJcbiAgICAgICAgaWYgKGVsZW1lbnQudmFsdWUgPT09IHRleHQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBjb25zdCBvbGRMZW5ndGggPSBlbGVtZW50LnZhbHVlLmxlbmd0aDtcclxuICAgICAgICBjb25zdCBuZXdMZW5ndGggPSB0ZXh0Lmxlbmd0aDtcclxuICAgICAgICBjb25zdCBkaWZmID0gbmV3TGVuZ3RoIC0gb2xkTGVuZ3RoO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQudmFsdWUgPSB0ZXh0O1xyXG4gICAgICAgIGVsZW1lbnQuc2V0U2VsZWN0aW9uUmFuZ2Uoc3RhcnQgKyBkaWZmLCBlbmQgKyBkaWZmKTtcclxuICAgIH1cclxuICAgIFxyXG4gICAgcHVibGljIHNldFRleHRDb250ZW50KGVsZW1lbnQ6IEhUTUxFbGVtZW50LCB0ZXh0OiBzdHJpbmcpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgICAgICBpZiAoIXRleHQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LnRleHRDb250ZW50ID0gdGV4dDtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgZ2V0VGV4dENvbnRlbnQoZWxlbWVudDogSFRNTEVsZW1lbnQpOiBzdHJpbmcge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuIFwiXCI7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50LnRleHRDb250ZW50KSByZXR1cm4gXCJcIjtcclxuICAgICAgICBcclxuICAgICAgICByZXR1cm4gZWxlbWVudC50ZXh0Q29udGVudDtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgYWRkSG9yaXpvbnRhbFNjcm9sbChlbGVtZW50OiBIVE1MRWxlbWVudCwgaTogbnVtYmVyKSA6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghaSkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQuc2Nyb2xsQnkoeyBsZWZ0OiBpLCBiZWhhdmlvcjogJ3Ntb290aCcgfSk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGNsaWNrRWxlbWVudChlbGVtZW50OiBIVE1MRWxlbWVudCkgOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LmNsaWNrKCk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGNsaWNrRWxlbWVudEJ5SWQoZWxlbWVudElkOnN0cmluZykgOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnRJZCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGNvbnN0IGVsZW1lbnQgPSBkb2N1bWVudC5nZXRFbGVtZW50QnlJZChlbGVtZW50SWQpO1xyXG4gICAgICAgIGlmIChlbGVtZW50ID09PSBudWxsKSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC5jbGljaygpO1xyXG4gICAgfVxyXG59XHJcblxyXG5leHBvcnQgZGVmYXVsdCBFbGVtZW50TGliIiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcblxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG5leHBvcnQgY2xhc3MgSGlnaGxpZ2h0TGliIHtcclxuICAgIHByaXZhdGUgaXNIaWdobGlnaHRKc0F2YWlsYWJsZSgpOiBib29sZWFuIHtcclxuICAgICAgICByZXR1cm4gdHlwZW9mIHdpbmRvdyAhPT0gJ3VuZGVmaW5lZCcgJiYgd2luZG93LmhsanMgIT09IHVuZGVmaW5lZDtcclxuICAgIH1cclxuICAgIFxyXG4gICAgcHJpdmF0ZSBlbnN1cmVFbGVtZW50RGF0YXNldChlbGVtZW50OiBIVE1MRWxlbWVudCk6IHZvaWQge1xyXG4gICAgICAgIGlmIChlbGVtZW50LmRhdGFzZXQpIHJldHVybjtcclxuICAgICAgICBPYmplY3QuZGVmaW5lUHJvcGVydHkoZWxlbWVudCwgJ2RhdGFzZXQnLCB7XHJcbiAgICAgICAgICAgIHZhbHVlOiB7fSxcclxuICAgICAgICAgICAgd3JpdGFibGU6IHRydWUsXHJcbiAgICAgICAgICAgIGVudW1lcmFibGU6IHRydWUsXHJcbiAgICAgICAgICAgIGNvbmZpZ3VyYWJsZTogdHJ1ZVxyXG4gICAgICAgIH0pO1xyXG4gICAgfVxyXG4gICAgXHJcbiAgICBwdWJsaWMgaGlnaGxpZ2h0QWxsKCk6IHZvaWQge1xyXG4gICAgICAgIGlmICghdGhpcy5pc0hpZ2hsaWdodEpzQXZhaWxhYmxlKCkpIHJldHVybjtcclxuICAgICAgICB3aW5kb3cuaGxqcyEuaGlnaGxpZ2h0QWxsKCk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGhpZ2hsaWdodEVsZW1lbnQoZWxlbWVudDogSFRNTEVsZW1lbnQpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNIaWdobGlnaHRKc0F2YWlsYWJsZSgpKSByZXR1cm47XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcblxyXG4gICAgICAgIHRoaXMuZW5zdXJlRWxlbWVudERhdGFzZXQoZWxlbWVudCk7XHJcbiAgICAgICAgaWYgKGVsZW1lbnQuZGF0YXNldCAmJiBlbGVtZW50LmRhdGFzZXQuaGlnaGxpZ2h0ZWQpIHtcclxuICAgICAgICAgICAgZGVsZXRlIGVsZW1lbnQuZGF0YXNldC5oaWdobGlnaHRlZDtcclxuICAgICAgICB9XHJcbiAgICAgICAgd2luZG93LmhsanMhLmhpZ2hsaWdodEVsZW1lbnQoZWxlbWVudCk7XHJcbiAgICB9XHJcbiAgICBcclxuICAgIHB1YmxpYyBzZXRDb250ZW50QW5kSGlnaGxpZ2h0KGVsZW1lbnQ6IEhUTUxFbGVtZW50LCBjb250ZW50OiBzdHJpbmcpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNIaWdobGlnaHRKc0F2YWlsYWJsZSgpKSByZXR1cm47XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm47XHJcbiAgICAgICAgaWYgKCFjb250ZW50KSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC50ZXh0Q29udGVudCA9IGNvbnRlbnQ7XHJcbiAgICAgICAgdGhpcy5oaWdobGlnaHRFbGVtZW50KGVsZW1lbnQpO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBjb25maWd1cmUob3B0aW9uczogYW55KTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzSGlnaGxpZ2h0SnNBdmFpbGFibGUoKSkgcmV0dXJuO1xyXG4gICAgICAgIHdpbmRvdy5obGpzIS5jb25maWd1cmUob3B0aW9ucyk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldEF2YWlsYWJsZUxhbmd1YWdlcygpOiBzdHJpbmdbXSB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzSGlnaGxpZ2h0SnNBdmFpbGFibGUoKSkgcmV0dXJuIFtdO1xyXG4gICAgICAgIHJldHVybiB3aW5kb3cuaGxqcyEubGlzdExhbmd1YWdlcygpO1xyXG4gICAgfVxyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmltcG9ydCB7S2V5Q29uZGl0aW9ufSBmcm9tIFwiLi4vQ29udHJhY3RzL0tleUNvbmRpdGlvblwiO1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuY29uc3Qga2V5c1RvU2tpcDogU2V0PHN0cmluZz4gPSBuZXcgU2V0KFtcInVcIiwgXCJiXCIsIFwiaVwiLCBcImFcIl0pO1xyXG5jb25zdCBhbGxvd1NwZWNpYWxDb25kaXRpb25zOiBLZXlDb25kaXRpb25bXSA9IFtcclxuICAgIChldmVudCwga2V5KSA9PiBldmVudC5jdHJsS2V5ICYmIGV2ZW50LnNoaWZ0S2V5ICYmIGtleSA9PT0gXCJpXCIsIC8vIFNraXAgYEN0cmwrU2hpZnQrSWBcclxuXTtcclxuXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuZXhwb3J0IGNsYXNzIEtleUxpc3RlbmVyTGliIHtcclxuICAgIC8vIE1haW4gZnVuY3Rpb24gdG8gcHJldmVudCBkZWZhdWx0IGJlaGF2aW9yXHJcbiAgICBwcml2YXRlIHByZXZlbnRLZXlEZWZhdWx0KGV2ZW50OiBLZXlib2FyZEV2ZW50KTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCFldmVudCkgcmV0dXJuO1xyXG4gICAgICAgIGNvbnN0IGtleSA9IGV2ZW50LmtleS50b0xvd2VyQ2FzZSgpO1xyXG5cclxuICAgICAgICAvLyBTcGVjaWFsIHRhYiBiZWhhdmlvdXJcclxuICAgICAgICBpZiAoa2V5ID09IFwidGFiXCIgJiYgIWV2ZW50LnNoaWZ0S2V5ICYmICFldmVudC5jdHJsS2V5KSB7XHJcbiAgICAgICAgICAgIGV2ZW50LnByZXZlbnREZWZhdWx0KCk7XHJcbiAgICAgICAgICAgIHJldHVybjtcclxuICAgICAgICB9XHJcblxyXG4gICAgICAgIC8vIEJsb2NrIGRlZmF1bHQgYmVoYXZpb3IgZm9yIGtleXMgdGhhdCBhcmUgcmVnaXN0ZXJlZFxyXG4gICAgICAgIGlmICghZXZlbnQuY3RybEtleSkgcmV0dXJuO1xyXG4gICAgICAgIGlmIChhbGxvd1NwZWNpYWxDb25kaXRpb25zLnNvbWUoY29uZGl0aW9uID0+IGNvbmRpdGlvbihldmVudCwga2V5KSkpIHJldHVybjtcclxuICAgICAgICBpZiAoIWtleXNUb1NraXAuaGFzKGtleSkpIHJldHVybjtcclxuICAgICAgICBldmVudC5wcmV2ZW50RGVmYXVsdCgpO1xyXG4gICAgfVxyXG5cclxuXHJcbiAgICBwdWJsaWMgYWRkUHJldmVudERlZmF1bHRMaXN0ZW5lcigpIDogdm9pZCB7XHJcbiAgICAgICAgZG9jdW1lbnQuYWRkRXZlbnRMaXN0ZW5lcihcImtleWRvd25cIix0aGlzLnByZXZlbnRLZXlEZWZhdWx0KVxyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyByZW1vdmVQcmV2ZW50RGVmYXVsdExpc3RlbmVyKCkgOiB2b2lkIHtcclxuICAgICAgICBkb2N1bWVudC5yZW1vdmVFdmVudExpc3RlbmVyKFwia2V5ZG93blwiLCB0aGlzLnByZXZlbnRLZXlEZWZhdWx0KVxyXG4gICAgfVxyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmltcG9ydCB7SUlucHV0RWxlbWVudH0gZnJvbSBcIi4uL0NvbnRyYWN0cy9JSW5wdXRFbGVtZW50XCI7XHJcbmltcG9ydCB7Q1NoYXJwVHVwbGV9IGZyb20gXCIuLi9Db250cmFjdHMvQ1NoYXJwVHVwbGVcIjtcclxuXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBub2luc3BlY3Rpb24gSlNVbnVzZWRHbG9iYWxTeW1ib2xzXHJcbmV4cG9ydCBjbGFzcyBUZXh0U2VsZWN0aW9uTGliIHtcclxuICAgIHByaXZhdGUgaXNWYWxpZElucHV0RWxlbWVudChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogYm9vbGVhbiB7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50KSByZXR1cm4gZmFsc2U7XHJcbiAgICAgICAgcmV0dXJuIGVsZW1lbnQudGFnTmFtZS50b0xvd2VyQ2FzZSgpID09PSBcImlucHV0XCIgfHwgZWxlbWVudC50YWdOYW1lLnRvTG93ZXJDYXNlKCkgPT09IFwidGV4dGFyZWFcIjtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgZ2V0U3RhcnRJbmRleChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogbnVtYmVyIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNWYWxpZElucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuIC0xO1xyXG4gICAgICAgIHJldHVybiBlbGVtZW50LnNlbGVjdGlvblN0YXJ0IHx8IDA7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldEVuZEluZGV4KGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiBudW1iZXIge1xyXG4gICAgICAgIGlmICghdGhpcy5pc1ZhbGlkSW5wdXRFbGVtZW50KGVsZW1lbnQpKSByZXR1cm4gLTE7XHJcbiAgICAgICAgcmV0dXJuIGVsZW1lbnQuc2VsZWN0aW9uRW5kIHx8IDA7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldEFzVHVwbGUoZWxlbWVudDogSUlucHV0RWxlbWVudCk6IENTaGFycFR1cGxlPG51bWJlciwgbnVtYmVyPiB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzVmFsaWRJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybiB7XHJcbiAgICAgICAgICAgIEl0ZW0xOiAtMSxcclxuICAgICAgICAgICAgSXRlbTI6IC0xXHJcbiAgICAgICAgfVxyXG4gICAgICAgIHJldHVybiB7XHJcbiAgICAgICAgICAgIEl0ZW0xOiBlbGVtZW50LnNlbGVjdGlvblN0YXJ0IHx8IDAsXHJcbiAgICAgICAgICAgIEl0ZW0yOiBlbGVtZW50LnNlbGVjdGlvbkVuZCB8fCAwXHJcbiAgICAgICAgfTtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgc2V0UmFuZ2UoZWxlbWVudDogSUlucHV0RWxlbWVudCwgc3RhcnQ6IG51bWJlciwgZW5kOiBudW1iZXIpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNWYWxpZElucHV0RWxlbWVudChlbGVtZW50KSkge1xyXG4gICAgICAgICAgICBjb25zb2xlLndhcm4oXCJpbnZhbGlkIGVsZW1lbnRcIilcclxuICAgICAgICAgICAgcmV0dXJuO1xyXG4gICAgICAgIH1cclxuICAgICAgICBcclxuICAgICAgICBpZiAoIU51bWJlci5pc0ludGVnZXIoc3RhcnQpIHx8IHN0YXJ0IDwgMCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghTnVtYmVyLmlzSW50ZWdlcihlbmQpIHx8IGVuZCA8IDApIHJldHVybjtcclxuICAgICAgICBpZiAoc3RhcnQgPiBlbmQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LmZvY3VzKCk7XHJcbiAgICAgICAgZWxlbWVudC5zZXRTZWxlY3Rpb25SYW5nZShzdGFydCwgZW5kKTtcclxuICAgIH1cclxufSIsIi8vIFRoZSBtb2R1bGUgY2FjaGVcbnZhciBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX18gPSB7fTtcblxuLy8gVGhlIHJlcXVpcmUgZnVuY3Rpb25cbmZ1bmN0aW9uIF9fd2VicGFja19yZXF1aXJlX18obW9kdWxlSWQpIHtcblx0Ly8gQ2hlY2sgaWYgbW9kdWxlIGlzIGluIGNhY2hlXG5cdHZhciBjYWNoZWRNb2R1bGUgPSBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX19bbW9kdWxlSWRdO1xuXHRpZiAoY2FjaGVkTW9kdWxlICE9PSB1bmRlZmluZWQpIHtcblx0XHRyZXR1cm4gY2FjaGVkTW9kdWxlLmV4cG9ydHM7XG5cdH1cblx0Ly8gQ3JlYXRlIGEgbmV3IG1vZHVsZSAoYW5kIHB1dCBpdCBpbnRvIHRoZSBjYWNoZSlcblx0dmFyIG1vZHVsZSA9IF9fd2VicGFja19tb2R1bGVfY2FjaGVfX1ttb2R1bGVJZF0gPSB7XG5cdFx0Ly8gbm8gbW9kdWxlLmlkIG5lZWRlZFxuXHRcdC8vIG5vIG1vZHVsZS5sb2FkZWQgbmVlZGVkXG5cdFx0ZXhwb3J0czoge31cblx0fTtcblxuXHQvLyBFeGVjdXRlIHRoZSBtb2R1bGUgZnVuY3Rpb25cblx0X193ZWJwYWNrX21vZHVsZXNfX1ttb2R1bGVJZF0uY2FsbChtb2R1bGUuZXhwb3J0cywgbW9kdWxlLCBtb2R1bGUuZXhwb3J0cywgX193ZWJwYWNrX3JlcXVpcmVfXyk7XG5cblx0Ly8gUmV0dXJuIHRoZSBleHBvcnRzIG9mIHRoZSBtb2R1bGVcblx0cmV0dXJuIG1vZHVsZS5leHBvcnRzO1xufVxuXG4iLCIiLCIvLyBzdGFydHVwXG4vLyBMb2FkIGVudHJ5IG1vZHVsZSBhbmQgcmV0dXJuIGV4cG9ydHNcbi8vIFRoaXMgZW50cnkgbW9kdWxlIGlzIHJlZmVyZW5jZWQgYnkgb3RoZXIgbW9kdWxlcyBzbyBpdCBjYW4ndCBiZSBpbmxpbmVkXG52YXIgX193ZWJwYWNrX2V4cG9ydHNfXyA9IF9fd2VicGFja19yZXF1aXJlX18oXCIuL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci5Db3JlLkpzL1R5cGVzY3JpcHRMaWIvSW5kZXgudHNcIik7XG4iLCIiXSwibmFtZXMiOltdLCJzb3VyY2VSb290IjoiIn0=
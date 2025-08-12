/******/ (() => { // webpackBootstrap
/******/ 	"use strict";
/******/ 	var __webpack_modules__ = ({

/***/ "./src/InfiniLore.InfiniBlazor/TypescriptLib/Index.ts":
/*!************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/TypescriptLib/Index.ts ***!
  \************************************************************/
/***/ ((__unused_webpack_module, exports, __webpack_require__) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.infiniBlazor = exports.InfiniBlazor = void 0;
const ElementLib_1 = __webpack_require__(/*! ./Libs/ElementLib */ "./src/InfiniLore.InfiniBlazor/TypescriptLib/Libs/ElementLib.ts");
const DocumentLib_1 = __webpack_require__(/*! ./Libs/DocumentLib */ "./src/InfiniLore.InfiniBlazor/TypescriptLib/Libs/DocumentLib.ts");
const InputElementLib_1 = __webpack_require__(/*! ./Libs/InputElementLib */ "./src/InfiniLore.InfiniBlazor/TypescriptLib/Libs/InputElementLib.ts");
const KeyDownListener_1 = __webpack_require__(/*! ./Libs/KeyDownListener */ "./src/InfiniLore.InfiniBlazor/TypescriptLib/Libs/KeyDownListener.ts");
class InfiniBlazor {
    constructor() {
        this.document = new DocumentLib_1.DocumentLib();
        this.element = new ElementLib_1.ElementLib();
        this.inputElement = new InputElementLib_1.InputElementLib();
        this.keyListener = new KeyDownListener_1.KeyListenerLib();
    }
}
exports.InfiniBlazor = InfiniBlazor;
exports.infiniBlazor = new InfiniBlazor();
exports["default"] = exports.infiniBlazor;
if (typeof window !== 'undefined') {
    window.infiniBlazor = exports.infiniBlazor;
}


/***/ }),

/***/ "./src/InfiniLore.InfiniBlazor/TypescriptLib/Libs/DocumentLib.ts":
/*!***********************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/TypescriptLib/Libs/DocumentLib.ts ***!
  \***********************************************************************/
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

/***/ "./src/InfiniLore.InfiniBlazor/TypescriptLib/Libs/ElementLib.ts":
/*!**********************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/TypescriptLib/Libs/ElementLib.ts ***!
  \**********************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.ElementLib = void 0;
class ElementLib {
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

/***/ "./src/InfiniLore.InfiniBlazor/TypescriptLib/Libs/InputElementLib.ts":
/*!***************************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/TypescriptLib/Libs/InputElementLib.ts ***!
  \***************************************************************************/
/***/ ((__unused_webpack_module, exports) => {


Object.defineProperty(exports, "__esModule", ({ value: true }));
exports.InputElementLib = void 0;
class InputElementLib {
    isValidInputElement(element) {
        if (!element)
            return false;
        return element.tagName.toLowerCase() === "input" || element.tagName.toLowerCase() === "textarea";
    }
    getSelectionStart(element) {
        if (!this.isValidInputElement(element))
            return -1;
        return element.selectionStart || 0;
    }
    getSelectionEnd(element) {
        if (!this.isValidInputElement(element))
            return -1;
        return element.selectionEnd || 0;
    }
    getSelection(element) {
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
    setSelectionRange(element, start, end) {
        if (!start)
            return;
        if (!end)
            return;
        if (!this.isValidInputElement(element))
            return;
        element.focus();
        element.setSelectionRange(start, end);
    }
}
exports.InputElementLib = InputElementLib;


/***/ }),

/***/ "./src/InfiniLore.InfiniBlazor/TypescriptLib/Libs/KeyDownListener.ts":
/*!***************************************************************************!*\
  !*** ./src/InfiniLore.InfiniBlazor/TypescriptLib/Libs/KeyDownListener.ts ***!
  \***************************************************************************/
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
/******/ 	var __webpack_exports__ = __webpack_require__("./src/InfiniLore.InfiniBlazor/TypescriptLib/Index.ts");
/******/ 	
/******/ })()
;
//# sourceMappingURL=data:application/json;charset=utf-8;base64,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QVEsQ0FBQyxtQkFBbUIsQ0FBQyxTQUFTLEVBQUUsSUFBSSxDQUFDLGlCQUFpQixDQUFDO0lBQ25FLENBQUM7Q0FDSjtBQXRCRCx3Q0FzQkM7Ozs7Ozs7VUNuQ0Q7VUFDQTs7VUFFQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTtVQUNBO1VBQ0E7VUFDQTs7VUFFQTtVQUNBOztVQUVBO1VBQ0E7VUFDQTs7OztVRXRCQTtVQUNBO1VBQ0E7VUFDQSIsInNvdXJjZXMiOlsid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3IvVHlwZXNjcmlwdExpYi9JbmRleC50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yL1R5cGVzY3JpcHRMaWIvTGlicy9Eb2N1bWVudExpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yL1R5cGVzY3JpcHRMaWIvTGlicy9FbGVtZW50TGliLnRzIiwid2VicGFjazovL2luZmluaWxvcmUvLi9zcmMvSW5maW5pTG9yZS5JbmZpbmlCbGF6b3IvVHlwZXNjcmlwdExpYi9MaWJzL0lucHV0RWxlbWVudExpYi50cyIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlLy4vc3JjL0luZmluaUxvcmUuSW5maW5pQmxhem9yL1R5cGVzY3JpcHRMaWIvTGlicy9LZXlEb3duTGlzdGVuZXIudHMiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2Jvb3RzdHJhcCIsIndlYnBhY2s6Ly9pbmZpbmlsb3JlL3dlYnBhY2svYmVmb3JlLXN0YXJ0dXAiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL3N0YXJ0dXAiLCJ3ZWJwYWNrOi8vaW5maW5pbG9yZS93ZWJwYWNrL2FmdGVyLXN0YXJ0dXAiXSwic291cmNlc0NvbnRlbnQiOlsiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuXHJcbmltcG9ydCB7RWxlbWVudExpYn0gZnJvbSBcIi4vTGlicy9FbGVtZW50TGliXCI7XHJcbmltcG9ydCB7RG9jdW1lbnRMaWJ9IGZyb20gXCIuL0xpYnMvRG9jdW1lbnRMaWJcIjtcclxuaW1wb3J0IHtJbnB1dEVsZW1lbnRMaWJ9IGZyb20gXCIuL0xpYnMvSW5wdXRFbGVtZW50TGliXCI7XHJcbmltcG9ydCB7S2V5TGlzdGVuZXJMaWJ9IGZyb20gXCIuL0xpYnMvS2V5RG93bkxpc3RlbmVyXCI7XHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBub2luc3BlY3Rpb24gSlNVbnVzZWRHbG9iYWxTeW1ib2xzXHJcbmV4cG9ydCBjbGFzcyBJbmZpbmlCbGF6b3Ige1xyXG4gICAgcHVibGljIGRvY3VtZW50IDogRG9jdW1lbnRMaWIgPSBuZXcgRG9jdW1lbnRMaWIoKTtcclxuICAgIHB1YmxpYyBlbGVtZW50IDogRWxlbWVudExpYiA9IG5ldyBFbGVtZW50TGliKCk7XHJcbiAgICBwdWJsaWMgaW5wdXRFbGVtZW50IDogSW5wdXRFbGVtZW50TGliID0gbmV3IElucHV0RWxlbWVudExpYigpO1xyXG4gICAgcHVibGljIGtleUxpc3RlbmVyIDogS2V5TGlzdGVuZXJMaWIgPSBuZXcgS2V5TGlzdGVuZXJMaWIoKTtcclxufVxyXG5cclxuLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG5leHBvcnQgY29uc3QgaW5maW5pQmxhem9yID0gbmV3IEluZmluaUJsYXpvcigpO1xyXG5leHBvcnQgZGVmYXVsdCBpbmZpbmlCbGF6b3I7XHJcblxyXG5kZWNsYXJlIGdsb2JhbCB7XHJcbiAgICBpbnRlcmZhY2UgV2luZG93IHtcclxuICAgICAgICBpbmZpbmlCbGF6b3I6IEluZmluaUJsYXpvcjtcclxuICAgIH1cclxufVxyXG5cclxuaWYgKHR5cGVvZiB3aW5kb3cgIT09ICd1bmRlZmluZWQnKSB7XHJcbiAgICB3aW5kb3cuaW5maW5pQmxhem9yID0gaW5maW5pQmxhem9yO1xyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcblxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gbm9pbnNwZWN0aW9uIEpTVW51c2VkR2xvYmFsU3ltYm9sc1xyXG5leHBvcnQgY2xhc3MgRG9jdW1lbnRMaWIge1xyXG4gICAgXHJcbiAgICAvLyBub2luc3BlY3Rpb24gSlNVbnVzZWRHbG9iYWxTeW1ib2xzXHJcbiAgICBwdWJsaWMgYWRkT3JVcGRhdGVFbGVtZW50QXRIZWFkKGVsZW1lbnRJZDogc3RyaW5nLCB0ZXh0Q29udGVudDogc3RyaW5nKTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50SWQpIHJldHVybjtcclxuICAgICAgICBpZiAoIXRleHRDb250ZW50KSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgY29uc3QgcG9zc2libGVFbGVtZW50ID0gZG9jdW1lbnQuaGVhZC5xdWVyeVNlbGVjdG9yKGAjJHtlbGVtZW50SWR9YCk7XHJcbiAgICAgICAgaWYgKHBvc3NpYmxlRWxlbWVudCAmJiBwb3NzaWJsZUVsZW1lbnQgaW5zdGFuY2VvZiBIVE1MU3R5bGVFbGVtZW50KSB7XHJcbiAgICAgICAgICAgIHBvc3NpYmxlRWxlbWVudC50ZXh0Q29udGVudCA9IHRleHRDb250ZW50O1xyXG4gICAgICAgICAgICByZXR1cm47XHJcbiAgICAgICAgfSBlbHNlIGlmIChwb3NzaWJsZUVsZW1lbnQpIHtcclxuICAgICAgICAgICAgZG9jdW1lbnQuaGVhZC5yZW1vdmVDaGlsZChwb3NzaWJsZUVsZW1lbnQpO1xyXG4gICAgICAgIH1cclxuXHJcbiAgICAgICAgY29uc3Qgc3R5bGVFbGVtZW50ID0gZG9jdW1lbnQuY3JlYXRlRWxlbWVudChcInN0eWxlXCIpO1xyXG4gICAgICAgIHN0eWxlRWxlbWVudC5pZCA9IGVsZW1lbnRJZDtcclxuICAgICAgICBzdHlsZUVsZW1lbnQudGV4dENvbnRlbnQgPSB0ZXh0Q29udGVudDtcclxuICAgICAgICBkb2N1bWVudC5oZWFkLmFwcGVuZENoaWxkKHN0eWxlRWxlbWVudCk7XHJcbiAgICB9ICAgIFxyXG59XHJcbiIsIi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBJbXBvcnRzXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG5cclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIENvZGVcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuZXhwb3J0IGNsYXNzIEVsZW1lbnRMaWIge1xyXG4gICAgcHVibGljIHNldFRleHRDb250ZW50KGVsZW1lbnQ6IEhUTUxFbGVtZW50LCB0ZXh0OiBzdHJpbmcpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgICAgICBpZiAoIXRleHQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LnRleHRDb250ZW50ID0gdGV4dDtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgZ2V0VGV4dENvbnRlbnQoZWxlbWVudDogSFRNTEVsZW1lbnQpOiBzdHJpbmcge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuIFwiXCI7XHJcbiAgICAgICAgaWYgKCFlbGVtZW50LnRleHRDb250ZW50KSByZXR1cm4gXCJcIjtcclxuICAgICAgICBcclxuICAgICAgICByZXR1cm4gZWxlbWVudC50ZXh0Q29udGVudDtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgYWRkSG9yaXpvbnRhbFNjcm9sbChlbGVtZW50OiBIVE1MRWxlbWVudCwgaTogbnVtYmVyKSA6IHZvaWQge1xyXG4gICAgICAgIGlmICghZWxlbWVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghaSkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQuc2Nyb2xsQnkoeyBsZWZ0OiBpLCBiZWhhdmlvcjogJ3Ntb290aCcgfSk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGNsaWNrRWxlbWVudChlbGVtZW50OiBIVE1MRWxlbWVudCkgOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybjtcclxuICAgICAgICBcclxuICAgICAgICBlbGVtZW50LmNsaWNrKCk7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGNsaWNrRWxlbWVudEJ5SWQoZWxlbWVudElkOnN0cmluZykgOiB2b2lkIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnRJZCkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGNvbnN0IGVsZW1lbnQgPSBkb2N1bWVudC5nZXRFbGVtZW50QnlJZChlbGVtZW50SWQpO1xyXG4gICAgICAgIGlmIChlbGVtZW50ID09PSBudWxsKSByZXR1cm47XHJcbiAgICAgICAgXHJcbiAgICAgICAgZWxlbWVudC5jbGljaygpO1xyXG4gICAgfVxyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmltcG9ydCB7SUlucHV0RWxlbWVudH0gZnJvbSBcIi4uL0NvbnRyYWN0cy9JSW5wdXRFbGVtZW50XCI7XHJcbmltcG9ydCB7Q1NoYXJwVHVwbGV9IGZyb20gXCIuLi9Db250cmFjdHMvQ1NoYXJwVHVwbGVcIjtcclxuXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBDb2RlXHJcbi8vIC0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLVxyXG4vLyBub2luc3BlY3Rpb24gSlNVbnVzZWRHbG9iYWxTeW1ib2xzXHJcbmV4cG9ydCBjbGFzcyBJbnB1dEVsZW1lbnRMaWIge1xyXG4gICAgcHJpdmF0ZSBpc1ZhbGlkSW5wdXRFbGVtZW50KGVsZW1lbnQ6IElJbnB1dEVsZW1lbnQpOiBib29sZWFuIHtcclxuICAgICAgICBpZiAoIWVsZW1lbnQpIHJldHVybiBmYWxzZTtcclxuICAgICAgICByZXR1cm4gZWxlbWVudC50YWdOYW1lLnRvTG93ZXJDYXNlKCkgPT09IFwiaW5wdXRcIiB8fCBlbGVtZW50LnRhZ05hbWUudG9Mb3dlckNhc2UoKSA9PT0gXCJ0ZXh0YXJlYVwiO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBnZXRTZWxlY3Rpb25TdGFydChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogbnVtYmVyIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNWYWxpZElucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuIC0xO1xyXG4gICAgICAgIHJldHVybiBlbGVtZW50LnNlbGVjdGlvblN0YXJ0IHx8IDA7XHJcbiAgICB9XHJcblxyXG4gICAgcHVibGljIGdldFNlbGVjdGlvbkVuZChlbGVtZW50OiBJSW5wdXRFbGVtZW50KTogbnVtYmVyIHtcclxuICAgICAgICBpZiAoIXRoaXMuaXNWYWxpZElucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuIC0xO1xyXG4gICAgICAgIHJldHVybiBlbGVtZW50LnNlbGVjdGlvbkVuZCB8fCAwO1xyXG4gICAgfVxyXG5cclxuICAgIHB1YmxpYyBnZXRTZWxlY3Rpb24oZWxlbWVudDogSUlucHV0RWxlbWVudCk6IENTaGFycFR1cGxlPG51bWJlciwgbnVtYmVyPiB7XHJcbiAgICAgICAgaWYgKCF0aGlzLmlzVmFsaWRJbnB1dEVsZW1lbnQoZWxlbWVudCkpIHJldHVybiB7XHJcbiAgICAgICAgICAgIEl0ZW0xOiAtMSxcclxuICAgICAgICAgICAgSXRlbTI6IC0xXHJcbiAgICAgICAgfVxyXG4gICAgICAgIHJldHVybiB7XHJcbiAgICAgICAgICAgIEl0ZW0xOiBlbGVtZW50LnNlbGVjdGlvblN0YXJ0IHx8IDAsXHJcbiAgICAgICAgICAgIEl0ZW0yOiBlbGVtZW50LnNlbGVjdGlvbkVuZCB8fCAwXHJcbiAgICAgICAgfTtcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgc2V0U2VsZWN0aW9uUmFuZ2UoZWxlbWVudDogSUlucHV0RWxlbWVudCwgc3RhcnQ6IG51bWJlciwgZW5kOiBudW1iZXIpOiB2b2lkIHtcclxuICAgICAgICBpZiAoIXN0YXJ0KSByZXR1cm47XHJcbiAgICAgICAgaWYgKCFlbmQpIHJldHVybjtcclxuICAgICAgICBpZiAoIXRoaXMuaXNWYWxpZElucHV0RWxlbWVudChlbGVtZW50KSkgcmV0dXJuO1xyXG4gICAgICAgIFxyXG4gICAgICAgIGVsZW1lbnQuZm9jdXMoKTtcclxuICAgICAgICBlbGVtZW50LnNldFNlbGVjdGlvblJhbmdlKHN0YXJ0LCBlbmQpO1xyXG4gICAgfVxyXG59IiwiLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbi8vIEltcG9ydHNcclxuLy8gLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tXHJcbmltcG9ydCB7S2V5Q29uZGl0aW9ufSBmcm9tIFwiLi4vQ29udHJhY3RzL0tleUNvbmRpdGlvblwiO1xyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuLy8gQ29kZVxyXG4vLyAtLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS0tLS1cclxuY29uc3Qga2V5c1RvU2tpcDogU2V0PHN0cmluZz4gPSBuZXcgU2V0KFtcInVcIiwgXCJiXCIsIFwiaVwiLCBcImFcIl0pO1xyXG5jb25zdCBhbGxvd1NwZWNpYWxDb25kaXRpb25zOiBLZXlDb25kaXRpb25bXSA9IFtcclxuICAgIChldmVudCwga2V5KSA9PiBldmVudC5jdHJsS2V5ICYmIGV2ZW50LnNoaWZ0S2V5ICYmIGtleSA9PT0gXCJpXCIsIC8vIFNraXAgYEN0cmwrU2hpZnQrSWBcclxuXTtcclxuXHJcbi8vIG5vaW5zcGVjdGlvbiBKU1VudXNlZEdsb2JhbFN5bWJvbHNcclxuZXhwb3J0IGNsYXNzIEtleUxpc3RlbmVyTGliIHtcclxuICAgIC8vIE1haW4gZnVuY3Rpb24gdG8gcHJldmVudCBkZWZhdWx0IGJlaGF2aW9yXHJcbiAgICBwcml2YXRlIHByZXZlbnRLZXlEZWZhdWx0KGV2ZW50OiBLZXlib2FyZEV2ZW50KTogdm9pZCB7XHJcbiAgICAgICAgaWYgKCFldmVudCkgcmV0dXJuO1xyXG4gICAgICAgIGlmICghZXZlbnQuY3RybEtleSkgcmV0dXJuO1xyXG5cclxuICAgICAgICBjb25zdCBrZXkgPSBldmVudC5rZXkudG9Mb3dlckNhc2UoKTtcclxuICAgICAgICBpZiAoYWxsb3dTcGVjaWFsQ29uZGl0aW9ucy5zb21lKGNvbmRpdGlvbiA9PiBjb25kaXRpb24oZXZlbnQsIGtleSkpKSByZXR1cm47XHJcblxyXG4gICAgICAgIC8vIEJsb2NrIGRlZmF1bHQgYmVoYXZpb3IgZm9yIGtleXMgaW4gdGhlIGtleXNUb1NraXAgc2V0XHJcbiAgICAgICAgaWYgKCFrZXlzVG9Ta2lwLmhhcyhrZXkpKSByZXR1cm47XHJcbiAgICAgICAgZXZlbnQucHJldmVudERlZmF1bHQoKTtcclxuICAgIH1cclxuXHJcblxyXG4gICAgcHVibGljIGFkZFByZXZlbnREZWZhdWx0TGlzdGVuZXIoKSA6IHZvaWQge1xyXG4gICAgICAgIGRvY3VtZW50LmFkZEV2ZW50TGlzdGVuZXIoXCJrZXlkb3duXCIsdGhpcy5wcmV2ZW50S2V5RGVmYXVsdClcclxuICAgIH1cclxuXHJcbiAgICBwdWJsaWMgcmVtb3ZlUHJldmVudERlZmF1bHRMaXN0ZW5lcigpIDogdm9pZCB7XHJcbiAgICAgICAgZG9jdW1lbnQucmVtb3ZlRXZlbnRMaXN0ZW5lcihcImtleWRvd25cIiwgdGhpcy5wcmV2ZW50S2V5RGVmYXVsdClcclxuICAgIH1cclxufSIsIi8vIFRoZSBtb2R1bGUgY2FjaGVcbnZhciBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX18gPSB7fTtcblxuLy8gVGhlIHJlcXVpcmUgZnVuY3Rpb25cbmZ1bmN0aW9uIF9fd2VicGFja19yZXF1aXJlX18obW9kdWxlSWQpIHtcblx0Ly8gQ2hlY2sgaWYgbW9kdWxlIGlzIGluIGNhY2hlXG5cdHZhciBjYWNoZWRNb2R1bGUgPSBfX3dlYnBhY2tfbW9kdWxlX2NhY2hlX19bbW9kdWxlSWRdO1xuXHRpZiAoY2FjaGVkTW9kdWxlICE9PSB1bmRlZmluZWQpIHtcblx0XHRyZXR1cm4gY2FjaGVkTW9kdWxlLmV4cG9ydHM7XG5cdH1cblx0Ly8gQ3JlYXRlIGEgbmV3IG1vZHVsZSAoYW5kIHB1dCBpdCBpbnRvIHRoZSBjYWNoZSlcblx0dmFyIG1vZHVsZSA9IF9fd2VicGFja19tb2R1bGVfY2FjaGVfX1ttb2R1bGVJZF0gPSB7XG5cdFx0Ly8gbm8gbW9kdWxlLmlkIG5lZWRlZFxuXHRcdC8vIG5vIG1vZHVsZS5sb2FkZWQgbmVlZGVkXG5cdFx0ZXhwb3J0czoge31cblx0fTtcblxuXHQvLyBFeGVjdXRlIHRoZSBtb2R1bGUgZnVuY3Rpb25cblx0X193ZWJwYWNrX21vZHVsZXNfX1ttb2R1bGVJZF0obW9kdWxlLCBtb2R1bGUuZXhwb3J0cywgX193ZWJwYWNrX3JlcXVpcmVfXyk7XG5cblx0Ly8gUmV0dXJuIHRoZSBleHBvcnRzIG9mIHRoZSBtb2R1bGVcblx0cmV0dXJuIG1vZHVsZS5leHBvcnRzO1xufVxuXG4iLCIiLCIvLyBzdGFydHVwXG4vLyBMb2FkIGVudHJ5IG1vZHVsZSBhbmQgcmV0dXJuIGV4cG9ydHNcbi8vIFRoaXMgZW50cnkgbW9kdWxlIGlzIHJlZmVyZW5jZWQgYnkgb3RoZXIgbW9kdWxlcyBzbyBpdCBjYW4ndCBiZSBpbmxpbmVkXG52YXIgX193ZWJwYWNrX2V4cG9ydHNfXyA9IF9fd2VicGFja19yZXF1aXJlX18oXCIuL3NyYy9JbmZpbmlMb3JlLkluZmluaUJsYXpvci9UeXBlc2NyaXB0TGliL0luZGV4LnRzXCIpO1xuIiwiIl0sIm5hbWVzIjpbXSwic291cmNlUm9vdCI6IiJ9
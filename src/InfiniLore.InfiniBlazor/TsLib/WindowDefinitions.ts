// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
import {getInputSelectionStart, getInputSelectionEnd, setInputSelectionRange, getInputSelection} from "./InputSelection";
import {addPreventDefaultListener, removePreventDefaultListener} from "./KeyDownListener";
import {setTextContent, getTextContent} from "./HtmlElementHelpers";
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
window.getInputSelectionStart = getInputSelectionStart;
window.getInputSelectionEnd = getInputSelectionEnd;
window.getInputSelection = getInputSelection;
window.setInputSelectionRange = setInputSelectionRange;

window.addPreventDefaultListener = addPreventDefaultListener;
window.removePreventDefaultListener = removePreventDefaultListener;

window.setTextContent = setTextContent;
window.getTextContent = getTextContent;
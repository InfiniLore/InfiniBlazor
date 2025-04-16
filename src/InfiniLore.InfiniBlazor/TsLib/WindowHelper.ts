// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
import {getInputSelectionStart, getInputSelectionEnd, setInputSelectionRange, getInputSelection} from "./InputSelection";
import {addPreventDefaultListener, removePreventDefaultListener} from "./KeyDownListener";
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
window.getInputSelectionStart = getInputSelectionStart;
window.getInputSelectionEnd = getInputSelectionEnd;
window.getInputSelection = getInputSelection;
window.setInputSelectionRange = setInputSelectionRange;

window.addPreventDefaultListener = addPreventDefaultListener;
window.removePreventDefaultListener = removePreventDefaultListener;
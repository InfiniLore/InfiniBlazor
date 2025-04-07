// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
import {getSelectionStart, getSelectionEnd, setSelectionRange} from "./Selection";
import {addPreventDefaultListener, removePreventDefaultListener} from "./KeyDownListener";
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
window.getSelectionStart = getSelectionStart;
window.getSelectionEnd = getSelectionEnd;
window.setSelectionRange = setSelectionRange;

window.addPreventDefaultListener = addPreventDefaultListener;
window.removePreventDefaultListener = removePreventDefaultListener;
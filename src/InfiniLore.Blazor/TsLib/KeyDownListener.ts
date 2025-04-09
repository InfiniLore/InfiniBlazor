// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------

// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
function preventKeyDefault(event:KeyboardEvent) : void {
    if (!event) return;
    if (!(event.ctrlKey)) return;
    if (event.key === "F5" || event.key === "F12") return;
    // if (event.ctrlKey && event.key.toLowerCase() === "z") return;
    // if (event.ctrlKey && event.key.toLowerCase() === "y") return;
    // if (event.ctrlKey && event.key.toLowerCase() === "c") return;
    // if (event.ctrlKey && event.key.toLowerCase() === "v") return;
    // if (event.ctrlKey && event.key.toLowerCase() === "x") return;
    if (event.ctrlKey && event.shiftKey && event.key.toLowerCase() === "i" ) return;
    event.preventDefault();
}

export function addPreventDefaultListener() : void {
    document.addEventListener("keydown",preventKeyDefault)
}

export function removePreventDefaultListener() : void {
    document.removeEventListener("keydown", preventKeyDefault)
}
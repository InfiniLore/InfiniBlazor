// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
import {KeyCondition} from "./Contracts";
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
const keysToSkip: Set<string> = new Set(["u", "b", "i", "a"]);
const allowSpecialConditions: KeyCondition[] = [
    (event, key) => event.ctrlKey && event.shiftKey && key === "i", // Skip `Ctrl+Shift+I`
];

// Main function to prevent default behavior
function preventKeyDefault(event: KeyboardEvent): void {
    if (!event) return;
    if (!event.ctrlKey) return;

    const key = event.key.toLowerCase();
    if (allowSpecialConditions.some(condition => condition(event, key))) return;

    // Block default behavior for keys in the keysToSkip set
    if (!keysToSkip.has(key)) return;
    event.preventDefault();
}


export function addPreventDefaultListener() : void {
    document.addEventListener("keydown",preventKeyDefault)
}

export function removePreventDefaultListener() : void {
    document.removeEventListener("keydown", preventKeyDefault)
}
// ---------------------------------------------------------------------------------------------------------------------
// Imports
// ---------------------------------------------------------------------------------------------------------------------
import {KeyCondition} from "../Contracts/KeyCondition";
// ---------------------------------------------------------------------------------------------------------------------
// Code
// ---------------------------------------------------------------------------------------------------------------------
const keysToSkip: Set<string> = new Set(["u", "b", "i", "a"]);
const allowSpecialConditions: KeyCondition[] = [
    (event, key) => event.ctrlKey && event.shiftKey && key === "i", // Skip `Ctrl+Shift+I`
];

// noinspection JSUnusedGlobalSymbols
export class KeyListenerLib {
    // Main function to prevent default behavior
    private preventKeyDefault(event: KeyboardEvent): void {
        if (!event) return;
        if (!event.ctrlKey) return;

        const key = event.key.toLowerCase();
        if (allowSpecialConditions.some(condition => condition(event, key))) return;

        // Block default behavior for keys in the keysToSkip set
        if (!keysToSkip.has(key)) return;
        event.preventDefault();
    }


    public addPreventDefaultListener() : void {
        document.addEventListener("keydown",this.preventKeyDefault)
    }

    public removePreventDefaultListener() : void {
        document.removeEventListener("keydown", this.preventKeyDefault)
    }
}
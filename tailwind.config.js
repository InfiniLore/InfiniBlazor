/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        "./**/*.{razor,html,cshtml}"
    ],
    theme: {
        extend: {
        },
    },
    variants: {
        extend: {
            width: ['responsive', 'hover', 'focus'],
        },
    },
    plugins: [
    ],
}


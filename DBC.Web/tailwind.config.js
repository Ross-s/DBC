/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./wwwroot/js/**/*.tsx"],
    theme: {
        extend: {},
    },
    plugins: [
        require('daisyui')
    ],
}


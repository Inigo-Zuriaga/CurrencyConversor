/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    "./src/**/*.{html,ts}", // Ajusta esta ruta según tu estructura de archivos
  ],
  theme: {
    extend: {
      colors: {
        light: '#6EC1E4',  // Ejemplo de un color claro para "primary"
        DEFAULT: '#3490dc', // El color principal (por defecto) de "primary"
        dark: '#2779bd',    // Ejemplo de un color oscuro para "primary"
      },
    },
  },
  plugins: [],
}

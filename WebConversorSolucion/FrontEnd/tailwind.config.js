/** @type {import('tailwindcss').Config} */

// const { addDynamicIconSelectors } = require('@iconify/tailwind')
//
// module.exports = {
//   content: [
//     "./src/**/*.{html,ts}", // Ajusta esta ruta según tu estructura de archivos
//   ],
//
//   theme: {
//     extend: {
//       fontFamily: {
//         regularb: 'var(--font-barlow)',
//         semiboldb: 'var(--font-barlowsemibold)',
//         boldb: 'var(--font-barlowbold)'
//
//       },
//
//       },
//       colors: {
//         blue3: '#bbe3ff',
//         blue4: '#8cd3ff',
//         primaryBlue: '#14213D',
//         primaryRed: '#93032E',
//         white2: '#F9F9F9',
//         light: '#6EC1E4',  // Ejemplo de un color claro para "primary"
//         DEFAULT: '#3490dc', // El color principal (por defecto) de "primary"
//         dark: '#2779bd',    // Ejemplo de un color oscuro para "primary"
//         link_blue:'#0a66c1'
//       },
//     },
//   screens: {
//     sm: '640px', // Móvil
//     md: '768px', // Tablet
//     lg: '1024px', // iPad o Desktop
//     xl: '1280px',
//   },
//
//   plugins: [addDynamicIconSelectors()],
//
// }T

const { addDynamicIconSelectors } = require('@iconify/tailwind');

module.exports = {
  content: [
    "./src/**/*.{html,ts}", // Ajusta esta ruta según tu estructura de archivos
  ],
  theme: {
    extend: {
      fontFamily: {
        regularb: 'var(--font-barlow)',
        semiboldb: 'var(--font-barlowsemibold)',
        boldb: 'var(--font-barlowbold)',
      },
      colors: {
        blue3: '#bbe3ff',
        blue4: '#8cd3ff',
        primaryBlue: '#14213D',
        primaryRed: '#93032E',
        white2: '#F9F9F9',
        light: '#6EC1E4',
        DEFAULT: '#3490dc', // Color principal de "primary"
        dark: '#2779bd',
        link_blue: '#0a66c1',
      },
      screens: {
        sm: '640px', // Móvil
        md: '768px', // Tablet
        lg: '1024px', // iPad o Desktop
        xl: '1280px',
      },
    },
  },
  plugins: [addDynamicIconSelectors()],
};


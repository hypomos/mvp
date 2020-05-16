module.exports = {
  theme: {
    spinner: (theme) => ({
      default: {
        color: '#dae1e7', // color you want to make the spinner
        size: '2em', // size of the spinner (used for both width and height)
        border: '2px', // border-width of the spinner (shouldn't be bigger than half the spinner's size)
        speed: '750ms', // the speed at which the spinner should rotate
      },
      // md: {
      //   color: theme('colors.red.500', 'red'),
      //   size: '2em',
      //   border: '2px',
      //   speed: '500ms',
      // },
    }),

    minWidth: {
      '40': '10rem'
    },
    extend: {
      spacing: {
        '80': '20rem',
        '84': '21rem',
        '88': '22rem',
        '1/2': '50%',
        '1/3': '33.333333%',
        '2/3': '66.666667%',
        '1/4': '25%',
        '2/4': '50%',
        '3/4': '75%',
        '1/5': '20%',
        '2/5': '40%',
        '3/5': '60%',
        '4/5': '80%',
        '1/6': '16.666667%',
        '2/6': '33.333333%',
        '3/6': '50%',
        '4/6': '66.666667%',
        '5/6': '83.333333%',
        '1/12': '8.333333%',
        '2/12': '16.666667%',
        '3/12': '25%',
        '4/12': '33.333333%',
        '5/12': '41.666667%',
        '6/12': '50%',
        '7/12': '58.333333%',
        '8/12': '66.666667%',
        '9/12': '75%',
        '10/12': '83.333333%',
        '11/12': '91.666667%'
      }
    }
  },
  variants: {
    textColor: ['responsive', 'hover', 'group-hover', 'focus', 'focus-within'],
    backgroundColor: ['responsive', 'hover', 'focus', 'focus-within'],
    spinner: ['responsive'],
  },
  plugins: [
    require('tailwindcss-spinner')(), // no options to configure
  ]
}

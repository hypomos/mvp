const defaultTheme = require('tailwindcss/defaultTheme');

module.exports = {
    future: {
        removeDeprecatedGapUtilities: true,
        purgeLayersByDefault: true,
        defaultLineHeights: true,
        standardFontHeights: true
    },
    purge: {
        enabled: process.env.NODE_ENV === 'production',
        content: ['./index.html', './src/**/*.vue', './src/**/*.js', './src/**/*.ts'],
    },
    theme: {
        extend: {
            fontFamily: {
                sans: ['Inter var', ...defaultTheme.fontFamily.sans],
            },
        },
    },
    plugins: [
        require('@tailwindcss/typography'),
        // require('@tailwindcss/custom-forms')
    ],
};
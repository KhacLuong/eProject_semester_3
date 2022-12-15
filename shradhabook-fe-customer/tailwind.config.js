/** @type {import('tailwindcss').Config} */
module.exports = {
    content: ["./src/**/*.{html,js}"],
    theme: {
        extend: {
            fontFamily: {
                sora: ['Sora', '"sans-serif"']
            },
            colors: {
                transparent: 'transparent',
                primaryColor: '#3f87f5',
                successColor: '#00c9a7',
                violetColor: '#886cff',
                dangerColor: {
                    default_1: '#de4436',
                    default_2: '#F65D4E',
                    hover_2: '#f4402f',
                },
                warningColor: '#ffc021',
                whiteColor: '#ffffff',
                grayColor: '#f9fbfd',
                darkColor: '#223143',
                blackColor: '#000',
                background_color: '#282828'
            },
            borderRadius: {
                'none': '0',
                'sm': '.125rem',
                DEFAULT: '.25rem',
                'lg': '.5rem',
                'full': '9999px',
            },
            opacity: {
                '0': '0',
                '20': '0.2',
                '40': '0.4',
                '60': '0.6',
                '80': '0.8',
                '100': '1',
            },
        },
    },
    plugins: [],
}

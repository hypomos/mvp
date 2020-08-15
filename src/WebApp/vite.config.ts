import type { UserConfig } from 'vite';
import commonjs from 'rollup-plugin-commonjs';

const config: UserConfig = {
    rollupInputOptions: {
        plugins: [
            commonjs({
                // explicitly specify unresolvable named exports
                namedExports: { 'oidc-client': ["UserManager"] }
            })
        ]
    },
    rollupOutputOptions: {
        plugins: [
            commonjs({
                // explicitly specify unresolvable named exports
                namedExports: { 'oidc-client': ["UserManager"] }
            })
        ]
    }
}
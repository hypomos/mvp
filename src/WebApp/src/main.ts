import { createApp } from 'vue';
import { vuexOidcCreateRouterMiddleware } from 'vuex-oidc';

import App from './App.vue';
import { store } from './store';
import { router } from './router';

import './assets/main.css';

router.beforeEach(vuexOidcCreateRouterMiddleware(store));

createApp(App).use(store).use(router).mount('#app');

import { createApp } from 'vue';
import { vuexOidcCreateRouterMiddleware } from 'vuex-oidc';

import App from './App.vue';
import { store } from './store';
import { router } from './router';

import MainLayout from './layouts/MainLayout.vue';
import EmptyLayout from './layouts/EmptyLayout.vue';

import './assets/main.css';

router.beforeEach(vuexOidcCreateRouterMiddleware(store));

const app = createApp(App);

app.component('default-layout', MainLayout);
app.component('empty-layout', EmptyLayout);

app.use(store);
app.use(router);

app.mount('#app');

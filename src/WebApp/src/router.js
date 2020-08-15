import { createRouter, createWebHistory } from 'vue-router';

import HelloWorld from './components/HelloWorld.vue';
import Home from './components/Home.vue';
import Simple from './components/Simple.vue';

export const HomePath = '/';
export const HelloPath = '/hello';
export const SimplePath = '/simple';
export const OidcCallbackPath = '/oidc-callback';

// there is also createWebHashHistory and createMemoryHistory
export const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: HomePath,
      name: 'home',
      component: Home,
      isPublic: true,
    },
    {
      path: HelloPath,
      name: 'hello',
      component: HelloWorld,
    },
    {
      path: SimplePath,
      name: 'simple',
      component: Simple,
    },
    {
      path: OidcCallbackPath, // Needs to match redirectUri (redirect_uri if you use snake case) in your oidcSettings
      name: 'oidcCallback',
      component: OidcCallback,
    },
  ],
});

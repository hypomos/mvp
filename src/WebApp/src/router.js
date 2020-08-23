import { createRouter, createWebHistory } from 'vue-router';

import HelloWorld from './components/HelloWorld.vue';
import Home from './components/Home.vue';
import Simple from './components/Simple.vue';

import OidcCallback from './components/OidcCallback.vue';
import OidcSilentRenew from './components/SilentRenew.vue';
import OidcError from './components/OidcError.vue';

import Secret from './components/Secret.vue';

export const HomePath = '/';
export const HelloPath = '/hello';
export const SimplePath = '/simple';

export const OidcCallbackPath = '/oidc-callback';
export const OidcSilentRenewPath = '/silentRenew';
export const OidcErrorPath = '/oidc-callback-error';

export const SecretPath = '/secret';

// there is also createWebHashHistory and createMemoryHistory
export const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: HomePath,
      name: 'home',
      component: Home,
      meta: {
        isPublic: true
      }
    },
    {
      path: HelloPath,
      name: 'hello',
      component: HelloWorld,
      meta: {
        isPublic: true
      }
    },
    {
      path: SimplePath,
      name: 'simple',
      component: Simple,
      meta: {
        isPublic: true
      }
    },
    {
      path: OidcSilentRenewPath, // Needs to match redirectUri (redirect_uri if you use snake case) in your oidcSettings
      name: 'silent-renew',
      component: OidcSilentRenew,
      meta: {
        isPublic: true
      }
    },
    {
      path: OidcCallbackPath, // Needs to match redirectUri (redirect_uri if you use snake case) in your oidcSettings
      name: 'oidcCallback',
      component: OidcCallback,
      meta: {
        isPublic: true
      }
    },
    {
      path: OidcErrorPath, // Needs to match redirectUri (redirect_uri if you use snake case) in your oidcSettings
      name: 'oidcError',
      component: OidcError,
      meta: {
        isPublic: true
      }
    },
    {
      path: SecretPath,
      name: 'secret',
      component: Secret,
      meta: {
        isPublic: false
      }
    }
  ],
});

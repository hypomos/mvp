import { createRouter, createWebHistory } from 'vue-router';

import HelloWorld from './components/Hello/HelloWorld.vue';
import HelloDetail from './components/Hello/HelloDetail.vue';
import Home from './components/Home.vue';
import Simple from './components/Simple.vue';

import OidcCallback from './components/OidcCallback.vue';
import OidcSilentRenew from './components/SilentRenew.vue';
import OidcError from './components/OidcError.vue';

import Secret from './components/Secret.vue';

// there is also createWebHashHistory and createMemoryHistory
export const router = createRouter({
  history: createWebHistory(),
  routes: [
    {
      path: '/',
      name: 'home',
      component: Home,
      meta: {
        isPublic: true
      }
    },
    {
      path: '/hello',
      name: 'hello',
      component: HelloWorld,
      meta: {
        isPublic: true
      },
      children: [
        {
          path: ':id',
          component: HelloDetail,
          name: 'helloDetail'
        }
      ]
    },
    {
      path: '/simple',
      name: 'simple',
      component: Simple,
      meta: {
        isPublic: true
      }
    },
    {
      path: '/silentRenew', // Needs to match redirectUri (redirect_uri if you use snake case) in your oidcSettings
      name: 'silent-renew',
      component: OidcSilentRenew,
      meta: {
        isPublic: true
      }
    },
    {
      path: '/oidc-callback', // Needs to match redirectUri (redirect_uri if you use snake case) in your oidcSettings
      name: 'oidcCallback',
      component: OidcCallback,
      meta: {
        isPublic: true
      }
    },
    {
      path: '/oidc-callback-error', // Needs to match redirectUri (redirect_uri if you use snake case) in your oidcSettings
      name: 'oidcError',
      component: OidcError,
      meta: {
        isPublic: true
      }
    },
    {
      path: '/secret',
      name: 'secret',
      component: Secret,
      meta: {
        isPublic: false
      }
    }
  ],
});

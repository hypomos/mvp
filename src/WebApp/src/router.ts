import { createRouter, createWebHistory } from 'vue-router';

import HelloWorld from './components/Hello/HelloWorld.vue';
import HelloDetail from './components/Hello/HelloDetail.vue';

import Home from './components/Home.vue';
import App from './components/App/App.vue';

import OidcCallback from './components/Oidc/OidcCallback.vue';
import OidcSilentRenew from './components/Oidc/SilentRenew.vue';
import OidcError from './components/Oidc/OidcError.vue';

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
        isPublic: true,
        layout: 'empty'
      }
    },
    {
      path: '/app',
      name: 'app',
      component: App,
      meta: {
        isPublic: false,
        layout: 'default'
      }
    },
    {
      path: '/hello',
      name: 'hello',
      component: HelloWorld,
      meta: {
        isPublic: true,
        layout: 'empty'
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
      path: '/silent-renew', // Needs to match redirectUri (redirect_uri if you use snake case) in your oidcSettings
      name: 'silent-renew',
      component: OidcSilentRenew,
      meta: {
        isPublic: true,
        layout: 'empty'
      }
    },
    {
      path: '/oidc-callback', // Needs to match redirectUri (redirect_uri if you use snake case) in your oidcSettings
      name: 'oidcCallback',
      component: OidcCallback,
      meta: {
        isPublic: true,
        layout: 'empty'
      }
    },
    {
      path: '/oidc-callback-error', // Needs to match redirectUri (redirect_uri if you use snake case) in your oidcSettings
      name: 'oidcError',
      component: OidcError,
      meta: {
        isPublic: true,
        layout: 'empty'
      }
    },
    {
      path: '/secret',
      name: 'secret',
      component: Secret,
      meta: {
        isPublic: false,
        layout: 'empty'
      }
    }
  ],
});

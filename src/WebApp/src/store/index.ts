import { createStore } from 'vuex';
import { vuexOidcCreateStoreModule } from 'vuex-oidc'

import { oidcSettings } from '../config/oidc'
import { State } from './state';

export const store = createStore<State>({
  state() {
    return {
      count: 1
    }
  },
  getters: {
    count: state => {
      return state.count;
    }
  },
  mutations: {
    incrementCount: (state, payload) => {
      state.count++;
    }
  },
  modules: {
    oidcStore: vuexOidcCreateStoreModule(
      oidcSettings,
      { isAuthenticatedBy: 'access_token' },
      {
        userLoaded: (user) => console.log('OIDC user is loaded:', user),
        userUnloaded: () => console.log('OIDC user is unloaded'),
        accessTokenExpiring: () => console.log('Access token will expire'),
        accessTokenExpired: () => console.log('Access token did expire'),
        silentRenewError: () => console.log('OIDC user is unloaded'),
        userSignedOut: () => console.log('OIDC user is signed out'),
        oidcError: () => console.log('An oidc error occured')
      })
  }
});
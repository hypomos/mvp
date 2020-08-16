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
    oidcStore: vuexOidcCreateStoreModule(oidcSettings, { isAuthenticatedBy: 'access_token' })
  }
});
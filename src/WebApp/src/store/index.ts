import { createStore } from 'vuex';
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
  }
});
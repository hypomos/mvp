import { createRouter, createWebHistory } from 'vue-router';

import HelloWorld from './components/HelloWorld.vue'
import Home from './components/Home.vue';
import Simple from './components/Simple.vue';

export const HomePath = {
    path: '/',
    component: Home,
  };
  

export const HelloPath = {
  path: '/hello',
  component: HelloWorld,
};

export const SimplePath = {
  path: '/simple',
  component: Simple,
};

// there is also createWebHashHistory and createMemoryHistory
export const router = createRouter({
  history: createWebHistory(),
  routes: [
      HomePath,
      HelloPath,
      SimplePath
    ],
});
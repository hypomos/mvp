<template>
  <img alt="Vue logo" src="./assets/logo.png" />

  <div id="nav">
    <router-link :to="{ name:'home' }">Home</router-link>
    <router-link :to="{ name:'hello' }">Hello</router-link>
    <router-link :to="{ name:'simple' }">Simple</router-link>
    <router-link :to="{ name:'secret' }">Secret</router-link>
    <router-link :to="{ name: 'helloDetail', params: { id: 1 }}">HelloDetail1</router-link>
    <signed-in-as />
  </div>
  <router-view />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { RouterLink, RouterView } from 'vue-router';

import SignedInAs from './components/SignedInAs.vue';

export default defineComponent({
  components: {
    RouterLink,
    RouterView,
    SignedInAs,
  },
});
</script>

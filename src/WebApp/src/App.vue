<template>
  <img alt="Vue logo" src="./assets/logo.png" />

  <div id="nav">
    <router-link :to="HomePath">Home</router-link>
    <router-link :to="HelloPath">Hello</router-link>
    <router-link :to="SimplePath">Simple</router-link>
  </div>
  <router-view />
</template>

<script lang="ts">
  import { RouterLink, RouterView } from 'vue-router';
  import { HomePath, HelloPath, SimplePath } from './router';

  export default {
    name: 'App',
    data() {
      return {
        HomePath,
        HelloPath,
        SimplePath,
      };
    },
    components: {
      RouterLink,
      RouterView,
    },
  };
</script>

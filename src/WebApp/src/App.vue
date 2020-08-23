<template>
  <img alt="Vue logo" src="./assets/logo.png" />

  <div id="nav">
    <router-link :to="HomePath">Home</router-link>
    <router-link :to="HelloPath">Hello</router-link>
    <router-link :to="SimplePath">Simple</router-link>
    <router-link :to="SecretPath">Secret</router-link>
    <signed-in-as />
  </div>
  <router-view />
</template>

<script lang="ts">
  import { RouterLink, RouterView } from 'vue-router';
  import { HomePath, HelloPath, SimplePath, SecretPath } from './router';

  import SignedInAs from './components/SignedInAs.vue';

  export default {
    name: 'App',
    data() {
      return {
        HomePath,
        HelloPath,
        SimplePath,
        SecretPath
      };
    },
    components: {
      RouterLink,
      RouterView,
      SignedInAs
    },
  };
</script>

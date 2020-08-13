<template>
  <img alt="Vue logo" src="./assets/logo.png" />

  <div id="nav">
    <router-link :to="homePath">Home</router-link>
    <router-link :to="helloPath">Hello</router-link>
    <router-link :to="simplePath">Simple</router-link>
  </div>
  <router-view />
</template>

<script lang="ts">
import { RouterLink, RouterView } from "vue-router";
import { HomePath, HelloPath, SimplePath } from "./router";

export default {
  name: "App",
  data() {
    return {
      homePath: HomePath.path,
      helloPath: HelloPath.path,
      simplePath: SimplePath.path
    };
  },
  components: {
    RouterLink,
    RouterView,
  },
};
</script>

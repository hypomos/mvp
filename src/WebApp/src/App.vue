<template>
  <!-- <img alt="Vue logo" src="./assets/logo.png" />

  <div id="nav">
    <router-link :to="{ name:'home' }">Home</router-link>
    <router-link :to="{ name: 'helloDetail', params: { id: 1 }}">HelloDetail1</router-link>
  </div> -->
  <component :is="layout">
    <router-view />
  </component>
</template>

<script lang="ts">
import { defineComponent, ref, computed } from 'vue';
import { useRouter } from 'vue-router';

const defaultLayout = 'default';

export default defineComponent({
  setup() {
    const { currentRoute } = useRouter();
    const layout = computed(
      () => `${currentRoute.value.meta.layout || defaultLayout}-layout`
    );
    return {
      layout,
    };
  },
});

</script>

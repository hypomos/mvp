<template>
  <div v-if="!isInitialized">
    Loading...
  </div>

  <div class="flex h-screen bg-gray-200 font-roboto" v-if="isInitialized">
    <Sidebar />

    <div class="flex-1 flex flex-col overflow-hidden">
      <Header />

      <main class="flex-1 overflow-x-hidden overflow-y-auto bg-gray-200">
        <div class="container mx-auto px-6 py-8">
          <slot />
        </div>
      </main>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';

import { useSidebar } from '../hooks/useSidebar';
import Sidebar from '../components/Sidebar/Sidebar.vue';
import Header from '../components/Header/Header.vue';
import { authenticatedRequest } from '../services/axios-auth';

export default defineComponent({
  // mounted() {
  //   authenticatedRequest('/api/', 'GET')
  //     .then(response => {
  //       debugger;
  //     });
  // },

  data() {
    return {
      isInitialized: false
    };
  },

  async mounted() {
    var result = await fetch('config.json');
    var json = await result.json() as { HypomosApp: string, HypomosApi: string, HypomosOidc: string};

    this.isInitialized = true;    
  },

  components: {
    Header,
    Sidebar,
  },
});
</script>
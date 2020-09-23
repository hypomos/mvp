<template>
  <header
    class="flex justify-between items-center py-4 px-6 bg-white border-b-4 border-indigo-600"
  >
    <div class="flex items-center">
      <!-- Hamburger Menu: -->
      <button
        @click="isOpen = true"
        class="text-gray-500 focus:outline-none lg:hidden"
      >
        <svg
          class="h-6 w-6"
          viewBox="0 0 24 24"
          fill="none"
          xmlns="http://www.w3.org/2000/svg"
        >
          <path
            d="M4 6H20M4 12H20M4 18H11"
            stroke="currentColor"
            stroke-width="2"
            stroke-linecap="round"
            stroke-linejoin="round"
          />
        </svg>
      </button>

      <!-- Search Box: -->
      <SearchBox />
    </div>

    <div class="flex items-center">
      <!-- Notifications -->
      <Notifications />

      <SignedInAs />
    </div>
  </header>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { useSidebar } from '../../hooks/useSidebar';

import SearchBox from './SearchBox.vue';
import Notifications from './Notifications.vue';
import SignedInAs from './SignedInAs.vue';

export default defineComponent({
  setup(_, { emit }) {
    const dropdownOpen = ref(false);
    const { isOpen } = useSidebar();

    return {
      isOpen,
      dropdownOpen,
    };
  },

  components: {
    SearchBox,
    Notifications,
    SignedInAs,
  },
});
</script>
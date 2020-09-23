<template>
  <div class="relative">
    <button
      @click="dropdownOpen = !dropdownOpen"
      class="relative z-10 block focus:outline-none"
    >
      <div>
        {{ oidcUser?.name }}
      </div>
    </button>

    <div
      v-show="dropdownOpen"
      @click="dropdownOpen = false"
      class="fixed inset-0 h-full w-full z-10"
    ></div>

    <div
      v-show="dropdownOpen"
      class="absolute right-0 mt-2 py-2 w-48 bg-white rounded-md shadow-xl z-20"
    >
      <a
        href="#"
        class="block px-4 py-2 text-sm text-gray-700 hover:bg-indigo-600 hover:text-white"
      >
        Profile
      </a>
      <a
        @click="signOutOidc"
        class="block px-4 py-2 text-sm text-gray-700 hover:bg-indigo-600 hover:text-white"
      >
        Log out
      </a>
    </div>
  </div>
</template>

<script lang="ts">
import { defineComponent, ref } from 'vue';
import { mapGetters, mapActions } from 'vuex';

export default defineComponent({
  setup() {
    const dropdownOpen = ref(false);

    return {
      dropdownOpen,
    };
  },

  computed: {
    ...mapGetters(['oidcUser']),
  },
  methods: {
    ...mapActions(['signOutOidc']),
  },
});
</script>
<template>
  <header
    class="flex justify-between items-center py-4 px-6 bg-white border-b-4 border-indigo-600"
  >
    <div>
      <!-- still empty -->
    </div>
    <RouterLink :to="{ name: 'app' }">Login</RouterLink>
  </header>

  <main class="flex justify-center pt-8">
    <article class="prose">
      <h1>Welcome to Hypomos</h1>

      <p>Please login to enjoy it's full functionality :)</p>
    </article>
  </main>
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { RouterLink } from 'vue-router';

export default defineComponent({
  name: 'Hello',
  components: {
    RouterLink,
  },
});
</script>

<template>
  <div>Loading...</div>
</template>

<script lang="ts">
import { mapActions } from 'vuex';
import { defineComponent } from 'vue';

export default defineComponent({
  name: 'OidcCallback',
  methods: {
    ...mapActions(['oidcSignInCallback']),
  },
  mounted() {
    this.oidcSignInCallback()
      .then((redirectPath) => {
        this.$router.push(redirectPath);
      })
      .catch((err) => {
        console.error(err);
        this.$router.push({ name: 'oidcError' }); // Handle errors any way you want
      });
  },
});
</script>

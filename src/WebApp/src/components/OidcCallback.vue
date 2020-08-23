<template>
  <div>
    Loading...
  </div>
</template>

<script lang="ts">
  import { mapActions } from 'vuex';
  import { defineComponent } from 'vue';
  import { OidcErrorPath } from '../router';

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
          this.$router.push(OidcErrorPath); // Handle errors any way you want
        });
    },
  });
</script>

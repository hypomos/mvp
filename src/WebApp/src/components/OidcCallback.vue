<template>
  <div>
    Loading...
  </div>
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
          debugger;
          this.$router.push(redirectPath);
        })
        .catch((err) => {
          debugger;
          console.error(err);
          this.$router.push('/oidc-callback-error'); // Handle errors any way you want
        });
    },
  });
</script>

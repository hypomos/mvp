<template>
  <div v-if="oidcUser">
    Signed in as {{ signedIn }}
    <button @click="signOutOidc">Sign out</button>
  </div>
</template>

<script lang="ts">
import { mapGetters, mapActions } from 'vuex';

export default {
  name: 'SignedInAs',
  
  computed: {
    signedIn():string {
      debugger;
      return this.$store.state.oidcStore?.user;
    },
    ...mapGetters(['oidcUser']),
  },
  methods: {
    ...mapActions(['signOutOidc']),
  },
};
</script>
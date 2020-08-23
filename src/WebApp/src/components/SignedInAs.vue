<template>
  <div v-if="oidcUser">
    Signed in as {{ oidcUser.email }}
    <button @click="signOutOidc">Sign out</button>
  </div>
</template>

<script lang="ts">
import { mapGetters, mapActions } from 'vuex';

export default {
  name: 'SignedInAs',
  computed: {
    ...mapGetters(['oidcUser']),
  },
  methods: {
    ...mapActions(['signOutOidc']),
  },
};
</script>
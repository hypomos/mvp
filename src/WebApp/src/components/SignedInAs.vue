<template>
  <div v-if="oidcUser">
    {{ oidcUser.name }}
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
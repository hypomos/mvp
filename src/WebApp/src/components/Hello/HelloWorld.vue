<template>
  <h1>{{ msg }}</h1>
  <button @click="increment">count is: {{ count }}</button>
  <p>
    Edit <code>components/Hello/HelloWorld.vue</code> to test hot module replacement.
  </p>

  <router-view />
</template>

<script lang="ts">
import { defineComponent } from 'vue';
import { mapGetters } from 'vuex';
import { RouterView } from 'vue-router';

export default defineComponent({
  name: 'HelloWorld',
  props: {
    msg: String,
  },
  computed: mapGetters({
    count: 'count',
  }),
  methods: {
    increment() {
      console.log('hello');
      this.$store.commit('incrementCount');
    },
  },
  components: {
    RouterView,
  },
});
</script>

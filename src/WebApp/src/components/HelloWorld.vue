<template>
  <h1>{{ msg }}</h1>
  <button @click="increment">count is: {{ count }}</button>
  <p>
    Edit <code>components/HelloWorld.vue</code> to test hot module replacement.
  </p>
  <h1>hallo lukas</h1>
</template>

<script lang="ts">
import { defineComponent } from "vue";
import { mapGetters } from "vuex";

export default defineComponent({
  name: "HelloWorld",
  props: {
    msg: String,
  },
  computed: mapGetters({
    count: "count",
  }),
  methods: {
    increment() {
      console.log("hello");
      this.$store.commit('incrementCount');
    },
  },
});
</script>

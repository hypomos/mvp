<template>
  <h1>Hello 2</h1>
  <button @click="counter++">{{ counter }}</button>
  <p>{{ counter }}</p>
</template>

<script>
  export default {
    data() {
      return {
        counter: 0,
      };
    },
  };
</script>

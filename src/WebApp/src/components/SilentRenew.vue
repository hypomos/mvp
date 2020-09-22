<template>
    <h1>Silent Renew in progress...</h1>
</template>

<script>
import { defineComponent } from 'vue';
import { vuexOidcProcessSilentSignInCallback } from 'vuex-oidc';

export default defineComponent({
    setup() {
        vuexOidcProcessSilentSignInCallback();
    }
});
</script>
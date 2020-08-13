<template>

  <p>Logged In: {{ isLoggedIn }}</p>

  <button class="btn" @click="login" v-if="!isLoggedIn">Login</button> //
  <button class="btn" @click="logout" v-if="isLoggedIn">Logout</button> //
  <button class="btn" @click="getProtectedApiData" v-if="isLoggedIn">
    Get API data
  </button>

</template>

<script lang="ts">
import AuthService from "./services/AuthService";

export default {
    name: "Login",
  
  data() {
    return {
      isLoggedIn: false,
      auth: new AuthService(),
    };
  },
  methods: {
    login() {
      console.log("hello");
      // console.log(new oidc.UserManager());

      //this.auth.login();
    },
    logout() {
      // this.auth.logout();
    },
  },
};
}
<script>
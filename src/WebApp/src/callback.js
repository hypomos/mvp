import Oidc from "oidc-client";

var mgr = new Oidc.UserManager({
  response_mode: "query",
  userStore: new Oidc.WebStorageStateStore(),
})
  .signinRedirectCallback()
  .then(function (user) {
    console.log("signin response success", user);
    window.location.href = "../";
  })
  .catch(function (err) {
    console.log(err);
  });

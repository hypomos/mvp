import { createUserManager } from 'redux-oidc';

const userManagerConfig = {
  client_id: 'hypomos-web-app',
  redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}/callback`,
  response_type: 'token id_token',
  scope: 'hypomos openid profile',
  authority: 'http://localhost:5000',
  silent_redirect_uri: `${window.location.protocol}//${window.location.hostname}${window.location.port ? `:${window.location.port}` : ''}/silent_renew.html`,
  automaticSilentRenew: true,
  filterProtocolClaims: true,
  loadUserInfo: true,
  post_logout_redirect_uri: `${window.location.protocol}//${window.location.hostname}:${window.location.port}`,
};

const userManager = createUserManager(userManagerConfig);

export default userManager;
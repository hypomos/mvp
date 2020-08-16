const STS_DOMAIN: string = "http://localhost:5000";
const BASE: string = "http://localhost:3000";

export const oidcSettings = {
  authority: STS_DOMAIN,
  clientId: 'hypomos-web-app',
  redirectUri: `${BASE}/oidc-callback`,
  responseType: 'id_token token',
  scope: 'openid profile hypomos',
  // automaticSilentRenew: true,
  // silentRedirectUri: `${BASE}/silent-renew`,
  postLogoutRedirectUri: `${BASE}/`,
  filterProtocolClaims: true,
};
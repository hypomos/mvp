const STS_DOMAIN: string = "https://localhost:5000";
const BASE: string = "https://localhost:3000";

export const oidcSettings = {
  authority: STS_DOMAIN,
  clientId: 'hypomos-web-app',
  redirectUri: `${BASE}/oidc-callback`,
  responseType: 'id_token token',
  scope: 'openid profile hypomos',
  automaticSilentRenew: true,
  silentRedirectUri: `${BASE}/silent-renew.html`,
  postLogoutRedirectUri: `${BASE}/`,
  filterProtocolClaims: true,
};
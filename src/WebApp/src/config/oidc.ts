import { VuexOidcClientSettings } from 'vuex-oidc';

const STS_DOMAIN: string = import.meta.env.VITE_HYPOMOS_OIDC;
const BASE: string = import.meta.env.VITE_HYPOMOS_APP;

export const oidcSettings: VuexOidcClientSettings = {
  authority: STS_DOMAIN,
  clientId: 'hypomos-web-app',
  redirectUri: `${BASE}/oidc-callback`,
  responseType: 'code',
  scope: 'openid profile hypomos',
  
  automaticSilentRenew: true,
  // silentRedirectUri: `${BASE}/silent-renew`,
    
  postLogoutRedirectUri: `${BASE}/`,
  // filterProtocolClaims: true,
};
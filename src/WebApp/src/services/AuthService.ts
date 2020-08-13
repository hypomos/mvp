import oidc from 'oidc-client';
 
export default class AuthService {
    private userManager: oidc.UserManager;
 
    constructor() {
        const STS_DOMAIN: string = 'https://localhost:5000';
        const BASE: string = 'https://localhost:3000'

        const settings: oidc.UserManagerSettings = {
            userStore: new oidc.WebStorageStateStore({ store: window.localStorage }),
            authority: STS_DOMAIN,
            client_id: 'hypomos-web-app',
            redirect_uri: `${BASE}/callback.html`,
            automaticSilentRenew: true,
            silent_redirect_uri: `${BASE}/silent-renew.html`,
            response_type: 'code',
            scope: 'openid profile hypomos',
            post_logout_redirect_uri: `${BASE}/`,
            filterProtocolClaims: true,
        };
 
        this.userManager = new oidc.UserManager(settings);
    }
 
    public getUser(): Promise<oidc.User> {
        return this.userManager.getUser();
    }
 
    public login(): Promise<void> {
        return this.userManager.signinRedirect();
    }
 
    public logout(): Promise<void> {
        return this.userManager.signoutRedirect();
    }
 
    public getAccessToken(): Promise<string> {
        return this.userManager.getUser().then((data: oidc.User) => {
            return data.access_token;
        });
    }
}
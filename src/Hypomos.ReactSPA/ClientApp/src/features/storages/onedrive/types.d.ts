declare module 'StorageOneDrive' {
  
  export type AuthComponentProps = {
    error: any;
    isAuthenticated: boolean;
    user: any;
    login: Function;
    logout: Function;
    getAccessToken: Function;
    setError: Function;
  }

  export type AuthProviderState = {
    error: any;
    isAuthenticated: boolean;
    user: any;
  }
}
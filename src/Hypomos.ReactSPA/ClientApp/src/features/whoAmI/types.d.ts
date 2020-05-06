import { MediaItem } from 'MyModels';

declare module 'MyModels' {
  export type WhoAmIDetails = {
    email: string,
    lastName: string,
    givenName: string
  }

  export type WhoAmI = {
    isLoggedIn: boolean,
    userDetails: WhoAmIDetails | null
  };

  export type Claims = {
    type: string,
    value: string
  }
}
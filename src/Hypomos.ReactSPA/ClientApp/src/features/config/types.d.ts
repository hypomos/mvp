declare module 'MyModels' {
  
  export type HypomosConfiguration = {
      machineName: string;
      apiEndpoints: {
        collection: string,
        mediaItems: string,
        hypomos: string,
        whoAmI: string,
      },
      oneDrive: {
          appId: string,
          redirectUri: string,
          scopes: Array<string>,
      },
  };
}

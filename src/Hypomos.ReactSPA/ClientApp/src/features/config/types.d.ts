import { HypomosConfiguration } from "MyModels";

declare module 'MyModels' {
  
  export type HypomosConfiguration = {
      machineName: string;
      apiEndpoints: {
        collection: string,
        mediaItems: string,
        hypomos: string,
        whoAmI: string
      }
  };
}

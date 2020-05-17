import { MediaItem } from 'MyModels';

declare module 'MyModels' {
    export type Collection = {
      userId: number,
      id: string;
      title: string;
      content: MediaItem[] | null;
    };
  }
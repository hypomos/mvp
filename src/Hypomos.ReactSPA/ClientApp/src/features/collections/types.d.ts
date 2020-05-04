import { MediaItem } from 'MyModels';

declare module 'MyModels' {
    export type Collection = {
      userId: number,
      id: number;
      title: string;
      content: MediaItem[] | null;
    };
  }
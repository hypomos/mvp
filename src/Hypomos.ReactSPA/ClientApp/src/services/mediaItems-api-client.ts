import axios from 'axios';

import { MediaItem, HypomosConfiguration } from 'MyModels';

const config = window.hypomosConfig as HypomosConfiguration;
const client = axios.create({ baseURL: config.apiEndpoints.mediaItems })
const part = 'photos';

export function loadMediaItems(): Promise<MediaItem[]> {
  return client.get(part)
    .then(r => r.data as MediaItem[]);
}

export function loadMediaItem(id: number): Promise<MediaItem> {
  return client
    .get(part, {
      params: {
        id: id
      }
    })
    .then(r => r.data as MediaItem);
}

export function createMediaItem(collection: MediaItem): Promise<MediaItem[]> {
  return client.post(part, collection)
    .then(r => loadMediaItems());
}

export function updateMediaItem(collection: MediaItem): Promise<MediaItem[]> {
  return client.put(part, collection)
    .then(r => loadMediaItems());
}

export function deleteMediaItem(collection: MediaItem): Promise<MediaItem[]> {
  return client.delete(part, { data: collection })
    .then(r => loadMediaItems());
}
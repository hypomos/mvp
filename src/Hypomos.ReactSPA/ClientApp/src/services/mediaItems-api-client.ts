import axios from 'axios';

import { MediaItem } from 'MyModels';

const client = axios.create({ baseURL: 'https://jsonplaceholder.typicode.com/' })
const url = 'photos';

export function loadMediaItems(): Promise<MediaItem[]> {
  return client.get(url)
    .then(r => r.data as MediaItem[]);
}

export function loadMediaItem(id: number): Promise<MediaItem> {
  return client
    .get(url, {
      params: {
        id: id
      }
    })
    .then(r => r.data as MediaItem);
}

export function createMediaItem(collection: MediaItem): Promise<MediaItem[]> {
  return client.post(url, collection)
    .then(r => loadMediaItems());
}

export function updateMediaItem(collection: MediaItem): Promise<MediaItem[]> {
  return client.put(url, collection)
    .then(r => loadMediaItems());
}

export function deleteMediaItem(collection: MediaItem): Promise<MediaItem[]> {
  return client.delete(url, { data: collection })
    .then(r => loadMediaItems());
}
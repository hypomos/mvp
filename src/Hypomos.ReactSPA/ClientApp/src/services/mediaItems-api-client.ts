import axios from 'axios';

import { MediaItem, HypomosConfiguration } from 'MyModels';

const part = 'photos';

function createClient() {
  const config = window.hypomosConfig as HypomosConfiguration;
  return axios.create({ baseURL: config.apiEndpoints.mediaItems })
}

export function loadMediaItems(): Promise<MediaItem[]> {
  return createClient().get(part)
    .then(r => r.data as MediaItem[]);
}

export function loadMediaItem(id: number): Promise<MediaItem> {
  return createClient()
    .get(part, {
      params: {
        id: id
      }
    })
    .then(r => r.data as MediaItem);
}

export function createMediaItem(collection: MediaItem): Promise<MediaItem[]> {
  return createClient().post(part, collection)
    .then(r => loadMediaItems());
}

export function updateMediaItem(collection: MediaItem): Promise<MediaItem[]> {
  return createClient().put(part, collection)
    .then(r => loadMediaItems());
}

export function deleteMediaItem(collection: MediaItem): Promise<MediaItem[]> {
  return createClient().delete(part, { data: collection })
    .then(r => loadMediaItems());
}
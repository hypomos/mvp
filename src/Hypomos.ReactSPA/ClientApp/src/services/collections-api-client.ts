import axios from 'axios';

import { Collection } from 'MyModels';

const client = axios.create({ baseURL: 'https://jsonplaceholder.typicode.com/' })
const url = 'albums';

export function loadCollections(): Promise<Collection[]> {
  return client.get(url)
    .then(r => r.data as Collection[]);
}

export function loadCollection(id: number): Promise<Collection> {
  return client
    .get(url, {
      params: {
        id: id
      }
    })
    .then(r => r.data as Collection);
}

export function createCollection(collection: Collection): Promise<Collection[]> {
  return client.post(url, collection)
    .then(r => loadCollections());
}

export function updateCollection(collection: Collection): Promise<Collection[]> {
  return client.put(url, collection)
    .then(r => loadCollections());
}

export function deleteCollection(collection: Collection): Promise<Collection[]> {
  return client.delete(url, { data: collection })
    .then(r => loadCollections());
}
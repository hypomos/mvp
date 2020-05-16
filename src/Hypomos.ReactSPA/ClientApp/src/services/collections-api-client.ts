import axios from 'axios';

import { Collection, HypomosConfiguration } from 'MyModels';

const url = 'albums';

function createClient() {
  const config = window.hypomosConfig as HypomosConfiguration;
  return axios.create({ baseURL: config.apiEndpoints.collection })  
}

export function loadCollections(): Promise<Collection[]> {
  return createClient().get(url)
    .then(r => r.data as Collection[]);
}

export function loadCollection(id: number): Promise<Collection> {
  return createClient()
    .get(url, {
      params: {
        id: id
      }
    })
    .then(r => r.data as Collection);
}

export function createCollection(collection: Collection): Promise<Collection[]> {
  return createClient().post(url, collection)
    .then(r => loadCollections());
}

export function updateCollection(collection: Collection): Promise<Collection[]> {
  return createClient().put(url, collection)
    .then(r => loadCollections());
}

export function deleteCollection(collection: Collection): Promise<Collection[]> {
  return createClient().delete(url, { data: collection })
    .then(r => loadCollections());
}
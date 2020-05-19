import axios from 'axios';

import { Storage, HypomosConfiguration } from 'MyModels';

const url = 'storages';

function createClient(token: string | undefined) {
  const config = window.hypomosConfig as HypomosConfiguration;
  const client = axios.create({ baseURL: config.apiEndpoints.hypomos });
  client.defaults.headers.common['Authorization'] = `Bearer ${token}`;
  return client;
}

export function loadStorages(token: string | undefined): Promise<Storage[]> {
  return createClient(token).get(url)
    .then(r => r.data as Storage[]);
}

export function loadStorage(id: number): Promise<Storage> {
  return createClient('')
    .get(url, {
      params: {
        id: id
      }
    })
    .then(r => r.data as Storage);
}

export function createStorage(collection: Storage): Promise<Storage[]> {
  return createClient('').post(url, collection)
    .then(r => loadStorages(''));
}

export function updateStorage(collection: Storage): Promise<Storage[]> {
  return createClient('').put(url, collection)
    .then(r => loadStorages(''));
}

export function deleteStorage(collection: Storage): Promise<Storage[]> {
  return createClient('').delete(url, { data: collection })
    .then(r => loadStorages(''));
}
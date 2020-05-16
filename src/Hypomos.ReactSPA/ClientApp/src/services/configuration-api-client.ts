import axios from 'axios';

import { HypomosConfiguration } from 'MyModels';

const client = axios.create({ baseURL: '/api/' })
const url = 'config';

export function getConfiguration(): Promise<HypomosConfiguration> {
  return client
    .get(url)
    .then(r => r.data as HypomosConfiguration);
}
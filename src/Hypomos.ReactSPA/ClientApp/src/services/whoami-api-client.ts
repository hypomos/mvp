import axios from 'axios';

import { WhoAmI, HypomosConfiguration } from 'MyModels';

const config = window.hypomosConfig as HypomosConfiguration;
const client = axios.create({ baseURL: config.apiEndpoints.whoAmI })
const url = 'WhoAmI';

export function loadWhoAmI(token: string | undefined): Promise<WhoAmI> {
  return client
    .get(url, { headers: { Authorization: `Bearer ${token}`}})
    .then(r => r.data as WhoAmI);
}
import axios from 'axios';

import { WhoAmI, HypomosConfiguration } from 'MyModels';

const url = 'WhoAmI';

function createClient() {
  const config = window.hypomosConfig as HypomosConfiguration;
  const client = axios.create({ baseURL: config.apiEndpoints.whoAmI });
  return client;
}

export function loadWhoAmI(token: string | undefined): Promise<WhoAmI> {
  return createClient()
    .get(url, { headers: { Authorization: `Bearer ${token}`}})
    .then(r => r.data as WhoAmI);
}
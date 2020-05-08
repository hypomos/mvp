import axios from 'axios';

import { WhoAmI } from 'MyModels';

const client = axios.create({ baseURL: 'http://localhost:5010/api/' })
const url = 'WhoAmI';

export function loadWhoAmI(token: string | undefined): Promise<WhoAmI> {
  debugger;
  return client
    .get(url, { headers: { Authorization: `Bearer ${token}`}})
    .then(r => r.data as WhoAmI);
}
import axios from 'axios';

import { WhoAmI } from 'MyModels';

const client = axios.create({ baseURL: 'http://localhost:5010/api/' })
const url = 'whoami';

export function loadWhoAmI(): Promise<WhoAmI> {
  return client
    .get(url)
    .then(r => r.data as WhoAmI);
}
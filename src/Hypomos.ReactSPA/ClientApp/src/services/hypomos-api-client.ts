import axios from 'axios';
import { HypomosConfiguration } from 'MyModels';

function createClient() {
    const config = window.hypomosConfig as HypomosConfiguration;
    return axios.create({ baseURL: config.apiEndpoints.hypomos })
}

export function getWhoAmI() {
    return createClient().get('/api/whoami');
}
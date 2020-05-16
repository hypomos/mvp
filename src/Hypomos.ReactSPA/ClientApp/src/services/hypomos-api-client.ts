import axios from 'axios';
import { HypomosConfiguration } from 'MyModels';

const config = window.hypomosConfig as HypomosConfiguration;
const client = axios.create({
    baseURL: config.apiEndpoints.hypomos
});

export function getWhoAmI() {
    return client.get('/api/whoami');
}
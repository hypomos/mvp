import axios from 'axios';

const client = axios.create({
    baseURL: 'http://localhost:5010/api/'
});

export function getWhoAmI() {
    return client.get('/api/whoami');
}
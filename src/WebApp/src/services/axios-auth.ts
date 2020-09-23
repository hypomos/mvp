import axios, { Method, ResponseType } from 'axios';
import { store } from '../store';

export function authenticatedRequest<TResponse>(url: string, method: Method, body?: string, responseType?: ResponseType) {

    debugger;
    
    const client = axios.create();
    const token = store.state.oidcStore?.access_token;
    client.defaults.headers.common['Authorization'] = token ? `Bearer ${token}` : null;

    return client.request<TResponse>({
        method: method,
        url: url,
        data: body,
        responseType: responseType ?? 'json'
    });
}
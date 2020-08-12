import { createAsyncAction, createAction } from 'typesafe-actions';
import { AuthProviderState } from 'StorageOneDrive';

// export const loadStoragesAsync = createAsyncAction(
//     'LOAD_STORAGES_REQUEST', 'LOAD_STORAGES_SUCCESS', 'LOAD_STORAGES_FAILURE', 'LOAD_STORAGES_CANCEL')
//     <undefined, Storage[], Error, undefined>();

export const createOneDriveStorage = createAction('CREATE_ONEDRIVE_STORAGE');

export const loadOneDriveUserAgent = createAsyncAction(
    'LOAD_USERAGENT_REQUEST', 'LOAD_USERAGENT_SUCCESS', 'LOAD_USERAGENT_FAILURE', 'LOAD_USERAGENT_CANCEL')
    <string, AuthProviderState, Error, undefined>();
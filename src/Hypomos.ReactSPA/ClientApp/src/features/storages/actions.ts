import { createAsyncAction, createAction } from 'typesafe-actions';
import { Storage } from 'MyModels';

export const createStorage = createAction('CREATE_STORAGE');

export const loadStoragesAsync = createAsyncAction(
    'LOAD_STORAGES_REQUEST', 'LOAD_STORAGES_SUCCESS', 'LOAD_STORAGES_FAILURE', 'LOAD_STORAGES_CANCEL')
    <undefined, Storage[], Error, undefined>();

export const loadStorageAsync = createAsyncAction(
    'LOAD_STORAGE_REQUEST', 'LOAD_STORAGE_SUCCESS', 'LOAD_STORAGE_FAILURE', 'LOAD_STORAGE_CANCEL')
    <string, Storage[], Error, undefined>();

export const createStorageAsync = createAsyncAction(
    'CREATE_STORAGE_REQUEST', 'CREATE_STORAGE_SUCCESS', 'CREATE_STORAGE_FAILURE', 'CREATE_STORAGE_CANCEL')
    <Storage, Storage[], Error, undefined>();

export const deleteStorageAsync = createAsyncAction(
    'DELETE_STORAGE_REQUEST', 'DELETE_STORAGE_SUCCESS', 'DELETE_STORAGE_FAILURE', 'DELETE_STORAGE_CANCEL')
    <Storage, Storage[], Storage, undefined>();

export const updateStorageAsync = createAsyncAction(
    'UPDATE_STORAGE_REQUEST', 'UPDATE_STORAGE_SUCCESS', 'UPDATE_STORAGE_FAILURE', 'UPDATE_STORAGE_CANCEL')
    <Storage, Storage[], Error, undefined>();


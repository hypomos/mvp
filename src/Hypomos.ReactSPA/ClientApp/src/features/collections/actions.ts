import { createAsyncAction } from 'typesafe-actions';
import { Collection } from 'MyModels';

export const loadCollectionsAsync = createAsyncAction(
    'LOAD_COLLECTIONS_REQUEST', 'LOAD_COLLECTIONS_SUCCESS', 'LOAD_COLLECTIONS_FAILURE', 'LOAD_COLLECTIONS_CANCEL')
    <undefined, Collection[], Error, undefined>();

export const loadCollectionAsync = createAsyncAction(
    'LOAD_COLLECTION_REQUEST', 'LOAD_COLLECTION_SUCCESS', 'LOAD_COLLECTION_FAILURE', 'LOAD_COLLECTION_CANCEL')
    <number, Collection[], Error, undefined>();

export const createCollectionAsync = createAsyncAction(
    'CREATE_COLLECTION_REQUEST', 'CREATE_COLLECTION_SUCCESS', 'CREATE_COLLECTION_FAILURE', 'CREATE_COLLECTION_CANCEL')
    <Collection, Collection[], Error, undefined>();

export const deleteCollectionAsync = createAsyncAction(
    'DELETE_COLLECTION_REQUEST', 'DELETE_COLLECTION_SUCCESS', 'DELETE_COLLECTION_FAILURE', 'DELETE_COLLECTION_CANCEL')
    <Collection, Collection[], Collection, undefined>();

export const updateCollectionAsync = createAsyncAction(
    'UPDATE_COLLECTION_REQUEST', 'UPDATE_COLLECTION_SUCCESS', 'UPDATE_COLLECTION_FAILURE', 'UPDATE_COLLECTION_CANCEL')
    <Collection, Collection[], Error, undefined>();
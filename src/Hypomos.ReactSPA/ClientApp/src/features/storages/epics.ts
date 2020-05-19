import { RootEpic } from 'MyTypes';
import { from, of } from 'rxjs';
import { filter, switchMap, map, catchError } from 'rxjs/operators';
import { isActionOf } from 'typesafe-actions';

import {
  loadStoragesAsync,
  createStorageAsync,
  updateStorageAsync,
  deleteStorageAsync,
} from './actions';

export const loadStoragesEpic: RootEpic = (action$, state$, { api }) =>
  action$.pipe(
    filter(isActionOf(loadStoragesAsync.request)),
    switchMap(() =>
      from(api.storages.loadStorages(state$.value.whoAmI.oidc.user?.access_token)).pipe(
        map(loadStoragesAsync.success),
        catchError(message => of(loadStoragesAsync.failure(message)))
      )
    )
  );

export const createStoragesEpic: RootEpic = (action$, state$, { api }) =>
  action$.pipe(
    filter(isActionOf(createStorageAsync.request)),
    switchMap(action =>
      from(api.storages.createStorage(action.payload)).pipe(
        map(createStorageAsync.success),
        catchError(message => of(createStorageAsync.failure(message)))
      )
    )
  );

export const updateStoragesEpic: RootEpic = (action$, state$, { api }) =>
  action$.pipe(
    filter(isActionOf(updateStorageAsync.request)),
    switchMap(action =>
      from(api.storages.updateStorage(action.payload)).pipe(
        map(updateStorageAsync.success),
        catchError(message => of(updateStorageAsync.failure(message)))
      )
    )
  );

export const deleteStoragesEpic: RootEpic = (action$, state$, { api, toast }) =>
  action$.pipe(
    filter(isActionOf(deleteStorageAsync.request)),
    switchMap(action =>
      from(api.storages.deleteStorage(action.payload)).pipe(
        map(deleteStorageAsync.success),
        catchError(message => {
          toast.error(message);
          return of(deleteStorageAsync.failure(action.payload));
        })
      )
    )
  );
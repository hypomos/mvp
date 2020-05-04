import { RootEpic } from 'MyTypes';
import { from, of } from 'rxjs';
import { filter, switchMap, map, catchError } from 'rxjs/operators';
import { isActionOf } from 'typesafe-actions';

import {
  loadCollectionsAsync,
  createCollectionAsync,
  updateCollectionAsync,
  deleteCollectionAsync,
} from './actions';

export const loadCollectionsEpic: RootEpic = (action$, state$, { api }) =>
  action$.pipe(
    filter(isActionOf(loadCollectionsAsync.request)),
    switchMap(() =>
      from(api.collections.loadCollections()).pipe(
        map(loadCollectionsAsync.success),
        catchError(message => of(loadCollectionsAsync.failure(message)))
      )
    )
  );

export const createCollectionsEpic: RootEpic = (action$, state$, { api }) =>
  action$.pipe(
    filter(isActionOf(createCollectionAsync.request)),
    switchMap(action =>
      from(api.collections.createCollection(action.payload)).pipe(
        map(createCollectionAsync.success),
        catchError(message => of(createCollectionAsync.failure(message)))
      )
    )
  );

export const updateCollectionsEpic: RootEpic = (action$, state$, { api }) =>
  action$.pipe(
    filter(isActionOf(updateCollectionAsync.request)),
    switchMap(action =>
      from(api.collections.updateCollection(action.payload)).pipe(
        map(updateCollectionAsync.success),
        catchError(message => of(updateCollectionAsync.failure(message)))
      )
    )
  );

export const deleteCollectionsEpic: RootEpic = (action$, state$, { api, toast }) =>
  action$.pipe(
    filter(isActionOf(deleteCollectionAsync.request)),
    switchMap(action =>
      from(api.collections.deleteCollection(action.payload)).pipe(
        map(deleteCollectionAsync.success),
        catchError(message => {
          toast.error(message);
          return of(deleteCollectionAsync.failure(action.payload));
        })
      )
    )
  );
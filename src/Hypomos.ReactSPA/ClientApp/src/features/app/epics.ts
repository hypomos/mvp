import { RootEpic } from 'MyTypes';
import { tap, ignoreElements, filter, first, map } from 'rxjs/operators';

import { isActionOf } from 'typesafe-actions';
import {
  loadCollectionsAsync,
  createCollectionAsync,
  updateCollectionAsync,
  deleteCollectionAsync,
} from '../collections/actions';

export const persistCollectionsInLocalStorage: RootEpic = (
  action$,
  store,
  { localStorage }
) =>
  action$.pipe(
    filter(
      isActionOf([
        loadCollectionsAsync.success,
        createCollectionAsync.success,
        updateCollectionAsync.success,
        deleteCollectionAsync.success,
      ])
    ),
    tap(_ => {
      // handle side-effects
      localStorage.set('collections', store.value.collections.collections);
    }),
    ignoreElements()
  );

export const loadDataOnAppStart: RootEpic = (action$, store, { api }) =>
  action$.pipe(
    first(),
    map(loadCollectionsAsync.request)
  );

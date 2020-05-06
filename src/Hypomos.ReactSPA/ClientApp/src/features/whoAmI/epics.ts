import { RootEpic } from 'MyTypes';

import { from, of } from 'rxjs';
import { filter, first, switchMap, map, catchError } from 'rxjs/operators';

import { isActionOf } from 'typesafe-actions';

import {
  loadWhoAmIAsync
} from './actions';

export const loadWhoAmIAsyncEpic: RootEpic = (action$, state$, { api }) =>
  action$.pipe(
    filter(isActionOf(loadWhoAmIAsync.request)),
    switchMap(() =>
      from(api.whoAmI.loadWhoAmI()).pipe(
        map(r => {
          debugger;
          return loadWhoAmIAsync.success(r);
        }),
        catchError(message => of(loadWhoAmIAsync.failure(message)))
      )
    )
  );

export const loadDataOnAppStart: RootEpic = (action$, store, { api }) =>
  action$.pipe(
    first(),
    map(loadWhoAmIAsync.request)
  );

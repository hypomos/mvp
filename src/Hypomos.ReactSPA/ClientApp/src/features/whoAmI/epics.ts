import { RootEpic } from 'MyTypes';

import { from, of } from 'rxjs';
import { filter, switchMap, map, catchError } from 'rxjs/operators';

import { isActionOf } from 'typesafe-actions';

import {
  loadWhoAmIAsync
} from './actions';

export const loadWhoAmIAsyncEpic: RootEpic = (action$, state$, { api }) => {

  return action$.pipe(
    filter(isActionOf(loadWhoAmIAsync.request)),
    switchMap(action => from(api.whoAmI.loadWhoAmI(action.payload))
      .pipe(
        map(loadWhoAmIAsync.success),
        catchError(message => of(loadWhoAmIAsync.failure(message)))
      )
    ));
}

import { from, of } from 'rxjs';
import { filter, switchMap, map, catchError, first } from 'rxjs/operators';
import { isActionOf } from 'typesafe-actions';

import { RootEpic } from 'MyTypes';
import {
  loadHypomosConfigurationAsync
} from './actions';

export const loadHypomosConfigurationAsyncEpic: RootEpic = (action$, store, { api }) => {
  return action$.pipe(
    filter(isActionOf(loadHypomosConfigurationAsync.request)),
    switchMap(action => from(api.config.getConfiguration())
      .pipe(
        map(loadHypomosConfigurationAsync.success),
        catchError(message => of(loadHypomosConfigurationAsync.failure(message)))
      )
    ));
}

export const loadConfigOnAppStart: RootEpic = (action$, store, { api }) =>
  action$.pipe(
    first(),
    map(loadHypomosConfigurationAsync.request)
  );

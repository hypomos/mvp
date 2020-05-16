import { push } from 'connected-react-router'
import { from, of } from 'rxjs';
import { filter, switchMap, map, catchError, first } from 'rxjs/operators';
import { isActionOf } from 'typesafe-actions';

import { RootEpic } from 'MyTypes';
import { getPath } from '../../router-paths';
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

export const loadHypomosConfigNavigateToAppUponSuccess: RootEpic = (action$) => {
  return action$.pipe(
    filter(isActionOf(loadHypomosConfigurationAsync.success)),
    switchMap(action => {
      debugger;
      window.hypomosConfig = action.payload;
      return of(getPath('home'))
        .pipe(
          map(a => push(a)),
        )
    }
    ));
}

export const loadConfigOnAppStart: RootEpic = (action$, store, { api }) =>
  action$.pipe(
    first(),
    map(loadHypomosConfigurationAsync.request)
  );

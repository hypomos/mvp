import { RootEpic } from 'MyTypes';
import { from, of } from 'rxjs';
import { filter, switchMap, map, catchError, first } from 'rxjs/operators';
import { isActionOf } from 'typesafe-actions';
import { UserAgentApplication } from 'msal';

import {
  loadOneDriveUserAgent,
} from './actions';

export const loadOneDriveOnAppStart: RootEpic = (action$, store, { api }) =>
  action$.pipe(
    first(),
    map(loadOneDriveUserAgent.request)
  );

export const loadOneDriveUserAgentAsyncEpic: RootEpic = (action$, state$, { api }) => {

  return action$.pipe(
    filter(isActionOf(loadOneDriveUserAgent.request)),
    switchMap(action => {
      const config = state$.value.config.config.oneDrive;

      const userAgentApplication = new UserAgentApplication({
        auth: {
          clientId: config.appId,
          redirectUri: config.redirectUri
        },
        cache: {
          cacheLocation: "sessionStorage",
          storeAuthStateInCookie: true
        }
      });

      return from(api.whoAmI.loadWhoAmI(action.payload))
        .pipe(
          map(loadOneDriveUserAgent.success),
          catchError(message => of(loadWhoAmIAsync.failure(message)))
        );
    }
    ));
}

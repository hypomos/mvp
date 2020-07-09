import { RootEpic } from 'MyTypes';
import { from, of } from 'rxjs';
import { filter, switchMap, map, catchError, first } from 'rxjs/operators';
import { isActionOf } from 'typesafe-actions';
import { UserAgentApplication } from 'msal';
import { AuthProviderState } from 'StorageOneDrive';

import {
  loadOneDriveUserAgent,
} from './actions';

//export const loadOneDriveOnAppStart: RootEpic = (action$, store, { api }) =>
//  action$.pipe(
//    first(),
//    map(loadOneDriveUserAgent.request)
//  );

export const loadOneDriveUserAgentAsyncEpic: RootEpic = (action$, state$, { api }) => {

  return action$.pipe(
    filter(isActionOf(loadOneDriveUserAgent.request)),
    switchMap(action => {
      debugger;

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

      var accounts = userAgentApplication.getAllAccounts();
      console.log(accounts);

      return from(api.whoAmI.loadWhoAmI(action.payload))
        .pipe(
          map(string => {
            debugger;
            var state: AuthProviderState = {
              error: null,
              isAuthenticated: false,
              user: null
            };

            return loadOneDriveUserAgent.success(state);
          }),
          catchError(message => of(loadOneDriveUserAgent.failure(message)))
        );
    }
    ));
}

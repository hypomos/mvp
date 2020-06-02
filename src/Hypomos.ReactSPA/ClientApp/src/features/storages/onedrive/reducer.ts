import { AuthProviderState } from 'StorageOneDrive';
import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';

import {
  loadOneDriveUserAgent,
} from './actions';

const reducer = combineReducers({
  isLoading: createReducer(false as boolean)
    .handleAction([loadOneDriveUserAgent.request], (state, action) => true)
    .handleAction(
      [loadOneDriveUserAgent.success, loadOneDriveUserAgent.failure],
      (state, action) => false
    ),
  authState: createReducer({} as AuthProviderState)
  .handleAction([loadOneDriveUserAgent.request], (state, action) => {})
  .handleAction([loadOneDriveUserAgent.success], (state, action) => action.payload)
  .handleAction([loadOneDriveUserAgent.failure], (state, action) => {}),
});

export default reducer;
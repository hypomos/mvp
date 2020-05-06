import { WhoAmI } from 'MyModels';
import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';

import {
  loadWhoAmIAsync
} from './actions';

const reducer = combineReducers({
  isLoadingUser: createReducer(false as boolean)
    .handleAction([loadWhoAmIAsync.request], (state, action) => true)
    .handleAction(
      [loadWhoAmIAsync.success, loadWhoAmIAsync.failure],
      (state, action) => false
    ),
  user: createReducer({} as WhoAmI)
    .handleAction(
      [
        loadWhoAmIAsync.success
      ],
      (state, action) => {
        debugger;
        return action.payload;
      }
    )
});

export default reducer;
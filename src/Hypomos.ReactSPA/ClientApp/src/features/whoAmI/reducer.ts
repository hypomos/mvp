import { reducer as oidcReducer } from "redux-oidc";
import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';

import { toggleDropDown } from './actions';

const reducer = combineReducers({
  oidc: oidcReducer,
  showDropDown: createReducer(false as boolean)
    .handleAction(toggleDropDown, (state, action) => !state),
});

export default reducer;
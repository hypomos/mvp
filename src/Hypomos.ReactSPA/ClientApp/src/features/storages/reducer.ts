import { Storage } from 'MyModels';
import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';

import {
  loadStoragesAsync,
  createStorageAsync,
  updateStorageAsync,
  deleteStorageAsync,
} from './actions';

const reducer = combineReducers({
  isLoadingStorages: createReducer(false as boolean)
    .handleAction([loadStoragesAsync.request], (state, action) => true)
    .handleAction(
      [loadStoragesAsync.success, loadStoragesAsync.failure],
      (state, action) => false
    ),
  storages: createReducer([] as Storage[])
    .handleAction(
      [
        loadStoragesAsync.success,
        createStorageAsync.success,
        updateStorageAsync.success,
        deleteStorageAsync.success,
      ],
      (state, action) => action.payload
    )
    .handleAction(createStorageAsync.request, (state, action) =>
      state.concat(action.payload)
    )
    .handleAction(updateStorageAsync.request, (state, action) =>
      state.map(i => (i.id === action.payload.id ? action.payload : i))
    )
    .handleAction(deleteStorageAsync.request, (state, action) =>
      state.filter(i => i.id !== action.payload.id)
    )
    .handleAction(deleteStorageAsync.failure, (state, action) =>
      state.concat(action.payload)
    ),
});

export default reducer;
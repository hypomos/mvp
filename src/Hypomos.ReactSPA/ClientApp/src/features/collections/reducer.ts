import { Collection } from 'MyModels';
import { combineReducers } from 'redux';
import { createReducer } from 'typesafe-actions';

import {
  loadCollectionsAsync,
  createCollectionAsync,
  updateCollectionAsync,
  deleteCollectionAsync,
} from './actions';

const reducer = combineReducers({
  isLoadingCollections: createReducer(false as boolean)
    .handleAction([loadCollectionsAsync.request], (state, action) => true)
    .handleAction(
      [loadCollectionsAsync.success, loadCollectionsAsync.failure],
      (state, action) => false
    ),
  collections: createReducer([] as Collection[])
    .handleAction(
      [
        loadCollectionsAsync.success,
        createCollectionAsync.success,
        updateCollectionAsync.success,
        deleteCollectionAsync.success,
      ],
      (state, action) => action.payload
    )
    .handleAction(createCollectionAsync.request, (state, action) => [
      ...state,
      action.payload,
    ])
    .handleAction(updateCollectionAsync.request, (state, action) =>
      state.map(i => (i.id === action.payload.id ? action.payload : i))
    )
    .handleAction(deleteCollectionAsync.request, (state, action) =>
      state.filter(i => i.id !== action.payload.id)
    )
    .handleAction(deleteCollectionAsync.failure, (state, action) =>
      state.concat(action.payload)
    ),
});

export default reducer;
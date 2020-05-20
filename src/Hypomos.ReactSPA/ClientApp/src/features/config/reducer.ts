import { combineReducers } from 'redux';

import { createReducer } from 'typesafe-actions';
import { HypomosConfiguration } from 'MyModels';

import {
    loadHypomosConfigurationAsync,
} from './actions';

const reducer = combineReducers({
    ready: createReducer(false as boolean)
        .handleAction([loadHypomosConfigurationAsync.request], (state, action) => false)
        .handleAction([loadHypomosConfigurationAsync.success], (state, action) => {
            window.hypomosConfig = action.payload;
            return true;
        })
        .handleAction([loadHypomosConfigurationAsync.failure], (state, action) => false),

    config: createReducer({} as HypomosConfiguration)
        .handleAction([loadHypomosConfigurationAsync.request], (state, action) => ({} as HypomosConfiguration))
        .handleAction([loadHypomosConfigurationAsync.success], (state, action) => action.payload)
        .handleAction([loadHypomosConfigurationAsync.failure], (state, action) => ({} as HypomosConfiguration)),
});

export default reducer;
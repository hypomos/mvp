import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import { History } from 'history';

import collections from '../features/collections/reducer';
import whoAmI from '../features/whoAmI/reducer';
import config from '../features/config/reducer';
import storages from '../features/storages/reducer';
import oneDriveStorages from '../features/storages/onedrive/reducer';

const rootReducer = (history: History<any>) =>
  combineReducers({
    router: connectRouter(history),
    collections,
    whoAmI,
    config,
    storages,
    oneDriveStorages
  });

export default rootReducer;
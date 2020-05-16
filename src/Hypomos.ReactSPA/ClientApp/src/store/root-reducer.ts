import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import { History } from 'history';

import collections from '../features/collections/reducer';
import whoAmI from '../features/whoAmI/reducer';
import config from '../features/config/reducer';

const rootReducer = (history: History<any>) =>
  combineReducers({
    router: connectRouter(history),
    collections,
    whoAmI,
    config
  });

export default rootReducer;
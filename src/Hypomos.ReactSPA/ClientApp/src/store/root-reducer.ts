import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import { History } from 'history';

import collections from '../features/collections/reducer';
import whoAmI from '../features/whoAmI/reducer';

const rootReducer = (history: History<any>) =>
  combineReducers({
    router: connectRouter(history),
    collections,
    whoAmI
  });

export default rootReducer;
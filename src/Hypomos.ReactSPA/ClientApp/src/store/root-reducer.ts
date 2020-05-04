import { combineReducers } from 'redux';
import { connectRouter } from 'connected-react-router';
import { History } from 'history';

import collections from '../features/collections/reducer';

const rootReducer = (history: History<any>) =>
  combineReducers({
    router: connectRouter(history),
    collections,
  });

export default rootReducer;
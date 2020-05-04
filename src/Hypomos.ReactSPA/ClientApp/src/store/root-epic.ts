import { combineEpics } from 'redux-observable';

import * as app from '../features/app/epics';
import * as collections from '../features/collections/epics';

export default combineEpics(...Object.values(app), ...Object.values(collections));
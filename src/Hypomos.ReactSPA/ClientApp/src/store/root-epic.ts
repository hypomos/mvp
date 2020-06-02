import { combineEpics } from 'redux-observable';

import * as collections from '../features/collections/epics';
import * as whoAmI from '../features/whoAmI/epics';
import * as config from '../features/config/epics';
import * as storages from '../features/storages/epics';
import * as oneDrive from '../features/storages/onedrive/epics';

export default combineEpics(...Object.values(collections), ...Object.values(whoAmI), ...Object.values(config), ...Object.values(storages), ...Object.values(oneDrive));
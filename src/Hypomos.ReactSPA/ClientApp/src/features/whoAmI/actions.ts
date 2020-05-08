import { createAsyncAction, createAction } from 'typesafe-actions';
import { WhoAmI } from 'MyModels';

export const loadWhoAmIAsync = createAsyncAction(
    'LOAD_WHOAMI_REQUEST', 'LOAD_WHOAMI_SUCCESS', 'LOAD_WHOAMI_FAILURE', 'LOAD_WHOAMI_CANCEL')
    <string | undefined, WhoAmI, WhoAmI, undefined>();

export const toggleDropDown = createAction('WHOAMI_TOGGLE_DROPDOWN')<undefined, undefined>();
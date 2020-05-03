import { createAction, createAsyncAction } from 'typesafe-actions';
import { ContentType, ClaimType } from '../models';

import hypomosApi from '../apis/hypomos';

export const selectContent = createAction('CONTENT_SELECTED')<ContentType>();

export const whoAmI = createAsyncAction(
    ['WHO_AM_I', (req: void) => {

        return hypomosApi
            .get('whoami')
            .then(r => r.data as ReadonlyArray<ClaimType>)
            .then(r => {
                console.log(r);
                return r;
            });
    }
    ], // request payload & meta creators
    ['WHO_AM_I_SUCCESS', (res: ReadonlyArray<ClaimType>) => res], // success payload creator
    ['WHO_AM_I_FAILURE', (err: Error) => err, (err: Error) => 'meta'], // failure payload & meta creators
    'WHO_AM_I_CANCEL', // optional cancel payload creator
  )();
    
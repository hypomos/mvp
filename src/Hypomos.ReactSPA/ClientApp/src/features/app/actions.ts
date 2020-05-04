import { createAsyncAction } from 'typesafe-actions';
import { AxiosResponse } from 'axios';

import services from '../../services';

export const whoAmI = createAsyncAction(
    ['WHO_AM_I', (req: void) => {
        return services.api.hypomos.getWhoAmI();
    }
    ], // request payload & meta creators
    ['WHO_AM_I_SUCCESS', (res: AxiosResponse<any>) => res], // success payload creator
    ['WHO_AM_I_FAILURE', (err: Error) => err, (err: Error) => 'meta'], // failure payload & meta creators
    'WHO_AM_I_CANCEL', // optional cancel payload creator
  )();
    
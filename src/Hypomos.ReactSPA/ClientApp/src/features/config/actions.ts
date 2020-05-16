import { createAsyncAction } from 'typesafe-actions';
import { HypomosConfiguration } from 'MyModels';

export const loadHypomosConfigurationAsync = createAsyncAction(
    'LOAD_HYPOMOS_CONFIG_REQUEST', 'LOAD_HYPOMOS_CONFIG_SUCCESS', 'LOAD_HYPOMOS_CONFIG_FAILURE', 'LOAD_HYPOMOS_CONFIG_CANCEL')
    <undefined, HypomosConfiguration, HypomosConfiguration, undefined>();
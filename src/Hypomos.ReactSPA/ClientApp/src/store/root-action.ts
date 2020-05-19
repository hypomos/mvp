import { routerActions } from 'connected-react-router';
import * as collectionActions from '../features/collections/actions';
import * as whoAmIActions from '../features/whoAmI/actions';
import * as configActions from '../features/config/actions';
import * as storageActions from '../features/storages/actions';

export default {
    router: routerActions,
    collection: collectionActions,
    whoAmI: whoAmIActions,
    config: configActions,
    storage: storageActions
}
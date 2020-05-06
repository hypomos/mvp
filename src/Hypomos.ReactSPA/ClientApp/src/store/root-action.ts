import { routerActions } from 'connected-react-router';
import * as collectionActions from '../features/collections/actions';
import * as whoAmIActions from '../features/whoAmI/actions';

export default {
    router: routerActions,
    collection: collectionActions,
    whoAmI: whoAmIActions
}
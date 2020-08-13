import { Store } from 'vuex';
import { State } from '../src/store/state';

declare module "@vue/runtime-core" {
  
  interface ComponentCustomProperties {
    $store: Store<State>;
  }
}
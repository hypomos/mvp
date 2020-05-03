import { combineReducers } from 'redux';
import { ActionType } from 'typesafe-actions';
import { DeepReadonly } from 'utility-types';

import { ContentType } from '../models';
import * as actions from '../actions';

export type HypomosActions = ActionType<typeof actions>;

export type HypomosState = DeepReadonly<{
    content: ContentType[],
    selectedContent: ContentType | null
}>;

const initialState: HypomosState = {
    content: [
        { title: 'Title A' },
        { title: 'Title B' },
        { title: 'Title C' },
        { title: 'Title D' }
    ],
    selectedContent: null
};

export default combineReducers<HypomosState, HypomosActions>({
    content: (state = initialState.content) => {
        return state;
    },
    selectedContent: (state = initialState.selectedContent, action) => {
        switch(action.type) {
            case 'CONTENT_SELECTED':
                return action.payload;

                default:
                    return state;
        }
    }    
});
import { combineReducers } from 'redux';

const sampleContentReducer = () => {
    return [
        { title: 'Title A'},
        { title: 'Title B'},
        { title: 'Title C'},
        { title: 'Title D'}
    ]
}

const sampleContentSelectedReducer = (something = null, action) => {
    if(action.type === 'CONTENT_SELECTED') {
        return action.payload;
    }

    return something;
}

export default combineReducers({
    content: sampleContentReducer,
    selectedContent : sampleContentSelectedReducer
});
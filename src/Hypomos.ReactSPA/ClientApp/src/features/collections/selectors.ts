import { RootState } from 'MyTypes';
// import { createSelector } from 'reselect';

export const getCollections = (state: RootState) => state.collections.collections;
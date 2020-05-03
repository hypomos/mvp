import { createAction } from 'typesafe-actions';
import { ContentType } from '../models';

export const selectContent = createAction('CONTENT_SELECTED')<ContentType>();

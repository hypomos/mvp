import * as logger from './logger-service';
import * as toast from './toast-service';
import * as localStorage from './local-storage-service';

import * as hypomos from './hypomos-api-client';
import * as collections from './collections-api-client';
import * as whoAmI from './whoami-api-client';

export default {
  logger,
  localStorage,
  toast,
  api: {
    hypomos,
    collections,
    whoAmI
  },
};
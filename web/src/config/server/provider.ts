/* eslint-disable sort-keys-fix/sort-keys-fix , typescript-sort-keys/interface */

import { FAST_API_URL } from "@/const/trace";

declare global {
  // eslint-disable-next-line @typescript-eslint/no-namespace
  namespace NodeJS {
    interface ProcessEnv {
      API_KEY_SELECT_MODE?: string;

      OPENAI_API_KEY?: string;
      OPENAI_PROXY_URL?: string;
      OPENAI_MODEL_LIST?: string;
      OPENAI_ENABLED_MODELS?: string;
      OPENAI_FUNCTION_REGIONS?: string;


      /**
       * @deprecated
       */
      CUSTOM_MODELS?: string;
      /**
       * @deprecated
       */
      OPENROUTER_CUSTOM_MODELS?: string;
    }
  }
}

export const getProviderConfig = () => {
  if (typeof process === 'undefined') {
    throw new Error('[Server Config] you are importing a server-only module outside of server');
  }


  // region format: iad1,sfo1
  let regions: string[] = [];
  if (process.env.OPENAI_FUNCTION_REGIONS) {
    regions = process.env.OPENAI_FUNCTION_REGIONS.split(',');
  }

  if (process.env.CUSTOM_MODELS) {
    console.warn(
      'DEPRECATED: `CUSTOM_MODELS` is deprecated, please use `OPENAI_MODEL_LIST` instead, we will remove `CUSTOM_MODELS` in the FastWki-Chat 1.0',
    );
  }


  return {
    API_KEY_SELECT_MODE: process.env.API_KEY_SELECT_MODE,
    FAST_API_URL: FAST_API_URL,
    OPENAI_API_KEY: process.env.OPENAI_API_KEY,
    OPENAI_PROXY_URL: process.env.OPENAI_PROXY_URL,
    OPENAI_MODEL_LIST: process.env.OPENAI_MODEL_LIST || process.env.CUSTOM_MODELS,
    OPENAI_FUNCTION_REGIONS: regions,
  };
};

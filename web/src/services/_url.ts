// TODO: 未来所有路由需要全部迁移到 trpc

/* eslint-disable sort-keys-fix/sort-keys-fix */
import { transform } from 'lodash-es';

import { withBasePath } from '@/utils/basePath';
import { ModelProvider } from '@/libs/agent-runtime';
import { VITE_API_URL } from '@/utils/env';

const mapWithBasePath = <T extends object>(apis: T): T => {
  return transform(apis, (result, value, key) => {
    if (typeof value === 'string') {
      // @ts-ignore
      result[key] = withBasePath(value);
    } else {
      result[key] = value;
    }
  });
};

export const API_ENDPOINTS = mapWithBasePath({
  proxy: '/api/proxy',
  oauth: '/api/auth',

  // agent markets
  market: VITE_API_URL + '/api/market',
  marketItem: (identifier: string) => withBasePath(VITE_API_URL + `/api/market/${identifier}`),

  // plugins
  gateway: '/api/plugin/gateway',
  pluginStore: VITE_API_URL +'/api/plugin/store',

  // chat
  chat: (provider: string) => {
    return VITE_API_URL+`/v1/chat/completions`
  },
  chatModels: (provider: string) => withBasePath(`/api/chat/models/${provider}`),

  // trace
  trace: '/api/trace',

  // image
  images: '/api/text-to-image/openai',

  // TTS & STT
  stt: '/api/openai/stt',
  tts: '/api/openai/tts',
  edge: '/api/tts/edge-speech',
  microsoft: '/api/tts/microsoft-speech',
});

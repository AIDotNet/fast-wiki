import OpenAI from 'openai';

import { FAST_API_URL } from '@/const/trace';

// create OpenAI instance
export const createOpenai = (userApiKey: string | null, endpoint?: string | null) => {

  const baseURL = FAST_API_URL.replace(/\/$/, '') + "/v1";

  // TODO: 分享对话没有apiKey
  const apiKey = userApiKey ?? "sk-guest";

  return new OpenAI({ apiKey, baseURL });
};

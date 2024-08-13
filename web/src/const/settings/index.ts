import { UserSettings } from '@/types/user/settings';

import { DEFAULT_AGENT } from './agent';
import { DEFAULT_COMMON_SETTINGS } from './common';
import { DEFAULT_LLM_CONFIG } from './llm';
import { DEFAULT_SYSTEM_AGENT_CONFIG } from './systemAgent';
import { DEFAULT_TOOL_CONFIG } from './tool';

export const COOKIE_CACHE_DAYS = 30;

export * from './agent';
export * from './llm';
export * from './systemAgent';
export * from './tool';

export const DEFAULT_SETTINGS: UserSettings = {
  defaultAgent: DEFAULT_AGENT,
  general: DEFAULT_COMMON_SETTINGS,
  keyVaults: {},
  languageModel: DEFAULT_LLM_CONFIG,
  systemAgent: DEFAULT_SYSTEM_AGENT_CONFIG,
  tool: DEFAULT_TOOL_CONFIG,
};

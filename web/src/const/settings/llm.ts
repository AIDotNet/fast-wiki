import {
  OpenAIProviderCard,
  filterEnabledModels,
} from '@/config/modelProviders';
import { ModelProvider } from '@/libs/agent-runtime';
import { UserModelProviderConfig } from '@/types/user/settings';

export const DEFAULT_LLM_CONFIG: any = {
  openai: {
    enabled: true,
    enabledModels: filterEnabledModels(OpenAIProviderCard),
  },
};

export const DEFAULT_MODEL = window.thor?.DEFAULT_MODEL;

export const DEFAULT_PROVIDER = ModelProvider.OpenAI;

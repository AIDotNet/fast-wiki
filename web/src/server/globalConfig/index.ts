import OpenAIProviderCard from '@/config/modelProviders/openai';
import { getServerConfig } from '@/config/server';
import { enableNextAuth } from '@/const/auth';
import { GlobalServerConfig } from '@/types/serverConfig';
import { extractEnabledModels, transformToChatModelCards } from '@/utils/parseModels';

import { parseAgentConfig } from './parseDefaultAgent';

export const getServerGlobalConfig = () => {
  const {
    ENABLE_LANGFUSE,

    DEFAULT_AGENT_CONFIG,
    OPENAI_MODEL_LIST,

  } = getServerConfig();

  const config: GlobalServerConfig = {
    defaultAgent: {
      config: parseAgentConfig(DEFAULT_AGENT_CONFIG),
    },

    enabledOAuthSSO: enableNextAuth,
    languageModel: {
      openai: {
        enabledModels: extractEnabledModels(OPENAI_MODEL_LIST),
        serverModelCards: transformToChatModelCards({
          defaultChatModels: OpenAIProviderCard.chatModels,
          modelString: OPENAI_MODEL_LIST,
        }),
      },

    },
    telemetry: {
      langfuse: ENABLE_LANGFUSE,
    },
  };

  return config;
};

export const getServerDefaultAgentConfig = () => {
  const { DEFAULT_AGENT_CONFIG } = getServerConfig();

  return parseAgentConfig(DEFAULT_AGENT_CONFIG) || {};
};

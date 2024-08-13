import { appEnv, getAppConfig } from '@/config/app';
import { getLLMConfig } from '@/config/llm';
import {
  OpenAIProviderCard,
} from '@/config/modelProviders';
import { enableNextAuth } from '@/const/auth';
import { parseSystemAgent } from '@/server/globalConfig/parseSystemAgent';
import { GlobalServerConfig } from '@/types/serverConfig';
import { extractEnabledModels, transformToChatModelCards } from '@/utils/parseModels';

import { parseAgentConfig } from './parseDefaultAgent';

export const getServerGlobalConfig = () => {
  const {  DEFAULT_AGENT_CONFIG } = getAppConfig();

  const {
    ENABLED_OPENAI,
    OPENAI_MODEL_LIST,
  } = getLLMConfig();

  const config: GlobalServerConfig = {
    defaultAgent: {
      config: parseAgentConfig(DEFAULT_AGENT_CONFIG),
    },
    enableUploadFileToServer: false,
    enabledAccessCode: false,
    enabledOAuthSSO: enableNextAuth,
    languageModel: {
      openai: {
        enabled: ENABLED_OPENAI,
        enabledModels: extractEnabledModels(OPENAI_MODEL_LIST),
        serverModelCards: transformToChatModelCards({
          defaultChatModels: OpenAIProviderCard.chatModels,
          modelString: OPENAI_MODEL_LIST,
        }),
      },
    },
    systemAgent: parseSystemAgent(appEnv.SYSTEM_AGENT),
    telemetry: {
      langfuse: false,
    },
  };

  return config;
};

export const getServerDefaultAgentConfig = () => {
  const { DEFAULT_AGENT_CONFIG } = getAppConfig();

  return parseAgentConfig(DEFAULT_AGENT_CONFIG) || {};
};

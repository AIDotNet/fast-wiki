

import { OpenAI } from '@lobehub/icons';

import { OpenAIProviderCard } from '@/config/modelProviders';
import { featureFlagsSelectors, useServerConfigStore } from '@/store/serverConfig';

import { ProviderItem } from '../../type';

export const useOpenAIProvider = (): ProviderItem => {
  const { showOpenAIProxyUrl, showOpenAIApiKey } = useServerConfigStore(featureFlagsSelectors);
  return {
    ...OpenAIProviderCard,
    proxyUrl: showOpenAIProxyUrl && {
      placeholder: 'http://api.token-ai.cn/',
    },
    showApiKey: showOpenAIApiKey,
    title: <OpenAI.Combine size={24} />,
  };
};


import { useMemo } from 'react';
import urlJoin from 'url-join';

import { ProviderItem } from '../type';
import { useAzureProvider } from './Azure';
import { useOpenAIProvider } from './OpenAI';

const BASE_DOC_URL = 'https:/AIDotNet.com/docs/usage/providers';

export const useProviderList = (): ProviderItem[] => {
  const azureProvider = useAzureProvider();
  const openAIProvider = useOpenAIProvider();

  return useMemo(
    () => [
      {
        ...openAIProvider,
        docUrl: urlJoin(BASE_DOC_URL, 'openai'),
      }
    ],
    [azureProvider],
  );
};

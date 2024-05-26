import { GlobalLLMProviderKey } from '@/types/settings';

import { UserStore } from '../../../store';
import { currentLLMSettings, getProviderConfigById } from './settings';

const isProviderEnabled = (provider: GlobalLLMProviderKey) => (s: UserStore) =>
  getProviderConfigById(provider)(s)?.enabled || false;

const isProviderEndpointNotEmpty = (provider: GlobalLLMProviderKey | string) => (s: UserStore) =>
  !!getProviderConfigById(provider)(s)?.endpoint;

const isProviderFetchOnClient = (provider: GlobalLLMProviderKey | string) => (s: UserStore) => {
  const config = getProviderConfigById(provider)(s);
  if (typeof config?.fetchOnClient !== 'undefined') return config?.fetchOnClient;

  return isProviderEndpointNotEmpty(provider)(s);
};

const getCustomModelCard =
  ({ id, provider }: { id?: string; provider?: string }) =>
  (s: UserStore) => {
    if (!provider) return;

    const config = getProviderConfigById(provider)(s);

    return config?.customModelCards?.find((m) => m.id === id);
  };

const currentEditingCustomModelCard = (s: UserStore) => {
  if (!s.editingCustomCardModel) return;
  const { id, provider } = s.editingCustomCardModel;

  return getCustomModelCard({ id, provider })(s);
};

const isAutoFetchModelsEnabled =
  (provider: GlobalLLMProviderKey) =>
  (s: UserStore): boolean => {
    return getProviderConfigById(provider)(s)?.autoFetchModelLists || false;
  };

const openAIConfig = (s: UserStore) => currentLLMSettings(s).openai;

export const modelConfigSelectors = {
  currentEditingCustomModelCard,
  getCustomModelCard,

  isAutoFetchModelsEnabled,
  isProviderEnabled,
  isProviderEndpointNotEmpty,
  isProviderFetchOnClient,

  openAIConfig,
};

import {  THOR_CHAT_AUTH_HEADER } from '@/const/auth';
import { useUserStore } from '@/store/user';
import { keyVaultsConfigSelectors } from '@/store/user/selectors';
import { GlobalLLMProviderKey } from '@/types/user/settings';

export const getProviderAuthPayload = (provider: string) => {
  const config = keyVaultsConfigSelectors.getVaultByProvider(provider as GlobalLLMProviderKey)(
    useUserStore.getState(),
  );

  return { apiKey: config?.apiKey, endpoint: config?.baseURL };
};


interface AuthParams {
  // eslint-disable-next-line no-undef
  headers?: HeadersInit;
  payload?: Record<string, any>;
  provider?: string;
}

// eslint-disable-next-line no-undef
export const createHeaderWithAuth = async (params?: AuthParams): Promise<HeadersInit> => {
  const token = localStorage.getItem('token') || '';
  return { ...params?.headers, [THOR_CHAT_AUTH_HEADER]: "Bearer " + token };
};

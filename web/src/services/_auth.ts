import { JWTPayload, THOR_CHAT_AUTH_HEADER } from '@/const/auth';
import { THOR_CHAT_ACCESS_CODE } from '@/const/fetch';
import { ModelProvider } from '@/libs/agent-runtime';
import { useUserStore } from '@/store/user';
import { keyVaultsConfigSelectors, userProfileSelectors } from '@/store/user/selectors';
import { GlobalLLMProviderKey } from '@/types/user/settings';
import { createJWT } from '@/utils/jwt';

export const getProviderAuthPayload = (provider: string) => {
  switch (provider) {
    case ModelProvider.Bedrock: {
      const { accessKeyId, region, secretAccessKey } = keyVaultsConfigSelectors.bedrockConfig(
        useUserStore.getState(),
      );

      const awsSecretAccessKey = secretAccessKey;
      const awsAccessKeyId = accessKeyId;

      const apiKey = (awsSecretAccessKey || '') + (awsAccessKeyId || '');

      return { apiKey, awsAccessKeyId, awsRegion: region, awsSecretAccessKey };
    }

    case ModelProvider.Azure: {
      const azure = keyVaultsConfigSelectors.azureConfig(useUserStore.getState());

      return {
        apiKey: azure.apiKey,
        azureApiVersion: azure.apiVersion,
        endpoint: azure.endpoint,
      };
    }

    case ModelProvider.Ollama: {
      const config = keyVaultsConfigSelectors.ollamaConfig(useUserStore.getState());

      return { endpoint: config?.baseURL };
    }

    default: {
      const config = keyVaultsConfigSelectors.getVaultByProvider(provider as GlobalLLMProviderKey)(
        useUserStore.getState(),
      );

      return { apiKey: config?.apiKey, endpoint: config?.baseURL };
    }
  }
};

const createAuthTokenWithPayload = async (payload = {}) => {
  const accessCode = keyVaultsConfigSelectors.password(useUserStore.getState());
  const userId = userProfileSelectors.userId(useUserStore.getState());

  return await createJWT<JWTPayload>({ accessCode, userId, ...payload });
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

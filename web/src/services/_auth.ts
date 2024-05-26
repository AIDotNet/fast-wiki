import { JWTPayload, LOBE_CHAT_AUTH_HEADER } from '@/const/auth';
import { ModelProvider } from '@/libs/agent-runtime';
import { useUserStore } from '@/store/user';
import { modelConfigSelectors, settingsSelectors } from '@/store/user/selectors';
import { createJWT } from '@/utils/jwt';

export const getProviderAuthPayload = (provider: string) => {
  switch (provider) {

    default: {

      const config = settingsSelectors.providerConfig(provider)(useUserStore.getState());

      return { apiKey: localStorage.getItem('token'), endpoint: config?.endpoint };
    }
  }
};

const createAuthTokenWithPayload = async (payload = {}) => {
  const accessCode = settingsSelectors.password(useUserStore.getState());

  return await createJWT<JWTPayload>({ accessCode, ...payload });
};

interface AuthParams {
  // eslint-disable-next-line no-undef
  headers?: HeadersInit;
  payload?: Record<string, any>;
  provider?: string;
}

// eslint-disable-next-line no-undef
export const createHeaderWithAuth = async (params?: AuthParams): Promise<HeadersInit> => {
  let payload = params?.payload || {};

  if (params?.provider) {
    payload = { ...payload, ...getProviderAuthPayload(params?.provider) };
  }

  const token = await createAuthTokenWithPayload(payload);

  // eslint-disable-next-line no-undef
  return { ...params?.headers, [LOBE_CHAT_AUTH_HEADER]: token };
};

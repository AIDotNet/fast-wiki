export const OPENAI_END_POINT = 'X-openai-end-point';
export const OPENAI_API_KEY_HEADER_KEY = 'X-openai-api-key';

export const LOBE_CHAT_ACCESS_CODE = 'X-lobe-chat-access-code';

export const OAUTH_AUTHORIZED = 'X-oauth-authorized';

/**
 * @deprecated
 */
export const getOpenAIAuthFromRequest = (req: Request) => {
  const apiKey = req.headers.get(OPENAI_API_KEY_HEADER_KEY);
  const accessCode = req.headers.get(LOBE_CHAT_ACCESS_CODE);
  const oauthAuthorizedStr = req.headers.get(OAUTH_AUTHORIZED);

  const oauthAuthorized = !!oauthAuthorizedStr;

  return { accessCode, apiKey, oauthAuthorized };
};

declare global {
  // eslint-disable-next-line @typescript-eslint/no-namespace
  namespace NodeJS {
    interface ProcessEnv {
      ACCESS_CODE?: string;
    }
  }
}


declare global {
  interface Window {
    thor: {
      DEFAULT_MODEL: string;
      DEFAULT_AVATAR: string;
      DEFAULT_INBOX_AVATAR: string;
      DEFAULT_USER_AVATAR: string;
    };
  }
}

export const getAppConfig = () => {
  const ACCESS_CODES = process.env.ACCESS_CODE?.split(',').filter(Boolean) || [];

  return {
    NEXT_PUBLIC_BASE_PATH: process.env.NEXT_PUBLIC_BASE_PATH || '',

    // Sentry
    NEXT_PUBLIC_ENABLE_SENTRY: !!process.env.NEXT_PUBLIC_SENTRY_DSN,

    ACCESS_CODES: ACCESS_CODES as any,

    AGENTS_INDEX_URL: !!process.env.AGENTS_INDEX_URL
      ? process.env.AGENTS_INDEX_URL
      : 'https://chat-agents.token-ai.com',

    DEFAULT_AGENT_CONFIG: process.env.DEFAULT_AGENT_CONFIG || '',
    SYSTEM_AGENT: process.env.SYSTEM_AGENT,

    PLUGINS_INDEX_URL: !!process.env.PLUGINS_INDEX_URL
      ? process.env.PLUGINS_INDEX_URL
      : 'https://chat-plugins.token-ai.com',

    PLUGIN_SETTINGS: process.env.PLUGIN_SETTINGS,
    SITE_URL: process.env.SITE_URL,
  };
};

export const appEnv = getAppConfig();

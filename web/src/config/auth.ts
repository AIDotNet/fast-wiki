declare global {
  // eslint-disable-next-line @typescript-eslint/no-namespace
  namespace NodeJS {
    interface ProcessEnv {
      // ===== Clerk ===== //
      NEXT_PUBLIC_CLERK_PUBLISHABLE_KEY?: string;
      CLERK_SECRET_KEY?: string;
      CLERK_WEBHOOK_SECRET?: string;

      // ===== Next Auth ===== //
      NEXT_AUTH_SECRET?: string;

      NEXT_AUTH_SSO_PROVIDERS?: string;

      AUTH0_CLIENT_ID?: string;
      AUTH0_CLIENT_SECRET?: string;
      AUTH0_ISSUER?: string;

      // Github
      GITHUB_CLIENT_ID?: string;
      GITHUB_CLIENT_SECRET?: string;

      // Azure AD
      AZURE_AD_CLIENT_ID?: string;
      AZURE_AD_CLIENT_SECRET?: string;
      AZURE_AD_TENANT_ID?: string;

      // AUTHENTIK
      AUTHENTIK_CLIENT_ID?: string;
      AUTHENTIK_CLIENT_SECRET?: string;
      AUTHENTIK_ISSUER?: string;

      // ZITADEL
      ZITADEL_CLIENT_ID?: string;
      ZITADEL_CLIENT_SECRET?: string;
      ZITADEL_ISSUER?: string;
    }
  }
}

export const getAuthConfig = () => {
  return {
    // Clerk
    NEXT_PUBLIC_ENABLE_CLERK_AUTH: !!process.env.NEXT_PUBLIC_CLERK_PUBLISHABLE_KEY,
    NEXT_PUBLIC_CLERK_PUBLISHABLE_KEY: process.env.NEXT_PUBLIC_CLERK_PUBLISHABLE_KEY,
    CLERK_SECRET_KEY: process.env.CLERK_SECRET_KEY,
    CLERK_WEBHOOK_SECRET: process.env.CLERK_WEBHOOK_SECRET,

    // Next Auth
    NEXT_PUBLIC_ENABLE_NEXT_AUTH: !!process.env.NEXT_AUTH_SECRET,
    NEXT_AUTH_SSO_PROVIDERS: process.env.NEXT_AUTH_SSO_PROVIDERS,
    NEXT_AUTH_SECRET: process.env.NEXT_AUTH_SECRET,

    // Auth0
    AUTH0_CLIENT_ID: process.env.AUTH0_CLIENT_ID,
    AUTH0_CLIENT_SECRET: process.env.AUTH0_CLIENT_SECRET,
    AUTH0_ISSUER: process.env.AUTH0_ISSUER,

    // Github
    GITHUB_CLIENT_ID: process.env.GITHUB_CLIENT_ID,
    GITHUB_CLIENT_SECRET: process.env.GITHUB_CLIENT_SECRET,

    // Azure AD
    AZURE_AD_CLIENT_ID: process.env.AZURE_AD_CLIENT_ID,
    AZURE_AD_CLIENT_SECRET: process.env.AZURE_AD_CLIENT_SECRET,
    AZURE_AD_TENANT_ID: process.env.AZURE_AD_TENANT_ID,

    // AUTHENTIK
    AUTHENTIK_CLIENT_ID: process.env.AUTHENTIK_CLIENT_ID,
    AUTHENTIK_CLIENT_SECRET: process.env.AUTHENTIK_CLIENT_SECRET,
    AUTHENTIK_ISSUER: process.env.AUTHENTIK_ISSUER,

    // ZITADEL
    ZITADEL_CLIENT_ID: process.env.ZITADEL_CLIENT_ID,
    ZITADEL_CLIENT_SECRET: process.env.ZITADEL_CLIENT_SECRET,
    ZITADEL_ISSUER: process.env.ZITADEL_ISSUER,
  };
};

export const authEnv = getAuthConfig();

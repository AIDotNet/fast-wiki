import { analyticsEnv } from '@/config/analytics';

import Vercel from './Vercel';

import  Plausible from './Plausible'
import  Posthog from './Posthog'
import  Umami from './Umami'
import  Clarity from  './Clarity'

const Analytics = () => {
  return (
    <>
      {analyticsEnv.ENABLE_VERCEL_ANALYTICS && <Vercel />}
      {analyticsEnv.ENABLED_PLAUSIBLE_ANALYTICS && (
        <Plausible
          domain={analyticsEnv.PLAUSIBLE_DOMAIN}
          scriptBaseUrl={analyticsEnv.PLAUSIBLE_SCRIPT_BASE_URL}
        />
      )}
      {analyticsEnv.ENABLED_POSTHOG_ANALYTICS && (
        <Posthog
          debug={analyticsEnv.DEBUG_POSTHOG_ANALYTICS}
          host={analyticsEnv.POSTHOG_HOST!}
          token={analyticsEnv.POSTHOG_KEY}
        />
      )}
      {analyticsEnv.ENABLED_UMAMI_ANALYTICS && (
        <Umami
          scriptUrl={analyticsEnv.UMAMI_SCRIPT_URL}
          websiteId={analyticsEnv.UMAMI_WEBSITE_ID}
        />
      )}
      {analyticsEnv.ENABLED_CLARITY_ANALYTICS && (
        <Clarity projectId={analyticsEnv.CLARITY_PROJECT_ID} />
      )}
    </>
  );
};

export default Analytics;

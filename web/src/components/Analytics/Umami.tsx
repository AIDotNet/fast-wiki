

import { useEffect } from 'react';

interface UmamiAnalyticsProps {
  scriptUrl: string;
  websiteId?: string;
}

const UmamiAnalytics = ({ scriptUrl, websiteId }: UmamiAnalyticsProps) => {
  useEffect(() => {
    if (websiteId) {
      const script = document.createElement('script');
      script.src = scriptUrl;
      script.defer = true;
      script.setAttribute('data-website-id', websiteId);
      document.head.appendChild(script);
      return () => {
        document.head.removeChild(script);
      };
    }
  }, [scriptUrl, websiteId]);

  return null;
};

export default UmamiAnalytics;
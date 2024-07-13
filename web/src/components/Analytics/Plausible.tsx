import { useEffect } from 'react';

interface PlausibleAnalyticsProps {
  domain?: string;
  scriptBaseUrl: string;
}

const PlausibleAnalytics = ({ domain, scriptBaseUrl }: PlausibleAnalyticsProps) => {
  useEffect(() => {
    if (domain) {
      const script = document.createElement('script');
      script.setAttribute('data-domain', domain);
      script.defer = true;
      script.src = `${scriptBaseUrl}/js/script.js`;
      document.head.appendChild(script);

      return () => {
        document.head.removeChild(script);
      };
    }
  }, [domain, scriptBaseUrl]);

  return null;
};

export default PlausibleAnalytics;


import qs from 'query-string';
import { memo, useEffect, useState } from 'react';

import { useQuery } from '@/hooks/useQuery';

import { LayoutProps } from './type';

const MOBILE_NAV_ROUTES = new Set(['/chat', '/market', '/me']);

const Layout = memo(({ children }: LayoutProps) => {
  const { showMobileWorkspace } = useQuery();
  const [pathname, setPathname] = useState('');

  useEffect(() => {
    setPathname(window.location.pathname);
  }, []);

  const { url } = qs.parseUrl(pathname);
  const showNav = !showMobileWorkspace && MOBILE_NAV_ROUTES.has(url);

  return (
    <>
      {children}
    </>
  );
});

Layout.displayName = 'MobileMainLayout';

export default Layout;
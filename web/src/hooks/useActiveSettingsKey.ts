import { useEffect, useState } from 'react';
import { useLocation } from 'react-router-dom';
import { useQuery } from '@/hooks/useQuery';
import { SettingsTabs } from '@/store/global/initialState';

/**
 * Returns the active setting page key (common/sync/agent/...)
 */
export const useActiveSettingsKey = () => {
  const [pathname, setPathname] = useState('');
  const { tab } = useQuery();
  const location = useLocation();

  useEffect(() => {
    setPathname(location.pathname);
  }, [location]);

  const tabs = pathname.split('/').at(-1);

  if (tabs === 'settings') return SettingsTabs.Common;

  if (tabs === 'modal') return tab as SettingsTabs;

  return tabs as SettingsTabs;
};

import { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom'; // 假设你使用的是react-router

import { SidebarTabKey } from '@/store/global/initialState';

/**
 * Returns the active tab key (chat/market/settings/...)
 * This version uses react-router instead of Next.js
 */
export const useActiveTabKey = () => {
  const location = useLocation();
  const [activeTabKey, setActiveTabKey] = useState<SidebarTabKey>(SidebarTabKey.Welcome); // 默认值根据你的需求调整

  useEffect(() => {
    const pathname = location.pathname;
    const foundKey = pathname.split('/').find(Boolean) as SidebarTabKey;
    setActiveTabKey(foundKey);
  }, [location.pathname]);

  return activeTabKey;
};
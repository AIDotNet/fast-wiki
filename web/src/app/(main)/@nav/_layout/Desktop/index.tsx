'use client';
declare var window: any;
import { SideNav } from '@lobehub/ui';
import { memo } from 'react';

import { useActiveTabKey } from '@/hooks/useActiveTabKey';

import Avatar from './Avatar';
import BottomActions from './BottomActions';
import TopActions from './TopActions';

const Nav = memo(() => {

  // 获取当前query中的sharedId
  if(typeof window === 'undefined'){
    return null;
  }
  const query = new URLSearchParams(window.location.search);
  const sharedId = query.get('sharedId');

  const sidebarKey = useActiveTabKey();
  if(sharedId){
    return null;
  }
  return (
    <SideNav
      avatar={<Avatar />}
      bottomActions={<BottomActions />}
      style={{ height: '100%', zIndex: 100 }}
      topActions={<TopActions tab={sidebarKey} />}
    />
  );
});

Nav.displayName = 'DesktopNav';

export default Nav;

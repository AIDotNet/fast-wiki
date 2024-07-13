import React, { PropsWithChildren } from 'react';
import { useNavigate } from 'react-router-dom';

import ServerLayout from '@/components/server/ServerLayout';
import { serverFeatureFlags } from '@/config/featureFlags';

import Desktop from './_layout/Desktop';
import Mobile from './_layout/Mobile';

const SessionSettingsLayout = ServerLayout({ Desktop, Mobile });

const Layout = ({ children }: PropsWithChildren) => {
  const navigate = useNavigate();
  const isAgentEditable = serverFeatureFlags().isAgentEditable;
  
  if (!isAgentEditable) {
    // 这里可以根据需要重定向到404页面或者是主页
    navigate('/404'); // 或者 navigate('/') 来重定向到主页
    return null; // 在重定向后返回null或者是一个空的组件
  }

  return <SessionSettingsLayout>{children}</SessionSettingsLayout>;
};

Layout.displayName = 'SessionSettingsLayout';

export default Layout;
import { ActionIcon, Logo, SideNav, Tooltip } from "@lobehub/ui";
import { Album, Settings2, Box, User, BotMessageSquare } from 'lucide-react';
import { memo, useEffect, useState } from "react";
import { Flexbox } from 'react-layout-kit';
import { Outlet } from "react-router-dom";
import { useNavigate } from "react-router-dom";

const DesktopLayout = memo(() => {
  const [tab, setTab] = useState<string>('chat');
  const navigate = useNavigate();

  const tabs = [{
    icon: BotMessageSquare,
    key: 'chat',
    description: '聊天',
    path: '/chat'
  }, {
    icon: Box,
    key: 'application',
    description: '应用',
    path: '/app'
  }, {
    icon: Album,
    key: 'wiki',
    description: '知识库',
    path: '/wiki'
  }, {
    icon: User,
    key: 'user',
    description: '用户管理',
    path: '/user'
  }]

  useEffect(()=>{
    // 获取当前路由匹配前缀一致的tab
    const currentTab = tabs.find(item => window.location.pathname.startsWith(item.path));
    if(currentTab) {
      setTab(currentTab.key);
    }
    
  })

  function updateTab(item: { key: string, path: string, description: string, icon: any }) {
    setTab(item.key);
    navigate(item.path);
  }

  return (<Flexbox
    height={'100%'}
    horizontal
    width={'100%'}
  >
    <SideNav
      avatar={<Logo size={40} />}
      bottomActions={<ActionIcon icon={Settings2} />}
      topActions={
        <>
          {tabs.map((item) => {
            return (
              <Tooltip arrow={true} placement='right' title={item.description}>
                <ActionIcon
                  active={tab === item.key}
                  icon={item.icon}
                  key={item.key}
                  onClick={() => updateTab(item)}
                  size="large"
                />
              </Tooltip>)
          })}
        </>
      }
    >
    </SideNav>
    <Outlet />
  </Flexbox>)
});

export default DesktopLayout;
import { Icon, MobileTabBar, MobileTabBarProps } from "@lobehub/ui";
import { memo, useEffect, useState } from "react";
import { Album, Box, User, BotMessageSquare } from 'lucide-react';
import { Flexbox } from 'react-layout-kit';
import { Outlet } from "react-router-dom";
import { useNavigate } from "react-router-dom";
import styled from 'styled-components';

const ActiveTitle = styled.span`
    color: #1890ff;
    
`;


const MobileLayout = memo(() => {
    const navigate = useNavigate();
    const [key, setTab] = useState<string>('chat');

    const items = [
        {
            icon: <Icon icon={BotMessageSquare} />,
            key: 'chat',
            title: key === 'chat' ? <ActiveTitle>聊天</ActiveTitle> : '聊天',
            path: '/chat',
            onClick: () => {
                navigate('/chat');
            }
        },
        {
            icon: <Icon icon={Box} />,
            key: 'application',
            path: '/app',
            title: key === 'application' ? <ActiveTitle>应用</ActiveTitle> : '应用',
            onClick: () => {
                navigate('/app');
            }
        },
        {
            icon: <Icon icon={Album} />,
            key: 'wiki',
            title: key === 'wiki' ? <ActiveTitle>知识库</ActiveTitle> : '知识库',
            path: '/wiki',
            onClick: () => {
                navigate('/wiki');
            }
        },
        {
            icon: <Icon icon={User} />,
            key: 'user',
            title: key === 'user' ? <ActiveTitle>用户管理</ActiveTitle> : '用户管理',
            path: '/user',
            onClick: () => {
                navigate('/user');
            }
        },
    ];

    useEffect(() => {
        // 获取当前路由匹配前缀一致的tab
        const currentTab = items.find(item => window.location.pathname.startsWith(item.path));
        if (currentTab) {
            setTab(currentTab.key);
        }

    })

    return (<Flexbox>
        <Outlet />
        <MobileTabBar activeKey={key} items={items} style={{
            bottom: 0,
            left: 0,
            position: 'absolute'
        }} />
    </Flexbox>)
});

export default MobileLayout;
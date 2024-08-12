import { useTheme } from 'antd-style';
import { memo, useState } from 'react';
import { Flexbox } from 'react-layout-kit';

import { usePlatform } from '@/hooks/usePlatform';

import { LayoutProps } from './type';
import { Button, Layout, Menu, theme } from 'antd';
import { useNavigate } from 'react-router-dom';
import { Logo } from '@lobehub/ui';
import { useActiveTabKey } from '@/hooks/useActiveTabKey';
import {
  FunctionOutlined,
  UserOutlined,
  ProjectOutlined,
  FileTextOutlined
} from '@ant-design/icons';

const { Header, Sider, Content } = Layout;

const DesktopLayout = memo<LayoutProps>(({ children }) => {
  const navigate = useNavigate();
  const sidebarKey = useActiveTabKey();
  // 解析query
  const query = new URLSearchParams(window.location.search);
  const sharedId = query.get('sharedId');
  

  const {
    token: { colorBgContainer },
  } = theme.useToken();
  console.log(sharedId);
  
  if (sharedId) {
    return children;
  }

  return (
    <Flexbox
      height={'100%'}
      horizontal
      style={{
        position: 'relative',
      }}
      width={'100%'}
    >

      <Layout style={{
        height: '100%',
      }}>

        <Sider trigger={null} collapsible >
          {<span style={{
            backgroundColor: colorBgContainer,
            display: 'flex',
            justifyContent: 'center',
            alignItems: 'center',
            height: 64,
            fontSize: 20,
            fontWeight: 600,
          }}>FastWiki</span>
          }
          <Menu
            mode='vertical'
            style={{
              height: '100%',
              borderRight: 0,
              background: colorBgContainer,
            }}
            defaultSelectedKeys={[sidebarKey]}
            items={[
              {
                key: 'project',
                icon: <ProjectOutlined />,
                label: '项目管理',
                onClick: () => {
                  navigate('/app');
                }
              },
              {
                key: 'wiki',
                icon: <FileTextOutlined />,
                label: '知识库管理',
                onClick: () => {
                  navigate('/wiki');
                }
              },
              {
                key: 'function-call',
                icon: <FunctionOutlined />,
                label: '函数管理',
                onClick: () => {
                  navigate('/function-call');
                }
              },
              {
                key: 'user',
                icon: <UserOutlined />,
                label: '用户中心',
                onClick: () => {
                  navigate('/user');
                }
              }
            ]}
          />
        </Sider>
        <Layout>
          <Content
            style={{
              minHeight: 280,
            }}
          >
            {children}
          </Content>
        </Layout>
      </Layout>
    </Flexbox>
  );
});

DesktopLayout.displayName = 'DesktopMainLayout';

export default DesktopLayout;
